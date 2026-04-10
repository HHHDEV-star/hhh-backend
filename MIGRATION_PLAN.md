# MIGRATION_PLAN.md — PHP → .NET 8 遷移計畫

> 本文件由 6 個平行子代理同時掃描 `C:/Users/thomaschen/Downloads/hhh_backend/hhh-admin/` 所產生。
>
> 目的：把舊版 PHP/Xoops 後台的剩餘功能，逐一遷移到目前的 .NET 8 clean architecture Web API（`hhh.webapi.admin`）。資料庫結構不變，所有新端點都要遵守 `CLAUDE.md` 裡的 **RESTful route 規則** 與 **`ApiResponse<T>` 統一包裝**。

---

## 0. 目前進度總覽

### 已完成 5 個 feature（略過，不在本計畫範圍內）

| Feature | .NET Folder | 對應 PHP |
|---|---|---|
| 登入 / JWT | `Auth/` | `login.php`, `logout.php`, `action/login.php`, `action/logout.php` |
| 管理員 | `Admins/` | `admin.php`, `admin_edit.php`, `admin_password.php` |
| 會員 | `Users/` | `_users.php`, `_users_edit.php` |
| 案例 | `Hcases/` | `_hcase*.php`（含 sort / model / mail / change_name） |
| 設計師 | `Hdesigners/` | `_hdesigner*.php`（含 sort / model / mail / mobile_sort） |

### 待遷移 — 按領域分組

| # | 領域 | Feature 數 | 檔案數 | 總工作量 |
|---|---|---|---|---|
| 1 | 內容（專欄 / 議題 / 影片 / 出版） | 5 | 16 | L + XL + 多個 S/M |
| 2 | 商務（品牌 / 產品 / 店家 / 活動 / 競賽 / 獎品 / 獲獎） | 7 | 23 | 1×L, 3×M, 3×S |
| 3 | 節目階層 / 附加資料 / 廣告 / LINE RSS | 4 | 14 | 1×L, 2×M, 1×S |
| 4 | 橫幅 / 站台設定 / 分店 / JSON 產生器 | 4 | 22 | 3×M, 1×L |
| 5 | EDM / 短網址 / 操作紀錄 / 影片報表 / 通用表格 | 5 | 14 | 1×XL, 1×M, 2×S, 1×REJECT |
| 6 | API / 排程 / 共通基礎建設 / 工具 | 雜項 | ~25 | S～M |
| **總計** | | **~25 feature** | **~114 檔** | |

---

## 1. 內容領域（Content）

### 1.1 專欄 `Hcolumns`（L）

- **PHP**：`_hcolumn.php`, `_hcolumn_edit.php`, `_hcolumn_mail_model.php`, `_hcolumn_model.php`, ~~`_hcolumn_adv.php`（死碼）~~
- **資料表**：`_hcolumn`, `_hdesigner`, `_hbrand`
- **操作**：列表 / 詳情 / CRUD / 排序 / 寄送通知信 / 品牌+設計師 picker / 觸發 `gencolumn_json_by_id.php` 重產 JSON
- **業務邏輯熱點**：
  - 多表 JOIN 搜尋（設計師名稱、品牌名稱、分類、標籤）
  - 上傳圖片後需二次 UPDATE 寫回新 ID 的 filename（兩步 transaction）
  - 通知信：設計師 email 攤平 + 個別寄送
- **.NET 建議**：
  - `hhh.application.admin/Hcolumns/` — 建立 service（有上傳、寄信、picker 邏輯）
  - DTO：`CreateOrUpdateHcolumnRequest`, `HcolumnListItemResponse`, `HcolumnDetailResponse`, `SendColumnNotificationRequest`
  - **Shared Picker**：`DesignerPickerItem`, `BrandPickerItem` — 會被 `Htopics`、`Hvideos` 重用，要抽到共用位置
- **路由**：
  ```
  GET    /api/hcolumns
  GET    /api/hcolumns/{id:int}
  POST   /api/hcolumns
  PUT    /api/hcolumns/{id:int}
  DELETE /api/hcolumns/{id:int}
  POST   /api/hcolumns/{id:int}/send-notification
  ```
- **風險 / 未知**：
  - `_hcolumn_adv.php` 是否真為死碼待確認
  - Python JSON 產生腳本已註解掉；目前靠 `gencolumn_json_by_id.php` 產出，.NET 要不要改成背景 job 需決定（見 §4.4）
  - 圖片上傳的時區 `+4 hours` 標註，需確認是否為 legacy bug
- **死碼**：`_hcolumn_adv.php`、`_hcolumn_edit.php` 內註解掉的 Python shell_exec

### 1.2 議題 `Htopics`（M）

- **PHP**：`_htopic.php`, `_htopic_edit.php`, `_htopic_mail_model.php`
- **資料表**：`_htopic`, `_hcase`, `_hcolumn`, `_hdesigner`, `_hbrand`
- **特色**：多選 picker（case + column 都存 CSV ID），兩張圖 `logo` + `seo_image`，SEO metadata 欄位
- **.NET 建議**：建立 service（多選 picker、設計師 email 攤平邏輯）
- **🐛 Bug**：`_htopic_mail_model.php` 第 57 行 SQL 缺 `FROM` 關鍵字 — 遷移前或遷移時順手修掉
- **路由**：標準 CRUD + `/api/htopics/{id}/send-notification` + picker sub-resources

### 1.3 議題 2 `Htopic2`（S）

- **PHP**：`_htopic2.php`, `_htopic2_edit.php`
- **資料表**：`_htopic2`（含 4 個關聯 CSV：designer / case / video / column）
- **特色**：沒有上傳、沒有寄信、logo 竟然是 text 欄位不是 file
- **.NET 建議**：thin CRUD（controller 直接戳 `XoopsContext`）
- **❓ 人工決定**：`_htopic2` vs `_htopic` 業務意義差在哪？是否還在使用？

### 1.4 影片 `Hvideos`（XL — 本領域最複雜）

- **PHP**：`_hvideo.php`, `_hvideo_edit.php`, `_hvideo_mail_model.php`, `_hvideo_model.php`
- **資料表**：`_hvideo`, `_hdesigner`, `_hcase`, `_hbrand`, `_hcolumn`
- **複雜度爆高的原因**：
  - 4 個影音平台解析：YouTube / Youku（可 iframe 嵌入）+ Tudou / iQiyi（只給連結）
  - **4 類標籤多選**（建築類型 / 風格 / 地區 / 節目類型 / 單元類型）+ 房型標籤 `tag_vpattern`，含「不限」互斥規則
  - 雙影片欄位：`iframe`（國際）+ `for_china`（中國備援）
  - Sponsor 欄位會在 text input 和 checkbox 之間切換（罕見的 UI 行為）
  - 設計師 → 案例 picker 的聯動過濾
  - 播放時間精度只到「小時」
- **.NET 建議**：
  - `hhh.application.admin/Hvideos/` — 一定要 service
  - 抽出 `IVideoIframeParser`（YouTube/Youku URL → iframe HTML）
  - 抽出 `ITagValidator`（處理「不限」互斥）
  - 多個 picker 路由（designers / cases / brands / columns）
- **新增路由**：`POST /api/hvideos/parse-iframe`（輸入 URL，回傳可嵌入 HTML 與平台判別）

### 1.5 出版 `Hpublish`（S）

- **PHP**：`_hpublish.php`, `_hpublish_edit.php`
- **資料表**：`_hpublish`
- **.NET 建議**：thin CRUD；logo 是 text 不是 file
- **❓ 人工決定**：logo 是否該改成真正的 file upload？`viewed` 是否由系統自增就好（不讓 admin 改）？

---

## 2. 商務領域（Commerce）

### 2.1 品牌 `Hbrands`（M）

- **PHP**：`_hbrand.php`, `_hbrand_edit.php`, `_hbrand_model.php`, `_hbrand_mail_model.php`, `_hbrand_sort.php`
- **資料表**：`_hbrand`
- **特色**：三張圖（logo / logo2 / background_mobile）、多個 CSV 欄位（btype / bstype / bspace）、FK 到 `_hvideo`、寄通知信給 sales_email
- **.NET 建議**：service（寄信 + 產品數統計 subquery）
- **❓ 人工決定**：`btype` CSV 要不要正規化成關聯表？`vr360_id` 是 FK 還是外部 ID？`bstype` / `bspace` 在表單上是註解掉的 — 能刪嗎？

### 2.2 產品 `Hproducts`（L — 本領域最大）

- **PHP**：`_hproduct.php`, `_hproduct_edit.php`, `_hproduct_img_model.php`, `_hproduct_mail_model.php`
- **資料表**：`_hproduct`, `_hproduct_img`
- **特色**：3 層分類樹（硬編碼在 PHP 裡的巢狀陣列）、多張圖排序、封面圖互斥（需 sync `_hproduct.cover` ↔ `_hproduct_img.is_cover`）
- **.NET 建議**：
  - `Hproducts/` — service 必要
  - 圖片上傳 + 排序 + 封面切換用子資源路由
  - 分類樹改成 seed data 或 config
- **路由**：
  ```
  GET    /api/products
  GET    /api/products/{id:int}
  POST   /api/products
  PUT    /api/products/{id:int}
  DELETE /api/products/{id:int}
  POST   /api/products/{id:int}/images
  PUT    /api/products/{id:int}/images/{imgId:int}
  DELETE /api/products/{id:int}/images/{imgId:int}
  POST   /api/products/{id:int}/images/{imgId:int}/set-cover
  GET    /api/products/categories           # 返回 3 層分類樹
  ```

### 2.3 店家 `Shops`（S）

- **PHP**：`shop.php`, `shop_edit.php`, `shop_model.php`, `shop_sort.php`
- **資料表**：`shop`
- **特色**：結構幾乎等於 `_hbrand`；程式碼可借用品牌模板
- **⚠️ 可疑 SQL**：`shop.php` 第 71 行子查詢 `_hproduct p WHERE p.id=tb.id` — 看起來是從 brand 複製過來沒改掉的 bug
- **❓ 人工決定**：shop 和 brand 業務上到底差在哪？能否合併成同一張表＋type 欄位？

### 2.4 活動抽獎 `Hevents`（M）

- **PHP**：`_hevent.php`, `_hevent_edit.php`, `_hevent_batch.php`
- **資料表**：`_hevent`, `_hprize`
- **特色**：
  - `type` 有 5 種（電視/廣播有獎徵答 / 網站活動 / 廠商優惠 / 座談會）
  - `question` 欄位是 `urlencode(json_encode(...))` — 要小心 serialization
  - 得獎名單獨立 form `_hevent_batch.php`
- **.NET 建議**：service；`EventQuestion` 建議設 value object
- **路由**：CRUD + `PUT /api/events/{id}/winner-list`

### 2.5 競賽 `Hcontests`（S）

- **PHP**：`_hcontest.php`, `_hcontest_edit.php`
- **資料表**：`_hcontest`（欄位命名 c1~c13 非常神秘）
- **.NET 建議**：thin CRUD；DTO 層把 c1~c13 映射成有意義的名稱
- **❓ 人工決定**：c4~c8 在列表頁沒顯示 — 真的有用嗎？`an`（末五碼）是什麼？

### 2.6 獎品 `Hprizes`（S）

- **PHP**：`_hprize.php`, `_hprize_edit.php`
- **資料表**：`_hprize`
- **.NET 建議**：thin CRUD，純 logo 上傳 + 文字欄位

### 2.7 設計師獲獎 `Hawards`（S）

- **PHP**：`_hawards.php`, `_hawards_edit.php`
- **資料表**：`_hawards`（FK 到 `_hdesigner` + `_hcase` — 兩邊都已遷移）
- **特色**：獎項 logo 是硬編碼的 6 張 PNG（金/銀/銅/優良/創作/傑出）
- **.NET 建議**：thin CRUD + `AwardType` enum

---

## 3. 節目階層 / 附加資料 / 廣告 / LINE RSS

### 3.1 節目 3 層架構 `Hprograms` / `HprogramChannels` / `HprogramUnits`（L — 本領域最複雜）

- **PHP**：`_hprog*.php`, `_hprog_chan*.php`, `_hprog_unit*.php`
- **資料表**：
  - `_hprog` — 節目
  - `_hprog_chan` — 頻道
  - `_hprog_unit` — 單元（FK 到 `_hdesigner` / `_hcase` / `_hbrand` / `_hvideo` / `_hcolumn`）
  - `_hprog_tbl` — 節目↔單元 join table，含 `order` 欄
- **階層 / cascade**：
  - Program 屬於一個 Channel
  - Program 透過 `_hprog_tbl` 對應多個 Unit（有順序）
  - **更新 Program 時會先 DELETE 全部 `_hprog_tbl`，再 re-INSERT** — 要注意 transaction
  - 刪 Program 會 cascade 刪 `_hprog_tbl`
- **.NET 建議**：
  - `hhh.application.admin/Hprograms/` — 三個 service（Programs / Channels / Units），或一個大 service 含三組方法
  - 路由考慮 nested：`GET /api/programs/{id}/units`
- **熱點**：Channel logo 更新時會 AJAX 觸發 `genprogram_json.php` — 在 .NET 改成 background job
- **❓ 人工決定**：`_hprog_tbl.order` 在 UI 上能編輯嗎？soft delete 要嗎？舊圖檔上傳新圖時要不要刪除？

### 3.2 附加資料 `Hext1`（M）

- **PHP**：`_hext1.php`, `_hext1_edit.php`, `_hext1_batch.php`, `_hext1_mail_model.php`
- **資料表**：`_hext1`
- **特色**：9 種外部露出平台（Yahoo房地產 / Hinet / PCHOME / 網易 / 太平洋 / 新浪 / UDN / Life / 智邦）、批次 CSV 匯入、寄信給設計師或廠商
- **.NET 建議**：service；CSV 批次匯入要包 transaction（目前 PHP 沒包）
- **路由**：CRUD + `POST /api/extended-publications/batch-import` + `POST /api/extended-publications/{id}/send-notification`
- **❓ 人工決定**：批次匯入要全有全無還是部分成功？`mcount` 在 email 失敗時要不要加？

### 3.3 廣告 `Had`（M）

- **PHP**：`_had.php`, `_had_edit.php`
- **資料表**：`_had`（40+ 種廣告版位：刊頭 / 電視 / 個案 / 專欄 / 影音 / 首頁...）
- **特色**：雙圖上傳（`adlogo` + `water_mark`）、排程上下架欄位 `start_time` / `end_time`、`click_counter`
- **.NET 建議**：service；時間驗證 end > start；版位 enum
- **❓ 人工決定**：
  - `_had.php` 第 35~38 行註解掉的「按時自動上下架」邏輯 — 要不要重啟成 background job？
  - `adlogo` vs `water_mark` 業務差別？
  - 40+ 版位是否移到查找表 `_had_type`？

### 3.4 LINE RSS `LineRss`（S）

- **PHP**：`_hLINE_RSS.php`, `_hLINE_RSS_edit.php`
- **資料表**：`_hLINE_RSS`（`dateString` YYYYMMDD + CSV column IDs + `updateTimeUnix`）
- **特色**：顯示「今天前後 ±10 天」的矩陣；今天之前的只讀、今天之後的可編輯
- **.NET 建議**：service（date-based 規則 + upsert）
- **❓ 人工決定**：LINE 真的還在消費這張表嗎？時區問題？`strID` 真的是該張表的 PK 嗎還是一對多？

---

## 4. 橫幅 / 站台設定 / 分店 / JSON 產生器

### 4.1 橫幅 `Banners`（M）

- **PHP**：`banner_setup.php`, `banner_tab.php`, `banner_tab2.php`, `banner_tab3.php`
- **資料表**：`banner_setup`（單 row，ID=1；內含 3 個 JSON 欄位 + 3 個 CSV 欄位）
  - `strarr_hcase_id`, `strarr_hdesigner_id`, `strarr_hbrand_id` — CSV
  - `editors_picks` — JSON 陣列（5 個精選 tab）
  - `recommend_brand` — JSON 陣列（2 個品牌 tab）
  - `recommend_designer` — 巢狀 JSON（5 tabs × 5 slots = 25 設計師卡片）
- **.NET 建議**：thin controller（純設定 CRUD，無業務邏輯）
- **路由**：
  ```
  GET   /api/banners
  PATCH /api/banners/editors-picks
  PATCH /api/banners/brand-slots
  PATCH /api/banners/designer-showcase
  POST  /api/banners/{section}/upload-photo
  ```
- **❓ 人工決定**：3 個 tab 檔是否都還在使用？圖片存放位置（本地 / S3 / CDN）？

### 4.2 站台設定 `SiteSettings`（M）

- **PHP**：`site_setup.php`, `site_setup2.php`, `site_setup3.php`, `mobile_inner_setup.php`
- **資料表**：`site_setup`（單 row，欄位涵蓋桌機 + 手機所有設定）
- **特色**：
  - `site_setup.php` — 6 個搜尋標籤 CSV
  - `site_setup2.php` — Vimeo 影片 + 手機首頁 hot case/video + 5 tab
  - `site_setup3.php` — 桌機前導影片 MP4
  - `mobile_inner_setup.php` — 手機內頁 banner（`belt_url` + `belt_img`, 294×100）
- **.NET 建議**：thin CRUD
- **❓ 人工決定**：要不要把單表拆成 `SiteSettingsDesktop` / `SiteSettingsMobile`？上傳舊檔要不要自動清理？

### 4.3 設計師分店 `DesignerBranches`（M）

- **PHP**：`designer_branch.php`, `designer_branch_edit.php`
- **資料表**：`designer_branch` + 觸發 `_hdesigner.update_time` 更新
- **特色**：每次新增/編輯/刪除分店都會摸 `_hdesigner.update_time`，並 AJAX 觸發 `gendesigner_json.php`（現在是阻塞呼叫）
- **.NET 建議**：service（跨表 timestamp 同步）
- **路由**：nested resource
  ```
  GET    /api/designers/{designer-id:int}/branches
  GET    /api/designers/{designer-id:int}/branches/{branch-id:int}
  POST   /api/designers/{designer-id:int}/branches
  PUT    /api/designers/{designer-id:int}/branches/{branch-id:int}
  DELETE /api/designers/{designer-id:int}/branches/{branch-id:int}
  ```
- **建議**：AJAX 觸發 JSON regen 改成背景 job，API 立即回傳

### 4.4 JSON 產生器 `JsonExports`（L — 重量級基礎建設）

**12 個 generator 檔案**（全部是 shell_exec Python 的薄包裝）：

| PHP | 用途 |
|---|---|
| `gen_columns_ads_json.php` | 專欄詳情頁廣告 |
| `genads_json.php` | 首頁 + 個案詳情廣告（8 變體） |
| `genbuilderlists_json.php` | 建商清單與詳情 |
| `gencases_json.php` | 案例清單與詳情（10+ shell_exec） |
| `gencolumn_json.php` + `gencolumn_json_by_id.php` | 專欄批次 + 單筆 |
| `gendesigner_json.php` | 設計師清單與詳情（8 變體） |
| `genindex_outside_link.php` | 首頁外部連結 |
| `genphoto_json.php` | 相簿詳情 |
| `genprogram_json.php` | 節目靜態資料 |
| `gentopic_json.php` | 議題清單與詳情 |
| `genvideo_json.php` | 影片清單與詳情 |

- **Python 腳本路徑**：`/var/www/html/` 底下，輸出到 `hhh-www/` 與 `hhh-mobile/`
- **.NET 建議**（**重要決策**）：
  1. **不要**機械式移植成 controller action — 這些都是慢 I/O、長時間跑的任務
  2. 改成 **background job**（Hangfire）或 `BackgroundService` + 佇列
  3. controller 只負責「排入佇列」，立即回 `202 Accepted` + `jobId`
  4. Python 腳本繼續用 `Process.Start` 呼叫 **或** 全部改寫成 C#（後者工作量大，但消除 Python 依賴）
- **路由**：
  ```
  POST /api/admin/json-exports/designers        # 202 Accepted
  POST /api/admin/json-exports/cases
  POST /api/admin/json-exports/{resource}/{id}
  GET  /api/admin/json-exports/jobs/{jobId}     # 查狀態
  ```
- **❓ 人工決定**：
  - 舊的 `HHH_NEW/` 路徑可以刪了嗎？
  - 要 rewrite Python 成 C# 還是保留 `Process.Start`？
  - 需要 CDN invalidation 嗎？

---

## 5. EDM / 短網址 / 操作紀錄 / 影片報表 / 通用表格

### 5.1 EDM 電子報 `Edm`（XL — 全專案最重的 feature）

- **PHP**：`edm_cycle.php`, `edm_log.php`, `edm_log_edit.php`, `edm_log_mail_model.php`, `edm_log_preview.php`
- **資料表**：`edm_log`, `_hnewspaper`, `_users`
- **舊做法的惡臭**：
  - 「一次寄一封」靠瀏覽器 `location.reload()` 刷新 → 根本不適合大量寄送
  - 寄送狀態存在 `$_SESSION` 跨 request
  - GA pixel 硬編碼 `UA-133449203-1`，沒有 per-recipient token
  - 內文從外部 URL 抓 HTML 再塞 email
- **.NET 建議**：
  - **兩個 service**：`IEdmCampaignService`（campaign CRUD）+ `IEdmSenderService`（寄送執行）
  - 寄送一定要改成 **Hangfire / background job**
  - 每封 email 要有獨立 tracking token
  - 訂閱者列表改用 DB-backed queue，不要靠 session
- **路由**：
  ```
  GET    /api/edm-campaigns
  POST   /api/edm-campaigns
  GET    /api/edm-campaigns/{id:int}
  PUT    /api/edm-campaigns/{id:int}
  DELETE /api/edm-campaigns/{id:int}
  POST   /api/edm-campaigns/{id:int}/send             # 202 Accepted
  GET    /api/edm-campaigns/{id:int}/send-progress    # 查進度
  ```
- **❓ 人工決定**：
  - 找不到 `send_edm()` 實作在哪（很可能在 `function.php` 裡，需進一步查）
  - GA pixel 的追蹤策略要不要升級成 GA4？
  - `_hnewspaper` 還有其他地方在用嗎？

### 5.2 短網址 `ShortUrls`（M）

- **PHP**：`short_url.php`, `short_url_edit.php`, `short_url_log_model.php`, `short_url_log_model2.php`
- **資料表**：`short_url`, `short_url_log`
- **特色**：
  - 4 個 tracking parameter（`track_a` ~ `track_d`）
  - 自動產生 4 字元 code（**🐛 重試邏輯有 bug** — 重試時沒 increment counter，可能無限迴圈）
  - 呼叫 linkpreview.net API（API key 硬編碼）
  - 呼叫 Facebook Graph API v3.2 重整 OG cache（**token 已硬編碼且版本已 deprecated**）
- **.NET 建議**：
  - `ShortUrls/` — service
  - 修掉 code 產生 retry bug
  - API keys 搬到 `appsettings.json`
  - Facebook Graph 升級或直接拔除
- **❓ 人工決定**：
  - 導流 handler（`/u/{code}` → 實際 URL）在哪裡實作？Click log 在哪邊寫入？
  - Facebook cache 重整還需要嗎？
  - `short_url_log_model` vs `short_url_log_model2` — 一個是圓餅圖、一個是原始 log，兩個都留

### 5.3 操作紀錄 `OperationLogs`（S）

- **PHP**：`_hoplog.php`, `_hoplog_edit.php`
- **資料表**：`_hoplog`（append-only audit log）
- **.NET 建議**：thin controller，只需 list + detail 兩個端點
- **重要事項**：**所有** CRUD service 都必須呼叫 `_save_log()` 的對等實作（例如 `IOperationLogWriter`），否則 audit trail 會斷
- **❓ 人工決定**：`sqlcmd` 欄位是否暴露給 admin 看？會不會洩漏敏感資料？

### 5.4 影片報表 `VideoReports`（S）

- **PHP**：`video_report.php`, `video_report2.php` + `action/output_video_report.php`, `action/output_video_report2.php`
- **資料表**：`_hvideo`, `_hbrand`, `_hdesigner`
- **.NET 建議**：thin controller；Excel 匯出用 `ClosedXML`（見 §6.1）
- **路由**：
  ```
  GET /api/video-reports/brands
  GET /api/video-reports/brands/export        # Excel 下載
  GET /api/video-reports/designers
  GET /api/video-reports/designers/export
  ```

### 5.5 通用資料表 `table.php` / `table_edit.php` — **❌ REJECT，不要移植**

- **為什麼拒絕**：
  - 泛 CRUD over 任何 table 是反模式
  - 授權無法 per-table 控管 — 一旦 admin 能呼叫 `table.php?name=_users` 就能編輯任何表
  - 違反 `CLAUDE.md` 的「只在有真正業務邏輯時建 service」原則 — 這個工具完全沒業務邏輯
- **正確做法**：
  1. 去舊後台 sidebar/menu 找出 `table.php?name=XXX` 的所有連結，列出實際使用的表
  2. **每張**都開一個 thin controller 或 service，根據有無 FK/validation 決定
- **工作量**：N/A（根據實際使用的表再估）

---

## 6. API / 排程 / 共通基礎建設

### 6.1 Bucket A — 需搬成 .NET endpoint

| PHP 檔 | 用途 | 建議路由 | 工作量 |
|---|---|---|---|
| `api/delete_case_img.php` | ⚠️ 檔案內容跟 `update_price.php` 一樣 — **copy-paste bug**，移植前先搞清楚實際行為 | `DELETE /api/products/{id}` 或其他 | S |
| `api/update_price.php` | 更新產品價格；舊 token 驗證 → 換 JWT | `PATCH /api/products/{id}/price` | S |
| `action/output_selection.xls.php` | 匯出選拔評分到 Excel（雙 sheet） | `GET /api/selections/{id}/export-scores` | M |
| `action/output_video_report.php` | 品牌影片報表 Excel | `GET /api/video-reports/brands/export` | S |
| `action/output_video_report2.php` | 設計師影片報表 Excel | `GET /api/video-reports/designers/export` | S |

- **Excel 套件選型**：
  - ✅ 推薦 `ClosedXML`（MIT License，支援 .xlsx）
  - `EPPlus` 需要授權檢查
  - PHPExcel 已淘汰

### 6.2 Bucket B — 搬成背景 job

| PHP 檔 | 說明 | 建議 | 工作量 |
|---|---|---|---|
| `schedule/update_photo_to_s3_per_week.php` | 每週 cron，替換 20 張表裡的 URL（從 `m.hhh.com.tw` → `cloud.hhh.com.tw`），重置設計師排序與電子報旗標 | `BackgroundService` 或 Quartz.NET scheduled job；舊/新 URL pair 移到 `appsettings.json` | M |

### 6.3 Bucket C — 共通基礎建設（`hhh.infrastructure/`）

| PHP | .NET 位置 | NuGet 替代 | 備註 |
|---|---|---|---|
| `function/class.resize_img.php`（GD） | `hhh.infrastructure/Media/ImageResizer.cs` | **ImageSharp** | GD 有 silent-fail 問題，ImageSharp 會拋例外 — 要處理好 |
| `function/class.tokenEncode.php`（Discuz authcode） | ❌ 不移植 | — | **全面用 JWT**；舊 API 如果還有前端呼叫，提供過渡 shim |
| `function/function.php`（寄信 section） | `hhh.infrastructure/Mail/EmailSender.cs` + `IEmailService` | **MailKit** | 統一 `SendAsync(to, subject, body, cc, bcc, isHtml)`；多個 SMTP provider 選一個，憑證進 secret manager |
| `include/upload_watermark_reuse_everytime.inc.php`（ImageMagick shell_exec） | `hhh.infrastructure/Media/ImageWatermarker.cs` | **ImageSharp** | 字型（`msjh.ttf`）與 logo（`LOGOS.png`）路徑改 config |
| `function/function.php`（工具函式 section） | `hhh.infrastructure/Common/` | .NET 內建 | `is_email()` → `System.Net.Mail.MailAddress`；`get_real_ip()` → `HttpContext.Connection.RemoteIpAddress`；`is_handheld_device()` → UAParser |

### 6.4 Bucket D — 不需移植（框架黏合 / 死碼）

| PHP | 原因 |
|---|---|
| `include/db_connect.inc.php` | EF Core + `appsettings.json` 取代 |
| `include/top.php` / `sidebar.php` / `footer.php` / `htmlhead.php` / `script.php` | 舊後台 HTML 版面 — 新版是 SPA + API，不需要 |
| `include/pagination.inc.php` | 分頁邏輯改用 query string + `ApiResponse<T>` |
| `include/page_manager.php` | 權限 / 選單配置 — 部分改到 `appsettings.json`，部分改用 `[Authorize(Policy = ...)]` |
| `include/dataTables.cht.json` | 前端 datatable 語系檔 — 搬到 `wwwroot/` 直接服務 |
| `include/db_connect/` | 資料庫抽象層，EF Core 取代 |
| `config.inc.php`, `config.local.php` | 改 `appsettings.json` + `appsettings.{Environment}.json` |
| `index.php`, `main.php` | SPA 入口，由 React/Vue 處理 |
| `a.php` | 未知測試檔 — 詢問後可能可刪 |
| `code_generator.php`, `code_generator_header.html`, `code_generator_footer.html` | 開發期間的 CRUD scaffolding 工具 — 新專案已經有 EF Core scaffold，無需移植 |
| `convert-to-webp.php`, `my_water_images.php`, `step2_fav_load_and_insert_to_newer.php`, `test_mail.php` | 一次性遷移腳本 / 測試工具 — 不需移植 |
| `ajax_photo.php`, `ajax_photo_upload.php` | 看情況，可能屬於某個 feature 的子路由 — 需確認後歸類 |

### 6.5 Bucket E — 需要人工決定

| 問題 | 預設建議 |
|---|---|
| 舊的 `api/update_price.php` 還有前端在呼叫嗎？ | 去前端 repo grep；若有就先提供過渡 shim |
| `.xls` vs `.xlsx`？ | 預設 `.xlsx`（ClosedXML） |
| 還需要 ImageMagick 嗎？ | 預設改全 ImageSharp |
| 用哪個 Mail provider？AWS SES / Office 365 / 本地 | 預設 AWS SES（如果 production 現在用這個） |
| URL rewrite 是一次性還是持續的？ | 若持續，要放 image serving middleware |

---

## 7. 共通基礎建設（先建好，下面所有 feature 都會用到）

把下列「橫跨多個 feature 的東西」先做掉，後面每個 feature 就單純多了：

1. **圖片上傳 + resize**（`IImageUploadService` + ImageSharp）— `Hcolumns`, `Htopics`, `Hvideos`, `Hbrands`, `Hproducts`, `Shops`, `Hevents`, `Hprizes`, `Had`, `HprogramChannels`, `HprogramUnits`, `Banners`, `SiteSettings`, `ShortUrls` 全部要
2. **Email service**（`IEmailService` + MailKit，配合 `appsettings.json` 設定）— `Hcolumns`, `Htopics`, `Hvideos`, `Hbrands`, `Hproducts`, `Hext1`, `Edm` 全部要
3. **Picker sub-resources**（共用 DTO：`DesignerPickerItem`, `BrandPickerItem`, `CasePickerItem`, `ColumnPickerItem`, `VideoPickerItem`）— `Hcolumns`, `Htopics`, `Hvideos`, `Hbrands`（品牌反向選產品）, `HprogramUnits`, `Banners`, `SiteSettings` 全部要
4. **操作紀錄寫入**（`IOperationLogWriter` — 對應 PHP 的 `_save_log()`）— 所有 CUD service 都要呼叫
5. **背景 job 框架**（Hangfire 或 `BackgroundService`）— `Edm`, `JsonExports`, `DesignerBranches` 的 JSON 觸發, Weekly S3 sync 都要
6. **Excel 匯出**（`ClosedXML`）— `VideoReports`, `Selections`
7. **檔案清理 / 版本**（上傳新圖時刪舊圖）— 所有含 file upload 的 feature 都要
8. **Config 化**：所有硬編碼的 URL / 路徑 / API key / token 都要搬到 `appsettings.json`

---

## 8. 建議遷移順序（phased roadmap）

### Phase 0 — 基礎建設（最先做，之後每個 feature 都輕鬆）
1. `IImageUploadService` + ImageSharp
2. `IEmailService` + MailKit + `appsettings.json:Mail`
3. `IOperationLogWriter`
4. Hangfire（或決定用 `BackgroundService`）+ `appsettings.json:Jobs`
5. Excel 匯出 helper（ClosedXML）

### Phase 1 — 快速 win（先刷一波 S 級）
1. **Hprizes**（S）
2. **Hpublish**（S）
3. **Htopic2**（S，先問清楚是不是死碼）
4. **Hawards**（S）
5. **OperationLogs**（S — 只讀）
6. **VideoReports**（S — 只讀 + Excel）
7. **Hcontests**（S — 先問 c1~c13 映射）

### Phase 2 — 中型 feature（M 級，多半需要 picker / 寄信 / upload）
1. **Htopics**（M — 先修 SQL bug）
2. **Hbrands**（M）
3. **Shops**（M — 借用 Hbrands 模板）
4. **Hevents**（M — 含批次得獎名單）
5. **Had**（M）
6. **Hext1**（M — 含 CSV 批次匯入）
7. **Banners**（M）
8. **SiteSettings**（M）
9. **DesignerBranches**（M — 含 JSON regen 背景 job）
10. **ShortUrls**（M — 含外部 API 整合）
11. **LineRss**（S — 但優先級低，需確認是否還在使用）

### Phase 3 — 大型 feature（L 級）
1. **Hcolumns**（L — picker 會被後面重用）
2. **Hproducts**（L — 3 層分類 + 封面同步）
3. **Hprograms / HprogramChannels / HprogramUnits**（L — 三表階層 + join）

### Phase 4 — 最重量級（XL 級）
1. **Hvideos**（XL — 影片解析 + 複雜標籤 + 多 picker）
2. **Edm**（XL — 需設計整套寄送管線 + tracking）
3. **JsonExports**（L — 需先決定是否 rewrite Python）

### Phase 5 — Bucket A 雜項 endpoint + 清理
1. `api/update_price.php` → `PATCH /api/products/{id}/price`
2. `api/delete_case_img.php` → 實際行為釐清後再決定路由
3. `action/output_selection.xls.php` → `GET /api/selections/{id}/export-scores`
4. Weekly S3 sync → `BackgroundService`
5. 清掉舊 PHP token 機制（完全改 JWT）
6. 評估 `table.php` 使用範圍並逐表取代

---

## 9. 所有需要人工決定的問題（彙整）

按領域分組，下次 sync 請一次回完避免來回：

### 9.1 內容領域
- `_hcolumn_adv.php` 是不是死碼可以刪？
- `_htopic2` 和 `_htopic` 到底有何不同？`_htopic2` 還在使用嗎？
- `_hpublish.logo` 要不要改成真正的 file upload？
- 影片平台是否還需要支援 Tudou / iQiyi（已 deprecated）？
- 影片 `sponsor` 欄位在 text / checkbox 之間切換的 UX 要保留還是簡化？
- 影片 `display_datetime` 真的只精確到小時嗎？

### 9.2 商務領域
- `_hbrand.btype` / `bstype` / `bspace` 要不要正規化成關聯表？`bstype` / `bspace` 在 edit form 是註解掉的 — 能刪嗎？
- `_hbrand.vr360_id` 是 FK 還是外部 ID？
- `shop.php` 和 `_hbrand.php` 業務差異？能否合併成同表 + type 欄？
- `shop.php` 第 71 行那個疑似 copy-paste bug 的 subquery 該怎麼處理？
- `_hcontest` 的 c1~c13 每個欄位真正意義？c4~c8 真的沒在用嗎？`an`（末五碼）是什麼？

### 9.3 節目 / 附加資料 / 廣告 / LINE
- `_hprog_tbl.order` 在 UI 是否可編輯（要不要排序 API）？
- 刪除 program / channel / unit 要 soft delete 還是 hard delete？
- `_hext1` 批次匯入要全有全無還是允許部分成功？
- `_hext1.mcount` 在寄信失敗時要不要加？
- `_had.php` 第 35~38 行註解掉的自動上下架要不要啟用成 background job？
- `_had.adlogo` 和 `water_mark` 業務意義差在哪？
- `_had` 40+ 版位要不要搬到查找表？
- LINE RSS 現在真的有在被 LINE 消費嗎？時區要不要明確化？

### 9.4 橫幅 / 站台設定 / 分店 / JSON
- `banner_tab` / `banner_tab2` / `banner_tab3` 三個檔現在都在用嗎？
- 上傳圖片要存哪？本地 / S3 / CDN？現在 production 用什麼？
- `site_setup` 要不要拆成 Desktop / Mobile 兩張表？
- JSON 產生器要 rewrite Python 成 C# 還是保留 `Process.Start`？
- JSON 產生器完成後要 invalidate CDN cache 嗎？
- `HHH_NEW/` 舊路徑可以刪了嗎？

### 9.5 EDM / 短網址 / 操作紀錄 / 通用表格
- `send_edm()` 實作在哪？舊信件模板邏輯要全照搬還是趁機重新設計？
- GA pixel 要升級成 GA4 嗎？要不要加 per-recipient unsubscribe token？
- 短網址導流 handler（`/u/{code}`）在哪實作？click log 誰寫？
- Facebook Graph v3.2 cache 重整還需要嗎？
- 操作紀錄 `sqlcmd` 欄位要暴露給 admin 看嗎？要不要過濾敏感資料？
- `table.php` 實際被用在哪些資料表上？（需要去舊後台 sidebar 盤點）

### 9.6 共通 / 基礎建設
- `api/delete_case_img.php` 目前檔案內容跟 `update_price.php` 一樣 — 實際行為是什麼？
- `api/update_price.php` 還有前端在呼叫嗎？需要過渡 shim 嗎？
- Excel 匯出 `.xls` vs `.xlsx` 可以換嗎？
- ImageMagick 可以完全換掉嗎？
- 現在 production 用哪個 Mail provider？憑證放在哪？有 MFA 嗎？
- Weekly S3 URL 替換是一次性還是持續的工作？
- `schedule/update_photo_to_s3_per_week.php` 裡那些註解掉的 `shell_exec` 還需要嗎？

---

## 10. 已知 bug 與陷阱（移植時順手修）

- 🐛 `_htopic_mail_model.php:57` — SQL 缺 `FROM` 關鍵字
- 🐛 `shop.php:71` — 疑似從 brand 複製貼上沒改的 `_hproduct` subquery
- 🐛 `short_url_edit.php` — code 產生 retry 邏輯沒 increment counter，理論上可能無限迴圈
- 🐛 `api/delete_case_img.php` ≡ `api/update_price.php` — 檔案內容重複，copy-paste 錯誤
- ⚠️ EDM `edm_cycle.php` — 靠 `$_SESSION` + `location.reload()` 做批次寄送，根本不能規模化
- ⚠️ Facebook Graph v3.2 access token 硬編碼在 `short_url_edit.php`
- ⚠️ linkpreview.net API key 硬編碼在 `short_url_log_model.php`
- ⚠️ SMTP 憑證硬編碼在 `function/function.php` 與 `config.inc.php`
- ⚠️ 資料庫密碼硬編碼在 `config.inc.php`
- ⚠️ `table.php` / `table_edit.php` 是 generic CRUD anti-pattern，不要移植
- ⚠️ `_hvideo` 中影片時間只到「小時」級，分秒必須是 00
- 📁 很多 `.bak` 檔（`_hcase.php.bak20240819`, `_hdesigner_edit.bak`, `_hprog_edit.bak`, `_htopic_edit.bak`, `_hcase_change_name.bak`）都屬於死碼，可刪

---

## 11. 給新 feature 開工的 Checklist（每個 feature 通用）

每寫一個新 feature 前，先跑過這張 checklist：

- [ ] 讀過 `CLAUDE.md` 的 RESTful routing 與 `ApiResponse<T>` 規則
- [ ] feature 資料夾名稱用複數（`Hcolumns/` 不是 `Hcolumn/`）
- [ ] `hhh.api.contracts.admin/<Feature>/` 建立 DTO
- [ ] `hhh.application.admin/<Feature>/` 決定「薄 CRUD」還是「有 service」
  - 有業務邏輯（多表 transaction、寄信、上傳、picker、背景 job）→ service
  - 純 CRUD → 直接 controller 戳 `XoopsContext`
- [ ] `hhh.webapi.admin/Controllers/<Feature>Controller.cs` 繼承 `ApiControllerBase`
- [ ] 路由用複數 kebab-case，CRUD 全走 `GET/POST/PUT/PATCH/DELETE`
- [ ] 非 CRUD 動作用 `/api/{resource}/{id}/{verb-kebab}`
- [ ] 每個 CUD 操作呼叫 `IOperationLogWriter` 寫 audit log
- [ ] 檔案上傳用 `IImageUploadService`
- [ ] 寄信用 `IEmailService`，不要自己 new `SmtpClient`
- [ ] 長時間任務包成 background job
- [ ] Success 路徑用 `ApiResponse<T>.Success(...)` / `.Created(...)`，訊息從 service `result.Message` 拿
- [ ] Failure 路徑用 `return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message))`
- [ ] 在 `DependencyInjection.cs` 註冊 service
- [ ] Swagger 標註 `[ProducesResponseType]`（4xx/500 基底類別已經處理）
- [ ] 保留舊 Chinese comments（XML doc）不要亂改

---

**本計畫文件由 6 個平行 Explore 代理掃描產生，若任何部分與實際程式碼不符，以程式碼為準。**

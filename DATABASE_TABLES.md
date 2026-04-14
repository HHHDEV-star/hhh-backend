# DATABASE_TABLES.md

本文件記錄目前專案中**實際有使用到的資料表**，以及對應的功能說明。

> 最後更新：2026-04-10

## Xoops 資料庫（透過 XoopsContext）

### 平台管理

| MySQL 表名 | EF Entity | 使用位置 | 功能 |
|---|---|---|---|
| `admin` | Admin | AuthService, AdminService | 後台管理員帳號 / 登入驗證 |
| `_hoplog` | Hoplog | OperationLogWriter, OperationLogService | 操作日誌（audit trail） |

### 會員

| MySQL 表名 | EF Entity | 使用位置 | 功能 |
|---|---|---|---|
| `_users` | User | UserService, ForumService | 會員帳號管理 |

### 設計師與作品

| MySQL 表名 | EF Entity | 使用位置 | 功能 |
|---|---|---|---|
| `_hdesigner` | Hdesigner | HdesignerService, HawardService, EditorialCaseService, SearchService, HcaseService, VideoReportService | 設計師資料 |
| `_hcase` | Hcase | HcaseService, EditorialCaseService, HawardService, SearchService, TagService | 設計案例 |
| `_hcase_img` | HcaseImg | HcaseService, TagService | 案例圖片 |

### 內容管理 / 編輯部

| MySQL 表名 | EF Entity | 使用位置 | 功能 |
|---|---|---|---|
| `_hcolumn` | Hcolumn | EditorialColumnService, BuilderProductService, SearchService, TagService | 專欄文章 |
| `_hvideo` | Hvideo | BuilderProductService, SearchService, TagService, VideoReportService | 影片內容 |
| `_htopic2` | Htopic2 | Htopic2Service | 專題 |
| `_hpublish` | Hpublish | HpublishService | 出版品 |
| `youtube_list` | YoutubeList | YoutubeService | YouTube 影片列表 |

### 獎項與競賽

| MySQL 表名 | EF Entity | 使用位置 | 功能 |
|---|---|---|---|
| `_hawards` | Haward | HawardService | 設計獎項 |
| `_hprize` | Hprize | HprizeService | 獎品 |
| `_hcontest` | Hcontest | HcontestService | 競賽活動 |

### 建商 / 建案

| MySQL 表名 | EF Entity | 使用位置 | 功能 |
|---|---|---|---|
| `builder` | Builder | BuilderService, BuilderProductService | 建商 |
| `builder_product` | BuilderProduct | BuilderProductService | 建案 |
| `_hproduct_img` | HproductImg | BuilderProductService | 建案圖片 |
| `_hbrand` | Hbrand | SearchService, VideoReportService | 品牌 |
| `_hproduct` | Hproduct | ProductService, SearchService | 產品 |

### 社群

| MySQL 表名 | EF Entity | 使用位置 | 功能 |
|---|---|---|---|
| `forum_article` | ForumArticle | ForumService | 論壇文章 |
| `forum_article_reply` | ForumArticleReply | ForumService | 論壇回覆 |
| `brief` | Brief | BriefService | 簡介 |
| `decoration` | Decoration | DecorationService | 裝潢 |
| `precise` | Precise | PreciseService | 精選 |

### 經紀人 / 裝修

| MySQL 表名 | EF Entity | 使用位置 | 功能 |
|---|---|---|---|
| `calculator` | Calculator | CalculatorService | 裝修計算機 |
| `calculator_request` | CalculatorRequest | CalculatorRequestService | 裝修計算需求 |
| `renovation_reuqest` | RenovationReuqest | RenovationService | 裝修需求 |
| `deco_record` | DecoRecord | DecoRecordService, RenovationService | 查證照紀錄 |
| `deco_record_img` | DecoRecordImg | DecoImageService | 查證照圖片 |

### RSS

| MySQL 表名 | EF Entity | 使用位置 | 功能 |
|---|---|---|---|
| `rss_yahoo` | RssYahoo | RssYahooService | Yahoo RSS |
| `rss_msn` | RssMsn | RssMsnService | MSN RSS |
| `rss_linetoday` | RssLinetoday | RssLineTodayService | LINE TODAY RSS |
| `rss_transfer` | RssTransfer | RssTransferService | RSS 轉發 |

### 廣告

| MySQL 表名 | EF Entity | 使用位置 | 功能 |
|---|---|---|---|
| `_had` | Had | AdService | 廣告管理 |

### 0809 來電

| MySQL 表名 | EF Entity | 使用位置 | 功能 |
|---|---|---|---|
| `callin_data` | CallinDatum | CallinDataService | 0809 來電資料 |

### 主要功能

| MySQL 表名 | EF Entity | 使用位置 | 功能 |
|---|---|---|---|
| `execute_form` | ExecuteForm | ExecuteFormService | 執行表單 |
| `site_setup` | SiteSetup | SearchService | 網站設定 |
| `search_history` | SearchHistory | SearchService | 搜尋歷史 |

## HHHBackstage 資料庫（透過 HHHBackstageContext）

目前 **0 個表**有被使用。已 scaffold 但尚未接入業務邏輯：

| MySQL 表名 | EF Entity | 狀態 |
|---|---|---|
| `acl_menu_group` | AclMenuGroup | 未使用 |
| `acl_menu_path` | AclMenuPath | 未使用 |
| `acl_projects` | AclProject | 未使用 |
| `acl_users` | AclUser | 未使用 |
| `acl_user_access` | AclUserAccess | 未使用 |
| `backend` | Backend | 未使用 |

## 統計

| 項目 | 數量 |
|---|---|
| Xoops 已使用 | 38 表 |
| Xoops 已 scaffold 未使用 | ~112 表 |
| HHHBackstage 已使用 | 0 表 |
| 使用率 | ~25%（38 / 150） |

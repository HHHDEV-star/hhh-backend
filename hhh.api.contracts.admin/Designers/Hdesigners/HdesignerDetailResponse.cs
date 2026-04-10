namespace hhh.api.contracts.admin.Designers.Hdesigners;

/// <summary>
/// 設計師完整資料（對應舊版 _hdesigner_edit.php 的讀取分支）
/// </summary>
/// <remarks>
/// 對應 _hdesigner 資料表欄位，僅暴露表單會用到的欄位；
/// clicks / is_send / top / json_ld / awards_name / awards_logo 等
/// 後端內部欄位暫不納入，待實際需求再補。
/// CSV 類欄位（region / location / type / style / phone / fax / address / mail / sales_mail / designer_mail / search_keywords）
/// 維持 DB 原樣的逗號分隔字串。
/// </remarks>
public class HdesignerDetailResponse
{
    /// <summary>設計師 ID</summary>
    public uint Id { get; set; }

    // 基本資料 -----------------------------------------------------------------

    /// <summary>頭像</summary>
    public string ImgPath { get; set; } = string.Empty;

    /// <summary>桌機版背景圖</summary>
    public string? Background { get; set; }

    /// <summary>手機版背景圖</summary>
    public string? BackgroundMobile { get; set; }

    /// <summary>公司抬頭</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>設計師名稱</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>是否為優質設計師（對應 premium 欄位，varchar(50)："0" / "1"）</summary>
    public string Premium { get; set; } = "0";

    // 分類 ----------------------------------------------------------------------

    /// <summary>接案區域（CSV）</summary>
    public string Region { get; set; } = string.Empty;

    /// <summary>設計師所在區域（CSV）</summary>
    public string Location { get; set; } = string.Empty;

    /// <summary>接案類型（CSV）</summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>接案風格（CSV）</summary>
    public string Style { get; set; } = string.Empty;

    // 接案條件 -------------------------------------------------------------------

    /// <summary>接案預算</summary>
    public string Budget { get; set; } = string.Empty;

    /// <summary>接案預算下限</summary>
    public uint MinBudget { get; set; }

    /// <summary>接案預算上限</summary>
    public uint MaxBudget { get; set; }

    /// <summary>接案坪數</summary>
    public string Area { get; set; } = string.Empty;

    /// <summary>特殊接案</summary>
    public string Special { get; set; } = string.Empty;

    /// <summary>收費方式</summary>
    public string Charge { get; set; } = string.Empty;

    /// <summary>付費方式</summary>
    public string Payment { get; set; } = string.Empty;

    // 聯絡資訊 -------------------------------------------------------------------

    /// <summary>免付費電話</summary>
    public string ServicePhone { get; set; } = string.Empty;

    /// <summary>電話（CSV）</summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>傳真（CSV）</summary>
    public string Fax { get; set; } = string.Empty;

    /// <summary>地址（CSV）</summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>E-mail（CSV）</summary>
    public string Mail { get; set; } = string.Empty;

    /// <summary>網站</summary>
    public string Website { get; set; } = string.Empty;

    /// <summary>其他網址連結</summary>
    public string Blog { get; set; } = string.Empty;

    /// <summary>FB page URL</summary>
    public string Fbpageurl { get; set; } = string.Empty;

    /// <summary>LINE ID</summary>
    public string? LineId { get; set; }

    // 內容 ----------------------------------------------------------------------

    /// <summary>品牌定位</summary>
    public string Position { get; set; } = string.Empty;

    /// <summary>設計理念（儲存時也會同步寫入 description 欄位）</summary>
    public string Idea { get; set; } = string.Empty;

    /// <summary>相關經歷</summary>
    public string Career { get; set; } = string.Empty;

    /// <summary>獲獎紀錄</summary>
    public string Awards { get; set; } = string.Empty;

    /// <summary>相關證照</summary>
    public string License { get; set; } = string.Empty;

    /// <summary>左側敘述（設計師頁面 SEO）</summary>
    public string? Seo { get; set; }

    /// <summary>Line/FB/Twitter 分享用 meta description</summary>
    public string? MetaDescription { get; set; }

    // 行政 ----------------------------------------------------------------------

    /// <summary>上線狀態（0:關 / 1:開）</summary>
    public bool Onoff { get; set; }

    /// <summary>指定【設計師群組】的會員 uid</summary>
    public uint XoopsUid { get; set; }

    /// <summary>業務的 email</summary>
    public string SalesMail { get; set; } = string.Empty;

    /// <summary>通知設計師的 email（CSV）</summary>
    public string DesignerMail { get; set; } = string.Empty;

    /// <summary>幸福經紀人 ID</summary>
    public ushort Guarantee { get; set; }

    /// <summary>站內搜尋額外關鍵字（手動維護）</summary>
    public string? SearchKeywords { get; set; }

    /// <summary>站內搜尋關鍵字（電腦自動更新，唯讀）</summary>
    public string? SearchKeywordsAuto { get; set; }

    /// <summary>座標 X</summary>
    public string CoordinateX { get; set; } = string.Empty;

    /// <summary>座標 Y</summary>
    public string CoordinateY { get; set; } = string.Empty;

    /// <summary>統一編號</summary>
    public string Taxid { get; set; } = string.Empty;

    // 排序（只讀；寫入走 /api/hdesigners/sort-order 與 /mobile-sort-order）------

    /// <summary>桌機版排序</summary>
    public uint Dorder { get; set; }

    /// <summary>手機版排序</summary>
    public int MobileOrder { get; set; }

    // 時間戳 ---------------------------------------------------------------------

    /// <summary>建立時間</summary>
    public DateTime CreatTime { get; set; }

    /// <summary>更新時間</summary>
    public DateTime UpdateTime { get; set; }
}

namespace hhh.api.contracts.admin.Designers.Hcases;

/// <summary>
/// 個案完整資料（對應舊版 _hcase_edit.php 的讀取分支）
/// </summary>
public class HcaseDetailResponse
{
    /// <summary>個案 ID</summary>
    public uint Id { get; set; }

    /// <summary>上架日期</summary>
    public DateOnly Sdate { get; set; }

    // 歸屬 -----------------------------------------------------------------

    /// <summary>所屬設計師 ID</summary>
    public uint HdesignerId { get; set; }

    /// <summary>所屬設計師公司抬頭（JOIN 自 _hdesigner）</summary>
    public string DesignerTitle { get; set; } = string.Empty;

    /// <summary>所屬設計師名稱（JOIN 自 _hdesigner）</summary>
    public string DesignerName { get; set; } = string.Empty;

    // 基本內容 -------------------------------------------------------------

    /// <summary>個案名稱</summary>
    public string Caption { get; set; } = string.Empty;

    /// <summary>短說明</summary>
    public string ShortDesc { get; set; } = string.Empty;

    /// <summary>長說明</summary>
    public string LongDesc { get; set; } = string.Empty;

    /// <summary>居住成員</summary>
    public string Member { get; set; } = string.Empty;

    // 費用 / 坪數 ----------------------------------------------------------

    /// <summary>裝潢費用</summary>
    public uint Fee { get; set; }

    /// <summary>裝潢費用補充說明</summary>
    public string Feedesc { get; set; } = string.Empty;

    /// <summary>是否自動計算裝潢費用（系統欄位，update 時會在 fee 被改動後自動設為 false）</summary>
    public bool AutoCountFee { get; set; }

    /// <summary>房屋坪數</summary>
    public uint Area { get; set; }

    /// <summary>房屋坪數補充說明</summary>
    public string AreaDesc { get; set; } = string.Empty;

    // 分類 -----------------------------------------------------------------

    /// <summary>房屋位置（縣市/區）</summary>
    public string Location { get; set; } = string.Empty;

    /// <summary>設計風格</summary>
    public string Style { get; set; } = string.Empty;

    /// <summary>自訂的設計風格</summary>
    public string Style2 { get; set; } = string.Empty;

    /// <summary>房屋類型</summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>房屋狀況</summary>
    public string Condition { get; set; } = string.Empty;

    // 空間規劃 -------------------------------------------------------------

    /// <summary>圖片提供</summary>
    public string Provider { get; set; } = string.Empty;

    /// <summary>空間格局</summary>
    public string Layout { get; set; } = string.Empty;

    /// <summary>主要建材</summary>
    public string Materials { get; set; } = string.Empty;

    // 顯示 -----------------------------------------------------------------

    /// <summary>封面圖</summary>
    public string Cover { get; set; } = string.Empty;

    /// <summary>推薦值</summary>
    public uint Recommend { get; set; }

    /// <summary>觀看數（唯讀）</summary>
    public uint Viewed { get; set; }

    /// <summary>上線狀態</summary>
    public bool Onoff { get; set; }

    // VR ------------------------------------------------------------------

    /// <summary>VR360 ID</summary>
    public string Vr360Id { get; set; } = string.Empty;

    /// <summary>iStaging ID</summary>
    public string? Istaging { get; set; }

    // 排序（唯讀，寫入走 sort-order 端點） --------------------------------

    /// <summary>排序值（corder；負值為首六區、0 為一般）</summary>
    public int Corder { get; set; }

    // Tag（唯讀，寫入時由系統依 style/style2/type/condition 自動合併） ----

    /// <summary>Tag（CSV：style,style2,type,condition 自動合併）</summary>
    public string? Tag { get; set; }

    /// <summary>Tag 最後更新時間</summary>
    public DateTime TagDatetime { get; set; }

    // 時間戳 ---------------------------------------------------------------

    /// <summary>建立時間</summary>
    public DateTime CreatTime { get; set; }

    /// <summary>更新時間</summary>
    public DateTime UpdateTime { get; set; }
}

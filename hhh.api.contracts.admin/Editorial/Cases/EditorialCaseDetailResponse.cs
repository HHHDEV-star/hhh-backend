namespace hhh.api.contracts.admin.Editorial.Cases;

/// <summary>
/// 編輯部 - 個案詳細資料(編輯 popup 用)
/// (對應舊版 hhh-api/.../base/v1/Cases.php → index_get(with id) → case_model::get_case_info($id)
///  舊版只回 12 個編輯欄位,這裡為了 REST 一致性,回完整的 case 資料 — 列表 17 欄 + 編輯 12 欄)
/// </summary>
public class EditorialCaseDetailResponse
{
    // ---- 列表也有的欄位 ----
    /// <summary>個案 ID</summary>
    public uint HcaseId { get; set; }

    /// <summary>個案名稱</summary>
    public string Caption { get; set; } = string.Empty;

    /// <summary>封面圖路徑</summary>
    public string Cover { get; set; } = string.Empty;

    /// <summary>上線狀態</summary>
    public bool Onoff { get; set; }

    /// <summary>上架日期</summary>
    public DateOnly Sdate { get; set; }

    /// <summary>建立時間</summary>
    public DateTime CreatTime { get; set; }

    /// <summary>觀看數</summary>
    public uint Viewed { get; set; }

    /// <summary>VR360 ID</summary>
    public string Vr360Id { get; set; } = string.Empty;

    /// <summary>iStaging ID</summary>
    public string? Istaging { get; set; }

    /// <summary>是否自動計算費用</summary>
    public bool AutoCountFee { get; set; }

    /// <summary>設計師 ID</summary>
    public uint HdesignerId { get; set; }

    /// <summary>設計風格</summary>
    public string Style { get; set; } = string.Empty;

    /// <summary>房屋狀況</summary>
    public string Condition { get; set; } = string.Empty;

    /// <summary>房屋位置</summary>
    public string Location { get; set; } = string.Empty;

    /// <summary>房屋類型</summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>更新時間</summary>
    public DateTime UpdateTime { get; set; }

    /// <summary>設計公司名稱(從 _hdesigner JOIN)</summary>
    public string DesignerTitle { get; set; } = string.Empty;

    // ---- 編輯 popup 額外用的欄位 ----
    /// <summary>Tag(舊版前端組成 style + ',' + type + ',' + condition)</summary>
    public string? Tag { get; set; }

    /// <summary>短說明</summary>
    public string ShortDesc { get; set; } = string.Empty;

    /// <summary>長說明</summary>
    public string LongDesc { get; set; } = string.Empty;

    /// <summary>居住成員</summary>
    public string Member { get; set; } = string.Empty;

    /// <summary>裝潢費用</summary>
    public uint Fee { get; set; }

    /// <summary>裝潢費用補充說明</summary>
    public string Feedesc { get; set; } = string.Empty;

    /// <summary>房屋坪數</summary>
    public uint Area { get; set; }

    /// <summary>房屋坪數補充說明</summary>
    public string AreaDesc { get; set; } = string.Empty;

    /// <summary>自訂的設計風格</summary>
    public string Style2 { get; set; } = string.Empty;

    /// <summary>圖片提供</summary>
    public string Provider { get; set; } = string.Empty;

    /// <summary>空間格局</summary>
    public string Layout { get; set; } = string.Empty;

    /// <summary>主要建材</summary>
    public string Materials { get; set; } = string.Empty;
}

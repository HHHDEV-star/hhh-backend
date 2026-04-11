namespace hhh.api.contracts.admin.Editorial.Cases;

/// <summary>
/// 編輯部 - 個案列表項目
/// (對應舊版 hhh-api/.../base/v1/Cases.php → index_get(no id) → case_model::get_case_lists()
///  欄位順序對齊 hhh-backstage 的 case_lists 後台 view)
/// </summary>
public class EditorialCaseListItem
{
    /// <summary>個案 ID</summary>
    public uint HcaseId { get; set; }

    /// <summary>個案名稱</summary>
    public string Caption { get; set; } = string.Empty;

    /// <summary>封面圖路徑</summary>
    public string Cover { get; set; } = string.Empty;

    /// <summary>上線狀態(true=開啟,false=關閉)</summary>
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
}

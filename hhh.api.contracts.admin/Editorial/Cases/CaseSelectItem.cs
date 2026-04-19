namespace hhh.api.contracts.admin.Editorial.Cases;

/// <summary>
/// 個案下拉選單項目（精簡欄位，供關聯選擇 / 下拉選單使用）
/// </summary>
public class CaseSelectItem
{
    /// <summary>個案 ID</summary>
    public uint HcaseId { get; set; }

    /// <summary>個案名稱</summary>
    public string Caption { get; set; } = string.Empty;

    /// <summary>封面圖</summary>
    public string Cover { get; set; } = string.Empty;

    /// <summary>所屬設計師 ID</summary>
    public uint HdesignerId { get; set; }

    /// <summary>設計師公司名稱</summary>
    public string DesignerTitle { get; set; } = string.Empty;

    /// <summary>上線狀態</summary>
    public bool Onoff { get; set; }

    /// <summary>上架日期</summary>
    public DateOnly Sdate { get; set; }
}

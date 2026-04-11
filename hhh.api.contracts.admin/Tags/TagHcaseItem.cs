namespace hhh.api.contracts.admin.Tags;

/// <summary>個案標籤列表項目(對應舊版 tag_model::get_hcase)</summary>
public class TagHcaseItem
{
    public uint HcaseId { get; set; }
    public uint HdesignerId { get; set; }
    public string? Tag { get; set; }
    public DateTime TagDatetime { get; set; }
    public string Caption { get; set; } = string.Empty;
    public string Style { get; set; } = string.Empty;
    public string Style2 { get; set; } = string.Empty;
    public DateTime CreatTime { get; set; }
    /// <summary>設計公司名稱(JOIN _hdesigner)</summary>
    public string DesignerTitle { get; set; } = string.Empty;
    /// <summary>設計師姓名(JOIN _hdesigner)</summary>
    public string DesignerName { get; set; } = string.Empty;
}

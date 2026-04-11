namespace hhh.api.contracts.admin.Tags;

/// <summary>影音標籤列表項目(對應舊版 tag_model::get_hvideo)</summary>
public class TagHvideoItem
{
    public uint HvideoId { get; set; }
    public uint HdesignerId { get; set; }
    public uint HcaseId { get; set; }
    public uint HbrandId { get; set; }
    public uint HcolumnId { get; set; }
    public string TagVtype { get; set; } = string.Empty;
    public string TagVpattern { get; set; } = string.Empty;
    public string? Tag { get; set; }
    public DateTime TagDatetime { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime CreatTime { get; set; }
}

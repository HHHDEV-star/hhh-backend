namespace hhh.api.contracts.admin.Tags;

/// <summary>圖庫標籤列表項目(對應舊版 tag_model::get_image)</summary>
public class TagImageItem
{
    public uint HcaseImgId { get; set; }
    public uint HcaseId { get; set; }
    public string? Tag1 { get; set; }
    public string? Tag2 { get; set; }
    public string? Tag3 { get; set; }
    public string? Tag4 { get; set; }
    public string? Tag5 { get; set; }
    public string? Title { get; set; }
    public string? TagMan { get; set; }
    public DateTime TagDatetime { get; set; }
    /// <summary>個案名稱(lookup from _hcase)</summary>
    public string CaseCaption { get; set; } = string.Empty;
}

namespace hhh.api.contracts.admin.Tags;

/// <summary>專欄標籤列表項目(對應舊版 tag_model::get_hcolumn)</summary>
public class TagHcolumnItem
{
    public uint HcolumnId { get; set; }
    public string? Tag { get; set; }
    public DateTime TagDatetime { get; set; }
    public bool Onoff { get; set; }
    public string Ctype { get; set; } = string.Empty;
    public string Ctitle { get; set; } = string.Empty;
    public string Clogo { get; set; } = string.Empty;
    public DateTime CreatTime { get; set; }
}

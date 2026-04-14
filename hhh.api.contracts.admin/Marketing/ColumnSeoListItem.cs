namespace hhh.api.contracts.admin.Marketing;

/// <summary>專欄 SEO 列表項目</summary>
public class ColumnSeoListItem
{
    /// <summary>專欄 ID</summary>
    public uint HcolumnId { get; set; }

    /// <summary>專欄標題</summary>
    public string Ctitle { get; set; } = string.Empty;

    /// <summary>SEO 標題（FB 分享標題）</summary>
    public string? SeoTitle { get; set; }

    /// <summary>SEO 圖片（FB 分享圖片）</summary>
    public string? SeoImage { get; set; }

    /// <summary>SEO 描述（FB 分享敘述）</summary>
    public string? SeoDescription { get; set; }
}

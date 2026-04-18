namespace hhh.api.contracts.admin.Marketing;

/// <summary>專欄 SEO 列表項目</summary>
public class ColumnSeoListItem
{
    /// <summary>專欄 ID</summary>
    public uint HcolumnId { get; set; }

    /// <summary>專欄標題</summary>
    public string Ctitle { get; set; } = string.Empty;

    /// <summary>專欄類別</summary>
    public string Ctype { get; set; } = string.Empty;

    /// <summary>專欄 Logo</summary>
    public string Clogo { get; set; } = string.Empty;

    /// <summary>上線狀態</summary>
    public bool Onoff { get; set; }

    /// <summary>觀看數</summary>
    public uint Viewed { get; set; }

    /// <summary>上架日期</summary>
    public DateOnly Sdate { get; set; }

    /// <summary>SEO 標題（FB 分享標題）</summary>
    public string? SeoTitle { get; set; }

    /// <summary>SEO 圖片（FB 分享圖片）</summary>
    public string? SeoImage { get; set; }

    /// <summary>SEO 描述（FB 分享敘述）</summary>
    public string? SeoDescription { get; set; }

    /// <summary>SEO 完成度（true = 三欄位皆有值）</summary>
    public bool SeoComplete { get; set; }

    /// <summary>更新時間</summary>
    public DateTime UpdateTime { get; set; }
}

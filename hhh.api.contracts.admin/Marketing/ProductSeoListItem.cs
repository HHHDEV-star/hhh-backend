namespace hhh.api.contracts.admin.Marketing;

/// <summary>產品 SEO 列表項目</summary>
public class ProductSeoListItem
{
    /// <summary>產品 ID</summary>
    public uint Id { get; set; }

    /// <summary>產品名稱</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>SEO 標題（FB 分享標題）</summary>
    public string? SeoTitle { get; set; }

    /// <summary>SEO 圖片（FB 分享圖片）</summary>
    public string? SeoImage { get; set; }

    /// <summary>上線狀態（true=開, false=關）</summary>
    public bool Onoff { get; set; }

    /// <summary>封面圖</summary>
    public string Cover { get; set; } = string.Empty;

    /// <summary>SEO 完成度（SeoTitle + SeoImage 皆有值）</summary>
    public bool SeoComplete { get; set; }

    /// <summary>更新時間</summary>
    public DateTime UpdatedAt { get; set; }
}

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
}

namespace hhh.api.contracts.admin.Social.Products;

/// <summary>後台產品 SEO 列表項目(對應 product_model::get_product_lists_seo)</summary>
public class ProductSeoItem
{
    public uint Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? SeoTitle { get; set; }
    public string? SeoImage { get; set; }

    /// <summary>上線狀態</summary>
    public bool Onoff { get; set; }

    /// <summary>封面圖片</summary>
    public string Cover { get; set; } = string.Empty;

    /// <summary>SEO 是否完整（seo_title + seo_image 皆有值）</summary>
    public bool SeoComplete { get; set; }
}

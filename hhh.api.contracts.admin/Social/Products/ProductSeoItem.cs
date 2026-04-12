namespace hhh.api.contracts.admin.Social.Products;

/// <summary>後台產品 SEO 列表項目(對應 product_model::get_product_lists_seo)</summary>
public class ProductSeoItem
{
    public uint Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? SeoTitle { get; set; }
    public string? SeoImage { get; set; }
}

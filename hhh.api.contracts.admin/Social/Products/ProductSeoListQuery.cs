using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Social.Products;

/// <summary>產品 SEO 列表查詢條件</summary>
public class ProductSeoListQuery : PagedRequest
{
    /// <summary>模糊比對產品名稱 / SEO 標題</summary>
    public string? Keyword { get; set; }

    /// <summary>SEO 完成狀態（complete/incomplete，依 seo_title + seo_image 判斷）</summary>
    public string? SeoStatus { get; set; }
}

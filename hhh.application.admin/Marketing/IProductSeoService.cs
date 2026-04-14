using hhh.api.contracts.admin.Marketing;
using hhh.application.admin.Common;

namespace hhh.application.admin.Marketing;

/// <summary>
/// 產品 SEO 管理服務
/// （對應舊版 PHP: Product.php 16.06–16.08 seo_get / seo_put / seoimage_put）
/// </summary>
public interface IProductSeoService
{
    /// <summary>取得產品 SEO 列表（全量，id DESC）</summary>
    Task<List<ProductSeoListItem>> GetListAsync(
        CancellationToken cancellationToken = default);

    /// <summary>批次更新產品 SEO 標題</summary>
    Task<OperationResult> BatchUpdateAsync(
        List<UpdateProductSeoItem> items,
        CancellationToken cancellationToken = default);

    /// <summary>批次更新產品 SEO 圖片</summary>
    Task<OperationResult> BatchUpdateImageAsync(
        List<UpdateProductSeoImageItem> items,
        CancellationToken cancellationToken = default);
}

using hhh.api.contracts.admin.Marketing;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;

namespace hhh.application.admin.Marketing;

/// <summary>
/// 個案 SEO 管理服務
/// （對應舊版 PHP: Cases.php 14.10–14.11 seo_get / seo_put / seoimage_put）
/// </summary>
public interface ICaseSeoService
{
    /// <summary>取得個案 SEO 列表（分頁，sdate DESC, hcase_id DESC��</summary>
    Task<PagedResponse<CaseSeoListItem>> GetListAsync(
        CaseSeoListQuery query,
        CancellationToken cancellationToken = default);

    /// <summary>批次更新個案 SEO 標題/描述</summary>
    Task<OperationResult> BatchUpdateAsync(
        List<UpdateCaseSeoItem> items,
        CancellationToken cancellationToken = default);

    /// <summary>批次更新個案 SEO 圖片</summary>
    Task<OperationResult> BatchUpdateImageAsync(
        List<UpdateCaseSeoImageItem> items,
        CancellationToken cancellationToken = default);
}

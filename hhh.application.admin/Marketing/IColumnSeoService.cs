using hhh.api.contracts.admin.Marketing;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;

namespace hhh.application.admin.Marketing;

/// <summary>
/// 專欄 SEO 管理服務
/// （對應舊版 PHP: Column.php 15.11–15.12 + image_put）
/// </summary>
public interface IColumnSeoService
{
    /// <summary>取得專欄 SEO 列表（分頁，sdate DESC, hcolumn_id DESC）</summary>
    Task<PagedResponse<ColumnSeoListItem>> GetListAsync(
        ColumnSeoListQuery query,
        CancellationToken cancellationToken = default);

    /// <summary>批次更新專欄 SEO 標題/描述</summary>
    Task<OperationResult> BatchUpdateAsync(
        List<UpdateColumnSeoItem> items,
        CancellationToken cancellationToken = default);

    /// <summary>批次更新專欄 SEO 圖片</summary>
    Task<OperationResult> BatchUpdateImageAsync(
        List<UpdateColumnSeoImageItem> items,
        CancellationToken cancellationToken = default);
}

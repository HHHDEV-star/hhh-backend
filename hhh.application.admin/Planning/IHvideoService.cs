using hhh.api.contracts.admin.Planning;
using hhh.api.contracts.Common;

namespace hhh.application.admin.Planning;

/// <summary>
/// 影音管理服務（_hvideo 主表）
/// （對應舊版 PHP: _hvideo.php）
/// </summary>
public interface IHvideoService
{
    /// <summary>
    /// 取得影音分頁列表（支援 keyword 跨欄位搜尋：
    /// ID / 標題 / 說明 / 標籤 / 關聯設計師 / 廠商 / 專欄）。
    /// </summary>
    Task<PagedResponse<HvideoListItem>> GetListAsync(
        HvideoListQuery query,
        CancellationToken cancellationToken = default);
}

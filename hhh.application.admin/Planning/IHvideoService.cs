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

    /// <summary>
    /// 取得影音精簡列表（不分頁，供下拉選單 / 關聯選擇使用）。
    /// 僅回傳上線中(onoff=1)的影音，依 hvideo_id DESC 排序。
    /// 可選 keyword 做標題模糊搜尋以支援前端 combo-box 即時過濾。
    /// </summary>
    Task<List<HvideoSelectItem>> GetSelectListAsync(
        string? keyword = null,
        CancellationToken cancellationToken = default);
}

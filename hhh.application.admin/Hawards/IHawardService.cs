using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Hawards;
using hhh.application.admin.Common;

namespace hhh.application.admin.Hawards;

/// <summary>
/// 後台得獎記錄服務（對應舊版 _hawards*.php 一整組）
/// </summary>
public interface IHawardService
{
    /// <summary>
    /// 取得得獎記錄分頁列表（對應舊版 _hawards.php）。
    /// 會 JOIN 設計師與個案資料以顯示中文名稱 / 標題。
    /// </summary>
    Task<PagedResponse<HawardListItem>> GetListAsync(
        HawardListRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 取得單一得獎記錄完整資料（對應舊版 _hawards_edit.php GET 分支）
    /// </summary>
    /// <returns>找不到時回傳 null。</returns>
    Task<HawardDetailResponse?> GetByIdAsync(
        uint id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 新增得獎記錄（對應舊版 _hawards_edit.php 的新增分支）。
    /// 會驗證 logo 白名單、設計師 / 個案 FK 是否存在。
    /// </summary>
    Task<OperationResult<uint>> CreateAsync(
        CreateHawardRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 更新得獎記錄（對應舊版 _hawards_edit.php 的更新分支）。
    /// 會驗證 logo 白名單、設計師 / 個案 FK 是否存在。
    /// </summary>
    Task<OperationResult<uint>> UpdateAsync(
        uint id,
        UpdateHawardRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 刪除得獎記錄（舊版 PHP 沒有此功能,新 API 補上）。
    /// </summary>
    Task<OperationResult<uint>> DeleteAsync(
        uint id,
        CancellationToken cancellationToken = default);
}

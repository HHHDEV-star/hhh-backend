using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Hdesigners;
using hhh.application.admin.Common;

namespace hhh.application.admin.Hdesigners;

/// <summary>
/// 後台設計師服務（對應舊版 _hdesigner*.php 一整組）
/// </summary>
public interface IHdesignerService
{
    /// <summary>
    /// 取得設計師分頁列表（對應舊版 _hdesigner.php）。
    /// 支援兩種搜尋:q 關鍵字跨欄位比對、或 SearchByIdOnly=true 精準比對 hdesigner_id。
    /// </summary>
    Task<PagedResponse<HdesignerListItem>> GetListAsync(
        HdesignerListRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 取得單一設計師完整資料（對應舊版 _hdesigner_edit.php GET 分支）
    /// </summary>
    /// <returns>找不到時回傳 null。</returns>
    Task<HdesignerDetailResponse?> GetByIdAsync(
        uint id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 新增設計師（對應舊版 _hdesigner_edit.php 的新增分支）。
    /// 寫入時會自動把 idea 同步到 description 欄位、creat_time / update_time 設為 UtcNow。
    /// </summary>
    Task<OperationResult<uint>> CreateAsync(
        CreateHdesignerRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 更新設計師（對應舊版 _hdesigner_edit.php 的更新分支）。
    /// 寫入時會自動把 idea 同步到 description 欄位、update_time 設為 UtcNow。
    /// 排序欄位(dorder / mobile_order)不在此處理,請改呼叫 UpdateSortOrderAsync / UpdateMobileSortOrderAsync。
    /// </summary>
    Task<OperationResult<uint>> UpdateAsync(
        uint id,
        UpdateHdesignerRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 批次更新桌機版排序（對應舊版 _hdesigner_sort.php）。
    /// 只會更新 onoff=1 的設計師,關閉狀態忽略。
    /// </summary>
    Task<OperationResult<uint>> UpdateSortOrderAsync(
        UpdateHdesignerSortOrderRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 批次更新手機版排序（對應舊版 _hdesigner_mobile_sort.php）。
    /// 更新 mobile_order 欄位。
    /// </summary>
    Task<OperationResult<uint>> UpdateMobileSortOrderAsync(
        UpdateHdesignerSortOrderRequest request,
        CancellationToken cancellationToken = default);
}

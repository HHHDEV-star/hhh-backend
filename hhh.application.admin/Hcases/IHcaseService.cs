using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Hcases;
using hhh.application.admin.Common;

namespace hhh.application.admin.Hcases;

/// <summary>
/// 後台個案服務（對應舊版 _hcase*.php 一整組）
/// </summary>
public interface IHcaseService
{
    /// <summary>
    /// 取得個案分頁列表（對應舊版 _hcase.php）。
    /// 支援 q 跨欄位搜尋、hdesignerId 過濾、分頁、白名單排序。
    /// </summary>
    Task<PagedResponse<HcaseListItem>> GetListAsync(
        HcaseListRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 取得單一個案完整資料（對應舊版 _hcase_edit.php GET 分支）。
    /// 會 JOIN _hdesigner 帶回設計師 title / name。
    /// </summary>
    /// <returns>找不到時回傳 null。</returns>
    Task<HcaseDetailResponse?> GetByIdAsync(
        uint id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 新增個案（對應舊版 _hcase_edit.php 的新增分支）。
    /// 寫入時會自動:
    ///  - 合併 style,style2,type,condition 為 tag
    ///  - tag_datetime / creat_time / update_time = UtcNow
    ///  - auto_count_fee 預設為 false
    /// </summary>
    Task<OperationResult<uint>> CreateAsync(
        CreateHcaseRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 更新個案（對應舊版 _hcase_edit.php 的更新分支）。
    /// 寫入時會自動:
    ///  - 重新合併 tag / 更新 tag_datetime
    ///  - 若 fee 有變動,自動把 auto_count_fee 設為 false
    ///  - update_time = UtcNow
    /// 排序欄位 corder 不在此處理。
    /// </summary>
    Task<OperationResult<uint>> UpdateAsync(
        uint id,
        UpdateHcaseRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 批次更新某設計師的個案排序（對應舊版 _hcase_sort.php）。
    /// featured 陣列 → corder = position - 1000;normal 陣列 → corder = 0。
    /// 同時把 _hdesigner.update_time 更新為 UtcNow。
    /// </summary>
    Task<OperationResult<uint>> UpdateSortOrderAsync(
        UpdateHcaseSortOrderRequest request,
        CancellationToken cancellationToken = default);
}

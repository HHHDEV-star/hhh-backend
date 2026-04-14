using hhh.api.contracts.admin.Main.Execute;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;

namespace hhh.application.admin.Main.Execute;

/// <summary>
/// 執行表單服務
/// </summary>
/// <remarks>
/// 對應舊版 PHP:hhh-api/.../base/v1/Execute.php(execute_model)
/// 後台 view:execute.php + execute_detail_*.php + execute_items.php 等
///
/// 注意:此模組規模很大(原 PHP 有 35+ endpoints)。
/// 核心 CRUD 已完整實作;流程性操作(approval / notification / complete / transfer 等)
/// 尚未實作,需要逐一讀取 execute_model 業務規則後補齊。
/// </remarks>
public interface IExecuteFormService
{
    // ---- 核心 CRUD(已實作) ----

    /// <summary>取得執行表單列表(is_delete='N',exf_id DESC)</summary>
    Task<PagedResponse<ExecuteFormListItem>> GetListAsync(ListQuery query, CancellationToken cancellationToken = default);

    /// <summary>取得單一執行表單</summary>
    Task<ExecuteFormListItem?> GetByIdAsync(uint exfId, CancellationToken cancellationToken = default);

    /// <summary>新增執行表單</summary>
    Task<OperationResult<uint>> CreateAsync(
        CreateExecuteFormRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>更新執行表單</summary>
    Task<OperationResult<uint>> UpdateAsync(
        uint exfId,
        UpdateExecuteFormRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>軟刪除(is_delete='Y')</summary>
    Task<OperationResult> SoftDeleteAsync(uint exfId, CancellationToken cancellationToken = default);
}

using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Platform.OperationLogs;

namespace hhh.application.admin.Platform.OperationLogs;

/// <summary>
/// 操作紀錄查詢服務
/// (對應舊版 PHP /backend/_hoplog.php)
/// 純讀取,不寫入(寫入由 IOperationLogWriter 負責)。
/// </summary>
public interface IOperationLogService
{
    Task<PagedResponse<OperationLogListItem>> GetListAsync(
        OperationLogListRequest request,
        CancellationToken cancellationToken = default);

    Task<OperationLogDetailResponse?> GetByIdAsync(
        uint id,
        CancellationToken cancellationToken = default);
}

using hhh.api.contracts.admin.Platform.SystemLogs;
using hhh.api.contracts.Common;

namespace hhh.application.admin.Platform.SystemLogs;

/// <summary>後台系統 API Log 查詢服務</summary>
/// <remarks>
/// 對應舊版 PHP:system/v1/System.php → backend_logs_get()
///             → System_model::get_backend_logs()
/// 資料來源：hhh_api.rest_backend_logs
/// </remarks>
public interface ISystemLogService
{
    /// <summary>取得 API Log 列表（分頁＋篩選）</summary>
    Task<PagedResponse<SystemLogListItem>> GetListAsync(
        SystemLogListRequest request, CancellationToken ct = default);
}

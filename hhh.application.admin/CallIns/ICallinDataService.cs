using hhh.api.contracts.admin.CallIns;
using hhh.application.admin.Common;

namespace hhh.application.admin.CallIns;

/// <summary>
/// 0809 來電資料服務
/// （對應舊版 PHP: Callin.php + Callin_model.php）
/// </summary>
public interface ICallinDataService
{
    /// <summary>取得 0809 來電資料列表（全量，含黑名單標示）</summary>
    Task<List<CallinDataListItem>> GetListAsync(
        CancellationToken cancellationToken = default);

    /// <summary>批次新增 0809 來電資料（重複檢查後僅新增不存在的紀錄）</summary>
    Task<OperationResult<BatchCreateCallinResult>> BatchCreateAsync(
        List<CallinDataItemRequest> items,
        CancellationToken cancellationToken = default);
}

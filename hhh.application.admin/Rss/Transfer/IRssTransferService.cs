using hhh.api.contracts.admin.Rss;
using hhh.api.contracts.Common;

namespace hhh.application.admin.Rss.Transfer;

/// <summary>RSS 轉接紀錄服務</summary>
public interface IRssTransferService
{
    Task<PagedResponse<RssTransferLogItem>> GetLogsAsync(
        RssTransferLogListQuery query,
        CancellationToken cancellationToken = default);

    Task<List<RssTransferStatItem>> GetStatisticsAsync(
        DateTime? startDate, DateTime? endDate,
        CancellationToken cancellationToken = default);
}

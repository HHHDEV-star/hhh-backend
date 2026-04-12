using hhh.api.contracts.admin.Rss;

namespace hhh.application.admin.Rss.Transfer;

/// <summary>RSS 轉接紀錄服務</summary>
public interface IRssTransferService
{
    Task<List<RssTransferLogItem>> GetLogsAsync(
        DateTime? startDate, DateTime? endDate,
        CancellationToken cancellationToken = default);

    Task<List<RssTransferStatItem>> GetStatisticsAsync(
        DateTime? startDate, DateTime? endDate,
        CancellationToken cancellationToken = default);
}

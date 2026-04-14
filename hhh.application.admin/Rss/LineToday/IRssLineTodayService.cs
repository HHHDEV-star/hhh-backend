using hhh.api.contracts.admin.Rss;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;

namespace hhh.application.admin.Rss.LineToday;

/// <summary>LineToday RSS 排程服務</summary>
public interface IRssLineTodayService
{
    Task<PagedResponse<RssLineTodayItem>> GetListAsync(ListQuery query, CancellationToken cancellationToken = default);
    Task<OperationResult<uint>> CreateAsync(RssLineTodayRequest request, CancellationToken cancellationToken = default);
    Task<OperationResult<uint>> UpdateAsync(uint id, RssLineTodayRequest request, CancellationToken cancellationToken = default);
}

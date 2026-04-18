using hhh.api.contracts.admin.Rss;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;

namespace hhh.application.admin.Rss.Msn;

/// <summary>MSN RSS 排程服務</summary>
public interface IRssMsnService
{
    Task<PagedResponse<RssScheduleItem>> GetListAsync(RssScheduleListQuery query, CancellationToken cancellationToken = default);
    Task<OperationResult<uint>> CreateAsync(RssScheduleRequest request, CancellationToken cancellationToken = default);
    Task<OperationResult<uint>> UpdateAsync(uint id, RssScheduleRequest request, CancellationToken cancellationToken = default);
}

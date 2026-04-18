using hhh.api.contracts.admin.WebSite.DecoRecords;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;

namespace hhh.application.admin.WebSite.DecoRecords;

/// <summary>查證照(室內裝修業登記)後台服務</summary>
public interface IDecoRecordService
{
    Task<PagedResponse<DecoRecordListItem>> GetListAsync(DecoRecordListQuery query, CancellationToken ct = default);
    Task<OperationResult> UpdateAsync(int bldsno, UpdateDecoRecordRequest request, CancellationToken ct = default);
}

using hhh.api.contracts.admin.Social.Precises;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;

namespace hhh.application.admin.Social.Precises;

/// <summary>精準名單白皮書 服務</summary>
public interface IPreciseService
{
    Task<PagedResponse<PreciseListItem>> GetListAsync(PreciseListQuery query, CancellationToken cancellationToken = default);
    Task<OperationResult<int>> CreateAsync(CreatePreciseRequest request, CancellationToken cancellationToken = default);
}

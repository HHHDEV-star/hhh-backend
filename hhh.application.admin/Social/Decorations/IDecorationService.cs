using hhh.api.contracts.admin.Social.Decorations;
using hhh.application.admin.Common;

namespace hhh.application.admin.Social.Decorations;

/// <summary>全室裝修收名單 服務</summary>
public interface IDecorationService
{
    Task<List<DecorationListItem>> GetListAsync(CancellationToken cancellationToken = default);
    Task<OperationResult<uint>> CreateAsync(CreateDecorationRequest request, CancellationToken cancellationToken = default);
}

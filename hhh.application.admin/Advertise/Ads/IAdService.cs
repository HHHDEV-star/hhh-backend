using hhh.api.contracts.admin.Advertise.Ads;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;

namespace hhh.application.admin.Advertise.Ads;

/// <summary>廣告管理服務(對應 _had 表)</summary>
public interface IAdService
{
    Task<PagedResponse<AdListItem>> GetListAsync(AdListQuery query, CancellationToken ct = default);
    Task<OperationResult<uint>> CreateAsync(CreateAdRequest request, CancellationToken ct = default);
    Task<OperationResult<uint>> UpdateAsync(uint adid, UpdateAdRequest request, CancellationToken ct = default);
    Task<OperationResult> DeleteAsync(uint adid, CancellationToken ct = default);
    Task<OperationResult> UpdateLogoAsync(uint adid, UpdateAdLogoRequest request, CancellationToken ct = default);
}

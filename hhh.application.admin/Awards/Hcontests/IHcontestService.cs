using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Awards.Hcontests;
using hhh.application.admin.Common;

namespace hhh.application.admin.Awards.Hcontests;

/// <summary>
/// 競賽報名管理服務
/// (對應舊版 PHP /backend/_hcontest.php、_hcontest_edit.php)
/// </summary>
public interface IHcontestService
{
    Task<PagedResponse<HcontestListItem>> GetListAsync(
        HcontestListRequest request,
        CancellationToken cancellationToken = default);

    Task<HcontestDetailResponse?> GetByIdAsync(
        uint id,
        CancellationToken cancellationToken = default);

    Task<OperationResult<uint>> CreateAsync(
        CreateHcontestRequest request,
        CancellationToken cancellationToken = default);

    Task<OperationResult<uint>> UpdateAsync(
        uint id,
        UpdateHcontestRequest request,
        CancellationToken cancellationToken = default);

    Task<OperationResult> DeleteAsync(
        uint id,
        CancellationToken cancellationToken = default);
}

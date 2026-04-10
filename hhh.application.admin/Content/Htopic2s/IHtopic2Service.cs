using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Content.Htopic2s;
using hhh.application.admin.Common;

namespace hhh.application.admin.Content.Htopic2s;

/// <summary>
/// 議題 2 管理服務
/// (對應舊版 PHP /backend/_htopic2.php、_htopic2_edit.php)
/// </summary>
public interface IHtopic2Service
{
    Task<PagedResponse<Htopic2ListItem>> GetListAsync(
        Htopic2ListRequest request,
        CancellationToken cancellationToken = default);

    Task<Htopic2DetailResponse?> GetByIdAsync(
        uint id,
        CancellationToken cancellationToken = default);

    Task<OperationResult<uint>> CreateAsync(
        CreateHtopic2Request request,
        CancellationToken cancellationToken = default);

    Task<OperationResult<uint>> UpdateAsync(
        uint id,
        UpdateHtopic2Request request,
        CancellationToken cancellationToken = default);

    Task<OperationResult> DeleteAsync(
        uint id,
        CancellationToken cancellationToken = default);
}

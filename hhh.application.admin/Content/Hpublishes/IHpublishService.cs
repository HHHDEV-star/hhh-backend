using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Content.Hpublishes;
using hhh.application.admin.Common;

namespace hhh.application.admin.Content.Hpublishes;

/// <summary>
/// 出版管理服務
/// (對應舊版 PHP /backend/_hpublish.php、_hpublish_edit.php)
/// </summary>
public interface IHpublishService
{
    Task<PagedResponse<HpublishListItem>> GetListAsync(
        HpublishListRequest request,
        CancellationToken cancellationToken = default);

    Task<HpublishDetailResponse?> GetByIdAsync(
        uint id,
        CancellationToken cancellationToken = default);

    Task<OperationResult<uint>> CreateAsync(
        CreateHpublishRequest request,
        CancellationToken cancellationToken = default);

    Task<OperationResult<uint>> UpdateAsync(
        uint id,
        UpdateHpublishRequest request,
        CancellationToken cancellationToken = default);

    Task<OperationResult> DeleteAsync(
        uint id,
        CancellationToken cancellationToken = default);
}

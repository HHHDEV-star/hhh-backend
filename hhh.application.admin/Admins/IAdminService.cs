using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Admins;
using hhh.application.admin.Common;

namespace hhh.application.admin.Admins;

public interface IAdminService
{
    /// <summary>
    /// 取得管理者分頁列表（對應舊版 admin.php）
    /// </summary>
    Task<PagedResponse<AdminListItem>> GetListAsync(
        AdminListRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 取得單一管理者（對應舊版 admin_edit.php 的 GET 模式）
    /// </summary>
    /// <returns>找不到時回傳 null。</returns>
    Task<AdminDetailResponse?> GetByIdAsync(
        uint id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 新增管理者（對應舊版 admin_edit.php 的 POST 新增分支）
    /// </summary>
    Task<OperationResult<uint>> CreateAsync(
        CreateAdminRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 更新管理者（對應舊版 admin_edit.php 的 POST 更新分支）
    /// </summary>
    Task<OperationResult<uint>> UpdateAsync(
        uint id,
        UpdateAdminRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 更新自己的 profile / 密碼（對應舊版 admin_password.php）。
    /// 與 UpdateAsync 的差別：不改 account / allow_page / is_active。
    /// </summary>
    Task<OperationResult<uint>> UpdateProfileAsync(
        uint id,
        UpdateAdminProfileRequest request,
        CancellationToken cancellationToken = default);
}

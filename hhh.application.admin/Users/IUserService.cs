using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Users;
using hhh.application.admin.Common;

namespace hhh.application.admin.Users;

public interface IUserService
{
    /// <summary>
    /// 取得會員分頁列表（對應舊版 _users.php）
    /// </summary>
    Task<PagedResponse<UserListItem>> GetListAsync(
        UserListRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 取得單一會員（對應舊版 _users_edit.php 的 GET 模式）
    /// </summary>
    /// <returns>找不到時回傳 null。</returns>
    Task<UserDetailResponse?> GetByIdAsync(
        uint id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 新增會員（對應舊版 _users_edit.php 的 POST 新增分支）
    /// </summary>
    Task<OperationResult<uint>> CreateAsync(
        CreateUserRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 更新會員（對應舊版 _users_edit.php 的 POST 更新分支）
    /// </summary>
    Task<OperationResult<uint>> UpdateAsync(
        uint id,
        UpdateUserRequest request,
        CancellationToken cancellationToken = default);
}

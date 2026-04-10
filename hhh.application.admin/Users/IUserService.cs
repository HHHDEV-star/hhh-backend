using hhh.api.contracts.admin.Users;

namespace hhh.application.admin.Users;

public interface IUserService
{
    /// <summary>
    /// 取得會員分頁列表（對應舊版 _users.php）
    /// </summary>
    Task<UserListResponse> GetListAsync(UserListRequest request, CancellationToken cancellationToken = default);
}

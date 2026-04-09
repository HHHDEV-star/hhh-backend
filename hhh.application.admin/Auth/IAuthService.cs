using hhh.api.contracts.admin.Auth;

namespace hhh.application.admin.Auth;

public interface IAuthService
{
    /// <summary>
    /// 管理後台登入
    /// </summary>
    Task<LoginResult> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);
}

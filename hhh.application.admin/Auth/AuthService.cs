using System.Security.Claims;
using hhh.api.contracts.admin.Auth;
using hhh.infrastructure.Auth;
using hhh.infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace hhh.application.admin.Auth;

public class AuthService : IAuthService
{
    private readonly XoopsContext _db;
    private readonly IJwtTokenGenerator _tokenGenerator;
    private readonly ILogger<AuthService> _logger;

    public AuthService(XoopsContext db, IJwtTokenGenerator tokenGenerator, ILogger<AuthService> logger)
    {
        _db = db;
        _tokenGenerator = tokenGenerator;
        _logger = logger;
    }

    public async Task<LoginResult> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        // 注意：沿用舊系統的字串比對邏輯（admin.pwd 欄位為 varchar(40)）。
        //      若要升級為 BCrypt/Argon2，請同步調整登入流程與資料遷移。
        var admin = await _db.Admins
            .AsNoTracking()
            .FirstOrDefaultAsync(
                a => a.Account == request.Account && a.Pwd == request.Pwd,
                cancellationToken);

        if (admin is null)
        {
            _logger.LogWarning("Login failed for account: {Account}", request.Account);
            return LoginResult.Fail(401, "帳密錯誤，登入失敗");
        }

        if (admin.IsActive == 0)
        {
            _logger.LogWarning("Disabled account tried to login: {Account}", request.Account);
            return LoginResult.Fail(403, "帳號已停用，無法登入");
        }

        // 把 Admin 實體攤平成 claims，token generator 本身不認得 Admin
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, admin.Id.ToString()),
            new(ClaimTypes.Name, admin.Account ?? string.Empty),
            new("name", admin.Name ?? string.Empty),
            new("email", admin.Email ?? string.Empty),
        };

        var (token, expiresIn) = _tokenGenerator.Generate(admin.Id.ToString(), claims);

        var response = new LoginResponse
        {
            AccessToken = token,
            TokenType = "Bearer",
            ExpiresIn = expiresIn,
            User = new AdminUserInfo
            {
                Id = admin.Id,
                Account = admin.Account,
                Name = admin.Name,
                Email = admin.Email,
                Tel = admin.Tel,
                AllowPage = admin.AllowPage,
            }
        };

        return LoginResult.Success(response);
    }
}

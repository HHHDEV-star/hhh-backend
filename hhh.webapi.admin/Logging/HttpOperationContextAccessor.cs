using System.Security.Claims;
using hhh.infrastructure.Logging;

namespace hhh.webapi.admin.Logging;

/// <summary>
/// <see cref="IOperationContextAccessor"/> 的 Web host 端實作，
/// 從 <see cref="IHttpContextAccessor"/> 讀取目前請求的 JWT claims + 連線 IP。
///
/// 對應關係：
/// <list type="bullet">
///   <item>UserId → ClaimTypes.NameIdentifier（AuthService 帶入 admin.Id）</item>
///   <item>UserName → 優先 "name"（admin.Name 中文姓名），退而求其次用 ClaimTypes.Name（admin.Account）</item>
///   <item>ClientIp → HttpContext.Connection.RemoteIpAddress（若有反向代理，之後可改讀 X-Forwarded-For）</item>
/// </list>
/// </summary>
public sealed class HttpOperationContextAccessor : IOperationContextAccessor
{
    private readonly IHttpContextAccessor _accessor;

    public HttpOperationContextAccessor(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public uint? UserId
    {
        get
        {
            var principal = _accessor.HttpContext?.User;
            if (principal is null) return null;

            var raw = principal.FindFirstValue(ClaimTypes.NameIdentifier)
                   ?? principal.FindFirstValue("sub");

            return uint.TryParse(raw, out var id) ? id : null;
        }
    }

    public string? UserName
    {
        get
        {
            var principal = _accessor.HttpContext?.User;
            if (principal is null) return null;

            // AuthService 用 "name" claim 存中文姓名，_hoplog.uname 也是存姓名
            return principal.FindFirstValue("name")
                ?? principal.FindFirstValue(ClaimTypes.Name)
                ?? principal.Identity?.Name;
        }
    }

    public string? ClientIp =>
        _accessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
}

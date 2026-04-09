using System.Security.Claims;

namespace hhh.infrastructure.Auth;

/// <summary>
/// JWT Token 產生器（技術實作，不綁定任何業務實體）
/// </summary>
public interface IJwtTokenGenerator
{
    /// <summary>
    /// 為指定的主體產生 JWT Token
    /// </summary>
    /// <param name="subject">Token 的 subject（通常是使用者 ID）</param>
    /// <param name="claims">要寫入 Token 的 claims（例如 name、email、role）</param>
    /// <returns>Token 字串與有效期（秒）</returns>
    (string Token, long ExpiresIn) Generate(string subject, IEnumerable<Claim> claims);
}

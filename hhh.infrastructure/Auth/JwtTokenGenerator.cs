using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace hhh.infrastructure.Auth;

/// <summary>
/// JWT Token 產生器實作
/// </summary>
/// <remarks>
/// 設定來源：appsettings.json 的 "Jwt" 區段（Issuer / Audience / SecretKey / ExpireMinutes）
/// 此類別純粹是技術實作，不認得任何業務實體（Admin / User / …），
/// 業務層負責將必要欄位攤平成 claims 後傳入。
/// </remarks>
public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IConfiguration _configuration;

    public JwtTokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public (string Token, long ExpiresIn) Generate(string subject, IEnumerable<Claim> claims)
    {
        var jwtSection = _configuration.GetSection("Jwt");
        var secretKey = jwtSection["SecretKey"]
            ?? throw new InvalidOperationException("Jwt:SecretKey is not configured.");
        var issuer = jwtSection["Issuer"];
        var audience = jwtSection["Audience"];
        var expireMinutes = int.TryParse(jwtSection["ExpireMinutes"], out var m) ? m : 120;

        var now = DateTime.UtcNow;

        var allClaims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, subject),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat,
                new DateTimeOffset(now).ToUnixTimeSeconds().ToString(),
                ClaimValueTypes.Integer64),
        };
        allClaims.AddRange(claims);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expires = now.AddMinutes(expireMinutes);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: allClaims,
            notBefore: now,
            expires: expires,
            signingCredentials: credentials);

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        var expiresIn = (long)TimeSpan.FromMinutes(expireMinutes).TotalSeconds;
        return (tokenString, expiresIn);
    }
}

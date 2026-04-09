namespace hhh.api.contracts.admin.Auth;

/// <summary>
/// 登入成功回應內容
/// </summary>
public class LoginResponse
{
    /// <summary>JWT Access Token</summary>
    public string AccessToken { get; set; } = null!;

    /// <summary>Token 型態，固定為 Bearer</summary>
    public string TokenType { get; set; } = "Bearer";

    /// <summary>Token 有效期（秒）</summary>
    public long ExpiresIn { get; set; }

    /// <summary>登入者資訊</summary>
    public AdminUserInfo User { get; set; } = null!;
}


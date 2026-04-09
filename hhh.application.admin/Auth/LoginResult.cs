using hhh.api.contracts.admin.Auth;

namespace hhh.application.admin.Auth;

/// <summary>
/// 登入作業結果（Application 層回傳的 domain result，與 HTTP 無關）
/// </summary>
public class LoginResult
{
    public bool IsSuccess { get; private init; }
    public int Code { get; private init; }
    public string Message { get; private init; } = string.Empty;
    public LoginResponse? Data { get; private init; }

    public static LoginResult Success(LoginResponse data) => new()
    {
        IsSuccess = true,
        Code = 200,
        Message = "登入成功",
        Data = data
    };

    public static LoginResult Fail(int code, string message) => new()
    {
        IsSuccess = false,
        Code = code,
        Message = message,
        Data = null
    };
}

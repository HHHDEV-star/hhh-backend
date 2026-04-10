namespace hhh.application.admin.Users;

/// <summary>
/// 會員新增 / 更新的 domain 結果。
/// Code 與 HTTP status 對齊：200 OK、201 Created、404 NotFound、409 Conflict。
/// </summary>
public class UserMutationResult
{
    public bool IsSuccess { get; init; }
    public int Code { get; init; }
    public string Message { get; init; } = string.Empty;

    /// <summary>成功時回傳的會員 ID（新增 = 新產生的 uid；更新 = 原 uid）</summary>
    public uint? UserId { get; init; }

    public static UserMutationResult Ok(uint userId, string message = "更新成功")
        => new() { IsSuccess = true, Code = 200, Message = message, UserId = userId };

    public static UserMutationResult Created(uint userId, string message = "新增成功")
        => new() { IsSuccess = true, Code = 201, Message = message, UserId = userId };

    public static UserMutationResult Fail(int code, string message)
        => new() { IsSuccess = false, Code = code, Message = message };
}

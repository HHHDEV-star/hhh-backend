namespace hhh.application.admin.Admins;

/// <summary>
/// 管理者新增 / 更新的 domain 結果。
/// Code 與 HTTP status 對齊：200 OK、201 Created、404 NotFound、409 Conflict。
/// </summary>
public class AdminMutationResult
{
    public bool IsSuccess { get; init; }
    public int Code { get; init; }
    public string Message { get; init; } = string.Empty;

    /// <summary>成功時回傳的管理者 ID（新增 = 新產生的 id；更新 = 原 id）</summary>
    public uint? AdminId { get; init; }

    public static AdminMutationResult Ok(uint adminId, string message = "更新成功")
        => new() { IsSuccess = true, Code = 200, Message = message, AdminId = adminId };

    public static AdminMutationResult Created(uint adminId, string message = "新增成功")
        => new() { IsSuccess = true, Code = 201, Message = message, AdminId = adminId };

    public static AdminMutationResult Fail(int code, string message)
        => new() { IsSuccess = false, Code = code, Message = message };
}

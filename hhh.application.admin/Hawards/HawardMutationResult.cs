namespace hhh.application.admin.Hawards;

/// <summary>
/// 後台得獎記錄新增 / 更新 / 刪除結果（不綁定 HTTP）
/// </summary>
public class HawardMutationResult
{
    public bool IsSuccess { get; init; }
    public int Code { get; init; }
    public string Message { get; init; } = string.Empty;
    public uint? HawardsId { get; init; }

    public static HawardMutationResult Ok(uint? hawardsId = null, string message = "更新成功")
        => new() { IsSuccess = true, Code = 200, Message = message, HawardsId = hawardsId };

    public static HawardMutationResult Created(uint hawardsId, string message = "新增成功")
        => new() { IsSuccess = true, Code = 201, Message = message, HawardsId = hawardsId };

    public static HawardMutationResult Deleted(string message = "刪除成功")
        => new() { IsSuccess = true, Code = 200, Message = message };

    public static HawardMutationResult Fail(int code, string message)
        => new() { IsSuccess = false, Code = code, Message = message };
}

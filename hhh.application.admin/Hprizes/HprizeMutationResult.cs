namespace hhh.application.admin.Hprizes;

/// <summary>
/// 後台獎品新增 / 更新 / 刪除結果（不綁定 HTTP）
/// </summary>
public class HprizeMutationResult
{
    public bool IsSuccess { get; init; }
    public int Code { get; init; }
    public string Message { get; init; } = string.Empty;
    public uint? HprizeId { get; init; }

    /// <summary>
    /// 若此次操作需要刪除舊上傳檔，這裡會回傳舊檔的相對路徑，
    /// controller 收到後呼叫 IImageUploadService.Delete 清理。
    /// </summary>
    public string? OldLogoRelativePath { get; init; }

    public static HprizeMutationResult Ok(
        uint? hprizeId = null,
        string message = "更新成功",
        string? oldLogoRelativePath = null)
        => new()
        {
            IsSuccess = true,
            Code = 200,
            Message = message,
            HprizeId = hprizeId,
            OldLogoRelativePath = oldLogoRelativePath,
        };

    public static HprizeMutationResult Created(uint hprizeId, string message = "新增成功")
        => new() { IsSuccess = true, Code = 201, Message = message, HprizeId = hprizeId };

    public static HprizeMutationResult Deleted(
        string message = "刪除成功",
        string? oldLogoRelativePath = null)
        => new()
        {
            IsSuccess = true,
            Code = 200,
            Message = message,
            OldLogoRelativePath = oldLogoRelativePath,
        };

    public static HprizeMutationResult Fail(int code, string message)
        => new() { IsSuccess = false, Code = code, Message = message };
}

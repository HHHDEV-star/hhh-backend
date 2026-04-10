using hhh.application.admin.Common;

namespace hhh.application.admin.Awards.Hprizes;

/// <summary>
/// 後台獎品新增 / 更新 / 刪除結果（不綁定 HTTP）。
/// 繼承 <see cref="OperationResult{TData}"/>（TData = uint 代表 hprize_id），
/// 額外攜帶需要由 controller 清理的舊檔相對路徑。
/// </summary>
public class HprizeMutationResult : OperationResult<uint>
{
    /// <summary>
    /// 若此次操作需要刪除舊上傳檔,這裡會回傳舊檔的相對路徑,
    /// controller 收到後呼叫 IImageUploadService.Delete 清理。
    /// </summary>
    public string? OldLogoRelativePath { get; init; }

    public static HprizeMutationResult Ok(
        uint hprizeId,
        string message = "更新成功",
        string? oldLogoRelativePath = null)
        => new()
        {
            Code = 200,
            Message = message,
            Data = hprizeId,
            OldLogoRelativePath = oldLogoRelativePath,
        };

    public static new HprizeMutationResult Created(uint hprizeId, string message = "新增成功")
        => new() { Code = 201, Message = message, Data = hprizeId };

    public static HprizeMutationResult Deleted(
        string message = "刪除成功",
        string? oldLogoRelativePath = null)
        => new()
        {
            Code = 200,
            Message = message,
            OldLogoRelativePath = oldLogoRelativePath,
        };

    public static new HprizeMutationResult Fail(int code, string message)
        => new() { Code = code, Message = message };
}

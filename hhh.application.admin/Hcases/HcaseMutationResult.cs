namespace hhh.application.admin.Hcases;

/// <summary>
/// 個案新增 / 更新 / 排序的 domain 結果。
/// Code 與 HTTP status 對齊：200 OK、201 Created、404 NotFound、409 Conflict。
/// </summary>
public class HcaseMutationResult
{
    public bool IsSuccess { get; init; }
    public int Code { get; init; }
    public string Message { get; init; } = string.Empty;

    /// <summary>成功時回傳的個案 ID（新增 = 新產生的 id；更新 = 原 id；批次排序 = null）</summary>
    public uint? HcaseId { get; init; }

    public static HcaseMutationResult Ok(uint? hcaseId = null, string message = "更新成功")
        => new() { IsSuccess = true, Code = 200, Message = message, HcaseId = hcaseId };

    public static HcaseMutationResult Created(uint hcaseId, string message = "新增成功")
        => new() { IsSuccess = true, Code = 201, Message = message, HcaseId = hcaseId };

    public static HcaseMutationResult Fail(int code, string message)
        => new() { IsSuccess = false, Code = code, Message = message };
}

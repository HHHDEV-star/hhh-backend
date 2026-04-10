namespace hhh.application.admin.Common;

/// <summary>
/// 應用層的通用操作結果(不綁定 HTTP)。
/// Code 與 HTTP status 對齊:200 OK、201 Created、404 NotFound、409 Conflict、400 BadRequest。
/// Controller 層負責把 <see cref="Code"/> 對映到 HTTP status code。
/// </summary>
/// <remarks>
/// 設計原則:
/// <list type="bullet">
///   <item>應用層永遠不應該拋例外代表「預期失敗」(找不到、重複、驗證失敗)。</item>
///   <item>只有真正未預期的錯誤才拋例外,由 Program.cs 的 global exception handler 接住。</item>
///   <item>需要額外回傳欄位的 feature 可以繼承這個類別(如 <c>HprizeMutationResult</c>)。</item>
/// </list>
/// </remarks>
public class OperationResult
{
    public int Code { get; init; }
    public string Message { get; init; } = string.Empty;

    /// <summary>Code 介於 200 ~ 299 之間視為成功。</summary>
    public bool IsSuccess => Code is >= 200 and < 300;

    public static OperationResult Ok(string message = "操作成功")
        => new() { Code = 200, Message = message };

    public static OperationResult Created(string message = "新增成功")
        => new() { Code = 201, Message = message };

    public static OperationResult NotFound(string message)
        => new() { Code = 404, Message = message };

    public static OperationResult Conflict(string message)
        => new() { Code = 409, Message = message };

    public static OperationResult BadRequest(string message)
        => new() { Code = 400, Message = message };

    public static OperationResult Fail(int code, string message)
        => new() { Code = code, Message = message };
}

/// <summary>
/// 帶 payload 的通用操作結果。成功時 <see cref="Data"/> 會帶值,
/// 失敗時 <see cref="Data"/> 為 default。
/// </summary>
/// <typeparam name="TData">
/// 成功時回傳的資料型別。Mutation 操作通常是新產生的 id(<c>uint</c>),
/// Query 操作則是對應的 DTO。
/// </typeparam>
public class OperationResult<TData> : OperationResult
{
    public TData? Data { get; init; }

    public static OperationResult<TData> Ok(TData data, string message = "操作成功")
        => new() { Code = 200, Message = message, Data = data };

    /// <summary>
    /// 成功但沒有 payload 要回(例如批次排序更新、只是「動作完成」)。
    /// <see cref="Data"/> 會是 default(TData?)。
    /// </summary>
    public static new OperationResult<TData> Ok(string message = "操作成功")
        => new() { Code = 200, Message = message };

    public static OperationResult<TData> Created(TData data, string message = "新增成功")
        => new() { Code = 201, Message = message, Data = data };

    public static new OperationResult<TData> NotFound(string message)
        => new() { Code = 404, Message = message };

    public static new OperationResult<TData> Conflict(string message)
        => new() { Code = 409, Message = message };

    public static new OperationResult<TData> BadRequest(string message)
        => new() { Code = 400, Message = message };

    public static new OperationResult<TData> Fail(int code, string message)
        => new() { Code = code, Message = message };
}

namespace hhh.api.contracts.Common;

/// <summary>
/// 統一 API 回應格式：{ code, message, data }
/// 所有 controller 都必須透過這個型別回傳，不論成功或失敗。
/// </summary>
public class ApiResponse<T>
{
    public int Code { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }

    public static ApiResponse<T> Success(T data, string message = "success")
        => new() { Code = 200, Message = message, Data = data };

    /// <summary>
    /// 201 Created 專用成功工廠。語意與 <see cref="Success"/> 一致，只是 HTTP status = 201。
    /// 用於 POST 建立資源的成功回傳，與 controller 的 <c>StatusCode(201, ...)</c> 搭配。
    /// </summary>
    public static ApiResponse<T> Created(T data, string message = "success")
        => new() { Code = 201, Message = message, Data = data };

    public static ApiResponse<T> Fail(int code, string message)
        => new() { Code = code, Message = message, Data = default };
}

/// <summary>
/// 無資料（或錯誤）回應的快速工廠。統一回 <see cref="ApiResponse{Object}"/>，
/// 讓 controller、exception handler、auth challenge、model validation 全部共用同一個型別，
/// 避免 Swagger schema 出現重複定義。
/// </summary>
public static class ApiResponse
{
    public static ApiResponse<object> Ok(string message = "success")
        => new() { Code = 200, Message = message, Data = null };

    public static ApiResponse<object> Error(int code, string message)
        => new() { Code = code, Message = message, Data = null };
}

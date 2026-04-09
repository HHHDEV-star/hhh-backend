namespace hhh.api.contracts.Common;

/// <summary>
/// 統一 API 回應格式 { code, message, data }
/// </summary>
public class ApiResponse<T>
{
    public int Code { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }

    public static ApiResponse<T> Success(T data, string message = "success")
        => new() { Code = 200, Message = message, Data = data };

    public static ApiResponse<T> Fail(int code, string message)
        => new() { Code = code, Message = message, Data = default };
}

/// <summary>
/// 不帶資料的 API 回應（錯誤或單純成功訊息）
/// </summary>
public class ApiResponse : ApiResponse<object>
{
    public static ApiResponse Ok(string message = "success")
        => new() { Code = 200, Message = message };

    public static ApiResponse Error(int code, string message)
        => new() { Code = code, Message = message };
}

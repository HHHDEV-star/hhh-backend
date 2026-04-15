namespace hhh.api.contracts.admin.Platform.SystemLogs;

/// <summary>
/// 後台系統 API Log 列表項目
/// （對應舊版 PHP:system/v1/System::backend_logs_get）
/// 資料來源：hhh_api.rest_backend_logs
/// </summary>
public class SystemLogListItem
{
    /// <summary>主鍵</summary>
    public int Id { get; set; }

    /// <summary>帳號</summary>
    public string Account { get; set; } = null!;

    /// <summary>使用的 API</summary>
    public string Uri { get; set; } = null!;

    /// <summary>傳送方式（GET/PUT/POST）</summary>
    public string Method { get; set; } = null!;

    /// <summary>傳入的值</summary>
    public string? Params { get; set; }

    /// <summary>鑰匙</summary>
    public string ApiKey { get; set; } = null!;

    /// <summary>IP</summary>
    public string IpAddress { get; set; } = null!;

    /// <summary>建立時間</summary>
    public DateTime Time { get; set; }

    /// <summary>執行時間</summary>
    public float? Rtime { get; set; }

    /// <summary>合法訪問</summary>
    public string Authorized { get; set; } = null!;

    /// <summary>回應碼</summary>
    public short? ResponseCode { get; set; }
}

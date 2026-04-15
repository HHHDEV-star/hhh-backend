using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Platform.SystemLogs;

/// <summary>
/// 後台系統 API Log 查詢條件
/// （對應舊版 PHP:System_model::get_backend_logs 的篩選參數）
/// 排序白名單：id / time / account / method
/// </summary>
public class SystemLogListRequest : PagedRequest
{
    /// <summary>帳號（精準比對）</summary>
    public string? Account { get; set; }

    /// <summary>使用的 API（模糊比對）</summary>
    public string? ApiUri { get; set; }

    /// <summary>傳送方式（GET/PUT/POST，精準比對）</summary>
    public string? ApiMethod { get; set; }

    /// <summary>起始時間（含）</summary>
    public DateTime? StartDate { get; set; }

    /// <summary>結束時間（含）</summary>
    public DateTime? EndDate { get; set; }
}

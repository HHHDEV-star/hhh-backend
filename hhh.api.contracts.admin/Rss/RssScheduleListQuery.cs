using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Rss;

/// <summary>
/// Yahoo / MSN RSS 排程列表查詢參數
/// </summary>
public class RssScheduleListQuery : PagedRequest
{
    /// <summary>排程日期起始（含），格式 yyyy-MM-dd</summary>
    public DateOnly? DateFrom { get; set; }

    /// <summary>排程日期結束（含），格式 yyyy-MM-dd</summary>
    public DateOnly? DateTo { get; set; }

    /// <summary>關鍵字搜尋：模糊比對 Hcolumn / Hcase</summary>
    public string? Keyword { get; set; }
}

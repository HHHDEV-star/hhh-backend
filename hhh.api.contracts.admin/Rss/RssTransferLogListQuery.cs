using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Rss;

/// <summary>
/// RSS 轉接紀錄列表查詢參數
/// </summary>
public class RssTransferLogListQuery : PagedRequest
{
    /// <summary>日期區間起始（含）</summary>
    public DateTime? StartDate { get; set; }

    /// <summary>日期區間結束（含）</summary>
    public DateTime? EndDate { get; set; }

    /// <summary>來源篩選（Source）</summary>
    public string? Source { get; set; }

    /// <summary>類型篩選（原始英文 type：brand / case / column / designer）</summary>
    public string? Type { get; set; }
}

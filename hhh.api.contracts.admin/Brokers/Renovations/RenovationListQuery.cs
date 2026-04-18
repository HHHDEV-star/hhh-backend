using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Brokers.Renovations;

/// <summary>裝修需求單列表查詢參數</summary>
public class RenovationListQuery : PagedRequest
{
    /// <summary>關鍵字（模糊比對 Name / Phone / Email）</summary>
    public string? Keyword { get; set; }

    /// <summary>類型篩選</summary>
    public string? Type { get; set; }

    /// <summary>建立日期起始</summary>
    public DateOnly? DateFrom { get; set; }

    /// <summary>建立日期結束</summary>
    public DateOnly? DateTo { get; set; }
}

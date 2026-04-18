using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Brokers.Calculators;

/// <summary>裝修計算機列表查詢參數</summary>
public class CalculatorListQuery : PagedRequest
{
    /// <summary>關鍵字（模糊比對 Name / Phone / Email）</summary>
    public string? Keyword { get; set; }

    /// <summary>建立日期起始</summary>
    public DateOnly? DateFrom { get; set; }

    /// <summary>建立日期結束</summary>
    public DateOnly? DateTo { get; set; }
}

using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Main.Execute;

/// <summary>執行表單列表查詢參數</summary>
public class ExecuteFormListQuery : PagedRequest
{
    /// <summary>關鍵字（模糊比對 Num / Company / Designer / SalesMan）</summary>
    public string? Keyword { get; set; }

    /// <summary>結案狀態篩選</summary>
    public string? IsClose { get; set; }

    /// <summary>合約日期起始</summary>
    public DateOnly? DateFrom { get; set; }

    /// <summary>合約日期結束</summary>
    public DateOnly? DateTo { get; set; }
}

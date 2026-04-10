namespace hhh.api.contracts.admin.CalculatorRequests;

/// <summary>
/// 裝修計算機需求列表回應
/// 舊 PHP 用「在每一筆 row 多塞一個 all_count 欄位」的方式回傳全表筆數,
/// 這邊改成更乾淨的結構:items + searchCount(篩選後筆數) + allCount(全表筆數)。
/// </summary>
public class CalculatorRequestListResponse
{
    /// <summary>篩選後的資料</summary>
    public List<CalculatorRequestListItem> Items { get; set; } = new();

    /// <summary>篩選後總筆數(= Items.Count)</summary>
    public int SearchCount { get; set; }

    /// <summary>calculator_request 全表總筆數(對應舊版 all_count 子查詢)</summary>
    public int AllCount { get; set; }
}

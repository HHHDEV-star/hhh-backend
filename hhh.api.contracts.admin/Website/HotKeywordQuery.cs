namespace hhh.api.contracts.admin.Website;

/// <summary>
/// 熱門關鍵字查詢參數（對應舊版 keyword.php 頁面）
/// </summary>
public class HotKeywordQuery
{
    /// <summary>起始日期(含)</summary>
    public DateOnly? Sdate { get; set; }

    /// <summary>結束日期(含)</summary>
    public DateOnly? Edate { get; set; }

    /// <summary>關鍵字(精確比對)</summary>
    public string? Keyword { get; set; }
}

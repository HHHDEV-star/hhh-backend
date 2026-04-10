namespace hhh.api.contracts.admin.Brokers.CalculatorRequests;

/// <summary>
/// 裝修計算機需求列表查詢參數
/// (對應舊版 hhh-api/application/controllers/third/v1/Calculator.php → requestindex_get)
/// </summary>
public class CalculatorRequestListQuery
{
    /// <summary>
    /// 起始日期(yyyy-MM-dd)。對應舊版 start_date,空白則不過濾。
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 結束日期(yyyy-MM-dd)。對應舊版 end_date,空白則不過濾。
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// 關鍵字搜尋。
    /// 規則(保留舊 PHP 行為):
    ///  - 若以 "09" 開頭,視為手機號碼;若長度為 10 則先轉成 "0912-345-678" 再 LIKE phone
    ///  - 否則跨欄位 LIKE: name / email / city / source_web / ca_type / h_class /
    ///    utm_source / utm_medium / utm_campaign / area
    ///  - 若關鍵字為「無 / 同意 / 不同意」,額外比對 marketing_consent (2 / 1 / 0)
    /// </summary>
    public string? Keyword { get; set; }
}

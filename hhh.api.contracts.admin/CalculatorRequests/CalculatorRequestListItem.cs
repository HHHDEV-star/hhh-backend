namespace hhh.api.contracts.admin.CalculatorRequests;

/// <summary>
/// 裝修計算機需求列表項目
/// (對應舊版 calculator_request 資料表,欄位順序對齊後台 Kendo Grid 欄位)
/// </summary>
public class CalculatorRequestListItem
{
    /// <summary>PK</summary>
    public int Id { get; set; }

    /// <summary>建立時間</summary>
    public DateTime CreateTime { get; set; }

    /// <summary>姓名</summary>
    public string? Name { get; set; }

    /// <summary>電話</summary>
    public string? Phone { get; set; }

    /// <summary>郵件</summary>
    public string? Email { get; set; }

    /// <summary>縣市區域</summary>
    public string? City { get; set; }

    /// <summary>房屋類型(全室裝修 A/B 版才有)</summary>
    public string? HClass { get; set; }

    /// <summary>坪數</summary>
    public string? Area { get; set; }

    /// <summary>裝修類型(輕裝修/全室裝修/局部裝修)</summary>
    public string? CaType { get; set; }

    /// <summary>需求來源平台(全室 A/全室 B/官網)</summary>
    public string? SourceWeb { get; set; }

    /// <summary>行銷同意(1:同意, 0:不同意, 2:無)</summary>
    public string? MarketingConsent { get; set; }

    /// <summary>UTM 來源</summary>
    public string? UtmSource { get; set; }

    /// <summary>UTM 媒介</summary>
    public string? UtmMedium { get; set; }

    /// <summary>UTM 活動</summary>
    public string? UtmCampaign { get; set; }
}

namespace hhh.api.contracts.admin.Brokers.Calculators;

/// <summary>
/// 裝修計算機列表項目
/// (對應舊版 calculator 資料表,欄位順序對齊後台 Kendo Grid 欄位)
/// </summary>
public class CalculatorListItem
{
    /// <summary>PK</summary>
    public uint Id { get; set; }

    /// <summary>建立時間</summary>
    public DateTime CreateTime { get; set; }

    /// <summary>聯絡姓名</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>聯絡電話</summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>電子郵件</summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>是否為 FB 帳號(Y / N)</summary>
    public string IsFb { get; set; } = string.Empty;

    /// <summary>裝修需求 / 是否同意聯繫(Y / N)</summary>
    public string ContactStatus { get; set; } = string.Empty;

    /// <summary>意見回饋</summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>房屋類型</summary>
    public string HouseType { get; set; } = string.Empty;

    /// <summary>裝修地區</summary>
    public string Location { get; set; } = string.Empty;

    /// <summary>屋齡</summary>
    public string HouseYear { get; set; } = string.Empty;

    /// <summary>地板材質</summary>
    public string Floor { get; set; } = string.Empty;

    /// <summary>隔間材質</summary>
    public string Compartment { get; set; } = string.Empty;

    /// <summary>天花板類型</summary>
    public string Ceiling { get; set; } = string.Empty;

    /// <summary>變動裝修</summary>
    public string ChangeArea { get; set; } = string.Empty;

    /// <summary>格局-房</summary>
    public string Room { get; set; } = string.Empty;

    /// <summary>格局-廳</summary>
    public string Liveroom { get; set; } = string.Empty;

    /// <summary>格局-衛</summary>
    public string Bathroom { get; set; } = string.Empty;

    /// <summary>坪數</summary>
    public ushort Pin { get; set; }

    /// <summary>裝修預估總金額(字串,例:"50萬 ~ 80萬")</summary>
    public string Total { get; set; } = string.Empty;

    /// <summary>貸款意願(Y / N)</summary>
    public string LoanStatus { get; set; } = string.Empty;

    /// <summary>找收納達人意願(Y / N)</summary>
    public string StorageStatus { get; set; } = string.Empty;

    /// <summary>是否尋找倉儲(Y / N)</summary>
    public string WarehousingStatus { get; set; } = string.Empty;

    /// <summary>是否租屋(Y / N)</summary>
    public string RentHouseStatus { get; set; } = string.Empty;

    /// <summary>來源 IP</summary>
    public string Ip { get; set; } = string.Empty;
}

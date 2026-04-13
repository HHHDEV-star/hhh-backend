namespace hhh.api.contracts.admin.Advertise.Ads;

/// <summary>廣告列表項目(對應 _had 表)</summary>
public class AdListItem
{
    public uint Adid { get; set; }
    public string Adtype { get; set; } = string.Empty;
    public string Adlogo { get; set; } = string.Empty;
    public string? AdlogoMobile { get; set; }
    public string AdlogoWebp { get; set; } = string.Empty;
    public string AdlogoMobileWebp { get; set; } = string.Empty;
    public string? LogoIcon { get; set; }
    public string Addesc { get; set; } = string.Empty;
    public string Adlongdesc { get; set; } = string.Empty;
    public string Adhref { get; set; } = string.Empty;
    public string? AdhrefR { get; set; }
    public string? Keyword { get; set; }
    public string Tabname { get; set; } = string.Empty;
    public bool Onoff { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public DateTime CreatTime { get; set; }
    public uint ClickCounter { get; set; }
    public int HdesignerId { get; set; }
    public int HbrandId { get; set; }
    public int BuilderProductId { get; set; }
    public string? WaterMark { get; set; }
    public string AltUse { get; set; } = string.Empty;
    public string? IndexChar1 { get; set; }
    public string? IndexChar21 { get; set; }
    public string? IndexChar22 { get; set; }
    public string? IndexChar23 { get; set; }
}

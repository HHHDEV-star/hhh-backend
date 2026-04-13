namespace hhh.api.contracts.admin.Advertise.Ads;

/// <summary>更新廣告圖片請求(adid 走 URL)。四個圖片欄位:桌機/手機 × 原圖/webp</summary>
public class UpdateAdLogoRequest
{
    public string? Adlogo { get; set; }
    public string? AdlogoMobile { get; set; }
    public string? AdlogoWebp { get; set; }
    public string? AdlogoMobileWebp { get; set; }
    public string? LogoIcon { get; set; }
}

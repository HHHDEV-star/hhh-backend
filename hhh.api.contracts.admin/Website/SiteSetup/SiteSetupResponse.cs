namespace hhh.api.contracts.admin.Website.SiteSetup;

/// <summary>
/// 全域設定(site_setup 單筆,id=1)
/// (對應舊版 Homepage/site_get → homepage_model::get_site_setup)
/// </summary>
public class SiteSetupResponse
{
    /// <summary>首頁影片 YouTube ID</summary>
    public string? YoutubeId { get; set; }

    /// <summary>首頁影片標題(max 15 字)</summary>
    public string? YoutubeTitle { get; set; }

    /// <summary>全站搜尋關鍵字(逗號分隔)</summary>
    public string? AllSearchTag { get; set; }

    /// <summary>討論區過濾字</summary>
    public string? ForumFilter { get; set; }
}

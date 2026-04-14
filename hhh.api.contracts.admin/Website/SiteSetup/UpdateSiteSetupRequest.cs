using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Website.SiteSetup;

/// <summary>
/// 更新全域設定
/// (對應舊版 Homepage/site_put → homepage_model::set_site_setup)
/// </summary>
public class UpdateSiteSetupRequest
{
    /// <summary>首頁影片 YouTube ID</summary>
    [StringLength(11)]
    public string? YoutubeId { get; set; }

    /// <summary>首頁影片標題(max 15 字)</summary>
    [StringLength(15)]
    public string? YoutubeTitle { get; set; }

    /// <summary>全站搜尋關鍵字(逗號分隔)</summary>
    [StringLength(200)]
    public string? AllSearchTag { get; set; }

    /// <summary>討論區過濾字</summary>
    [StringLength(200)]
    public string? ForumFilter { get; set; }
}

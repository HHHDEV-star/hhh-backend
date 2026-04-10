namespace hhh.api.contracts.admin.VideoReports;

/// <summary>
/// 廠商影音統計表回應(對應舊版 /backend/video_report.php)。
/// </summary>
public class VideoReportBrandResponse
{
    /// <summary>所有廠商群組(以 hbrand_id ASC 排序)</summary>
    public IReadOnlyList<VideoReportBrandGroup> Groups { get; set; } = Array.Empty<VideoReportBrandGroup>();

    /// <summary>廠商總數</summary>
    public int TotalBrands { get; set; }

    /// <summary>影片總數(跨所有廠商)</summary>
    public int TotalVideos { get; set; }
}

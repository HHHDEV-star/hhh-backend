namespace hhh.api.contracts.admin.Reports.VideoReports;

/// <summary>
/// 設計師影音統計表回應(對應舊版 /backend/video_report2.php)。
/// </summary>
public class VideoReportDesignerResponse
{
    /// <summary>所有設計師群組(以 hdesigner_id ASC 排序)</summary>
    public IReadOnlyList<VideoReportDesignerGroup> Groups { get; set; } = Array.Empty<VideoReportDesignerGroup>();

    /// <summary>設計師總數</summary>
    public int TotalDesigners { get; set; }

    /// <summary>影片總數(跨所有設計師)</summary>
    public int TotalVideos { get; set; }
}

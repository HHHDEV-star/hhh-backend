namespace hhh.api.contracts.admin.Reports.VideoReports;

/// <summary>
/// 廠商影音統計表的一個廠商群組,包含該廠商底下所有影片。
/// </summary>
public class VideoReportBrandGroup
{
    /// <summary>廠商 ID(hbrand_id)</summary>
    public uint BrandId { get; set; }

    /// <summary>廠商名稱</summary>
    public string BrandTitle { get; set; } = string.Empty;

    /// <summary>該廠商的影片總數</summary>
    public int TotalVideos { get; set; }

    /// <summary>該廠商底下的影片明細</summary>
    public IReadOnlyList<VideoReportItem> Videos { get; set; } = Array.Empty<VideoReportItem>();
}

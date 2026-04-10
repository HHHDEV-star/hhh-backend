namespace hhh.api.contracts.admin.Reports.VideoReports;

/// <summary>
/// 設計師影音統計表的一個設計師群組,包含該設計師底下所有影片。
/// </summary>
public class VideoReportDesignerGroup
{
    /// <summary>設計師 ID(hdesigner_id)</summary>
    public uint DesignerId { get; set; }

    /// <summary>設計公司名稱</summary>
    public string DesignerTitle { get; set; } = string.Empty;

    /// <summary>該設計師的影片總數</summary>
    public int TotalVideos { get; set; }

    /// <summary>該設計師底下的影片明細</summary>
    public IReadOnlyList<VideoReportItem> Videos { get; set; } = Array.Empty<VideoReportItem>();
}

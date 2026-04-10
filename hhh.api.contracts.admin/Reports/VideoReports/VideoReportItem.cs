namespace hhh.api.contracts.admin.Reports.VideoReports;

/// <summary>
/// 影音統計表中的單筆影片明細(廠商/設計師報表共用)。
/// </summary>
public class VideoReportItem
{
    /// <summary>影片 ID(hvideo_id)</summary>
    public uint VideoId { get; set; }

    /// <summary>影片標題</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>影片分類(tag_vtype)</summary>
    public string TagVtype { get; set; } = string.Empty;

    /// <summary>上線日期(舊版 PHP LEFT(creat_time,10) 只取日期部分)</summary>
    public DateOnly CreateTime { get; set; }
}

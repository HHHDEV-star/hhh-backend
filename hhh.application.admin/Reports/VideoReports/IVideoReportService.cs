using hhh.api.contracts.admin.Reports.VideoReports;

namespace hhh.application.admin.Reports.VideoReports;

/// <summary>
/// 影音統計報表服務
/// (對應舊版 PHP /backend/video_report.php、video_report2.php)
/// </summary>
public interface IVideoReportService
{
    /// <summary>
    /// 取得廠商影音統計表
    /// (對應 video_report.php:hbrand_id != 0,以 hbrand_id ASC 排序)
    /// </summary>
    Task<VideoReportBrandResponse> GetBrandReportAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 取得設計師影音統計表
    /// (對應 video_report2.php:hdesigner_id != 0,以 hdesigner_id ASC 排序)
    /// </summary>
    Task<VideoReportDesignerResponse> GetDesignerReportAsync(CancellationToken cancellationToken = default);
}

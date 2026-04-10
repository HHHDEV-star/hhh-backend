using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Reports.VideoReports;
using hhh.application.admin.Reports.VideoReports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hhh.webapi.admin.Controllers.Reports;

/// <summary>
/// 報表
/// </summary>
/// <remarks>
/// 後台「報表」業務類別下所有 endpoint 集中於此 controller。
///
/// 對應舊版 PHP:
///  - 影音統計:/backend/video_report.php、video_report2.php
///
/// 備註:舊版 PHP 的 Excel 匯出(action/output_video_report.php 系列)尚未移植,
/// 需要 ClosedXML 之類的 infra 後再補,不在本階段範圍。
/// </remarks>
[Route("api/reports")]
[Authorize]
[Tags("Reports")]
public class ReportsController : ApiControllerBase
{
    private readonly IVideoReportService _videoReportService;

    public ReportsController(IVideoReportService videoReportService)
    {
        _videoReportService = videoReportService;
    }

    /// <summary>取得廠商影音統計表</summary>
    /// <remarks>
    /// 對應舊版 /backend/video_report.php。
    /// 條件:hbrand_id != 0,以 hbrand_id ASC 排序,每個廠商帶出所有影片。
    /// 回應採 grouped 結構(不像舊 PHP 把重複欄位改成空字串),前端自行決定呈現。
    /// </remarks>
    [HttpGet("video-reports/brands")]
    [ProducesResponseType(typeof(ApiResponse<VideoReportBrandResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBrandVideoReport(CancellationToken cancellationToken)
    {
        var data = await _videoReportService.GetBrandReportAsync(cancellationToken);
        return Ok(ApiResponse<VideoReportBrandResponse>.Success(data));
    }

    /// <summary>取得設計師影音統計表</summary>
    /// <remarks>
    /// 對應舊版 /backend/video_report2.php。
    /// 條件:hdesigner_id != 0,以 hdesigner_id ASC 排序,每個設計師帶出所有影片。
    /// </remarks>
    [HttpGet("video-reports/designers")]
    [ProducesResponseType(typeof(ApiResponse<VideoReportDesignerResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDesignerVideoReport(CancellationToken cancellationToken)
    {
        var data = await _videoReportService.GetDesignerReportAsync(cancellationToken);
        return Ok(ApiResponse<VideoReportDesignerResponse>.Success(data));
    }
}

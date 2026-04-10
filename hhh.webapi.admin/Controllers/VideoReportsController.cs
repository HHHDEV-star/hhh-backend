using hhh.api.contracts.Common;
using hhh.api.contracts.admin.VideoReports;
using hhh.infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hhh.webapi.admin.Controllers;

/// <summary>
/// 影音統計報表 API(對應舊版 /backend/video_report.php、video_report2.php)。
/// 純讀取 thin controller,直接戳 XoopsContext。
///
/// 備註:舊版 PHP 的 Excel 匯出(action/output_video_report.php 系列)尚未移植,
/// 需要 ClosedXML 之類的 infra 後再補,不在本階段範圍。
/// </summary>
[Route("api/video-reports")]
[Authorize]
public class VideoReportsController : ApiControllerBase
{
    private readonly XoopsContext _db;

    public VideoReportsController(XoopsContext db)
    {
        _db = db;
    }

    /// <summary>
    /// 取得廠商影音統計表
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/video_report.php
    /// 條件:hbrand_id != 0,以 hbrand_id ASC 排序,每個廠商帶出所有影片。
    /// 回應採 grouped 結構(不像舊 PHP 把重複欄位改成空字串),前端自行決定呈現。
    /// </remarks>
    [HttpGet("brands")]
    [ProducesResponseType(typeof(ApiResponse<VideoReportBrandResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBrandReport(CancellationToken cancellationToken)
    {
        var rows = await (
            from v in _db.Hvideos.AsNoTracking()
            join b in _db.Hbrands.AsNoTracking() on v.HbrandId equals b.HbrandId
            where v.HbrandId != 0
            orderby v.HbrandId, v.HvideoId
            select new
            {
                v.HvideoId,
                v.HbrandId,
                BrandTitle = b.Title,
                v.Title,
                v.TagVtype,
                v.CreatTime,
            })
            .ToListAsync(cancellationToken);

        var groups = rows
            .GroupBy(r => new { r.HbrandId, r.BrandTitle })
            .Select(g => new VideoReportBrandGroup
            {
                BrandId = g.Key.HbrandId,
                BrandTitle = g.Key.BrandTitle,
                TotalVideos = g.Count(),
                Videos = g.Select(v => new VideoReportItem
                {
                    VideoId = v.HvideoId,
                    Title = v.Title,
                    TagVtype = v.TagVtype,
                    CreateTime = DateOnly.FromDateTime(v.CreatTime),
                }).ToList(),
            })
            .ToList();

        var response = new VideoReportBrandResponse
        {
            Groups = groups,
            TotalBrands = groups.Count,
            TotalVideos = rows.Count,
        };

        return Ok(ApiResponse<VideoReportBrandResponse>.Success(response));
    }

    /// <summary>
    /// 取得設計師影音統計表
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/video_report2.php
    /// 條件:hdesigner_id != 0,以 hdesigner_id ASC 排序,每個設計師帶出所有影片。
    /// </remarks>
    [HttpGet("designers")]
    [ProducesResponseType(typeof(ApiResponse<VideoReportDesignerResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDesignerReport(CancellationToken cancellationToken)
    {
        var rows = await (
            from v in _db.Hvideos.AsNoTracking()
            join d in _db.Hdesigners.AsNoTracking() on v.HdesignerId equals d.HdesignerId
            where v.HdesignerId != 0
            orderby v.HdesignerId, v.HvideoId
            select new
            {
                v.HvideoId,
                v.HdesignerId,
                DesignerTitle = d.Title,
                v.Title,
                v.TagVtype,
                v.CreatTime,
            })
            .ToListAsync(cancellationToken);

        var groups = rows
            .GroupBy(r => new { r.HdesignerId, r.DesignerTitle })
            .Select(g => new VideoReportDesignerGroup
            {
                DesignerId = g.Key.HdesignerId,
                DesignerTitle = g.Key.DesignerTitle,
                TotalVideos = g.Count(),
                Videos = g.Select(v => new VideoReportItem
                {
                    VideoId = v.HvideoId,
                    Title = v.Title,
                    TagVtype = v.TagVtype,
                    CreateTime = DateOnly.FromDateTime(v.CreatTime),
                }).ToList(),
            })
            .ToList();

        var response = new VideoReportDesignerResponse
        {
            Groups = groups,
            TotalDesigners = groups.Count,
            TotalVideos = rows.Count,
        };

        return Ok(ApiResponse<VideoReportDesignerResponse>.Success(response));
    }
}

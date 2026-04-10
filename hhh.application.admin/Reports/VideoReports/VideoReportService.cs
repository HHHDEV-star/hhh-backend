using hhh.api.contracts.admin.Reports.VideoReports;
using hhh.infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Reports.VideoReports;

public class VideoReportService : IVideoReportService
{
    private readonly XoopsContext _db;

    public VideoReportService(XoopsContext db)
    {
        _db = db;
    }

    public async Task<VideoReportBrandResponse> GetBrandReportAsync(CancellationToken cancellationToken = default)
    {
        // 對應舊 PHP video_report.php
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

        return new VideoReportBrandResponse
        {
            Groups = groups,
            TotalBrands = groups.Count,
            TotalVideos = rows.Count,
        };
    }

    public async Task<VideoReportDesignerResponse> GetDesignerReportAsync(CancellationToken cancellationToken = default)
    {
        // 對應舊 PHP video_report2.php
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

        return new VideoReportDesignerResponse
        {
            Groups = groups,
            TotalDesigners = groups.Count,
            TotalVideos = rows.Count,
        };
    }
}

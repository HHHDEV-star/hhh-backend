using hhh.api.contracts.admin.Rss;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using hhh.infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Rss.LineToday;

public class RssLineTodayService : IRssLineTodayService
{
    private readonly XoopsContext _db;

    public RssLineTodayService(XoopsContext db) => _db = db;

    public async Task<PagedResponse<RssLineTodayItem>> GetListAsync(ListQuery query, CancellationToken cancellationToken = default)
    {
        // 對應舊 PHP rss_linetoday_model::get():ORDER BY date DESC
        // 注意:舊版還會逐行檢查 CDN 上有沒有 .mp4 檔並塞 mp4 bool,
        // 因為是 remote IO(HTTP HEAD per row)且用途不明,這裡先跳過不做。
        return await _db.RssLinetodays
            .AsNoTracking()
            .OrderByDescending(r => r.Date)
            .Select(r => new RssLineTodayItem
            {
                Id = r.Id,
                Date = r.Date,
                Hvideo = r.Hvideo,
                CreateTime = r.CreateTime,
                UpdateTime = r.UpdateTime,
            })
            .ToPagedResponseAsync(query.Page, query.PageSize, cancellationToken);
    }

    public async Task<OperationResult<uint>> CreateAsync(
        RssLineTodayRequest request, CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;
        var entity = new RssLinetoday
        {
            Date = request.Date,
            Hvideo = request.Hvideo,
            CreateTime = now,
            UpdateTime = now,
        };

        _db.RssLinetodays.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        return OperationResult<uint>.Created(entity.Id);
    }

    public async Task<OperationResult<uint>> UpdateAsync(
        uint id, RssLineTodayRequest request, CancellationToken cancellationToken = default)
    {
        var entity = await _db.RssLinetodays.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        if (entity is null)
            return OperationResult<uint>.NotFound("找不到 LineToday RSS 排程");

        entity.Date = request.Date;
        entity.Hvideo = request.Hvideo;
        entity.UpdateTime = DateTime.UtcNow;

        await _db.SaveChangesAsync(cancellationToken);
        return OperationResult<uint>.Ok(id, "修改成功");
    }
}

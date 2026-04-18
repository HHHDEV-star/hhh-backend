using hhh.api.contracts.admin.Rss;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using hhh.infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Rss.Msn;

public class RssMsnService : IRssMsnService
{
    private readonly XoopsContext _db;

    public RssMsnService(XoopsContext db) => _db = db;

    public async Task<PagedResponse<RssScheduleItem>> GetListAsync(RssScheduleListQuery query, CancellationToken cancellationToken = default)
    {
        var q = _db.RssMsns.AsNoTracking().AsQueryable();

        // 日期區間篩選（entity Date 為 DateOnly）
        if (query.DateFrom is { } dateFrom)
        {
            q = q.Where(r => r.Date >= dateFrom);
        }

        if (query.DateTo is { } dateTo)
        {
            q = q.Where(r => r.Date <= dateTo);
        }

        // 關鍵字篩選（Hcolumn / Hcase）
        if (!string.IsNullOrWhiteSpace(query.Keyword))
        {
            var kw = $"%{query.Keyword}%";
            q = q.Where(r =>
                EF.Functions.Like(r.Hcolumn, kw) ||
                EF.Functions.Like(r.Hcase, kw));
        }

        return await q
            .OrderByDescending(r => r.Date)
            .Select(r => new RssScheduleItem
            {
                Id = r.Id,
                Date = r.Date,
                Hcolumn = r.Hcolumn,
                Hcase = r.Hcase,
                CreateTime = r.CreateTime,
                UpdateTime = r.UpdateTime,
            })
            .ToPagedResponseAsync(query.Page, query.PageSize, cancellationToken);
    }

    public async Task<OperationResult<uint>> CreateAsync(
        RssScheduleRequest request, CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;
        var entity = new RssMsn
        {
            Date = request.Date,
            Hcolumn = request.Hcolumn,
            Hcase = request.Hcase,
            CreateTime = now,
            UpdateTime = now,
        };

        _db.RssMsns.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        return OperationResult<uint>.Created(entity.Id);
    }

    public async Task<OperationResult<uint>> UpdateAsync(
        uint id, RssScheduleRequest request, CancellationToken cancellationToken = default)
    {
        var entity = await _db.RssMsns.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        if (entity is null)
            return OperationResult<uint>.NotFound("找不到 MSN RSS 排程");

        entity.Date = request.Date;
        entity.Hcolumn = request.Hcolumn;
        entity.Hcase = request.Hcase;
        entity.UpdateTime = DateTime.UtcNow;

        await _db.SaveChangesAsync(cancellationToken);
        return OperationResult<uint>.Ok(id, "修改成功");
    }
}

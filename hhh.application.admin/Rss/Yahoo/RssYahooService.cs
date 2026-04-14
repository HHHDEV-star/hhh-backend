using hhh.api.contracts.admin.Rss;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using hhh.infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Rss.Yahoo;

public class RssYahooService : IRssYahooService
{
    private readonly XoopsContext _db;

    public RssYahooService(XoopsContext db) => _db = db;

    public async Task<PagedResponse<RssScheduleItem>> GetListAsync(ListQuery query, CancellationToken cancellationToken = default)
    {
        return await _db.RssYahoos
            .AsNoTracking()
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
        var entity = new RssYahoo
        {
            Date = request.Date,
            Hcolumn = request.Hcolumn,
            Hcase = request.Hcase,
            CreateTime = now,
            UpdateTime = now,
        };

        _db.RssYahoos.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        return OperationResult<uint>.Created(entity.Id);
    }

    public async Task<OperationResult<uint>> UpdateAsync(
        uint id, RssScheduleRequest request, CancellationToken cancellationToken = default)
    {
        var entity = await _db.RssYahoos.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        if (entity is null)
            return OperationResult<uint>.NotFound("找不到 Yahoo RSS 排程");

        entity.Date = request.Date;
        entity.Hcolumn = request.Hcolumn;
        entity.Hcase = request.Hcase;
        entity.UpdateTime = DateTime.UtcNow;

        await _db.SaveChangesAsync(cancellationToken);
        return OperationResult<uint>.Ok(id, "修改成功");
    }
}

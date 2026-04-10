using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Content.Htopic2s;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using hhh.infrastructure.Extensions;
using hhh.infrastructure.Logging;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Content.Htopic2s;

public class Htopic2Service : IHtopic2Service
{
    private const string PageName = "議題2";

    private readonly XoopsContext _db;
    private readonly IOperationLogWriter _logWriter;

    public Htopic2Service(XoopsContext db, IOperationLogWriter logWriter)
    {
        _db = db;
        _logWriter = logWriter;
    }

    public async Task<PagedResponse<Htopic2ListItem>> GetListAsync(
        Htopic2ListRequest request,
        CancellationToken cancellationToken = default)
    {
        var query = _db.Htopic2s.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.Q))
        {
            var q = request.Q.Trim();
            var like = $"%{q}%";
            query = query.Where(h =>
                EF.Functions.Like(h.Id.ToString(), like) ||
                EF.Functions.Like(h.Title, like) ||
                EF.Functions.Like(h.Desc, like));
        }

        if (request.Onoff is { } onoff)
        {
            byte flag = (byte)(onoff ? 1 : 0);
            query = query.Where(h => h.Onoff == flag);
        }

        var ordered = ApplyOrdering(query, request.Sort, request.By);

        return await ordered
            .Select(h => new Htopic2ListItem
            {
                Id = h.Id,
                Title = h.Title,
                Desc = h.Desc,
                Logo = h.Logo,
                StrarrHdesignerId = h.StrarrHdesignerId,
                StrarrHcaseId = h.StrarrHcaseId,
                StrarrHvideoId = h.StrarrHvideoId,
                StrarrHcolumnId = h.StrarrHcolumnId,
                Onoff = h.Onoff == 1,
            })
            .ToPagedResponseAsync(request.Page, request.PageSize, cancellationToken);
    }

    public async Task<Htopic2DetailResponse?> GetByIdAsync(
        uint id,
        CancellationToken cancellationToken = default)
    {
        return await _db.Htopic2s
            .AsNoTracking()
            .Where(h => h.Id == id)
            .Select(h => new Htopic2DetailResponse
            {
                Id = h.Id,
                Title = h.Title,
                Desc = h.Desc,
                Logo = h.Logo,
                StrarrHdesignerId = h.StrarrHdesignerId,
                StrarrHcaseId = h.StrarrHcaseId,
                StrarrHvideoId = h.StrarrHvideoId,
                StrarrHcolumnId = h.StrarrHcolumnId,
                Onoff = h.Onoff == 1,
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<OperationResult<uint>> CreateAsync(
        CreateHtopic2Request request,
        CancellationToken cancellationToken = default)
    {
        var entity = new Htopic2
        {
            Title = request.Title,
            Desc = request.Desc ?? string.Empty,
            Logo = request.Logo ?? string.Empty,
            StrarrHdesignerId = request.StrarrHdesignerId ?? string.Empty,
            StrarrHcaseId = request.StrarrHcaseId ?? string.Empty,
            StrarrHvideoId = request.StrarrHvideoId ?? string.Empty,
            StrarrHcolumnId = request.StrarrHcolumnId ?? string.Empty,
            Onoff = (byte)(request.Onoff ? 1 : 0),
        };

        _db.Htopic2s.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Create,
            $"新增議題2 id={entity.Id} 標題={request.Title}",
            cancellationToken: cancellationToken);

        return OperationResult<uint>.Created(entity.Id);
    }

    public async Task<OperationResult<uint>> UpdateAsync(
        uint id,
        UpdateHtopic2Request request,
        CancellationToken cancellationToken = default)
    {
        var entity = await _db.Htopic2s
            .FirstOrDefaultAsync(h => h.Id == id, cancellationToken);

        if (entity is null)
            return OperationResult<uint>.NotFound("找不到議題 2 資料");

        entity.Title = request.Title;
        entity.Desc = request.Desc ?? string.Empty;
        entity.Logo = request.Logo ?? string.Empty;
        entity.StrarrHdesignerId = request.StrarrHdesignerId ?? string.Empty;
        entity.StrarrHcaseId = request.StrarrHcaseId ?? string.Empty;
        entity.StrarrHvideoId = request.StrarrHvideoId ?? string.Empty;
        entity.StrarrHcolumnId = request.StrarrHcolumnId ?? string.Empty;
        entity.Onoff = (byte)(request.Onoff ? 1 : 0);

        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Update,
            $"修改議題2 id={id} 標題={request.Title}",
            cancellationToken: cancellationToken);

        return OperationResult<uint>.Ok(id, "修改成功");
    }

    public async Task<OperationResult> DeleteAsync(
        uint id,
        CancellationToken cancellationToken = default)
    {
        var entity = await _db.Htopic2s
            .FirstOrDefaultAsync(h => h.Id == id, cancellationToken);

        if (entity is null)
            return OperationResult.NotFound("找不到議題 2 資料");

        var oldTitle = entity.Title;

        _db.Htopic2s.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Delete,
            $"刪除議題2 id={id} 標題={oldTitle}",
            cancellationToken: cancellationToken);

        return OperationResult.Ok("刪除成功");
    }

    private static IOrderedQueryable<Htopic2> ApplyOrdering(
        IQueryable<Htopic2> query,
        string? sort,
        string? by)
    {
        var isAsc = string.Equals(by, "ASC", StringComparison.OrdinalIgnoreCase);
        var key = sort?.ToLowerInvariant();

        return key switch
        {
            "title" => isAsc ? query.OrderBy(h => h.Title) : query.OrderByDescending(h => h.Title),
            "onoff" => isAsc ? query.OrderBy(h => h.Onoff) : query.OrderByDescending(h => h.Onoff),
            _ => isAsc ? query.OrderBy(h => h.Id) : query.OrderByDescending(h => h.Id),
        };
    }
}

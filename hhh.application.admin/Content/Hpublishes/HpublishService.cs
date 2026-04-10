using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Content.Hpublishes;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using hhh.infrastructure.Extensions;
using hhh.infrastructure.Logging;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Content.Hpublishes;

public class HpublishService : IHpublishService
{
    private const string PageName = "出版";

    private readonly XoopsContext _db;
    private readonly IOperationLogWriter _logWriter;

    public HpublishService(XoopsContext db, IOperationLogWriter logWriter)
    {
        _db = db;
        _logWriter = logWriter;
    }

    public async Task<PagedResponse<HpublishListItem>> GetListAsync(
        HpublishListRequest request,
        CancellationToken cancellationToken = default)
    {
        var query = _db.Hpublishes.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.Q))
        {
            var q = request.Q.Trim();
            var like = $"%{q}%";
            query = query.Where(h =>
                EF.Functions.Like(h.HpublishId.ToString(), like) ||
                EF.Functions.Like(h.Title, like) ||
                EF.Functions.Like(h.Author, like) ||
                EF.Functions.Like(h.Type, like) ||
                EF.Functions.Like(h.Desc, like));
        }

        if (!string.IsNullOrWhiteSpace(request.Type))
        {
            var type = request.Type.Trim();
            query = query.Where(h => h.Type == type);
        }

        var ordered = ApplyOrdering(query, request.Sort, request.By);

        return await ordered
            .Select(h => new HpublishListItem
            {
                Id = h.HpublishId,
                Type = h.Type,
                Logo = h.Logo,
                Title = h.Title,
                Author = h.Author,
                Pdate = h.Pdate,
                Viewed = h.Viewed,
                Recommend = h.Recommend,
            })
            .ToPagedResponseAsync(request.Page, request.PageSize, cancellationToken);
    }

    public async Task<HpublishDetailResponse?> GetByIdAsync(
        uint id,
        CancellationToken cancellationToken = default)
    {
        return await _db.Hpublishes
            .AsNoTracking()
            .Where(h => h.HpublishId == id)
            .Select(h => new HpublishDetailResponse
            {
                Id = h.HpublishId,
                Type = h.Type,
                Logo = h.Logo,
                Title = h.Title,
                Author = h.Author,
                Pdate = h.Pdate,
                Desc = h.Desc,
                Editor = h.Editor,
                Viewed = h.Viewed,
                Recommend = h.Recommend,
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<OperationResult<uint>> CreateAsync(
        CreateHpublishRequest request,
        CancellationToken cancellationToken = default)
    {
        var entity = new Hpublish
        {
            Type = request.Type,
            Logo = request.Logo ?? string.Empty,
            Title = request.Title,
            Author = request.Author,
            Pdate = request.Pdate,
            Desc = request.Desc ?? string.Empty,
            Editor = request.Editor ?? string.Empty,
            Recommend = request.Recommend,
            // viewed 系統累計,建立時固定 0
            Viewed = 0,
        };

        _db.Hpublishes.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Create,
            $"新增出版 id={entity.HpublishId} 書名={request.Title} 作者={request.Author}",
            cancellationToken: cancellationToken);

        return OperationResult<uint>.Created(entity.HpublishId);
    }

    public async Task<OperationResult<uint>> UpdateAsync(
        uint id,
        UpdateHpublishRequest request,
        CancellationToken cancellationToken = default)
    {
        var entity = await _db.Hpublishes
            .FirstOrDefaultAsync(h => h.HpublishId == id, cancellationToken);

        if (entity is null)
            return OperationResult<uint>.NotFound("找不到出版資料");

        entity.Type = request.Type;
        entity.Logo = request.Logo ?? string.Empty;
        entity.Title = request.Title;
        entity.Author = request.Author;
        entity.Pdate = request.Pdate;
        entity.Desc = request.Desc ?? string.Empty;
        entity.Editor = request.Editor ?? string.Empty;
        entity.Recommend = request.Recommend;
        // Viewed 維持原值(前台累計),不接受 API 覆寫

        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Update,
            $"修改出版 id={id} 書名={request.Title} 作者={request.Author}",
            cancellationToken: cancellationToken);

        return OperationResult<uint>.Ok(id, "修改成功");
    }

    public async Task<OperationResult> DeleteAsync(
        uint id,
        CancellationToken cancellationToken = default)
    {
        var entity = await _db.Hpublishes
            .FirstOrDefaultAsync(h => h.HpublishId == id, cancellationToken);

        if (entity is null)
            return OperationResult.NotFound("找不到出版資料");

        var oldTitle = entity.Title;
        var oldAuthor = entity.Author;

        _db.Hpublishes.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Delete,
            $"刪除出版 id={id} 書名={oldTitle} 作者={oldAuthor}",
            cancellationToken: cancellationToken);

        return OperationResult.Ok("刪除成功");
    }

    private static IOrderedQueryable<Hpublish> ApplyOrdering(
        IQueryable<Hpublish> query,
        string? sort,
        string? by)
    {
        var isAsc = string.Equals(by, "ASC", StringComparison.OrdinalIgnoreCase);
        var key = sort?.ToLowerInvariant();

        return key switch
        {
            "title" => isAsc ? query.OrderBy(h => h.Title) : query.OrderByDescending(h => h.Title),
            "author" => isAsc ? query.OrderBy(h => h.Author) : query.OrderByDescending(h => h.Author),
            "type" => isAsc ? query.OrderBy(h => h.Type) : query.OrderByDescending(h => h.Type),
            "pdate" => isAsc ? query.OrderBy(h => h.Pdate) : query.OrderByDescending(h => h.Pdate),
            "viewed" => isAsc ? query.OrderBy(h => h.Viewed) : query.OrderByDescending(h => h.Viewed),
            "recommend" => isAsc ? query.OrderBy(h => h.Recommend) : query.OrderByDescending(h => h.Recommend),
            _ => isAsc ? query.OrderBy(h => h.HpublishId) : query.OrderByDescending(h => h.HpublishId),
        };
    }
}

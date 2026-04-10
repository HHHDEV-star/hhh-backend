using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Awards.Hcontests;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using hhh.infrastructure.Extensions;
using hhh.infrastructure.Logging;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Awards.Hcontests;

public class HcontestService : IHcontestService
{
    private const string PageName = "競賽報名";

    private readonly XoopsContext _db;
    private readonly IOperationLogWriter _logWriter;

    public HcontestService(XoopsContext db, IOperationLogWriter logWriter)
    {
        _db = db;
        _logWriter = logWriter;
    }

    public async Task<PagedResponse<HcontestListItem>> GetListAsync(
        HcontestListRequest request,
        CancellationToken cancellationToken = default)
    {
        var query = _db.Hcontests.AsNoTracking();

        // 關鍵字搜尋 ----------------------------------------------------------
        if (!string.IsNullOrWhiteSpace(request.Q))
        {
            var q = request.Q.Trim();
            var like = $"%{q}%";
            query = query.Where(h =>
                EF.Functions.Like(h.ContestId.ToString(), like) ||
                EF.Functions.Like(h.ClassType, like) ||
                EF.Functions.Like(h.Year.ToString(), like) ||
                EF.Functions.Like(h.C1, like) ||
                EF.Functions.Like(h.C2, like) ||
                EF.Functions.Like(h.C3, like) ||
                EF.Functions.Like(h.C9, like));
        }

        // 精準過濾 ------------------------------------------------------------
        if (request.Year is { } year)
        {
            query = query.Where(h => h.Year == year);
        }

        if (!string.IsNullOrWhiteSpace(request.ClassType))
        {
            var ct = request.ClassType.Trim();
            query = query.Where(h => h.ClassType == ct);
        }

        if (request.Finalist is { } finalist)
        {
            byte flag = (byte)(finalist ? 1 : 0);
            query = query.Where(h => h.Finalist == flag);
        }

        var ordered = ApplyOrdering(query, request.Sort, request.By);

        return await ordered
            .Select(h => new HcontestListItem
            {
                Id = h.ContestId,
                Year = h.Year,
                C1 = h.C1,
                C2 = h.C2,
                C3 = h.C3,
                C9 = h.C9,
                An = h.An,
                Finalist = h.Finalist == 1,
            })
            .ToPagedResponseAsync(request.Page, request.PageSize, cancellationToken);
    }

    public async Task<HcontestDetailResponse?> GetByIdAsync(
        uint id,
        CancellationToken cancellationToken = default)
    {
        return await _db.Hcontests
            .AsNoTracking()
            .Where(h => h.ContestId == id)
            .Select(h => new HcontestDetailResponse
            {
                Id = h.ContestId,
                Uid = h.Uid,
                Year = h.Year,
                ClassType = h.ClassType,
                C1 = h.C1,
                C2 = h.C2,
                C3 = h.C3,
                C4 = h.C4,
                C5 = h.C5,
                C6 = h.C6,
                C7 = h.C7,
                C8 = h.C8,
                C9 = h.C9,
                C10 = h.C10,
                C11 = h.C11,
                C12 = h.C12,
                C13 = h.C13,
                Applytime = h.Applytime,
                Pay = h.Pay,
                An = h.An,
                Score = h.Score,
                Wp = h.Wp,
                WpDetail = h.WpDetail,
                Finalist = h.Finalist == 1,
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<OperationResult<uint>> CreateAsync(
        CreateHcontestRequest request,
        CancellationToken cancellationToken = default)
    {
        var entity = new Hcontest
        {
            Uid = request.Uid,
            Year = request.Year,
            ClassType = request.ClassType ?? string.Empty,
            C1 = request.C1 ?? string.Empty,
            C2 = request.C2 ?? string.Empty,
            C3 = request.C3 ?? string.Empty,
            C4 = request.C4 ?? string.Empty,
            C5 = request.C5 ?? string.Empty,
            C6 = request.C6 ?? string.Empty,
            C7 = request.C7 ?? string.Empty,
            C8 = request.C8 ?? string.Empty,
            C9 = request.C9 ?? string.Empty,
            C10 = request.C10 ?? string.Empty,
            C11 = request.C11 ?? string.Empty,
            C12 = request.C12 ?? string.Empty,
            C13 = request.C13 ?? string.Empty,
            Applytime = request.Applytime ?? DateTime.Now,
            Pay = request.Pay ?? string.Empty,
            An = request.An ?? string.Empty,
            Score = request.Score ?? string.Empty,
            Wp = request.Wp,
            WpDetail = request.WpDetail ?? string.Empty,
            Finalist = (byte)(request.Finalist ? 1 : 0),
        };

        _db.Hcontests.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Create,
            $"新增競賽報名 id={entity.ContestId} 年份={request.Year} 報名者={request.C2}",
            cancellationToken: cancellationToken);

        return OperationResult<uint>.Created(entity.ContestId);
    }

    public async Task<OperationResult<uint>> UpdateAsync(
        uint id,
        UpdateHcontestRequest request,
        CancellationToken cancellationToken = default)
    {
        var entity = await _db.Hcontests
            .FirstOrDefaultAsync(h => h.ContestId == id, cancellationToken);

        if (entity is null)
            return OperationResult<uint>.NotFound("找不到競賽報名資料");

        entity.Uid = request.Uid;
        entity.Year = request.Year;
        entity.ClassType = request.ClassType ?? string.Empty;
        entity.C1 = request.C1 ?? string.Empty;
        entity.C2 = request.C2 ?? string.Empty;
        entity.C3 = request.C3 ?? string.Empty;
        entity.C4 = request.C4 ?? string.Empty;
        entity.C5 = request.C5 ?? string.Empty;
        entity.C6 = request.C6 ?? string.Empty;
        entity.C7 = request.C7 ?? string.Empty;
        entity.C8 = request.C8 ?? string.Empty;
        entity.C9 = request.C9 ?? string.Empty;
        entity.C10 = request.C10 ?? string.Empty;
        entity.C11 = request.C11 ?? string.Empty;
        entity.C12 = request.C12 ?? string.Empty;
        entity.C13 = request.C13 ?? string.Empty;
        // applytime 若沒傳就保留原值(不覆寫成 Now)
        if (request.Applytime is { } newTime)
        {
            entity.Applytime = newTime;
        }
        entity.Pay = request.Pay ?? string.Empty;
        entity.An = request.An ?? string.Empty;
        entity.Score = request.Score ?? string.Empty;
        entity.Wp = request.Wp;
        entity.WpDetail = request.WpDetail ?? string.Empty;
        entity.Finalist = (byte)(request.Finalist ? 1 : 0);

        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Update,
            $"修改競賽報名 id={id} 年份={request.Year} 報名者={request.C2}",
            cancellationToken: cancellationToken);

        return OperationResult<uint>.Ok(id, "修改成功");
    }

    public async Task<OperationResult> DeleteAsync(
        uint id,
        CancellationToken cancellationToken = default)
    {
        var entity = await _db.Hcontests
            .FirstOrDefaultAsync(h => h.ContestId == id, cancellationToken);

        if (entity is null)
            return OperationResult.NotFound("找不到競賽報名資料");

        var oldYear = entity.Year;
        var oldName = entity.C2;

        _db.Hcontests.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Delete,
            $"刪除競賽報名 id={id} 年份={oldYear} 報名者={oldName}",
            cancellationToken: cancellationToken);

        return OperationResult.Ok("刪除成功");
    }

    /// <summary>
    /// 排序白名單:未列出的欄位會 fallback 到 ContestId。
    /// </summary>
    private static IOrderedQueryable<Hcontest> ApplyOrdering(
        IQueryable<Hcontest> query,
        string? sort,
        string? by)
    {
        var isAsc = string.Equals(by, "ASC", StringComparison.OrdinalIgnoreCase);
        var key = sort?.ToLowerInvariant();

        return key switch
        {
            "year" => isAsc ? query.OrderBy(h => h.Year) : query.OrderByDescending(h => h.Year),
            "classtype" => isAsc ? query.OrderBy(h => h.ClassType) : query.OrderByDescending(h => h.ClassType),
            "applytime" => isAsc ? query.OrderBy(h => h.Applytime) : query.OrderByDescending(h => h.Applytime),
            "finalist" => isAsc ? query.OrderBy(h => h.Finalist) : query.OrderByDescending(h => h.Finalist),
            "wp" => isAsc ? query.OrderBy(h => h.Wp) : query.OrderByDescending(h => h.Wp),
            _ => isAsc ? query.OrderBy(h => h.ContestId) : query.OrderByDescending(h => h.ContestId),
        };
    }
}

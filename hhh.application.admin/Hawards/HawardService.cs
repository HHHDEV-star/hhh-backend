using hhh.api.contracts.admin.Hawards;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using hhh.infrastructure.Logging;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Hawards;

public class HawardService : IHawardService
{
    private const string PageName = "設計師獲獎";

    private readonly XoopsContext _db;
    private readonly IOperationLogWriter _logWriter;

    public HawardService(XoopsContext db, IOperationLogWriter logWriter)
    {
        _db = db;
        _logWriter = logWriter;
    }

    /// <summary>
    /// 獎項 LOGO 白名單（比照舊 PHP _hawards.php 的 award_name() 硬編碼 6 個選項）。
    /// key = DB 儲存值（檔名），value = 顯示用中文名稱。
    /// </summary>
    private static readonly IReadOnlyDictionary<string, string> LogoCatalog =
        new Dictionary<string, string>(StringComparer.Ordinal)
        {
            ["award_1.png"] = "幸福空間亞洲設計金獎",
            ["award_2.png"] = "幸福空間亞洲設計銀獎",
            ["award_3.png"] = "幸福空間亞洲設計銅獎",
            ["award_4.png"] = "幸福空間亞洲設計優良",
            ["award_5.png"] = "幸福空間亞洲設計創作",
            ["award_6.png"] = "幸福空間亞洲設計傑出",
        };

    public async Task<HawardListResponse> GetListAsync(
        HawardListRequest request,
        CancellationToken cancellationToken = default)
    {
        // 對應舊 PHP：SELECT *, (SELECT name FROM _hdesigner...), (SELECT caption FROM _hcase...)
        //             FROM _hawards WHERE ...
        // 這邊改用 LINQ LEFT JOIN 做一次查詢，避免 N+1。
        var query =
            from h in _db.Hawards.AsNoTracking()
            join d in _db.Hdesigners.AsNoTracking()
                on h.HdesignerId equals d.HdesignerId into dj
            from d in dj.DefaultIfEmpty()
            join c in _db.Hcases.AsNoTracking()
                on h.HcaseId equals c.HcaseId into cj
            from c in cj.DefaultIfEmpty()
            select new
            {
                h.HawardsId,
                h.AwardsName,
                h.HdesignerId,
                HdesignerName = d != null ? d.Name : string.Empty,
                h.HcaseId,
                HcaseCaption = c != null ? c.Caption : string.Empty,
                h.Logo,
                h.Onoff,
            };

        // 關鍵字搜尋 ----------------------------------------------------------
        if (!string.IsNullOrWhiteSpace(request.Q))
        {
            var q = request.Q.Trim();
            var like = $"%{q}%";
            query = query.Where(x =>
                EF.Functions.Like(x.HawardsId.ToString(), like) ||
                EF.Functions.Like(x.AwardsName, like) ||
                EF.Functions.Like(x.HdesignerId.ToString(), like) ||
                EF.Functions.Like(x.HcaseId.ToString(), like) ||
                EF.Functions.Like(x.HdesignerName, like) ||
                EF.Functions.Like(x.HcaseCaption, like));
        }

        if (request.HdesignerId is { } designerId)
        {
            query = query.Where(x => x.HdesignerId == designerId);
        }

        if (request.HcaseId is { } caseId)
        {
            query = query.Where(x => x.HcaseId == caseId);
        }

        var total = await query.LongCountAsync(cancellationToken);

        // 排序白名單（直接在 anonymous projection 上套用）------------------------
        var isAsc = string.Equals(request.By, "ASC", StringComparison.OrdinalIgnoreCase);
        var key = request.Sort?.ToLowerInvariant();

        query = key switch
        {
            "awardsname" => isAsc
                ? query.OrderBy(x => x.AwardsName)
                : query.OrderByDescending(x => x.AwardsName),
            "hdesignerid" => isAsc
                ? query.OrderBy(x => x.HdesignerId)
                : query.OrderByDescending(x => x.HdesignerId),
            "hcaseid" => isAsc
                ? query.OrderBy(x => x.HcaseId)
                : query.OrderByDescending(x => x.HcaseId),
            "onoff" => isAsc
                ? query.OrderBy(x => x.Onoff)
                : query.OrderByDescending(x => x.Onoff),
            _ => isAsc
                ? query.OrderBy(x => x.HawardsId)
                : query.OrderByDescending(x => x.HawardsId),
        };

        var page = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        var items = page
            .Select(x => new HawardListItem
            {
                Id = x.HawardsId,
                AwardsName = x.AwardsName,
                HdesignerId = x.HdesignerId,
                HdesignerName = x.HdesignerName ?? string.Empty,
                HcaseId = x.HcaseId,
                HcaseCaption = x.HcaseCaption ?? string.Empty,
                Logo = x.Logo,
                LogoLabel = ResolveLogoLabel(x.Logo),
                Onoff = x.Onoff == 1,
            })
            .ToList();

        return new HawardListResponse
        {
            Items = items,
            Total = total,
            Page = request.Page,
            PageSize = request.PageSize,
        };
    }

    public async Task<HawardDetailResponse?> GetByIdAsync(
        uint id,
        CancellationToken cancellationToken = default)
    {
        var row = await (
            from h in _db.Hawards.AsNoTracking()
            join d in _db.Hdesigners.AsNoTracking()
                on h.HdesignerId equals d.HdesignerId into dj
            from d in dj.DefaultIfEmpty()
            join c in _db.Hcases.AsNoTracking()
                on h.HcaseId equals c.HcaseId into cj
            from c in cj.DefaultIfEmpty()
            where h.HawardsId == id
            select new
            {
                h.HawardsId,
                h.AwardsName,
                h.HdesignerId,
                HdesignerName = d != null ? d.Name : string.Empty,
                h.HcaseId,
                HcaseCaption = c != null ? c.Caption : string.Empty,
                h.Logo,
                h.Onoff,
            }).FirstOrDefaultAsync(cancellationToken);

        if (row is null)
            return null;

        return new HawardDetailResponse
        {
            Id = row.HawardsId,
            AwardsName = row.AwardsName,
            HdesignerId = row.HdesignerId,
            HdesignerName = row.HdesignerName ?? string.Empty,
            HcaseId = row.HcaseId,
            HcaseCaption = row.HcaseCaption ?? string.Empty,
            Logo = row.Logo,
            LogoLabel = ResolveLogoLabel(row.Logo),
            Onoff = row.Onoff == 1,
        };
    }

    public async Task<HawardMutationResult> CreateAsync(
        CreateHawardRequest request,
        CancellationToken cancellationToken = default)
    {
        // logo 白名單
        if (!LogoCatalog.ContainsKey(request.Logo))
        {
            return HawardMutationResult.Fail(400, "不支援的獎項 LOGO");
        }

        // FK 存在性驗證
        var designerExists = await _db.Hdesigners
            .AsNoTracking()
            .AnyAsync(d => d.HdesignerId == request.HdesignerId, cancellationToken);
        if (!designerExists)
        {
            return HawardMutationResult.Fail(400, "指定的設計師不存在");
        }

        var caseExists = await _db.Hcases
            .AsNoTracking()
            .AnyAsync(c => c.HcaseId == request.HcaseId, cancellationToken);
        if (!caseExists)
        {
            return HawardMutationResult.Fail(400, "指定的個案不存在");
        }

        var entity = new Haward
        {
            AwardsName = request.AwardsName,
            HdesignerId = request.HdesignerId,
            HcaseId = request.HcaseId,
            Logo = request.Logo,
            Onoff = (byte)(request.Onoff ? 1 : 0),
        };

        _db.Hawards.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Create,
            $"新增得獎記錄 id={entity.HawardsId} 設計師={request.HdesignerId} 個案={request.HcaseId} 獎項={request.AwardsName}",
            cancellationToken: cancellationToken);

        return HawardMutationResult.Created(entity.HawardsId);
    }

    public async Task<HawardMutationResult> UpdateAsync(
        uint id,
        UpdateHawardRequest request,
        CancellationToken cancellationToken = default)
    {
        var entity = await _db.Hawards
            .FirstOrDefaultAsync(x => x.HawardsId == id, cancellationToken);

        if (entity is null)
            return HawardMutationResult.Fail(404, "找不到得獎記錄");

        if (!LogoCatalog.ContainsKey(request.Logo))
        {
            return HawardMutationResult.Fail(400, "不支援的獎項 LOGO");
        }

        // FK 只有在異動時才去查，避免每次 PUT 都多兩條 SELECT
        if (entity.HdesignerId != request.HdesignerId)
        {
            var designerExists = await _db.Hdesigners
                .AsNoTracking()
                .AnyAsync(d => d.HdesignerId == request.HdesignerId, cancellationToken);
            if (!designerExists)
            {
                return HawardMutationResult.Fail(400, "指定的設計師不存在");
            }
        }

        if (entity.HcaseId != request.HcaseId)
        {
            var caseExists = await _db.Hcases
                .AsNoTracking()
                .AnyAsync(c => c.HcaseId == request.HcaseId, cancellationToken);
            if (!caseExists)
            {
                return HawardMutationResult.Fail(400, "指定的個案不存在");
            }
        }

        entity.AwardsName = request.AwardsName;
        entity.HdesignerId = request.HdesignerId;
        entity.HcaseId = request.HcaseId;
        entity.Logo = request.Logo;
        entity.Onoff = (byte)(request.Onoff ? 1 : 0);

        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Update,
            $"修改得獎記錄 id={id} 設計師={request.HdesignerId} 個案={request.HcaseId} 獎項={request.AwardsName}",
            cancellationToken: cancellationToken);

        return HawardMutationResult.Ok(id);
    }

    public async Task<HawardMutationResult> DeleteAsync(
        uint id,
        CancellationToken cancellationToken = default)
    {
        var entity = await _db.Hawards
            .FirstOrDefaultAsync(x => x.HawardsId == id, cancellationToken);

        if (entity is null)
            return HawardMutationResult.Fail(404, "找不到得獎記錄");

        _db.Hawards.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Delete,
            $"刪除得獎記錄 id={id} 設計師={entity.HdesignerId} 個案={entity.HcaseId}",
            cancellationToken: cancellationToken);

        return HawardMutationResult.Deleted();
    }

    // ---------------------------------------------------------------------
    // Helpers
    // ---------------------------------------------------------------------

    private static string ResolveLogoLabel(string? logo)
    {
        if (string.IsNullOrEmpty(logo))
            return string.Empty;
        return LogoCatalog.TryGetValue(logo, out var label) ? label : string.Empty;
    }
}

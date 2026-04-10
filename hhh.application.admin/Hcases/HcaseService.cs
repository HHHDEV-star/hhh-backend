using hhh.api.contracts.admin.Hcases;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using hhh.infrastructure.Logging;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Hcases;

public class HcaseService : IHcaseService
{
    private const string PageName = "案例";

    private readonly XoopsContext _db;
    private readonly IOperationLogWriter _logWriter;

    public HcaseService(XoopsContext db, IOperationLogWriter logWriter)
    {
        _db = db;
        _logWriter = logWriter;
    }

    public async Task<HcaseListResponse> GetListAsync(
        HcaseListRequest request,
        CancellationToken cancellationToken = default)
    {
        // 對應舊 PHP：SELECT * FROM _hcase tb
        //   + 跨 _hdesigner JOIN（取 title / name）
        //   + 跨 _hcase_img 統計圖片數量
        var query = _db.Hcases.AsNoTracking();

        // 設計師過濾 ----------------------------------------------------------
        if (request.HdesignerId is { } designerId && designerId > 0)
        {
            query = query.Where(h => h.HdesignerId == designerId);
        }

        // 關鍵字搜尋 ----------------------------------------------------------
        // 舊 PHP：跨 hcase_id / caption / location / style / type + 設計師 name/title
        if (!string.IsNullOrWhiteSpace(request.Q))
        {
            var q = request.Q.Trim();
            var like = $"%{q}%";

            // 先找出符合條件的設計師 ID（name / title）
            var matchingDesignerIds = _db.Hdesigners
                .Where(d => EF.Functions.Like(d.Name, like) || EF.Functions.Like(d.Title, like))
                .Select(d => d.HdesignerId);

            query = query.Where(h =>
                EF.Functions.Like(h.HcaseId.ToString(), like) ||
                EF.Functions.Like(h.Caption, like) ||
                EF.Functions.Like(h.Location, like) ||
                EF.Functions.Like(h.Style, like) ||
                EF.Functions.Like(h.Type, like) ||
                matchingDesignerIds.Contains(h.HdesignerId));
        }

        var total = await query.LongCountAsync(cancellationToken);

        var ordered = ApplyOrdering(query, request.Sort, request.By);

        // JOIN _hdesigner 取 title / name + 計算 _hcase_img 數量
        var items = await (
            from h in ordered
            join d in _db.Hdesigners on h.HdesignerId equals d.HdesignerId into dj
            from d in dj.DefaultIfEmpty()
            select new HcaseListItem
            {
                Id = h.HcaseId,
                Caption = h.Caption,
                Cover = h.Cover,
                Location = h.Location,
                Style = h.Style,
                Type = h.Type,
                Condition = h.Condition,
                HdesignerId = h.HdesignerId,
                DesignerTitle = d != null ? d.Title : string.Empty,
                DesignerName = d != null ? d.Name : string.Empty,
                Viewed = h.Viewed,
                PhotoCount = _db.HcaseImgs.Count(i => i.HcaseId == h.HcaseId),
                AutoCountFee = h.AutoCountFee,
                Corder = h.Corder,
                Onoff = h.Onoff == 1,
                CreatTime = h.CreatTime,
            })
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return new HcaseListResponse
        {
            Items = items,
            Total = total,
            Page = request.Page,
            PageSize = request.PageSize,
        };
    }

    public async Task<HcaseDetailResponse?> GetByIdAsync(
        uint id,
        CancellationToken cancellationToken = default)
    {
        var detail = await (
            from h in _db.Hcases.AsNoTracking()
            where h.HcaseId == id
            join d in _db.Hdesigners on h.HdesignerId equals d.HdesignerId into dj
            from d in dj.DefaultIfEmpty()
            select new HcaseDetailResponse
            {
                Id = h.HcaseId,
                Sdate = h.Sdate,
                HdesignerId = h.HdesignerId,
                DesignerTitle = d != null ? d.Title : string.Empty,
                DesignerName = d != null ? d.Name : string.Empty,
                Caption = h.Caption,
                ShortDesc = h.ShortDesc,
                LongDesc = h.LongDesc,
                Member = h.Member,
                Fee = h.Fee,
                Feedesc = h.Feedesc,
                AutoCountFee = h.AutoCountFee,
                Area = h.Area,
                AreaDesc = h.AreaDesc,
                Location = h.Location,
                Style = h.Style,
                Style2 = h.Style2,
                Type = h.Type,
                Condition = h.Condition,
                Provider = h.Provider,
                Layout = h.Layout,
                Materials = h.Materials,
                Cover = h.Cover,
                Recommend = h.Recommend,
                Viewed = h.Viewed,
                Onoff = h.Onoff == 1,
                Vr360Id = h.Vr360Id,
                Istaging = h.Istaging,
                Corder = h.Corder,
                Tag = h.Tag,
                TagDatetime = h.TagDatetime,
                CreatTime = h.CreatTime,
                UpdateTime = h.UpdateTime,
            })
            .FirstOrDefaultAsync(cancellationToken);

        return detail;
    }

    public async Task<HcaseMutationResult> CreateAsync(
        CreateHcaseRequest request,
        CancellationToken cancellationToken = default)
    {
        // 設計師存在性檢查（避免孤兒 case）
        var designerExists = await _db.Hdesigners
            .AnyAsync(d => d.HdesignerId == request.HdesignerId, cancellationToken);
        if (!designerExists)
            return HcaseMutationResult.Fail(404, "找不到指定的設計師");

        var now = DateTime.UtcNow;

        var entity = new Hcase
        {
            HdesignerId = request.HdesignerId,
            Sdate = request.Sdate,
            Caption = request.Caption,
            ShortDesc = request.ShortDesc ?? string.Empty,
            LongDesc = request.LongDesc ?? string.Empty,
            Member = request.Member ?? string.Empty,

            Fee = request.Fee,
            Feedesc = request.Feedesc ?? string.Empty,
            Area = request.Area,
            AreaDesc = request.AreaDesc ?? string.Empty,

            Location = request.Location ?? string.Empty,
            Style = request.Style ?? string.Empty,
            Style2 = request.Style2 ?? string.Empty,
            Type = request.Type ?? string.Empty,
            Condition = request.Condition ?? string.Empty,

            Provider = request.Provider ?? string.Empty,
            Layout = request.Layout ?? string.Empty,
            Materials = request.Materials ?? string.Empty,

            Cover = request.Cover ?? string.Empty,
            Recommend = request.Recommend,
            Onoff = (byte)(request.Onoff ? 1 : 0),

            Vr360Id = request.Vr360Id ?? string.Empty,
            Istaging = request.Istaging,

            // 舊 PHP：tag = style,style2,type,condition（去重空字串）
            Tag = BuildTag(request.Style, request.Style2, request.Type, request.Condition),
            TagDatetime = now,

            // 系統預設
            AutoCountFee = false,
            Corder = 0,
            CreatTime = now,
            UpdateTime = now,

            // 必填但 API 不開放的舊欄位
            Level = string.Empty,
            CaseTop = string.Empty,
            SdateOrder = string.Empty,
        };

        _db.Hcases.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Create,
            $"新增案例 id={entity.HcaseId} 標題={request.Caption} 設計師={request.HdesignerId}",
            cancellationToken: cancellationToken);

        return HcaseMutationResult.Created(entity.HcaseId);
    }

    public async Task<HcaseMutationResult> UpdateAsync(
        uint id,
        UpdateHcaseRequest request,
        CancellationToken cancellationToken = default)
    {
        var entity = await _db.Hcases
            .FirstOrDefaultAsync(h => h.HcaseId == id, cancellationToken);

        if (entity is null)
            return HcaseMutationResult.Fail(404, "找不到個案");

        // 若切換所屬設計師，也要確認新設計師存在
        if (entity.HdesignerId != request.HdesignerId)
        {
            var designerExists = await _db.Hdesigners
                .AnyAsync(d => d.HdesignerId == request.HdesignerId, cancellationToken);
            if (!designerExists)
                return HcaseMutationResult.Fail(404, "找不到指定的設計師");
        }

        // 舊 PHP 行為：使用者手動改了 fee，就把 auto_count_fee 關掉
        if (entity.Fee != request.Fee)
        {
            entity.AutoCountFee = false;
        }

        entity.HdesignerId = request.HdesignerId;
        entity.Sdate = request.Sdate;
        entity.Caption = request.Caption;
        entity.ShortDesc = request.ShortDesc ?? string.Empty;
        entity.LongDesc = request.LongDesc ?? string.Empty;
        entity.Member = request.Member ?? string.Empty;

        entity.Fee = request.Fee;
        entity.Feedesc = request.Feedesc ?? string.Empty;
        entity.Area = request.Area;
        entity.AreaDesc = request.AreaDesc ?? string.Empty;

        entity.Location = request.Location ?? string.Empty;
        entity.Style = request.Style ?? string.Empty;
        entity.Style2 = request.Style2 ?? string.Empty;
        entity.Type = request.Type ?? string.Empty;
        entity.Condition = request.Condition ?? string.Empty;

        entity.Provider = request.Provider ?? string.Empty;
        entity.Layout = request.Layout ?? string.Empty;
        entity.Materials = request.Materials ?? string.Empty;

        entity.Cover = request.Cover ?? string.Empty;
        entity.Recommend = request.Recommend;
        entity.Onoff = (byte)(request.Onoff ? 1 : 0);

        entity.Vr360Id = request.Vr360Id ?? string.Empty;
        entity.Istaging = request.Istaging;

        // 重新合併 tag + 更新 tag_datetime
        var now = DateTime.UtcNow;
        entity.Tag = BuildTag(request.Style, request.Style2, request.Type, request.Condition);
        entity.TagDatetime = now;
        entity.UpdateTime = now;

        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Update,
            $"修改案例 id={id} 標題={request.Caption} 設計師={request.HdesignerId}",
            cancellationToken: cancellationToken);

        return HcaseMutationResult.Ok(id);
    }

    public async Task<HcaseMutationResult> UpdateSortOrderAsync(
        UpdateHcaseSortOrderRequest request,
        CancellationToken cancellationToken = default)
    {
        // 確認設計師存在
        var designer = await _db.Hdesigners
            .FirstOrDefaultAsync(d => d.HdesignerId == request.HdesignerId, cancellationToken);
        if (designer is null)
            return HcaseMutationResult.Fail(404, "找不到指定的設計師");

        // 收集兩個陣列中出現的所有 hcase_id，確保全部屬於同一設計師
        var featuredIds = request.Featured.Select(i => i.Id).ToList();
        var normalIds = request.Normal.Select(i => i.Id).ToList();
        var allIds = featuredIds.Concat(normalIds).Distinct().ToList();

        if (allIds.Count == 0)
            return HcaseMutationResult.Ok(message: "沒有要更新的個案");

        var entities = await _db.Hcases
            .Where(h => allIds.Contains(h.HcaseId) && h.HdesignerId == request.HdesignerId)
            .ToListAsync(cancellationToken);

        var lookup = entities.ToDictionary(h => h.HcaseId);

        // 首六區：corder = position - 1000（負值）
        foreach (var item in request.Featured)
        {
            if (lookup.TryGetValue(item.Id, out var h))
            {
                h.Corder = item.Position - 1000;
            }
        }

        // 一般區：corder = 0（PHP 註解：「首六以外不更新」實際是寫回 0）
        foreach (var item in request.Normal)
        {
            if (lookup.TryGetValue(item.Id, out var h))
            {
                h.Corder = 0;
            }
        }

        // 比照舊 PHP：順便把該設計師的 update_time 打新
        designer.UpdateTime = DateTime.UtcNow;

        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Update,
            $"案例排序 設計師={request.HdesignerId} 首六={request.Featured.Count}筆 一般={request.Normal.Count}筆",
            cancellationToken: cancellationToken);

        return HcaseMutationResult.Ok(message: "個案排序已更新");
    }

    // ---------------------------------------------------------------------
    // Helpers
    // ---------------------------------------------------------------------

    /// <summary>
    /// 合併 style / style2 / type / condition 為 tag 欄位。
    /// 對應舊 PHP：
    ///   str_replace(',,', ',', "'" . $style . ',' . $style2 . ',' . $type . ',' . $condition . "'")
    /// 這裡改成更穩健的作法：過濾空字串再用逗號 Join。
    /// </summary>
    private static string BuildTag(string? style, string? style2, string? type, string? condition)
    {
        var parts = new[] { style, style2, type, condition }
            .Where(p => !string.IsNullOrWhiteSpace(p))
            .Select(p => p!.Trim())
            .ToArray();

        return string.Join(',', parts);
    }

    /// <summary>
    /// 排序白名單：未列出的欄位會 fallback 到 HcaseId。
    /// </summary>
    private static IOrderedQueryable<Hcase> ApplyOrdering(
        IQueryable<Hcase> query,
        string? sort,
        string? by)
    {
        var isAsc = string.Equals(by, "ASC", StringComparison.OrdinalIgnoreCase);
        var key = sort?.ToLowerInvariant();

        return key switch
        {
            "caption" => isAsc ? query.OrderBy(h => h.Caption) : query.OrderByDescending(h => h.Caption),
            "hdesignerid" => isAsc ? query.OrderBy(h => h.HdesignerId) : query.OrderByDescending(h => h.HdesignerId),
            "viewed" => isAsc ? query.OrderBy(h => h.Viewed) : query.OrderByDescending(h => h.Viewed),
            "corder" => isAsc ? query.OrderBy(h => h.Corder) : query.OrderByDescending(h => h.Corder),
            "creattime" => isAsc ? query.OrderBy(h => h.CreatTime) : query.OrderByDescending(h => h.CreatTime),
            "updatetime" => isAsc ? query.OrderBy(h => h.UpdateTime) : query.OrderByDescending(h => h.UpdateTime),
            "onoff" => isAsc ? query.OrderBy(h => h.Onoff) : query.OrderByDescending(h => h.Onoff),
            "sdate" => isAsc ? query.OrderBy(h => h.Sdate) : query.OrderByDescending(h => h.Sdate),
            "recommend" => isAsc ? query.OrderBy(h => h.Recommend) : query.OrderByDescending(h => h.Recommend),
            _ => isAsc ? query.OrderBy(h => h.HcaseId) : query.OrderByDescending(h => h.HcaseId),
        };
    }
}

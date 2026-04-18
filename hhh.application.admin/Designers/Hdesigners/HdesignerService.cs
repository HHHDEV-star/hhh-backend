using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Designers.Hdesigners;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using hhh.infrastructure.Extensions;
using hhh.infrastructure.Logging;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Designers.Hdesigners;

public class HdesignerService : IHdesignerService
{
    private const string PageName = "設計師";

    private readonly XoopsContext _db;
    private readonly IOperationLogWriter _logWriter;

    public HdesignerService(XoopsContext db, IOperationLogWriter logWriter)
    {
        _db = db;
        _logWriter = logWriter;
    }

    public async Task<PagedResponse<HdesignerListItem>> GetListAsync(
        HdesignerListRequest request,
        CancellationToken cancellationToken = default)
    {
        // 對應舊 PHP:SELECT * FROM _hdesigner WHERE ...
        var query = _db.Hdesigners.AsNoTracking().AsQueryable();

        // 關鍵字搜尋 ----------------------------------------------------------
        if (!string.IsNullOrWhiteSpace(request.Q))
        {
            var q = request.Q.Trim();

            if (request.SearchByIdOnly)
            {
                // 舊 PHP:只比 hdesigner_id = $id
                if (uint.TryParse(q, out var id))
                {
                    query = query.Where(h => h.HdesignerId == id);
                }
                else
                {
                    // 明確指定 id 模式但傳字串,直接回空集合
                    query = query.Where(_ => false);
                }
            }
            else
            {
                // 舊 PHP:跨欄位 LIKE '%q%'(id / title / name / mail / website / phone / address)
                var like = $"%{q}%";
                query = query.Where(h =>
                    EF.Functions.Like(h.HdesignerId.ToString(), like) ||
                    EF.Functions.Like(h.Title, like) ||
                    EF.Functions.Like(h.Name, like) ||
                    EF.Functions.Like(h.Mail, like) ||
                    EF.Functions.Like(h.Website, like) ||
                    EF.Functions.Like(h.Phone, like) ||
                    EF.Functions.Like(h.Address, like));
            }
        }

        // 上線狀態篩選
        if (request.Onoff is { } onoff)
        {
            query = query.Where(h => h.Onoff == onoff);
        }

        // 幸福經紀人篩選
        if (request.Guarantee is { } guarantee)
        {
            query = guarantee == 0
                ? query.Where(h => h.Guarantee == 0)
                : query.Where(h => h.Guarantee != 0);
        }

        // 接案區域篩選（region 為 CSV，用 LIKE 模糊比對）
        if (!string.IsNullOrWhiteSpace(request.Region))
        {
            var like = $"%{request.Region.Trim()}%";
            query = query.Where(h => EF.Functions.Like(h.Region, like));
        }

        // 建立日期區間篩選
        if (request.DateFrom is { } dateFrom)
        {
            var from = dateFrom.ToDateTime(TimeOnly.MinValue);
            query = query.Where(h => h.CreatTime >= from);
        }
        if (request.DateTo is { } dateTo)
        {
            var to = dateTo.ToDateTime(TimeOnly.MaxValue);
            query = query.Where(h => h.CreatTime <= to);
        }

        var ordered = ApplyOrdering(query, request.Sort, request.By);

        return await ordered
            .Select(h => new HdesignerListItem
            {
                Id = h.HdesignerId,
                ImgPath = h.ImgPath,
                Title = h.Title,
                Name = h.Name,
                Mail = h.Mail,
                Phone = h.Phone,
                ServicePhone = h.ServicePhone,
                Address = h.Address,
                Website = h.Website,
                Region = h.Region,
                Style = h.Style,
                Onoff = h.Onoff == 1,
                Guarantee = h.Guarantee,
                Clicks = h.Clicks,
                CaseCount = _db.Hcases.Count(c => c.HdesignerId == h.HdesignerId),
                Dorder = h.Dorder,
                MobileOrder = h.MobileOrder,
                CreatTime = h.CreatTime,
                UpdateTime = h.UpdateTime,
            })
            .ToPagedResponseAsync(request.Page, request.PageSize, cancellationToken);
    }

    public async Task<List<HdesignerSelectItem>> GetSelectListAsync(
        CancellationToken cancellationToken = default)
    {
        // 精簡列表:僅回傳下拉選單所需欄位,不分頁、不篩選。
        // 預設依 hdesigner_id DESC 排序,與分頁版預設排序一致。
        return await _db.Hdesigners
            .AsNoTracking()
            .OrderByDescending(h => h.HdesignerId)
            .Select(h => new HdesignerSelectItem
            {
                Id = h.HdesignerId,
                ImgPath = h.ImgPath,
                Title = h.Title,
                Name = h.Name,
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<HdesignerDetailResponse?> GetByIdAsync(
        uint id,
        CancellationToken cancellationToken = default)
    {
        var h = await _db.Hdesigners
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.HdesignerId == id, cancellationToken);

        if (h is null)
            return null;

        return MapToDetail(h);
    }

    public async Task<OperationResult<uint>> CreateAsync(
        CreateHdesignerRequest request,
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        var entity = new Hdesigner
        {
            // 基本資料
            Title = request.Title,
            Name = request.Name,
            ImgPath = request.ImgPath ?? string.Empty,
            Background = request.Background,
            BackgroundMobile = request.BackgroundMobile,
            Premium = request.Premium ?? "0",

            // 分類 CSV
            Region = request.Region ?? string.Empty,
            Location = request.Location ?? string.Empty,
            Type = request.Type ?? string.Empty,
            Style = request.Style ?? string.Empty,

            // 接案條件
            Budget = request.Budget ?? string.Empty,
            MinBudget = request.MinBudget,
            MaxBudget = request.MaxBudget,
            Area = request.Area ?? string.Empty,
            Special = request.Special ?? string.Empty,
            Charge = request.Charge ?? string.Empty,
            Payment = request.Payment ?? string.Empty,

            // 聯絡資訊
            ServicePhone = request.ServicePhone ?? string.Empty,
            Phone = request.Phone ?? string.Empty,
            Fax = request.Fax ?? string.Empty,
            Address = request.Address ?? string.Empty,
            Mail = request.Mail ?? string.Empty,
            Website = request.Website ?? string.Empty,
            Blog = request.Blog ?? string.Empty,
            Fbpageurl = request.Fbpageurl ?? string.Empty,
            LineId = request.LineId,

            // 內容
            Position = request.Position ?? string.Empty,
            Idea = request.Idea ?? string.Empty,
            // idea 自動同步到 description（比照舊 PHP 行為）
            Description = request.Idea ?? string.Empty,
            Career = request.Career ?? string.Empty,
            Awards = request.Awards ?? string.Empty,
            License = request.License ?? string.Empty,
            Seo = request.Seo,
            MetaDescription = request.MetaDescription,

            // 行政
            Onoff = (byte)(request.Onoff ? 1 : 0),
            XoopsUid = request.XoopsUid,
            SalesMail = request.SalesMail ?? string.Empty,
            DesignerMail = request.DesignerMail ?? string.Empty,
            Guarantee = request.Guarantee,
            SearchKeywords = request.SearchKeywords,
            CoordinateX = request.CoordinateX ?? string.Empty,
            CoordinateY = request.CoordinateY ?? string.Empty,
            Taxid = request.Taxid ?? string.Empty,

            // 必填但表單不會帶的舊欄位，給 DB 預設可接受的空值
            AwardsName = string.Empty,
            AwardsLogo = string.Empty,
            Top = "N",
            OrderComputer = string.Empty,
            OrderMb = string.Empty,
            TopSix = string.Empty,
            Emering = string.Empty,

            // 時間戳
            CreatTime = now,
            UpdateTime = now,
        };

        _db.Hdesigners.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Create,
            $"新增設計師 id={entity.HdesignerId} 名稱={request.Title} / {request.Name}",
            cancellationToken: cancellationToken);

        return OperationResult<uint>.Created(entity.HdesignerId);
    }

    public async Task<OperationResult<uint>> UpdateAsync(
        uint id,
        UpdateHdesignerRequest request,
        CancellationToken cancellationToken = default)
    {
        var h = await _db.Hdesigners
            .FirstOrDefaultAsync(x => x.HdesignerId == id, cancellationToken);

        if (h is null)
            return OperationResult<uint>.NotFound("找不到設計師");

        // 基本資料
        h.Title = request.Title;
        h.Name = request.Name;
        h.ImgPath = request.ImgPath ?? string.Empty;
        h.Background = request.Background;
        h.BackgroundMobile = request.BackgroundMobile;
        h.Premium = request.Premium ?? "0";

        // 分類 CSV
        h.Region = request.Region ?? string.Empty;
        h.Location = request.Location ?? string.Empty;
        h.Type = request.Type ?? string.Empty;
        h.Style = request.Style ?? string.Empty;

        // 接案條件
        h.Budget = request.Budget ?? string.Empty;
        h.MinBudget = request.MinBudget;
        h.MaxBudget = request.MaxBudget;
        h.Area = request.Area ?? string.Empty;
        h.Special = request.Special ?? string.Empty;
        h.Charge = request.Charge ?? string.Empty;
        h.Payment = request.Payment ?? string.Empty;

        // 聯絡資訊
        h.ServicePhone = request.ServicePhone ?? string.Empty;
        h.Phone = request.Phone ?? string.Empty;
        h.Fax = request.Fax ?? string.Empty;
        h.Address = request.Address ?? string.Empty;
        h.Mail = request.Mail ?? string.Empty;
        h.Website = request.Website ?? string.Empty;
        h.Blog = request.Blog ?? string.Empty;
        h.Fbpageurl = request.Fbpageurl ?? string.Empty;
        h.LineId = request.LineId;

        // 內容
        h.Position = request.Position ?? string.Empty;
        h.Idea = request.Idea ?? string.Empty;
        // idea 自動同步到 description（比照舊 PHP 行為）
        h.Description = request.Idea ?? string.Empty;
        h.Career = request.Career ?? string.Empty;
        h.Awards = request.Awards ?? string.Empty;
        h.License = request.License ?? string.Empty;
        h.Seo = request.Seo;
        h.MetaDescription = request.MetaDescription;

        // 行政
        h.Onoff = (byte)(request.Onoff ? 1 : 0);
        h.XoopsUid = request.XoopsUid;
        h.SalesMail = request.SalesMail ?? string.Empty;
        h.DesignerMail = request.DesignerMail ?? string.Empty;
        h.Guarantee = request.Guarantee;
        h.SearchKeywords = request.SearchKeywords;
        h.CoordinateX = request.CoordinateX ?? string.Empty;
        h.CoordinateY = request.CoordinateY ?? string.Empty;
        h.Taxid = request.Taxid ?? string.Empty;

        h.UpdateTime = DateTime.UtcNow;

        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Update,
            $"修改設計師 id={id} 名稱={request.Title} / {request.Name}",
            cancellationToken: cancellationToken);

        return OperationResult<uint>.Ok(id, "更新成功");
    }

    public async Task<OperationResult<uint>> UpdateSortOrderAsync(
        UpdateHdesignerSortOrderRequest request,
        CancellationToken cancellationToken = default)
    {
        // 對應舊 _hdesigner_sort.php：批次 UPDATE _hdesigner SET dorder=? WHERE hdesigner_id=? AND onoff=1
        var ids = request.Items.Select(i => i.Id).Distinct().ToList();

        var entities = await _db.Hdesigners
            .Where(h => ids.Contains(h.HdesignerId) && h.Onoff == 1)
            .ToListAsync(cancellationToken);

        var lookup = entities.ToDictionary(h => h.HdesignerId);

        foreach (var item in request.Items)
        {
            if (lookup.TryGetValue(item.Id, out var h))
            {
                h.Dorder = (uint)Math.Max(0, item.Order);
            }
        }

        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Update,
            $"設計師桌機版排序更新 {entities.Count}筆",
            cancellationToken: cancellationToken);

        return OperationResult<uint>.Ok("桌機版排序已更新");
    }

    public async Task<OperationResult<uint>> UpdateMobileSortOrderAsync(
        UpdateHdesignerSortOrderRequest request,
        CancellationToken cancellationToken = default)
    {
        // 對應舊 _hdesigner_mobile_sort.php：批次 UPDATE _hdesigner SET mobile_order=? WHERE hdesigner_id=?
        var ids = request.Items.Select(i => i.Id).Distinct().ToList();

        var entities = await _db.Hdesigners
            .Where(h => ids.Contains(h.HdesignerId))
            .ToListAsync(cancellationToken);

        var lookup = entities.ToDictionary(h => h.HdesignerId);

        foreach (var item in request.Items)
        {
            if (lookup.TryGetValue(item.Id, out var h))
            {
                h.MobileOrder = item.Order;
            }
        }

        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Update,
            $"設計師手機版排序更新 {entities.Count}筆",
            cancellationToken: cancellationToken);

        return OperationResult<uint>.Ok("手機版排序已更新");
    }

    // ---------------------------------------------------------------------
    // Helpers
    // ---------------------------------------------------------------------

    private static HdesignerDetailResponse MapToDetail(Hdesigner h) => new()
    {
        Id = h.HdesignerId,
        ImgPath = h.ImgPath,
        Background = h.Background,
        BackgroundMobile = h.BackgroundMobile,
        Title = h.Title,
        Name = h.Name,
        Premium = h.Premium ?? "0",

        Region = h.Region,
        Location = h.Location,
        Type = h.Type,
        Style = h.Style,

        Budget = h.Budget,
        MinBudget = h.MinBudget,
        MaxBudget = h.MaxBudget,
        Area = h.Area,
        Special = h.Special,
        Charge = h.Charge,
        Payment = h.Payment,

        ServicePhone = h.ServicePhone,
        Phone = h.Phone,
        Fax = h.Fax,
        Address = h.Address,
        Mail = h.Mail,
        Website = h.Website,
        Blog = h.Blog,
        Fbpageurl = h.Fbpageurl,
        LineId = h.LineId,

        Position = h.Position,
        Idea = h.Idea,
        Career = h.Career,
        Awards = h.Awards,
        License = h.License,
        Seo = h.Seo,
        MetaDescription = h.MetaDescription,

        Onoff = h.Onoff == 1,
        XoopsUid = h.XoopsUid,
        SalesMail = h.SalesMail,
        DesignerMail = h.DesignerMail,
        Guarantee = h.Guarantee,
        SearchKeywords = h.SearchKeywords,
        SearchKeywordsAuto = h.SearchKeywordsAuto,
        CoordinateX = h.CoordinateX,
        CoordinateY = h.CoordinateY,
        Taxid = h.Taxid,

        Dorder = h.Dorder,
        MobileOrder = h.MobileOrder,

        CreatTime = h.CreatTime,
        UpdateTime = h.UpdateTime,
    };

    /// <summary>
    /// 排序白名單：未列出的欄位會 fallback 到 HdesignerId。
    /// 不信任 client 直接組 SQL ORDER BY 片段，避免 injection。
    /// </summary>
    private static IOrderedQueryable<Hdesigner> ApplyOrdering(
        IQueryable<Hdesigner> query,
        string? sort,
        string? by)
    {
        var isAsc = string.Equals(by, "ASC", StringComparison.OrdinalIgnoreCase);
        var key = sort?.ToLowerInvariant();

        return key switch
        {
            "title" => isAsc ? query.OrderBy(h => h.Title) : query.OrderByDescending(h => h.Title),
            "name" => isAsc ? query.OrderBy(h => h.Name) : query.OrderByDescending(h => h.Name),
            "dorder" => isAsc ? query.OrderBy(h => h.Dorder) : query.OrderByDescending(h => h.Dorder),
            "mobileorder" => isAsc ? query.OrderBy(h => h.MobileOrder) : query.OrderByDescending(h => h.MobileOrder),
            "onoff" => isAsc ? query.OrderBy(h => h.Onoff) : query.OrderByDescending(h => h.Onoff),
            "clicks" => isAsc ? query.OrderBy(h => h.Clicks) : query.OrderByDescending(h => h.Clicks),
            "creattime" => isAsc ? query.OrderBy(h => h.CreatTime) : query.OrderByDescending(h => h.CreatTime),
            "updatetime" => isAsc ? query.OrderBy(h => h.UpdateTime) : query.OrderByDescending(h => h.UpdateTime),
            _ => isAsc ? query.OrderBy(h => h.HdesignerId) : query.OrderByDescending(h => h.HdesignerId),
        };
    }
}

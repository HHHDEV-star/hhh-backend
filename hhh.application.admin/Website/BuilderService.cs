using hhh.api.contracts.admin.Website;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using hhh.infrastructure.Extensions;
using hhh.infrastructure.Logging;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Website;

public class BuilderService : IBuilderService
{
    private const string PageName = "建商";

    private readonly XoopsContext _db;
    private readonly IOperationLogWriter _logWriter;

    public BuilderService(XoopsContext db, IOperationLogWriter logWriter)
    {
        _db = db;
        _logWriter = logWriter;
    }

    /// <inheritdoc />
    public async Task<BuilderListResponse> GetListAsync(
        BuilderListQuery query,
        CancellationToken cancellationToken = default)
    {
        var q = _db.Builders.AsNoTracking().AsQueryable();

        // 上線狀態篩選(不帶 = 全部)
        if (query.Onoff is { } onoff)
        {
            q = q.Where(b => b.Onoff == onoff);
        }

        // 關鍵字搜尋:公司名稱 / 電話 / 地址 / Email / 建案名稱
        if (!string.IsNullOrWhiteSpace(query.Keyword))
        {
            var like = $"%{query.Keyword.Trim()}%";

            // 先查出「建案名稱」符合的 builder_id 清單(correlated subquery)
            var matchingBuilderIds = _db.BuilderProducts
                .Where(p => EF.Functions.Like(p.Name, like))
                .Select(p => p.BuilderId)
                .Distinct();

            q = q.Where(b =>
                EF.Functions.Like(b.Title, like) ||
                EF.Functions.Like(b.Phone, like) ||
                EF.Functions.Like(b.Address, like) ||
                EF.Functions.Like(b.Email, like) ||
                matchingBuilderIds.Contains(b.BuilderId));
        }

        // 分頁主查詢
        var paged = await q
            .OrderByDescending(b => b.BuilderId)
            .Select(b => new BuilderListItem
            {
                Id = b.BuilderId,
                Logo = b.Logo,
                Title = b.Title,
                Onoff = b.Onoff,
                Phone = b.Phone,
                Email = b.Email,
                Address = b.Address,
                CreatTime = b.CreatTime,
                ProductCount = _db.BuilderProducts.Count(p => p.BuilderId == b.BuilderId),
            })
            .ToPagedResponseAsync(query.Page, query.PageSize, cancellationToken);

        // 全域統計摘要(不受上方 keyword / onoff 篩選影響)
        // EF Core 不支援同 DbContext 並行查詢,4 個 COUNT 依序執行
        var builderTotal = await _db.Builders.CountAsync(cancellationToken);
        var builderOnoffCount = await _db.Builders
            .CountAsync(b => b.Onoff == 1, cancellationToken);
        var productTotal = await _db.BuilderProducts.CountAsync(cancellationToken);
        var productOnoffCount = await _db.BuilderProducts
            .CountAsync(
                p => _db.Builders.Any(b => b.BuilderId == p.BuilderId && b.Onoff == 1),
                cancellationToken);

        return new BuilderListResponse
        {
            Items = paged.Items,
            Total = paged.Total,
            Page = paged.Page,
            PageSize = paged.PageSize,
            Summary = new BuilderListSummary
            {
                BuilderTotal = builderTotal,
                BuilderOnoffCount = builderOnoffCount,
                ProductTotal = productTotal,
                ProductOnoffCount = productOnoffCount,
            },
        };
    }

    /// <inheritdoc />
    public async Task<BuilderDetailResponse?> GetByIdAsync(
        uint id, CancellationToken cancellationToken = default)
    {
        return await _db.Builders
            .AsNoTracking()
            .Where(b => b.BuilderId == id)
            .Select(b => new BuilderDetailResponse
            {
                Id = b.BuilderId,
                Logo = b.Logo,
                Logo2 = b.Logo2,
                Title = b.Title,
                SubCompany = b.SubCompany,
                ServicePhone = b.ServicePhone,
                Phone = b.Phone,
                Address = b.Address,
                Website = b.Website,
                Fbpageurl = b.Fbpageurl,
                Email = b.Email,
                Intro = b.Intro,
                History = b.History,
                Desc = b.Desc,
                Gchoice = b.Gchoice,
                HvideoId = b.HvideoId,
                Recommend = b.Recommend,
                Border = b.Border,
                Onoff = b.Onoff,
                CreatTime = b.CreatTime,
                Vr360Id = b.Vr360Id,
                Clicks = b.Clicks,
                BackgroundMobile = b.BackgroundMobile,
                IsSend = b.IsSend,
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<OperationResult<uint>> CreateAsync(
        CreateBuilderRequest request, CancellationToken cancellationToken = default)
    {
        var entity = new Builder
        {
            Title = request.Title,
            Logo = request.Logo ?? string.Empty,
            Logo2 = string.Empty,
            SubCompany = null,
            ServicePhone = string.Empty,
            Phone = request.Phone ?? string.Empty,
            Address = request.Address ?? string.Empty,
            Website = string.Empty,
            Fbpageurl = string.Empty,
            Email = request.Email ?? string.Empty,
            Intro = request.Intro ?? string.Empty,
            History = string.Empty,
            Desc = request.Desc ?? string.Empty,
            Gchoice = string.Empty,
            HvideoId = 0,
            Recommend = 0,
            Border = 0,
            Onoff = request.Onoff,
            CreatTime = DateTime.Now,
            Vr360Id = string.Empty,
            Clicks = 0,
            BackgroundMobile = null,
            IsSend = 0,
        };

        _db.Builders.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Create,
            $"新增建商 id={entity.BuilderId} 名稱={request.Title}",
            cancellationToken: cancellationToken);

        return OperationResult<uint>.Created(entity.BuilderId, "新增成功");
    }

    /// <inheritdoc />
    public async Task<OperationResult<uint>> UpdateAsync(
        uint id, UpdateBuilderRequest request, CancellationToken cancellationToken = default)
    {
        var entity = await _db.Builders
            .FirstOrDefaultAsync(b => b.BuilderId == id, cancellationToken);

        if (entity is null)
            return OperationResult<uint>.NotFound("找不到建商");

        entity.Title = request.Title;
        entity.Logo = request.Logo ?? string.Empty;
        entity.Onoff = request.Onoff;
        entity.Email = request.Email ?? string.Empty;
        entity.Phone = request.Phone ?? string.Empty;
        entity.Intro = request.Intro ?? string.Empty;
        entity.Desc = request.Desc ?? string.Empty;
        entity.Address = request.Address ?? string.Empty;

        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Update,
            $"修改建商 id={id} 名稱={request.Title}",
            cancellationToken: cancellationToken);

        return OperationResult<uint>.Ok(id, "修改成功");
    }

    /// <inheritdoc />
    public async Task<OperationResult<uint>> DeleteAsync(
        uint id, CancellationToken cancellationToken = default)
    {
        var entity = await _db.Builders
            .FirstOrDefaultAsync(b => b.BuilderId == id, cancellationToken);

        if (entity is null)
            return OperationResult<uint>.NotFound("找不到建商");

        var oldTitle = entity.Title;

        _db.Builders.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Delete,
            $"刪除建商 id={id} 名稱={oldTitle}",
            cancellationToken: cancellationToken);

        return OperationResult<uint>.Ok(id, "刪除成功");
    }

    /// <inheritdoc />
    public async Task<List<BuilderDropdownItem>> GetDropdownAsync(
        CancellationToken cancellationToken = default)
    {
        return await _db.Builders
            .AsNoTracking()
            .Select(b => new BuilderDropdownItem
            {
                Value = b.BuilderId,
                Name = $"{b.BuilderId}-{b.Title}",
            })
            .ToListAsync(cancellationToken);
    }
}

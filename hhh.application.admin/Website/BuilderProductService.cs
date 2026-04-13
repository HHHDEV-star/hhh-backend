using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Website;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using hhh.infrastructure.Extensions;
using hhh.infrastructure.Logging;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Website;

/// <summary>
/// 建案管理 service（含建案圖片 + 設定封面）。
/// 對應舊版 PHP: builder_model.php 的 product / product_img / img 相關方法。
/// </summary>
public class BuilderProductService : IBuilderProductService
{
    private const string PageName = "建案";
    private const string ImagePageName = "建案圖片";

    private readonly XoopsContext _db;
    private readonly IOperationLogWriter _logWriter;

    public BuilderProductService(XoopsContext db, IOperationLogWriter logWriter)
    {
        _db = db;
        _logWriter = logWriter;
    }

    // =========================================================================
    // BuilderProduct 建案
    // =========================================================================

    /// <inheritdoc />
    public async Task<PagedResponse<BuilderProductListItem>> GetListAsync(
        BuilderProductListRequest request,
        CancellationToken cancellationToken = default)
    {
        var query = _db.BuilderProducts.AsNoTracking();

        // 關鍵字搜尋 ----------------------------------------------------------
        if (!string.IsNullOrWhiteSpace(request.Q))
        {
            var q = request.Q.Trim();
            var like = $"%{q}%";

            // 先找出符合名稱的建商 ID
            var matchingBuilderIds = _db.Builders
                .Where(b => EF.Functions.Like(b.Title, like))
                .Select(b => b.BuilderId);

            query = query.Where(p =>
                EF.Functions.Like(p.BuilderProductId.ToString(), like) ||
                EF.Functions.Like(p.Name, like) ||
                EF.Functions.Like(p.City, like) ||
                matchingBuilderIds.Contains(p.BuilderId));
        }

        // 精準過濾 ------------------------------------------------------------
        if (request.BuilderId is { } builderId)
        {
            query = query.Where(p => p.BuilderId == builderId);
        }

        if (request.Onoff is { } onoff)
        {
            query = query.Where(p => p.Onoff == onoff);
        }

        // 排序白名單 ----------------------------------------------------------
        var ordered = ApplyOrdering(query, request.Sort, request.By);

        // 先投影出基本欄位（不含 ColumnIds / VideoIds）
        var paged = await ordered
            .Join(
                _db.Builders.AsNoTracking(),
                p => p.BuilderId,
                b => b.BuilderId,
                (p, b) => new BuilderProductListItem
                {
                    Id = p.BuilderProductId,
                    BuilderId = p.BuilderId,
                    BuilderTitle = b.Title,
                    Name = p.Name,
                    Types = p.Types,
                    City = p.City,
                    Onoff = p.Onoff,
                    UpdateTime = p.UpdateTime,
                    Cover = p.Cover,
                })
            .ToPagedResponseAsync(request.Page, request.PageSize, cancellationToken);

        // 回填 ColumnIds / VideoIds（in-memory 查詢，避免 subquery 問題）
        if (paged.Items.Count > 0)
        {
            var productIds = paged.Items.Select(x => x.Id).ToList();

            var columnMap = await _db.Hcolumns
                .AsNoTracking()
                .Where(c => productIds.Contains(c.BuilderProductId))
                .GroupBy(c => c.BuilderProductId)
                .Select(g => new { ProductId = g.Key, Ids = g.Select(c => c.HcolumnId).ToList() })
                .ToDictionaryAsync(x => x.ProductId, x => x.Ids, cancellationToken);

            var videoMap = await _db.Hvideos
                .AsNoTracking()
                .Where(v => productIds.Contains(v.BuilderProductId))
                .GroupBy(v => v.BuilderProductId)
                .Select(g => new { ProductId = g.Key, Ids = g.Select(v => v.HvideoId).ToList() })
                .ToDictionaryAsync(x => x.ProductId, x => x.Ids, cancellationToken);

            foreach (var item in paged.Items)
            {
                if (columnMap.TryGetValue(item.Id, out var cols))
                    item.ColumnIds = cols;
                if (videoMap.TryGetValue(item.Id, out var vids))
                    item.VideoIds = vids;
            }
        }

        return paged;
    }

    /// <inheritdoc />
    public async Task<BuilderProductDetailResponse?> GetByIdAsync(
        uint id,
        CancellationToken cancellationToken = default)
    {
        var detail = await _db.BuilderProducts
            .AsNoTracking()
            .Where(p => p.BuilderProductId == id)
            .Join(
                _db.Builders.AsNoTracking(),
                p => p.BuilderId,
                b => b.BuilderId,
                (p, b) => new BuilderProductDetailResponse
                {
                    Id = p.BuilderProductId,
                    BuilderId = p.BuilderId,
                    BuilderTitle = b.Title,
                    Name = p.Name,
                    Types = p.Types,
                    Btag = p.Btag,
                    BuilderType = p.BuilderType,
                    Layout = p.Layout,
                    TotalPrice = p.TotalPrice,
                    UnitPrice = p.UnitPrice,
                    Descr = p.Descr,
                    Brief = p.Brief,
                    Address = p.Address,
                    City = p.City,
                    Website = p.Website,
                    ServicePhone = p.ServicePhone,
                    Phone = p.Phone,
                    Email = p.Email,
                    Cover = p.Cover,
                    YtCover = p.YtCover,
                    Onoff = p.Onoff,
                    UpdatedAt = p.UpdatedAt,
                    UpdateTime = p.UpdateTime,
                    Clicks = p.Clicks,
                    Sort = p.Sort,
                    Istaging = p.Istaging,
                    SalesEmail = p.SalesEmail,
                    SalesAssistantEmail = p.SalesAssistantEmail,
                    LejuUrl = p.LejuUrl,
                    IsVideo = p.IsVideo,
                    ReviewA = p.ReviewA,
                    ReviewB = p.ReviewB,
                    ReviewC = p.ReviewC,
                    ReviewD = p.ReviewD,
                    ReviewE = p.ReviewE,
                    W = p.W,
                    H = p.H,
                })
            .FirstOrDefaultAsync(cancellationToken);

        if (detail is null) return null;

        // 回填 ColumnIds / VideoIds
        detail.ColumnIds = await _db.Hcolumns
            .AsNoTracking()
            .Where(c => c.BuilderProductId == id)
            .Select(c => c.HcolumnId)
            .ToListAsync(cancellationToken);

        detail.VideoIds = await _db.Hvideos
            .AsNoTracking()
            .Where(v => v.BuilderProductId == id)
            .Select(v => v.HvideoId)
            .ToListAsync(cancellationToken);

        return detail;
    }

    /// <inheritdoc />
    public async Task<OperationResult<uint>> CreateAsync(
        CreateBuilderProductRequest request,
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.Now;

        var entity = new BuilderProduct
        {
            BuilderId = request.BuilderId,
            Name = request.Name,
            Types = request.Types,
            Btag = request.Btag,
            BuilderType = request.BuilderType,
            Layout = request.Layout,
            TotalPrice = request.TotalPrice,
            UnitPrice = request.UnitPrice,
            Descr = request.Descr ?? string.Empty,
            Brief = request.Brief ?? string.Empty,
            Address = request.Address ?? string.Empty,
            City = request.City ?? string.Empty,
            Website = request.Website,
            ServicePhone = request.ServicePhone,
            Phone = request.Phone ?? string.Empty,
            Email = request.Email,
            Cover = string.Empty,
            YtCover = null,
            Onoff = request.Onoff,
            UpdatedAt = now,
            UpdateTime = now,
            Clicks = 0,
            Sort = 0,
            Istaging = request.Istaging,
            SalesEmail = request.SalesEmail,
            SalesAssistantEmail = request.SalesAssistantEmail,
            LejuUrl = request.LejuUrl,
            IsVideo = "0",
            ReviewA = request.ReviewA,
            ReviewB = request.ReviewB,
            ReviewC = request.ReviewC,
            ReviewD = request.ReviewD,
            ReviewE = request.ReviewE,
            W = null,
            H = null,
            IsSend = 0,
        };

        _db.BuilderProducts.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        // 連動 _hcolumn / _hvideo（對應 PHP add_hcolumn_and_video_by_builder_product_id）
        await LinkColumnsAndVideosAsync(
            entity.BuilderProductId, request.ColumnIds, request.VideoIds, cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Create,
            $"新增建案 id={entity.BuilderProductId} 名稱={request.Name}",
            cancellationToken: cancellationToken);

        return OperationResult<uint>.Created(entity.BuilderProductId, "新增成功");
    }

    /// <inheritdoc />
    public async Task<OperationResult<uint>> UpdateAsync(
        uint id,
        UpdateBuilderProductRequest request,
        CancellationToken cancellationToken = default)
    {
        var entity = await _db.BuilderProducts
            .FirstOrDefaultAsync(p => p.BuilderProductId == id, cancellationToken);

        if (entity is null)
            return OperationResult<uint>.NotFound("找不到建案");

        // 更新主表欄位
        entity.BuilderId = request.BuilderId;
        entity.Name = request.Name;
        entity.Types = request.Types;
        entity.Btag = request.Btag;
        entity.BuilderType = request.BuilderType;
        entity.Layout = request.Layout;
        entity.TotalPrice = request.TotalPrice;
        entity.UnitPrice = request.UnitPrice;
        entity.Descr = request.Descr ?? string.Empty;
        entity.Brief = request.Brief ?? string.Empty;
        entity.Address = request.Address ?? string.Empty;
        entity.City = request.City ?? string.Empty;
        entity.Website = request.Website;
        entity.ServicePhone = request.ServicePhone;
        entity.Phone = request.Phone ?? string.Empty;
        entity.Email = request.Email;
        if (request.Cover is not null)
            entity.Cover = request.Cover;
        entity.Istaging = request.Istaging;
        entity.SalesEmail = request.SalesEmail;
        entity.SalesAssistantEmail = request.SalesAssistantEmail;
        entity.LejuUrl = request.LejuUrl;
        entity.ReviewA = request.ReviewA;
        entity.ReviewB = request.ReviewB;
        entity.ReviewC = request.ReviewC;
        entity.ReviewD = request.ReviewD;
        entity.ReviewE = request.ReviewE;
        entity.H = request.H;
        entity.Onoff = request.Onoff;
        entity.UpdateTime = DateTime.Now;
        entity.IsVideo = "0";

        await _db.SaveChangesAsync(cancellationToken);

        // 先解除舊的 video 連結（對應 PHP: UPDATE _hvideo SET builder_product_id=0 WHERE builder_product_id=X）
        await _db.Hvideos
            .Where(v => v.BuilderProductId == id)
            .ExecuteUpdateAsync(s => s.SetProperty(v => v.BuilderProductId, 0u), cancellationToken);

        // 重新連動 _hcolumn / _hvideo
        await LinkColumnsAndVideosAsync(id, request.ColumnIds, request.VideoIds, cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Update,
            $"修改建案 id={id} 名稱={request.Name}",
            cancellationToken: cancellationToken);

        return OperationResult<uint>.Ok(id, "修改成功");
    }

    /// <inheritdoc />
    public async Task<OperationResult<uint>> DeleteAsync(
        uint id,
        CancellationToken cancellationToken = default)
    {
        var entity = await _db.BuilderProducts
            .FirstOrDefaultAsync(p => p.BuilderProductId == id, cancellationToken);

        if (entity is null)
            return OperationResult<uint>.NotFound("找不到建案");

        var oldName = entity.Name;

        _db.BuilderProducts.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Delete,
            $"刪除建案 id={id} 名稱={oldName}",
            cancellationToken: cancellationToken);

        return OperationResult<uint>.Ok(id, "刪除成功");
    }

    /// <inheritdoc />
    public async Task<List<BuilderProductDropdownItem>> GetDropdownAsync(
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP get_builder_for_dropdown: onoff=1, ORDER BY builder_product_id DESC
        return await _db.BuilderProducts
            .AsNoTracking()
            .Where(p => p.Onoff == 1)
            .OrderByDescending(p => p.BuilderProductId)
            .Select(p => new BuilderProductDropdownItem
            {
                Id = p.BuilderProductId,
                Name = $"{p.BuilderProductId}-{p.Name}",
            })
            .ToListAsync(cancellationToken);
    }

    // =========================================================================
    // BuilderProductImage 建案圖片
    // =========================================================================

    /// <inheritdoc />
    public async Task<List<BuilderProductImageListItem>> GetImagesAsync(
        uint productId,
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP get_builder_product_img_lists: ORDER BY is_cover DESC, order_no ASC
        return await _db.HproductImgs
            .AsNoTracking()
            .Where(i => i.BuilderProductId == productId)
            .Join(
                _db.BuilderProducts.AsNoTracking(),
                i => i.BuilderProductId,
                p => p.BuilderProductId,
                (i, p) => new BuilderProductImageListItem
                {
                    Id = i.Id,
                    Name = i.Name,
                    Title = i.Title,
                    Descr = i.Descr,
                    OrderNo = i.OrderNo,
                    IsCover = i.IsCover,
                    BuilderProductName = p.Name,
                })
            .OrderByDescending(x => x.IsCover)
            .ThenBy(x => x.OrderNo)
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<OperationResult<uint>> CreateImageAsync(
        uint productId,
        CreateBuilderProductImageRequest request,
        CancellationToken cancellationToken = default)
    {
        // 檢查 parent 是否存在
        var productExists = await _db.BuilderProducts
            .AnyAsync(p => p.BuilderProductId == productId, cancellationToken);
        if (!productExists)
            return OperationResult<uint>.NotFound("找不到建案");

        var entity = new HproductImg
        {
            BuilderProductId = productId,
            HproductId = 0,
            Name = request.Name,
            Title = string.Empty,
            Descr = string.Empty,
            OrderNo = request.OrderNo,
            IsCover = false,
        };

        _db.HproductImgs.Add(entity);

        // 同步更新 parent update_time
        await _db.BuilderProducts
            .Where(p => p.BuilderProductId == productId)
            .ExecuteUpdateAsync(
                s => s.SetProperty(p => p.UpdateTime, DateTime.Now),
                cancellationToken);

        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            ImagePageName,
            OperationAction.Create,
            $"新增建案圖片 id={entity.Id} 建案id={productId} 檔名={request.Name}",
            cancellationToken: cancellationToken);

        return OperationResult<uint>.Created(entity.Id, "新增成功");
    }

    /// <inheritdoc />
    public async Task<OperationResult<uint>> UpdateImageAsync(
        uint productId,
        uint imageId,
        UpdateBuilderProductImageRequest request,
        CancellationToken cancellationToken = default)
    {
        var entity = await _db.HproductImgs
            .FirstOrDefaultAsync(
                i => i.Id == imageId && i.BuilderProductId == productId,
                cancellationToken);

        if (entity is null)
            return OperationResult<uint>.NotFound("找不到建案圖片");

        entity.Title = request.Title ?? string.Empty;
        entity.Descr = request.Descr ?? string.Empty;
        entity.OrderNo = request.OrderNo;

        // 同步更新 parent update_time
        await _db.BuilderProducts
            .Where(p => p.BuilderProductId == productId)
            .ExecuteUpdateAsync(
                s => s.SetProperty(p => p.UpdateTime, DateTime.Now),
                cancellationToken);

        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            ImagePageName,
            OperationAction.Update,
            $"修改建案圖片 id={imageId} 建案id={productId}",
            cancellationToken: cancellationToken);

        return OperationResult<uint>.Ok(imageId, "修改成功");
    }

    /// <inheritdoc />
    public async Task<OperationResult<uint>> DeleteImageAsync(
        uint productId,
        uint imageId,
        CancellationToken cancellationToken = default)
    {
        var entity = await _db.HproductImgs
            .FirstOrDefaultAsync(
                i => i.Id == imageId && i.BuilderProductId == productId,
                cancellationToken);

        if (entity is null)
            return OperationResult<uint>.NotFound("找不到建案圖片");

        _db.HproductImgs.Remove(entity);

        // 同步更新 parent update_time
        await _db.BuilderProducts
            .Where(p => p.BuilderProductId == productId)
            .ExecuteUpdateAsync(
                s => s.SetProperty(p => p.UpdateTime, DateTime.Now),
                cancellationToken);

        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            ImagePageName,
            OperationAction.Delete,
            $"刪除建案圖片 id={imageId} 建案id={productId}",
            cancellationToken: cancellationToken);

        return OperationResult<uint>.Ok(imageId, "刪除成功");
    }

    /// <inheritdoc />
    public async Task<OperationResult<uint>> SetCoverAsync(
        uint productId,
        SetCoverRequest request,
        CancellationToken cancellationToken = default)
    {
        var product = await _db.BuilderProducts
            .FirstOrDefaultAsync(p => p.BuilderProductId == productId, cancellationToken);

        if (product is null)
            return OperationResult<uint>.NotFound("找不到建案");

        // 1. 更新 builder_product.cover
        product.Cover = request.Cover;
        product.UpdateTime = DateTime.Now;

        // 2. Reset 所有圖片的 is_cover
        await _db.HproductImgs
            .Where(i => i.BuilderProductId == productId)
            .ExecuteUpdateAsync(
                s => s.SetProperty(i => i.IsCover, false),
                cancellationToken);

        // 3. 設定指定圖片為封面
        await _db.HproductImgs
            .Where(i => i.Id == request.ImageId)
            .ExecuteUpdateAsync(
                s => s.SetProperty(i => i.IsCover, true),
                cancellationToken);

        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            ImagePageName,
            OperationAction.Update,
            $"設定封面 建案id={productId} 圖片id={request.ImageId}",
            cancellationToken: cancellationToken);

        return OperationResult<uint>.Ok(productId, "設定封面成功");
    }

    // =========================================================================
    // Helpers
    // =========================================================================

    /// <summary>
    /// 連動 _hcolumn / _hvideo 的 builder_product_id。
    /// 對應 PHP add_hcolumn_and_video_by_builder_product_id。
    /// </summary>
    private async Task LinkColumnsAndVideosAsync(
        uint productId,
        List<uint>? columnIds,
        List<uint>? videoIds,
        CancellationToken cancellationToken)
    {
        if (columnIds is { Count: > 0 })
        {
            await _db.Hcolumns
                .Where(c => columnIds.Contains(c.HcolumnId))
                .ExecuteUpdateAsync(
                    s => s.SetProperty(c => c.BuilderProductId, productId),
                    cancellationToken);
        }

        if (videoIds is { Count: > 0 })
        {
            await _db.Hvideos
                .Where(v => videoIds.Contains(v.HvideoId))
                .ExecuteUpdateAsync(
                    s => s.SetProperty(v => v.BuilderProductId, productId),
                    cancellationToken);

            // 設定 is_video 旗標
            await _db.BuilderProducts
                .Where(p => p.BuilderProductId == productId)
                .ExecuteUpdateAsync(
                    s => s.SetProperty(p => p.IsVideo, "1"),
                    cancellationToken);
        }
    }

    /// <summary>
    /// 排序白名單：未列出的欄位會 fallback 到 BuilderProductId DESC。
    /// </summary>
    private static IOrderedQueryable<BuilderProduct> ApplyOrdering(
        IQueryable<BuilderProduct> query,
        string? sort,
        string? by)
    {
        var isAsc = string.Equals(by, "ASC", StringComparison.OrdinalIgnoreCase);
        var key = sort?.ToLowerInvariant();

        return key switch
        {
            "name" => isAsc ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name),
            "builderid" => isAsc ? query.OrderBy(p => p.BuilderId) : query.OrderByDescending(p => p.BuilderId),
            "city" => isAsc ? query.OrderBy(p => p.City) : query.OrderByDescending(p => p.City),
            "onoff" => isAsc ? query.OrderBy(p => p.Onoff) : query.OrderByDescending(p => p.Onoff),
            "updatetime" => isAsc ? query.OrderBy(p => p.UpdateTime) : query.OrderByDescending(p => p.UpdateTime),
            _ => isAsc ? query.OrderBy(p => p.BuilderProductId) : query.OrderByDescending(p => p.BuilderProductId),
        };
    }
}

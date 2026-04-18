using hhh.api.contracts.admin.Social.Products;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Social.Products;

public class ProductService : IProductService
{
    private readonly XoopsContext _db;

    public ProductService(XoopsContext db) => _db = db;

    public async Task<PagedResponse<ProductListItem>> GetListAsync(ProductListQuery query, CancellationToken ct = default)
    {
        var q = _db.Hproducts.AsNoTracking().AsQueryable();

        // 關鍵字搜尋：產品名稱
        if (!string.IsNullOrWhiteSpace(query.Keyword))
        {
            var like = $"%{query.Keyword.Trim()}%";
            q = q.Where(p => EF.Functions.Like(p.Name, like));
        }

        // 上線狀態篩選
        if (query.Onoff is { } onoff)
        {
            q = q.Where(p => p.Onoff == (sbyte)onoff);
        }

        // 分類1篩選
        if (!string.IsNullOrWhiteSpace(query.Cate1))
        {
            q = q.Where(p => p.Cate1 == query.Cate1);
        }

        // 廠商ID篩選
        if (query.HbrandId is { } hbrandId)
        {
            q = q.Where(p => p.HbrandId == hbrandId);
        }

        return await q
            .OrderByDescending(p => p.Id)
            .Select(p => new ProductListItem
            {
                Id = p.Id,
                HbrandId = p.HbrandId,
                Name = p.Name,
                Cate1 = p.Cate1,
                Cate2 = p.Cate2,
                Cate3 = p.Cate3,
                Space = p.Space,
                Cover = p.Cover,
                Onoff = p.Onoff == 1,
            })
            .ToPagedResponseAsync(query.Page, query.PageSize, ct);
    }

    public async Task<OperationResult> UpdateAsync(
        uint id, UpdateProductRequest request, CancellationToken ct = default)
    {
        var entity = await _db.Hproducts.FirstOrDefaultAsync(p => p.Id == id, ct);
        if (entity is null) return OperationResult.NotFound("找不到產品");

        entity.Onoff = (sbyte)(request.Onoff ? 1 : 0);
        entity.Cate1 = request.Cate1;
        entity.Cate2 = request.Cate2;
        if (request.Cate3 is not null) entity.Cate3 = request.Cate3;
        if (request.Space is not null) entity.Space = request.Space;

        await _db.SaveChangesAsync(ct);
        return OperationResult.Ok("產品修改成功");
    }

    public async Task<PagedResponse<ProductSeoItem>> GetSeoListAsync(ProductSeoListQuery query, CancellationToken ct = default)
    {
        var q = _db.Hproducts.AsNoTracking().AsQueryable();

        // 關鍵字搜尋：產品名稱 / SEO 標題
        if (!string.IsNullOrWhiteSpace(query.Keyword))
        {
            var like = $"%{query.Keyword.Trim()}%";
            q = q.Where(p =>
                EF.Functions.Like(p.Name, like) ||
                (p.SeoTitle != null && EF.Functions.Like(p.SeoTitle, like)));
        }

        // SEO 完成狀態篩選
        if (!string.IsNullOrWhiteSpace(query.SeoStatus))
        {
            if (string.Equals(query.SeoStatus, "complete", StringComparison.OrdinalIgnoreCase))
            {
                q = q.Where(p =>
                    p.SeoTitle != null && p.SeoTitle != "" &&
                    p.SeoImage != null && p.SeoImage != "");
            }
            else if (string.Equals(query.SeoStatus, "incomplete", StringComparison.OrdinalIgnoreCase))
            {
                q = q.Where(p =>
                    p.SeoTitle == null || p.SeoTitle == "" ||
                    p.SeoImage == null || p.SeoImage == "");
            }
        }

        return await q
            .OrderByDescending(p => p.Id)
            .Select(p => new ProductSeoItem
            {
                Id = p.Id,
                Name = p.Name,
                SeoTitle = p.SeoTitle,
                SeoImage = p.SeoImage,
                Onoff = p.Onoff == 1,
                Cover = p.Cover,
                SeoComplete = p.SeoTitle != null && p.SeoTitle != "" &&
                              p.SeoImage != null && p.SeoImage != "",
            })
            .ToPagedResponseAsync(query.Page, query.PageSize, ct);
    }

    public async Task<OperationResult> UpdateSeoImageAsync(
        uint id, UpdateProductSeoImageRequest request, CancellationToken ct = default)
    {
        var entity = await _db.Hproducts.FirstOrDefaultAsync(p => p.Id == id, ct);
        if (entity is null) return OperationResult.NotFound("找不到產品");

        entity.SeoImage = request.SeoImage;
        await _db.SaveChangesAsync(ct);
        return OperationResult.Ok("SEO 圖片更新成功");
    }
}

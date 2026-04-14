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

    public async Task<PagedResponse<ProductListItem>> GetListAsync(ListQuery query, CancellationToken ct = default)
    {
        return await _db.Hproducts
            .AsNoTracking()
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

    public async Task<List<ProductSeoItem>> GetSeoListAsync(CancellationToken ct = default)
    {
        return await _db.Hproducts
            .AsNoTracking()
            .OrderByDescending(p => p.Id)
            .Select(p => new ProductSeoItem
            {
                Id = p.Id,
                Name = p.Name,
                SeoTitle = p.SeoTitle,
                SeoImage = p.SeoImage,
            })
            .ToListAsync(ct);
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

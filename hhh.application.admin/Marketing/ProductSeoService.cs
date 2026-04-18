using hhh.api.contracts.admin.Marketing;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Marketing;

public class ProductSeoService : IProductSeoService
{
    private readonly XoopsContext _db;

    public ProductSeoService(XoopsContext db) => _db = db;

    /// <inheritdoc />
    public async Task<PagedResponse<ProductSeoListItem>> GetListAsync(
        ProductSeoListQuery query,
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: SELECT id, name, seo_title, seo_image
        //           FROM _hproduct ORDER BY id DESC
        var q = _db.Hproducts.AsNoTracking().AsQueryable();

        // 關鍵字篩選（Name / SeoTitle）
        if (!string.IsNullOrWhiteSpace(query.Keyword))
        {
            var kw = $"%{query.Keyword}%";
            q = q.Where(p =>
                EF.Functions.Like(p.Name, kw) ||
                (p.SeoTitle != null && EF.Functions.Like(p.SeoTitle, kw)));
        }

        // 上線狀態篩選（Hproduct.Onoff 為 sbyte）
        if (query.Onoff.HasValue)
            q = q.Where(p => p.Onoff == (sbyte)query.Onoff.Value);

        // SEO 完成度篩選（SeoTitle + SeoImage 皆非空 = complete）
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

        return await q
            .OrderByDescending(p => p.Id)
            .Select(p => new ProductSeoListItem
            {
                Id = p.Id,
                Name = p.Name,
                SeoTitle = p.SeoTitle,
                SeoImage = p.SeoImage,
                Onoff = p.Onoff != 0,
                Cover = p.Cover ?? string.Empty,
                SeoComplete = p.SeoTitle != null && p.SeoTitle != "" &&
                              p.SeoImage != null && p.SeoImage != "",
                UpdatedAt = p.UpdatedAt,
            })
            .ToPagedResponseAsync(query.Page, query.PageSize, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<OperationResult> BatchUpdateAsync(
        List<UpdateProductSeoItem> items,
        CancellationToken cancellationToken = default)
    {
        if (items is not { Count: > 0 })
            return OperationResult.BadRequest("請提供至少一筆資料");

        // 對應 PHP: foreach → UPDATE _hproduct SET seo_title WHERE id
        foreach (var item in items)
        {
            await _db.Hproducts
                .Where(p => p.Id == item.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(p => p.SeoTitle, item.SeoTitle),
                    cancellationToken);
        }

        return OperationResult.Ok("產品 SEO 修改成功");
    }

    /// <inheritdoc />
    public async Task<OperationResult> BatchUpdateImageAsync(
        List<UpdateProductSeoImageItem> items,
        CancellationToken cancellationToken = default)
    {
        if (items is not { Count: > 0 })
            return OperationResult.BadRequest("請提供至少一筆資料");

        // 對應 PHP: foreach → UPDATE _hproduct SET seo_image WHERE id
        foreach (var item in items)
        {
            await _db.Hproducts
                .Where(p => p.Id == item.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(p => p.SeoImage, item.SeoImage),
                    cancellationToken);
        }

        return OperationResult.Ok("產品 SEO 圖片更新成功");
    }
}

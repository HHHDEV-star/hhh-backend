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
        ListQuery query,
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: SELECT id, name, seo_title, seo_image
        //           FROM _hproduct ORDER BY id DESC
        return await _db.Hproducts
            .AsNoTracking()
            .OrderByDescending(p => p.Id)
            .Select(p => new ProductSeoListItem
            {
                Id = p.Id,
                Name = p.Name,
                SeoTitle = p.SeoTitle,
                SeoImage = p.SeoImage,
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

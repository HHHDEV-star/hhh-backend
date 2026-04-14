using hhh.api.contracts.admin.Marketing;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Marketing;

public class CaseSeoService : ICaseSeoService
{
    private readonly XoopsContext _db;

    public CaseSeoService(XoopsContext db) => _db = db;

    /// <inheritdoc />
    public async Task<List<CaseSeoListItem>> GetListAsync(
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: SELECT hcase_id, caption, seo_title, seo_image, seo_description
        //           FROM _hcase ORDER BY sdate DESC, hcase_id DESC
        return await _db.Hcases
            .AsNoTracking()
            .OrderByDescending(c => c.Sdate)
            .ThenByDescending(c => c.HcaseId)
            .Select(c => new CaseSeoListItem
            {
                HcaseId = c.HcaseId,
                Caption = c.Caption,
                SeoTitle = c.SeoTitle,
                SeoImage = c.SeoImage,
                SeoDescription = c.SeoDescription,
            })
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<OperationResult> BatchUpdateAsync(
        List<UpdateCaseSeoItem> items,
        CancellationToken cancellationToken = default)
    {
        if (items is not { Count: > 0 })
            return OperationResult.BadRequest("請提供至少一筆資料");

        // 對應 PHP: foreach → UPDATE _hcase SET seo_title, seo_description WHERE hcase_id
        foreach (var item in items)
        {
            await _db.Hcases
                .Where(c => c.HcaseId == item.HcaseId)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(c => c.SeoTitle, item.SeoTitle)
                    .SetProperty(c => c.SeoDescription, item.SeoDescription),
                    cancellationToken);
        }

        return OperationResult.Ok("個案 SEO 修改成功");
    }

    /// <inheritdoc />
    public async Task<OperationResult> BatchUpdateImageAsync(
        List<UpdateCaseSeoImageItem> items,
        CancellationToken cancellationToken = default)
    {
        if (items is not { Count: > 0 })
            return OperationResult.BadRequest("請提供至少一筆資料");

        // 對應 PHP: foreach → UPDATE _hcase SET seo_image WHERE hcase_id
        foreach (var item in items)
        {
            await _db.Hcases
                .Where(c => c.HcaseId == item.HcaseId)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(c => c.SeoImage, item.SeoImage),
                    cancellationToken);
        }

        return OperationResult.Ok("個案 SEO 圖片更新成功");
    }
}

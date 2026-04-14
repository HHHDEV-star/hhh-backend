using hhh.api.contracts.admin.Marketing;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Marketing;

public class ColumnSeoService : IColumnSeoService
{
    private readonly XoopsContext _db;

    public ColumnSeoService(XoopsContext db) => _db = db;

    /// <inheritdoc />
    public async Task<List<ColumnSeoListItem>> GetListAsync(
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: SELECT hcolumn_id, ctitle, seo_title, seo_image, seo_description
        //           FROM _hcolumn ORDER BY sdate DESC, hcolumn_id DESC
        return await _db.Hcolumns
            .AsNoTracking()
            .OrderByDescending(c => c.Sdate)
            .ThenByDescending(c => c.HcolumnId)
            .Select(c => new ColumnSeoListItem
            {
                HcolumnId = c.HcolumnId,
                Ctitle = c.Ctitle,
                SeoTitle = c.SeoTitle,
                SeoImage = c.SeoImage,
                SeoDescription = c.SeoDescription,
            })
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<OperationResult> BatchUpdateAsync(
        List<UpdateColumnSeoItem> items,
        CancellationToken cancellationToken = default)
    {
        if (items is not { Count: > 0 })
            return OperationResult.BadRequest("請提供至少一筆資料");

        // 對應 PHP: foreach → UPDATE _hcolumn SET seo_title, seo_description WHERE hcolumn_id
        foreach (var item in items)
        {
            await _db.Hcolumns
                .Where(c => c.HcolumnId == item.HcolumnId)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(c => c.SeoTitle, item.SeoTitle)
                    .SetProperty(c => c.SeoDescription, item.SeoDescription),
                    cancellationToken);
        }

        return OperationResult.Ok("專欄 SEO 修改成功");
    }

    /// <inheritdoc />
    public async Task<OperationResult> BatchUpdateImageAsync(
        List<UpdateColumnSeoImageItem> items,
        CancellationToken cancellationToken = default)
    {
        if (items is not { Count: > 0 })
            return OperationResult.BadRequest("請提供至少一筆資料");

        // 對應 PHP: foreach → UPDATE _hcolumn SET seo_image WHERE hcolumn_id
        foreach (var item in items)
        {
            await _db.Hcolumns
                .Where(c => c.HcolumnId == item.HcolumnId)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(c => c.SeoImage, item.SeoImage),
                    cancellationToken);
        }

        return OperationResult.Ok("專欄 SEO 圖片更新成功");
    }
}

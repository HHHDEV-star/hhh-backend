using hhh.api.contracts.admin.Marketing;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Marketing;

public class ColumnSeoService : IColumnSeoService
{
    private readonly XoopsContext _db;

    public ColumnSeoService(XoopsContext db) => _db = db;

    /// <inheritdoc />
    public async Task<PagedResponse<ColumnSeoListItem>> GetListAsync(
        ColumnSeoListQuery query,
        CancellationToken cancellationToken = default)
    {
        var q = _db.Hcolumns.AsNoTracking().AsQueryable();

        // 關鍵字搜尋：專欄標題 / SEO 標題
        if (!string.IsNullOrWhiteSpace(query.Keyword))
        {
            var like = $"%{query.Keyword.Trim()}%";
            q = q.Where(c =>
                EF.Functions.Like(c.Ctitle, like) ||
                EF.Functions.Like(c.SeoTitle ?? "", like));
        }

        // 專欄類別篩選
        if (!string.IsNullOrWhiteSpace(query.Ctype))
        {
            q = q.Where(c => c.Ctype == query.Ctype.Trim());
        }

        // 上線狀態篩選
        if (query.Onoff is { } onoff)
        {
            q = q.Where(c => c.Onoff == onoff);
        }

        // SEO 完成度篩選
        if (string.Equals(query.SeoStatus, "complete", StringComparison.OrdinalIgnoreCase))
        {
            q = q.Where(c =>
                c.SeoTitle != null && c.SeoTitle != "" &&
                c.SeoImage != null && c.SeoImage != "" &&
                c.SeoDescription != null && c.SeoDescription != "");
        }
        else if (string.Equals(query.SeoStatus, "incomplete", StringComparison.OrdinalIgnoreCase))
        {
            q = q.Where(c =>
                c.SeoTitle == null || c.SeoTitle == "" ||
                c.SeoImage == null || c.SeoImage == "" ||
                c.SeoDescription == null || c.SeoDescription == "");
        }

        // 上架日期區間篩選
        if (query.DateFrom is { } dateFrom)
        {
            q = q.Where(c => c.Sdate >= dateFrom);
        }
        if (query.DateTo is { } dateTo)
        {
            q = q.Where(c => c.Sdate <= dateTo);
        }

        return await q
            .OrderByDescending(c => c.Sdate)
            .ThenByDescending(c => c.HcolumnId)
            .Select(c => new ColumnSeoListItem
            {
                HcolumnId = c.HcolumnId,
                Ctitle = c.Ctitle,
                Ctype = c.Ctype,
                Clogo = c.Clogo,
                Onoff = c.Onoff == 1,
                Viewed = c.Viewed,
                Sdate = c.Sdate,
                SeoTitle = c.SeoTitle,
                SeoImage = c.SeoImage,
                SeoDescription = c.SeoDescription,
                SeoComplete = c.SeoTitle != null && c.SeoTitle != ""
                           && c.SeoImage != null && c.SeoImage != ""
                           && c.SeoDescription != null && c.SeoDescription != "",
                UpdateTime = c.UpdateTime,
            })
            .ToPagedResponseAsync(query.Page, query.PageSize, cancellationToken);
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

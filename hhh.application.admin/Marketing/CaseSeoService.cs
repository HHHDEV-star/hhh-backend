using hhh.api.contracts.admin.Marketing;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Marketing;

public class CaseSeoService : ICaseSeoService
{
    private readonly XoopsContext _db;

    public CaseSeoService(XoopsContext db) => _db = db;

    /// <inheritdoc />
    public async Task<PagedResponse<CaseSeoListItem>> GetListAsync(
        CaseSeoListQuery query,
        CancellationToken cancellationToken = default)
    {
        var q = from c in _db.Hcases.AsNoTracking()
                join d in _db.Hdesigners.AsNoTracking() on c.HdesignerId equals d.HdesignerId into dj
                from d in dj.DefaultIfEmpty()
                select new { c, d };

        // 關鍵字搜尋：個案名稱 / SEO 標題
        if (!string.IsNullOrWhiteSpace(query.Keyword))
        {
            var like = $"%{query.Keyword.Trim()}%";
            q = q.Where(x =>
                EF.Functions.Like(x.c.Caption, like) ||
                EF.Functions.Like(x.c.SeoTitle ?? "", like));
        }

        // 設計師篩選
        if (query.HdesignerId is { } hdesignerId)
        {
            q = q.Where(x => x.c.HdesignerId == hdesignerId);
        }

        // 設計風格篩選（style 為 CSV，用 LIKE 模糊比對）
        if (!string.IsNullOrWhiteSpace(query.Style))
        {
            var like = $"%{query.Style.Trim()}%";
            q = q.Where(x => EF.Functions.Like(x.c.Style, like));
        }

        // 上線狀態篩選
        if (query.Onoff is { } onoff)
        {
            q = q.Where(x => x.c.Onoff == onoff);
        }

        // SEO 完成度篩選
        if (string.Equals(query.SeoStatus, "complete", StringComparison.OrdinalIgnoreCase))
        {
            q = q.Where(x =>
                x.c.SeoTitle != null && x.c.SeoTitle != "" &&
                x.c.SeoImage != null && x.c.SeoImage != "" &&
                x.c.SeoDescription != null && x.c.SeoDescription != "");
        }
        else if (string.Equals(query.SeoStatus, "incomplete", StringComparison.OrdinalIgnoreCase))
        {
            q = q.Where(x =>
                x.c.SeoTitle == null || x.c.SeoTitle == "" ||
                x.c.SeoImage == null || x.c.SeoImage == "" ||
                x.c.SeoDescription == null || x.c.SeoDescription == "");
        }

        // 上架日期區間篩選
        if (query.DateFrom is { } dateFrom)
        {
            q = q.Where(x => x.c.Sdate >= dateFrom);
        }
        if (query.DateTo is { } dateTo)
        {
            q = q.Where(x => x.c.Sdate <= dateTo);
        }

        return await q
            .OrderByDescending(x => x.c.Sdate)
            .ThenByDescending(x => x.c.HcaseId)
            .Select(x => new CaseSeoListItem
            {
                HcaseId = x.c.HcaseId,
                Caption = x.c.Caption,
                HdesignerId = x.c.HdesignerId,
                DesignerName = x.d != null ? x.d.Title : string.Empty,
                Cover = x.c.Cover,
                Style = x.c.Style,
                Type = x.c.Type,
                Onoff = x.c.Onoff == 1,
                Viewed = x.c.Viewed,
                Sdate = x.c.Sdate,
                SeoTitle = x.c.SeoTitle,
                SeoImage = x.c.SeoImage,
                SeoDescription = x.c.SeoDescription,
                SeoComplete = x.c.SeoTitle != null && x.c.SeoTitle != ""
                           && x.c.SeoImage != null && x.c.SeoImage != ""
                           && x.c.SeoDescription != null && x.c.SeoDescription != "",
                UpdateTime = x.c.UpdateTime,
            })
            .ToPagedResponseAsync(query.Page, query.PageSize, cancellationToken);
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

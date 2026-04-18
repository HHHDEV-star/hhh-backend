using hhh.api.contracts.admin.Tags;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Tags;

public class TagService : ITagService
{
    private readonly XoopsContext _db;

    public TagService(XoopsContext db) => _db = db;

    // =========================================================================
    // Hcase
    // =========================================================================

    public async Task<PagedResponse<TagHcaseItem>> GetHcaseTagsAsync(
        TagHcaseListQuery query, CancellationToken ct = default)
    {
        var q = from c in _db.Hcases.AsNoTracking()
                join d in _db.Hdesigners.AsNoTracking() on c.HdesignerId equals d.HdesignerId into dj
                from d in dj.DefaultIfEmpty()
                select new { c, d };

        if (query.HdesignerId is { } did && did > 0)
            q = q.Where(x => x.c.HdesignerId == did);

        if (!string.IsNullOrWhiteSpace(query.SearchTag))
        {
            var like = $"%{query.SearchTag.Trim()}%";
            q = q.Where(x => x.c.Tag != null && EF.Functions.Like(x.c.Tag, like));
        }

        // 舊 PHP:禁止無條件全撈
        if (query.HdesignerId is null or 0 && string.IsNullOrWhiteSpace(query.SearchTag))
            return new PagedResponse<TagHcaseItem> { Page = query.Page, PageSize = query.PageSize };

        return await q.OrderByDescending(x => x.c.HcaseId)
            .Select(x => new TagHcaseItem
            {
                HcaseId = x.c.HcaseId,
                HdesignerId = x.c.HdesignerId,
                Tag = x.c.Tag,
                TagDatetime = x.c.TagDatetime,
                Caption = x.c.Caption,
                Style = x.c.Style,
                Style2 = x.c.Style2,
                CreatTime = x.c.CreatTime,
                DesignerTitle = x.d != null ? x.d.Title : string.Empty,
                DesignerName = x.d != null ? x.d.Name : string.Empty,
            }).ToPagedResponseAsync(query.Page, query.PageSize, ct);
    }

    public async Task<OperationResult> UpdateHcaseTagAsync(
        uint hcaseId, UpdateTagRequest request, CancellationToken ct = default)
    {
        var entity = await _db.Hcases.FirstOrDefaultAsync(c => c.HcaseId == hcaseId, ct);
        if (entity is null) return OperationResult.NotFound("找不到個案");

        entity.Tag = request.Tag;
        entity.TagDatetime = DateTime.UtcNow;
        await _db.SaveChangesAsync(ct);

        return OperationResult.Ok("標籤更新成功");
    }

    // =========================================================================
    // Hcolumn
    // =========================================================================

    public async Task<PagedResponse<TagHcolumnItem>> GetHcolumnTagsAsync(
        TagHcolumnListQuery query, CancellationToken ct = default)
    {
        // 舊 PHP:至少要有一個條件才查
        if (string.IsNullOrWhiteSpace(query.Ctype) && string.IsNullOrWhiteSpace(query.Ctitle)
            && query.StartDate is null && query.EndDate is null && string.IsNullOrWhiteSpace(query.SearchTag))
            return new PagedResponse<TagHcolumnItem> { Page = query.Page, PageSize = query.PageSize };

        var q = _db.Hcolumns.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Ctype))
            q = q.Where(c => c.Ctype == query.Ctype.Trim());

        if (!string.IsNullOrWhiteSpace(query.Ctitle))
        {
            var like = $"%{query.Ctitle.Trim()}%";
            q = q.Where(c => EF.Functions.Like(c.Ctitle, like));
        }

        if (query.StartDate is { } sd)
            q = q.Where(c => DateOnly.FromDateTime(c.CreatTime) >= sd);

        if (query.EndDate is { } ed)
            q = q.Where(c => DateOnly.FromDateTime(c.CreatTime) <= ed);

        if (!string.IsNullOrWhiteSpace(query.SearchTag))
        {
            var like = $"%{query.SearchTag.Trim()}%";
            q = q.Where(c => c.Tag != null && EF.Functions.Like(c.Tag, like));
        }

        return await q.OrderByDescending(c => c.HcolumnId)
            .Select(c => new TagHcolumnItem
            {
                HcolumnId = c.HcolumnId,
                Tag = c.Tag,
                TagDatetime = c.TagDatetime,
                Onoff = c.Onoff == 1,
                Ctype = c.Ctype,
                Ctitle = c.Ctitle,
                Clogo = c.Clogo,
                CreatTime = c.CreatTime,
            }).ToPagedResponseAsync(query.Page, query.PageSize, ct);
    }

    public async Task<OperationResult> UpdateHcolumnTagAsync(
        uint hcolumnId, UpdateTagRequest request, CancellationToken ct = default)
    {
        var entity = await _db.Hcolumns.FirstOrDefaultAsync(c => c.HcolumnId == hcolumnId, ct);
        if (entity is null) return OperationResult.NotFound("找不到專欄");

        entity.Tag = request.Tag;
        entity.TagDatetime = DateTime.UtcNow;
        await _db.SaveChangesAsync(ct);

        return OperationResult.Ok("標籤更新成功");
    }

    // =========================================================================
    // Hvideo
    // =========================================================================

    public async Task<PagedResponse<TagHvideoItem>> GetHvideoTagsAsync(
        TagHvideoListQuery query, CancellationToken ct = default)
    {
        if (query.HdesignerId is null or 0 && string.IsNullOrWhiteSpace(query.Title)
            && query.StartDate is null && query.EndDate is null && string.IsNullOrWhiteSpace(query.SearchTag))
            return new PagedResponse<TagHvideoItem> { Page = query.Page, PageSize = query.PageSize };

        var q = _db.Hvideos.AsNoTracking().AsQueryable();

        if (query.HdesignerId is { } did && did > 0)
            q = q.Where(v => v.HdesignerId == did);

        if (!string.IsNullOrWhiteSpace(query.Title))
        {
            var like = $"%{query.Title.Trim()}%";
            q = q.Where(v => EF.Functions.Like(v.Name, like));
        }

        if (query.StartDate is { } sd)
            q = q.Where(v => DateOnly.FromDateTime(v.CreatTime) >= sd);

        if (query.EndDate is { } ed)
            q = q.Where(v => DateOnly.FromDateTime(v.CreatTime) <= ed);

        if (!string.IsNullOrWhiteSpace(query.SearchTag))
        {
            var like = $"%{query.SearchTag.Trim()}%";
            q = q.Where(v => v.Tag != null && EF.Functions.Like(v.Tag, like));
        }

        return await q.OrderByDescending(v => v.HvideoId)
            .Select(v => new TagHvideoItem
            {
                HvideoId = v.HvideoId,
                HdesignerId = v.HdesignerId,
                HcaseId = v.HcaseId,
                HbrandId = v.HbrandId,
                HcolumnId = v.HcolumnId,
                TagVtype = v.TagVtype,
                TagVpattern = v.TagVpattern,
                Tag = v.Tag,
                TagDatetime = v.TagDatetime,
                Title = v.Name,
                CreatTime = v.CreatTime,
            }).ToPagedResponseAsync(query.Page, query.PageSize, ct);
    }

    public async Task<OperationResult> UpdateHvideoTagAsync(
        uint hvideoId, UpdateTagRequest request, CancellationToken ct = default)
    {
        var entity = await _db.Hvideos.FirstOrDefaultAsync(v => v.HvideoId == hvideoId, ct);
        if (entity is null) return OperationResult.NotFound("找不到影音");

        entity.Tag = request.Tag;
        entity.TagDatetime = DateTime.UtcNow;
        await _db.SaveChangesAsync(ct);

        return OperationResult.Ok("標籤更新成功");
    }

    // =========================================================================
    // Image (_hcase_img)
    // =========================================================================

    public async Task<PagedResponse<TagImageItem>> GetImageTagsAsync(
        TagImageListQuery query, CancellationToken ct = default)
    {
        if (query.HcaseId is null or 0 && string.IsNullOrWhiteSpace(query.SearchTag))
            return new PagedResponse<TagImageItem> { Page = query.Page, PageSize = query.PageSize };

        var q = from img in _db.HcaseImgs.AsNoTracking()
                join c in _db.Hcases.AsNoTracking() on img.HcaseId equals c.HcaseId into cj
                from c in cj.DefaultIfEmpty()
                select new { img, CaseCaption = c != null ? c.Caption : string.Empty };

        if (query.HcaseId is { } cid && cid > 0)
            q = q.Where(x => x.img.HcaseId == cid);

        if (!string.IsNullOrWhiteSpace(query.SearchTag))
        {
            var like = $"%{query.SearchTag.Trim()}%";
            q = q.Where(x =>
                (x.img.Tag1 != null && EF.Functions.Like(x.img.Tag1, like)) ||
                (x.img.Tag2 != null && EF.Functions.Like(x.img.Tag2, like)) ||
                (x.img.Tag3 != null && EF.Functions.Like(x.img.Tag3, like)) ||
                (x.img.Tag4 != null && EF.Functions.Like(x.img.Tag4, like)) ||
                (x.img.Tag5 != null && EF.Functions.Like(x.img.Tag5, like)));
        }

        return await q.OrderByDescending(x => x.img.HcaseImgId)
            .Select(x => new TagImageItem
            {
                HcaseImgId = x.img.HcaseImgId,
                HcaseId = x.img.HcaseId,
                Tag1 = x.img.Tag1,
                Tag2 = x.img.Tag2,
                Tag3 = x.img.Tag3,
                Tag4 = x.img.Tag4,
                Tag5 = x.img.Tag5,
                Title = x.img.Title,
                TagMan = x.img.TagMan,
                TagDatetime = x.img.TagDatetime,
                CaseCaption = x.CaseCaption,
            }).ToPagedResponseAsync(query.Page, query.PageSize, ct);
    }

    public async Task<OperationResult> UpdateImageTagAsync(
        uint hcaseImgId, UpdateImageTagRequest request, string? operatorEmail,
        CancellationToken ct = default)
    {
        var entity = await _db.HcaseImgs.FirstOrDefaultAsync(i => i.HcaseImgId == hcaseImgId, ct);
        if (entity is null) return OperationResult.NotFound("找不到圖片");

        entity.Tag1 = request.Tag1;
        entity.Tag2 = request.Tag2;
        entity.Tag3 = request.Tag3;
        entity.Tag4 = request.Tag4;
        entity.Tag5 = request.Tag5;
        if (request.Title is not null) entity.Title = request.Title;
        entity.TagMan = operatorEmail ?? string.Empty;
        entity.TagDatetime = DateTime.UtcNow;

        await _db.SaveChangesAsync(ct);

        return OperationResult.Ok("圖庫標籤更新成功");
    }
}

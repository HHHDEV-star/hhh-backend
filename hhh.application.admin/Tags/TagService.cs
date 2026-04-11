using hhh.api.contracts.admin.Tags;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Tags;

public class TagService : ITagService
{
    private readonly XoopsContext _db;

    public TagService(XoopsContext db) => _db = db;

    // =========================================================================
    // Hcase
    // =========================================================================

    public async Task<List<TagHcaseItem>> GetHcaseTagsAsync(
        uint? hdesignerId, string? searchTag, CancellationToken ct = default)
    {
        var q = from c in _db.Hcases.AsNoTracking()
                join d in _db.Hdesigners.AsNoTracking() on c.HdesignerId equals d.HdesignerId into dj
                from d in dj.DefaultIfEmpty()
                select new { c, d };

        if (hdesignerId is { } did && did > 0)
            q = q.Where(x => x.c.HdesignerId == did);

        if (!string.IsNullOrWhiteSpace(searchTag))
        {
            var like = $"%{searchTag.Trim()}%";
            q = q.Where(x => x.c.Tag != null && EF.Functions.Like(x.c.Tag, like));
        }

        // 舊 PHP:禁止無條件全撈
        if (hdesignerId is null or 0 && string.IsNullOrWhiteSpace(searchTag))
            return new List<TagHcaseItem>();

        return await q.Select(x => new TagHcaseItem
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
        }).ToListAsync(ct);
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

    public async Task<List<TagHcolumnItem>> GetHcolumnTagsAsync(
        string? ctype, string? ctitle, DateOnly? startDate, DateOnly? endDate,
        string? searchTag, CancellationToken ct = default)
    {
        // 舊 PHP:至少要有一個條件才查
        if (string.IsNullOrWhiteSpace(ctype) && string.IsNullOrWhiteSpace(ctitle)
            && startDate is null && endDate is null && string.IsNullOrWhiteSpace(searchTag))
            return new List<TagHcolumnItem>();

        var q = _db.Hcolumns.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(ctype))
            q = q.Where(c => c.Ctype == ctype.Trim());

        if (!string.IsNullOrWhiteSpace(ctitle))
        {
            var like = $"%{ctitle.Trim()}%";
            q = q.Where(c => EF.Functions.Like(c.Ctitle, like));
        }

        if (startDate is { } sd)
            q = q.Where(c => DateOnly.FromDateTime(c.CreatTime) >= sd);

        if (endDate is { } ed)
            q = q.Where(c => DateOnly.FromDateTime(c.CreatTime) <= ed);

        if (!string.IsNullOrWhiteSpace(searchTag))
        {
            var like = $"%{searchTag.Trim()}%";
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
            }).ToListAsync(ct);
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

    public async Task<List<TagHvideoItem>> GetHvideoTagsAsync(
        uint? hdesignerId, string? title, DateOnly? startDate, DateOnly? endDate,
        string? searchTag, CancellationToken ct = default)
    {
        if (hdesignerId is null or 0 && string.IsNullOrWhiteSpace(title)
            && startDate is null && endDate is null && string.IsNullOrWhiteSpace(searchTag))
            return new List<TagHvideoItem>();

        var q = _db.Hvideos.AsNoTracking().AsQueryable();

        if (hdesignerId is { } did && did > 0)
            q = q.Where(v => v.HdesignerId == did);

        if (!string.IsNullOrWhiteSpace(title))
        {
            var like = $"%{title.Trim()}%";
            q = q.Where(v => EF.Functions.Like(v.Name, like));
        }

        if (startDate is { } sd)
            q = q.Where(v => DateOnly.FromDateTime(v.CreatTime) >= sd);

        if (endDate is { } ed)
            q = q.Where(v => DateOnly.FromDateTime(v.CreatTime) <= ed);

        if (!string.IsNullOrWhiteSpace(searchTag))
        {
            var like = $"%{searchTag.Trim()}%";
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
            }).ToListAsync(ct);
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

    public async Task<List<TagImageItem>> GetImageTagsAsync(
        uint? hcaseId, string? searchTag, CancellationToken ct = default)
    {
        if (hcaseId is null or 0 && string.IsNullOrWhiteSpace(searchTag))
            return new List<TagImageItem>();

        var q = from img in _db.HcaseImgs.AsNoTracking()
                join c in _db.Hcases.AsNoTracking() on img.HcaseId equals c.HcaseId into cj
                from c in cj.DefaultIfEmpty()
                select new { img, CaseCaption = c != null ? c.Caption : string.Empty };

        if (hcaseId is { } cid && cid > 0)
            q = q.Where(x => x.img.HcaseId == cid);

        if (!string.IsNullOrWhiteSpace(searchTag))
        {
            var like = $"%{searchTag.Trim()}%";
            q = q.Where(x =>
                (x.img.Tag1 != null && EF.Functions.Like(x.img.Tag1, like)) ||
                (x.img.Tag2 != null && EF.Functions.Like(x.img.Tag2, like)) ||
                (x.img.Tag3 != null && EF.Functions.Like(x.img.Tag3, like)) ||
                (x.img.Tag4 != null && EF.Functions.Like(x.img.Tag4, like)) ||
                (x.img.Tag5 != null && EF.Functions.Like(x.img.Tag5, like)));
        }

        return await q.Select(x => new TagImageItem
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
        }).ToListAsync(ct);
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

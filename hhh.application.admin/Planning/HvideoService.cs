using hhh.api.contracts.admin.Planning;
using hhh.api.contracts.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Planning;

public class HvideoService : IHvideoService
{
    private readonly XoopsContext _db;

    public HvideoService(XoopsContext db)
    {
        _db = db;
    }

    /// <inheritdoc />
    public async Task<PagedResponse<HvideoListItem>> GetListAsync(
        HvideoListQuery query,
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: _hvideo.php
        // WHERE 1=1
        //   AND ( hvideo_id LIKE '%q%' OR title LIKE '%q%' OR `desc` LIKE '%q%'
        //      OR tag_vtype LIKE '%q%' OR tag_vpattern LIKE '%q%'
        //      OR hdesigner_id IN (SELECT hdesigner_id FROM _hdesigner WHERE name LIKE ... OR title LIKE ...)
        //      OR hbrand_id    IN (SELECT hbrand_id    FROM _hbrand    WHERE title LIKE ...)
        //      OR hcolumn_id   IN (SELECT hcolumn_id   FROM _hcolumn   WHERE ctitle LIKE ...) )
        // ORDER BY hvideo_id DESC
        var q = _db.Hvideos.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Keyword))
        {
            var keyword = query.Keyword.Trim();
            var like = $"%{keyword}%";

            // 若 keyword 可解析為 uint,同時支援 ID 精準比對
            // (PHP 原本是 hvideo_id LIKE '%q%',這裡用等值比對更準確、避免欄位型別轉換警告)
            var idMatch = uint.TryParse(keyword, out var kwId) ? kwId : (uint?)null;

            var designerIds = _db.Hdesigners
                .Where(d => EF.Functions.Like(d.Name, like) || EF.Functions.Like(d.Title, like))
                .Select(d => d.HdesignerId);

            var brandIds = _db.Hbrands
                .Where(b => EF.Functions.Like(b.Title, like))
                .Select(b => b.HbrandId);

            var columnIds = _db.Hcolumns
                .Where(c => EF.Functions.Like(c.Ctitle, like))
                .Select(c => c.HcolumnId);

            q = q.Where(v =>
                (idMatch.HasValue && v.HvideoId == idMatch.Value) ||
                EF.Functions.Like(v.Title, like) ||
                EF.Functions.Like(v.Desc, like) ||
                EF.Functions.Like(v.TagVtype, like) ||
                EF.Functions.Like(v.TagVpattern, like) ||
                designerIds.Contains(v.HdesignerId) ||
                brandIds.Contains(v.HbrandId) ||
                columnIds.Contains(v.HcolumnId));
        }

        // 後端列表固定 hvideo_id DESC(PHP 預設 sort=1 by=DESC,等同 ORDER BY 第一欄)
        return await q
            .OrderByDescending(v => v.HvideoId)
            .Select(v => new HvideoListItem
            {
                HvideoId = v.HvideoId,
                VfileType = v.VfileType,
                Title = v.Title,
                Desc = v.Desc,
                HdesignerId = v.HdesignerId,
                DesignerTitle = _db.Hdesigners
                    .Where(d => d.HdesignerId == v.HdesignerId)
                    .Select(d => d.Title)
                    .FirstOrDefault() ?? string.Empty,
                DesignerName = _db.Hdesigners
                    .Where(d => d.HdesignerId == v.HdesignerId)
                    .Select(d => d.Name)
                    .FirstOrDefault() ?? string.Empty,
                HcaseId = v.HcaseId,
                HcaseCaption = _db.Hcases
                    .Where(c => c.HcaseId == v.HcaseId)
                    .Select(c => c.Caption)
                    .FirstOrDefault() ?? string.Empty,
                TagVtype = v.TagVtype,
                TagVpattern = v.TagVpattern,
                HbrandId = v.HbrandId,
                BrandTitle = _db.Hbrands
                    .Where(b => b.HbrandId == v.HbrandId)
                    .Select(b => b.Title)
                    .FirstOrDefault() ?? string.Empty,
                HcolumnId = v.HcolumnId,
                ColumnCtitle = _db.Hcolumns
                    .Where(c => c.HcolumnId == v.HcolumnId)
                    .Select(c => c.Ctitle)
                    .FirstOrDefault() ?? string.Empty,
                Iframe = v.Iframe,
                ForChina = v.ForChina,
                DisplayDatetime = v.DisplayDatetime,
            })
            .ToPagedResponseAsync(query.Page, query.PageSize, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<List<HvideoSelectItem>> GetSelectListAsync(
        string? keyword = null,
        CancellationToken cancellationToken = default)
    {
        var q = _db.Hvideos.AsNoTracking()
            .Where(v => v.Onoff == 1);

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var like = $"%{keyword.Trim()}%";
            q = q.Where(v => EF.Functions.Like(v.Title, like) || EF.Functions.Like(v.Name, like));
        }

        return await (
            from v in q
            join d in _db.Hdesigners.AsNoTracking() on v.HdesignerId equals d.HdesignerId into dj
            from d in dj.DefaultIfEmpty()
            orderby v.HvideoId descending
            select new HvideoSelectItem
            {
                HvideoId = v.HvideoId,
                Title = v.Title,
                Name = v.Name,
                HdesignerId = v.HdesignerId,
                DesignerTitle = d != null ? d.Title : string.Empty,
                Onoff = true,
            })
            .ToListAsync(cancellationToken);
    }
}

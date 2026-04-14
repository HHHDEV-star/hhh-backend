using hhh.api.contracts.admin.Website.HomepageInnerSets;
<<<<<<< HEAD
using hhh.api.contracts.Common;
=======
>>>>>>> origin/main
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Website.HomepageInnerSets;

public class HomepageInnerSetService : IHomepageInnerSetService
{
    // 合法的 theme_type 值
    private static readonly HashSet<string> ValidThemeTypes = new(StringComparer.OrdinalIgnoreCase)
    {
        "case", "video", "column", "product", "ad", "designer", "brand", "fans", "week"
    };

    private readonly XoopsContext _db;

    public HomepageInnerSetService(XoopsContext db)
    {
        _db = db;
    }

    /// <inheritdoc />
<<<<<<< HEAD
    public async Task<PagedResponse<HomepageInnerSetListItem>> GetListAsync(
        ListQuery query, CancellationToken cancellationToken = default)
=======
    public async Task<List<HomepageInnerSetListItem>> GetListAsync(
        CancellationToken cancellationToken = default)
>>>>>>> origin/main
    {
        // 對應 PHP: homepage_model::get_innerset()
        // 1. 計算每個 outer_set 中 onoff='Y' 的筆數
        var countByOuterSet = await _db.HomepageSets
            .AsNoTracking()
            .Where(h => h.Onoff == "Y")
            .GroupBy(h => h.OuterSet)
            .Select(g => new { OuterSet = g.Key, Count = g.Count() })
            .ToDictionaryAsync(x => x.OuterSet, x => x.Count, cancellationToken);

        // 2. 主查詢: homepage_set JOIN outer_site_set
        var rows = await _db.HomepageSets
            .AsNoTracking()
            .Join(
                _db.OuterSiteSets.AsNoTracking(),
                hs => hs.OuterSet,
                oss => oss.OssId,
                (hs, oss) => new
                {
                    hs.PsId,
                    hs.MappingId,
                    hs.InnerSort,
                    hs.ThemeType,
                    hs.OuterSet,
                    hs.Onoff,
                    hs.StartTime,
                    hs.EndTime,
                    OssTitle = oss.Title,
                    OSort = oss.Sort,
                    MaxRow = oss.MaxRow,
                })
            .OrderBy(x => x.OuterSet)
            .ThenBy(x => x.InnerSort)
            .ToListAsync(cancellationToken);

        // 3. 收集各 theme_type 的 mapping_id，批次查詢 caption
        var caseIds = rows.Where(r => r.ThemeType == "case").Select(r => r.MappingId).Distinct().ToList();
        var videoIds = rows.Where(r => r.ThemeType == "video").Select(r => r.MappingId).Distinct().ToList();
        var columnIds = rows.Where(r => r.ThemeType == "column").Select(r => r.MappingId).Distinct().ToList();
        var adIds = rows.Where(r => r.ThemeType == "ad").Select(r => r.MappingId).Distinct().ToList();
        var productIds = rows.Where(r => r.ThemeType is "product" or "brand").Select(r => r.MappingId).Distinct().ToList();
        var designerIds = rows.Where(r => r.ThemeType == "designer").Select(r => r.MappingId).Distinct().ToList();

        var caseCaptions = caseIds.Count > 0
            ? await _db.Hcases.AsNoTracking()
                .Where(c => caseIds.Contains(c.HcaseId))
                .ToDictionaryAsync(c => c.HcaseId, c => c.Caption, cancellationToken)
            : new Dictionary<uint, string>();

        var videoCaptions = videoIds.Count > 0
            ? await _db.Hvideos.AsNoTracking()
                .Where(v => videoIds.Contains(v.HvideoId))
                .ToDictionaryAsync(v => v.HvideoId, v => v.Name, cancellationToken)
            : new Dictionary<uint, string>();

        var columnCaptions = columnIds.Count > 0
            ? await _db.Hcolumns.AsNoTracking()
                .Where(c => columnIds.Contains(c.HcolumnId))
                .ToDictionaryAsync(c => c.HcolumnId, c => c.Ctitle, cancellationToken)
            : new Dictionary<uint, string>();

        var adData = adIds.Count > 0
            ? (await _db.Hads.AsNoTracking()
                .Where(a => adIds.Contains(a.Adid))
                .Select(a => new { a.Adid, a.Addesc, a.Adlogo })
                .ToListAsync(cancellationToken))
                .ToDictionary(a => a.Adid, a => (Addesc: a.Addesc, Adlogo: a.Adlogo))
            : new Dictionary<uint, (string Addesc, string Adlogo)>();

        var productCaptions = productIds.Count > 0
            ? await _db.Hproducts.AsNoTracking()
                .Where(p => productIds.Contains(p.Id))
                .ToDictionaryAsync(p => p.Id, p => p.Name, cancellationToken)
            : new Dictionary<uint, string>();

        var designerCaptions = designerIds.Count > 0
            ? await _db.Hdesigners.AsNoTracking()
                .Where(d => designerIds.Contains(d.HdesignerId))
                .ToDictionaryAsync(d => d.HdesignerId, d => d.Name, cancellationToken)
            : new Dictionary<uint, string>();

<<<<<<< HEAD
        // 4. 組裝結果(先全量組裝,再手動分頁——因為 enrichment 需 in-memory)
        var allItems = new List<HomepageInnerSetListItem>(rows.Count);
=======
        // 4. 組裝結果
        var result = new List<HomepageInnerSetListItem>(rows.Count);
>>>>>>> origin/main
        foreach (var r in rows)
        {
            string? caption = null;
            string? link = null;

            switch (r.ThemeType)
            {
                case "case":
                    caseCaptions.TryGetValue(r.MappingId, out caption);
                    link = $"https://hhh.com.tw/case-post.php?id={r.MappingId}";
                    break;
                case "video":
                    videoCaptions.TryGetValue(r.MappingId, out caption);
                    link = $"https://hhh.com.tw/video-post.php?id={r.MappingId}";
                    break;
                case "column":
                    columnCaptions.TryGetValue(r.MappingId, out caption);
                    link = $"https://hhh.com.tw/column-post.php?id={r.MappingId}";
                    break;
                case "ad":
                    if (adData.TryGetValue(r.MappingId, out var ad))
                    {
                        caption = ad.Addesc;
                        link = ad.Adlogo;
                    }
                    break;
                case "product":
                    productCaptions.TryGetValue(r.MappingId, out caption);
                    link = $"https://hhh.com.tw/product-post.php?id={r.MappingId}";
                    break;
                case "designer":
                    designerCaptions.TryGetValue(r.MappingId, out caption);
                    link = $"https://hhh.com.tw/designer-index.php?designer_id={r.MappingId}";
                    break;
                case "brand":
                    productCaptions.TryGetValue(r.MappingId, out caption);
                    link = $"https://hhh.com.tw/brand-index.php?brand_id={r.MappingId}";
                    break;
            }

            countByOuterSet.TryGetValue(r.OuterSet, out var countAll);
            var color = countAll > r.MaxRow ? 1 : 0;

<<<<<<< HEAD
            allItems.Add(new HomepageInnerSetListItem
=======
            result.Add(new HomepageInnerSetListItem
>>>>>>> origin/main
            {
                PsId = r.PsId,
                MappingId = r.MappingId,
                InnerSort = r.InnerSort,
                ThemeType = r.ThemeType,
                OuterSet = r.OuterSet,
                Onoff = r.Onoff,
                StartTime = r.StartTime,
                EndTime = r.EndTime,
                Title = r.OssTitle,
                OSort = r.OSort,
                Caption = caption,
                Link = link,
                Color = color,
            });
        }

<<<<<<< HEAD
        var paged = allItems
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToList();

        return new PagedResponse<HomepageInnerSetListItem>
        {
            Items = paged,
            Total = allItems.Count,
            Page = query.Page,
            PageSize = query.PageSize,
        };
=======
        return result;
>>>>>>> origin/main
    }

    /// <inheritdoc />
    public async Task<OperationResult<uint>> CreateAsync(
        CreateHomepageInnerSetRequest request,
        CancellationToken cancellationToken = default)
    {
        // 驗證 theme_type
        if (!ValidThemeTypes.Contains(request.ThemeType))
            return OperationResult<uint>.BadRequest("theme_type 資料不符");

        // 驗證時間
        if (request.StartTime > request.EndTime)
            return OperationResult<uint>.BadRequest("開始時間不可大於結束時間");

        var now = DateTime.Now;
        var entity = new HomepageSet
        {
            MappingId = request.MappingId,
            InnerSort = request.InnerSort,
            ThemeType = request.ThemeType,
            OuterSet = request.OuterSet,
            Onoff = request.Onoff,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            CreateTime = now,
            UpdateTime = now,
        };

        _db.HomepageSets.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        return OperationResult<uint>.Created(entity.PsId, "區塊元素成功建立");
    }

    /// <inheritdoc />
    public async Task<OperationResult<uint>> UpdateAsync(
        uint psId,
        UpdateHomepageInnerSetRequest request,
        CancellationToken cancellationToken = default)
    {
        // 驗證 theme_type
        if (!ValidThemeTypes.Contains(request.ThemeType))
            return OperationResult<uint>.BadRequest("theme_type 資料不符");

        // 驗證時間
        if (request.StartTime > request.EndTime)
            return OperationResult<uint>.BadRequest("開始時間不可大於結束時間");

        var entity = await _db.HomepageSets
            .FirstOrDefaultAsync(h => h.PsId == psId, cancellationToken);

        if (entity is null)
            return OperationResult<uint>.NotFound("找不到區塊元素");

        entity.MappingId = request.MappingId;
        entity.InnerSort = request.InnerSort;
        entity.ThemeType = request.ThemeType;
        entity.OuterSet = request.OuterSet;
        entity.Onoff = request.Onoff;
        entity.StartTime = request.StartTime;
        entity.EndTime = request.EndTime;
        entity.UpdateTime = DateTime.Now;

        await _db.SaveChangesAsync(cancellationToken);

        return OperationResult<uint>.Ok(psId, "區塊元素成功修改");
    }

    /// <inheritdoc />
    public async Task<OperationResult> DeleteAsync(
        uint psId,
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: homepage_model::del_innerset() — hard delete
        var entity = await _db.HomepageSets
            .FirstOrDefaultAsync(h => h.PsId == psId, cancellationToken);

        if (entity is null)
            return OperationResult.NotFound("找不到區塊元素");

        _db.HomepageSets.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);

        return OperationResult.Ok("區塊元素刪除成功");
    }
}

using hhh.api.contracts.admin.Main.Search;
using hhh.infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Main.Search;

public class SearchService : ISearchService
{
    private readonly XoopsContext _db;

    public SearchService(XoopsContext db)
    {
        _db = db;
    }

    public async Task<List<string>> GetKeywordTagsAsync(CancellationToken cancellationToken = default)
    {
        // 對應舊 PHP search_model::get_keyword_lists():
        //   SELECT all_search_tag FROM site_setup (single row)
        var setup = await _db.SiteSetups
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        if (string.IsNullOrWhiteSpace(setup?.AllSearchTag))
            return new List<string>();

        return setup.AllSearchTag
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .ToList();
    }

    public async Task<List<SearchKeywordItem>> GetHotKeywordsAsync(CancellationToken cancellationToken = default)
    {
        // 對應舊 PHP search_model::get_search_history_lists():
        //   SELECT keyword, SUM(today_count) as total
        //   FROM search_history
        //   GROUP BY keyword
        //   ORDER BY total DESC
        return await _db.SearchHistories
            .AsNoTracking()
            .GroupBy(s => s.Keyword)
            .Select(g => new SearchKeywordItem
            {
                Keyword = g.Key,
                TotalCount = g.Sum(s => (long)s.TodayCount),
            })
            .OrderByDescending(x => x.TotalCount)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<AutocompleteItem>> GetAutocompleteAsync(CancellationToken cancellationToken = default)
    {
        // 對應舊 PHP search_model::get_autocomplete_data():
        //   SELECT title, name FROM _hdesigner WHERE onoff=1
        //   + SELECT title FROM _hbrand WHERE onoff=1
        //   合併成 flat array
        var designers = await _db.Hdesigners
            .AsNoTracking()
            .Where(d => d.Onoff == 1)
            .Select(d => new { d.Title, d.Name })
            .ToListAsync(cancellationToken);

        var brands = await _db.Hbrands
            .AsNoTracking()
            .Where(b => b.Onoff == 1)
            .Select(b => b.Title)
            .ToListAsync(cancellationToken);

        var result = new List<AutocompleteItem>();

        foreach (var d in designers)
        {
            result.Add(new AutocompleteItem { Name = d.Title });
            if (!string.IsNullOrWhiteSpace(d.Name) && d.Name != d.Title)
            {
                result.Add(new AutocompleteItem { Name = d.Name });
            }
        }

        foreach (var b in brands)
        {
            result.Add(new AutocompleteItem { Name = b });
        }

        return result;
    }

    public async Task<List<SearchBackendResultItem>> SearchBackendAsync(
        SearchBackendQuery query,
        CancellationToken cancellationToken = default)
    {
        // 對應舊 PHP searchall_model:
        //   switch(type) → 各 table 的 OR-LIKE 搜尋,只回 id + title
        return query.Type.ToLowerInvariant() switch
        {
            "case" => await SearchCasesAsync(query.Keyword, cancellationToken),
            "column" => await SearchColumnsAsync(query.Keyword, cancellationToken),
            "designer" => await SearchDesignersAsync(query.Keyword, cancellationToken),
            "video" => await SearchVideosAsync(query.Keyword, cancellationToken),
            "brand" => await SearchBrandsAsync(query.Keyword, cancellationToken),
            "product" => await SearchProductsAsync(query.Keyword, cancellationToken),
            _ => new List<SearchBackendResultItem>(),
        };
    }

    // -------------------------------------------------------------------------
    // Private: per-type backend search (對應 searchall_model 的各方法)
    // -------------------------------------------------------------------------

    private async Task<List<SearchBackendResultItem>> SearchCasesAsync(
        string? keyword, CancellationToken ct)
    {
        var q = _db.Hcases.AsNoTracking().Where(c => c.Onoff == 1);

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var like = $"%{keyword.Trim()}%";
            q = q.Where(c =>
                EF.Functions.Like(c.HcaseId.ToString(), like) ||
                EF.Functions.Like(c.Caption, like) ||
                EF.Functions.Like(c.Location, like) ||
                EF.Functions.Like(c.Style, like) ||
                EF.Functions.Like(c.Type, like) ||
                EF.Functions.Like(c.Condition, like));
        }

        return await q
            .OrderByDescending(c => c.HcaseId)
            .Select(c => new SearchBackendResultItem { Id = c.HcaseId, Title = c.Caption })
            .ToListAsync(ct);
    }

    private async Task<List<SearchBackendResultItem>> SearchColumnsAsync(
        string? keyword, CancellationToken ct)
    {
        var q = _db.Hcolumns.AsNoTracking().Where(c => c.Onoff == 1);

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var like = $"%{keyword.Trim()}%";
            q = q.Where(c =>
                EF.Functions.Like(c.HcolumnId.ToString(), like) ||
                EF.Functions.Like(c.Ctitle, like) ||
                EF.Functions.Like(c.Ctype, like) ||
                EF.Functions.Like(c.Cdesc, like));
        }

        return await q
            .OrderByDescending(c => c.HcolumnId)
            .Select(c => new SearchBackendResultItem { Id = c.HcolumnId, Title = c.Ctitle })
            .ToListAsync(ct);
    }

    private async Task<List<SearchBackendResultItem>> SearchDesignersAsync(
        string? keyword, CancellationToken ct)
    {
        var q = _db.Hdesigners.AsNoTracking().Where(d => d.Onoff == 1);

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var like = $"%{keyword.Trim()}%";
            q = q.Where(d =>
                EF.Functions.Like(d.HdesignerId.ToString(), like) ||
                EF.Functions.Like(d.Title, like) ||
                EF.Functions.Like(d.Name, like));
        }

        return await q
            .OrderByDescending(d => d.HdesignerId)
            .Select(d => new SearchBackendResultItem
            {
                Id = d.HdesignerId,
                Title = d.Title,
                SubTitle = d.Name,
            })
            .ToListAsync(ct);
    }

    private async Task<List<SearchBackendResultItem>> SearchVideosAsync(
        string? keyword, CancellationToken ct)
    {
        // 注意:舊 PHP 沒有 onoff 過濾
        var q = _db.Hvideos.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var like = $"%{keyword.Trim()}%";
            q = q.Where(v =>
                EF.Functions.Like(v.HvideoId.ToString(), like) ||
                EF.Functions.Like(v.Name, like));
        }

        return await q
            .OrderByDescending(v => v.HvideoId)
            .Select(v => new SearchBackendResultItem { Id = v.HvideoId, Title = v.Name })
            .ToListAsync(ct);
    }

    private async Task<List<SearchBackendResultItem>> SearchBrandsAsync(
        string? keyword, CancellationToken ct)
    {
        var q = _db.Hbrands.AsNoTracking().Where(b => b.Onoff == 1);

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var like = $"%{keyword.Trim()}%";
            q = q.Where(b =>
                EF.Functions.Like(b.HbrandId.ToString(), like) ||
                EF.Functions.Like(b.Title, like));
        }

        return await q
            .OrderByDescending(b => b.HbrandId)
            .Select(b => new SearchBackendResultItem { Id = b.HbrandId, Title = b.Title })
            .ToListAsync(ct);
    }

    private async Task<List<SearchBackendResultItem>> SearchProductsAsync(
        string? keyword, CancellationToken ct)
    {
        var q = _db.Hproducts.AsNoTracking().Where(p => p.Onoff == 1);

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var like = $"%{keyword.Trim()}%";
            q = q.Where(p =>
                EF.Functions.Like(p.Id.ToString(), like) ||
                EF.Functions.Like(p.Name, like));
        }

        return await q
            .OrderByDescending(p => p.Id)
            .Select(p => new SearchBackendResultItem { Id = p.Id, Title = p.Name })
            .ToListAsync(ct);
    }
}

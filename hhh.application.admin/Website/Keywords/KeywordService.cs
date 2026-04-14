using hhh.api.contracts.admin.Main.Search;
using hhh.api.contracts.admin.Website;
using hhh.infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Website.Keywords;

public class KeywordService : IKeywordService
{
    private readonly XoopsContext _db;

    public KeywordService(XoopsContext db)
    {
        _db = db;
    }

    /// <inheritdoc />
    public async Task<List<SearchKeywordItem>> GetHotKeywordsAsync(
        HotKeywordQuery query,
        CancellationToken cancellationToken = default)
    {
        // 對應舊版 PHP: search_model::get_search_history_lists($input)
        // SELECT keyword, SUM(today_count) AS total
        // FROM search_history
        // WHERE date_added BETWEEN sdate AND edate  -- optional
        // AND keyword = 'keyword'                    -- optional, exact match
        // GROUP BY keyword ORDER BY total DESC
        var q = _db.SearchHistories.AsNoTracking().AsQueryable();

        if (query.Sdate.HasValue)
            q = q.Where(s => s.DateAdded >= query.Sdate.Value);

        if (query.Edate.HasValue)
            q = q.Where(s => s.DateAdded <= query.Edate.Value);

        if (!string.IsNullOrWhiteSpace(query.Keyword))
            q = q.Where(s => s.Keyword == query.Keyword.Trim());

        return await q
            .GroupBy(s => s.Keyword)
            .Select(g => new SearchKeywordItem
            {
                Keyword = g.Key,
                TotalCount = g.Sum(s => (long)s.TodayCount),
            })
            .OrderByDescending(x => x.TotalCount)
            .ToListAsync(cancellationToken);
    }
}

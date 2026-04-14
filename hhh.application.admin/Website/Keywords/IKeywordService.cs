using hhh.api.contracts.admin.Main.Search;
using hhh.api.contracts.admin.Website;

namespace hhh.application.admin.Website.Keywords;

public interface IKeywordService
{
    /// <summary>
    /// 取得熱門關鍵字統計（支援日期區間 / 精確關鍵字篩選）
    /// </summary>
    Task<List<SearchKeywordItem>> GetHotKeywordsAsync(
        HotKeywordQuery query,
        CancellationToken cancellationToken = default);
}

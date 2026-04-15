using hhh.api.contracts.admin.Main.Search;
using hhh.api.contracts.admin.Website;
using hhh.api.contracts.Common;

namespace hhh.application.admin.Website.Keywords;

public interface IKeywordService
{
    /// <summary>
    /// 取得熱門關鍵字統計（支援日期區間 / 精確關鍵字篩選）
    /// </summary>
    Task<PagedResponse<SearchKeywordItem>> GetHotKeywordsAsync(
        HotKeywordQuery query,
        ListQuery listQuery,
        CancellationToken cancellationToken = default);
}

using hhh.api.contracts.admin.Main.Search;

namespace hhh.application.admin.Main.Search;

/// <summary>
/// 搜尋服務
/// </summary>
/// <remarks>
/// 對應舊版 PHP:hhh-api/.../base/v1/Search.php(search_model + searchall_model)
/// 後台 view:keyword.php(hot_keyword)、searchall.php(lists_back)
/// </remarks>
public interface ISearchService
{
    /// <summary>
    /// 取得搜尋標籤(從 site_setup.all_search_tag 讀取,逗號分隔)
    /// 對應舊版 11.3 keyword_get
    /// </summary>
    Task<List<string>> GetKeywordTagsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 取得熱門關鍵字(GROUP BY keyword,依搜尋次數 DESC)
    /// 對應舊版 11.4 hot_keyword_get
    /// </summary>
    Task<List<SearchKeywordItem>> GetHotKeywordsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 取得自動完成資料(合併設計師 + 品牌的 title/name)
    /// 對應舊版 11.6 autocomplete_get
    /// </summary>
    Task<List<AutocompleteItem>> GetAutocompleteAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 後台搜尋(switch type → 對應 table 的 OR-LIKE 搜尋)
    /// 對應舊版 11.5 lists_back_get
    /// </summary>
    Task<List<SearchBackendResultItem>> SearchBackendAsync(
        SearchBackendQuery query,
        CancellationToken cancellationToken = default);
}

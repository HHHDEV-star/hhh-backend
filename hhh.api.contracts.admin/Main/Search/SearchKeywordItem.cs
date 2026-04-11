namespace hhh.api.contracts.admin.Main.Search;

/// <summary>
/// 熱門關鍵字項目
/// (對應舊版 Search/hot_keyword_get → search_model::get_search_history_lists)
/// </summary>
public class SearchKeywordItem
{
    /// <summary>關鍵字</summary>
    public string Keyword { get; set; } = string.Empty;

    /// <summary>搜尋次數合計</summary>
    public long TotalCount { get; set; }
}

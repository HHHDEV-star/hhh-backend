namespace hhh.api.contracts.admin.Main.Search;

/// <summary>
/// 自動完成項目
/// (對應舊版 Search/autocomplete_get → search_model::get_autocomplete_data)
/// </summary>
public class AutocompleteItem
{
    /// <summary>名稱(設計師 title/name 或品牌 title)</summary>
    public string Name { get; set; } = string.Empty;
}

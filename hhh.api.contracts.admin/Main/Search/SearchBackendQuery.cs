using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Main.Search;

/// <summary>
/// 後台搜尋查詢參數
/// (對應舊版 Search/lists_back_get → searchall_model)
/// </summary>
public class SearchBackendQuery
{
    /// <summary>搜尋類型(case / column / designer / video / brand / product)</summary>
    [Required]
    public string Type { get; set; } = string.Empty;

    /// <summary>關鍵字(跨欄位 OR LIKE)</summary>
    public string? Keyword { get; set; }
}

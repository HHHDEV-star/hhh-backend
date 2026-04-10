namespace hhh.api.contracts.admin.Htopic2s;

/// <summary>
/// 議題 2 列表分頁回應。
/// </summary>
public class Htopic2ListResponse
{
    /// <summary>當頁資料</summary>
    public IReadOnlyList<Htopic2ListItem> Items { get; set; } = Array.Empty<Htopic2ListItem>();

    /// <summary>符合條件的總筆數</summary>
    public long Total { get; set; }

    /// <summary>當前頁碼</summary>
    public int Page { get; set; }

    /// <summary>每頁筆數</summary>
    public int PageSize { get; set; }

    /// <summary>總頁數</summary>
    public int TotalPages => PageSize > 0
        ? (int)Math.Ceiling(Total / (double)PageSize)
        : 0;
}

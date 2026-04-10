namespace hhh.api.contracts.admin.Hdesigners;

/// <summary>
/// 設計師列表分頁回應
/// </summary>
public class HdesignerListResponse
{
    /// <summary>當頁資料</summary>
    public IReadOnlyList<HdesignerListItem> Items { get; set; } = Array.Empty<HdesignerListItem>();

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

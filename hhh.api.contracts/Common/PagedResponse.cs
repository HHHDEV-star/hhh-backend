namespace hhh.api.contracts.Common;

/// <summary>
/// 分頁查詢的共用回應格式。所有回傳分頁列表的 API 都使用這個類別,
/// 不再為每個 feature 重複定義 Items/Total/Page/PageSize/TotalPages。
/// </summary>
/// <typeparam name="T">清單項目型別(通常是 <c>XxxListItem</c>)</typeparam>
public class PagedResponse<T>
{
    /// <summary>當頁資料</summary>
    public IReadOnlyList<T> Items { get; set; } = Array.Empty<T>();

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

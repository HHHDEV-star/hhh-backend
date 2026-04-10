using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.Common;

/// <summary>
/// 分頁查詢的共用基底。所有 *ListRequest 繼承這個類別,
/// 只要加上自己 feature-specific 的過濾欄位即可,不再重複定義
/// Page / PageSize / Sort / By。
/// </summary>
/// <remarks>
/// Sort 預設為 null(讓 service 的 ApplyOrdering fallback 到 PK);
/// 若某個 feature 希望有不同的預設排序欄位,可以在子類別直接覆寫:
/// <code>
/// public override string? Sort { get; set; } = "id";
/// </code>
/// </remarks>
public abstract class PagedRequest
{
    /// <summary>頁碼(從 1 開始)</summary>
    [Range(1, int.MaxValue, ErrorMessage = "頁碼必須大於等於 1")]
    public int Page { get; set; } = 1;

    /// <summary>每頁筆數(1 ~ 100)</summary>
    [Range(1, 100, ErrorMessage = "每頁筆數必須在 1 ~ 100 之間")]
    public int PageSize { get; set; } = 15;

    /// <summary>
    /// 排序欄位。各 feature 有自己的白名單,不在白名單的值會 fallback 到 PK。
    /// </summary>
    public virtual string? Sort { get; set; }

    /// <summary>排序方向:ASC / DESC(大小寫不拘),預設 DESC。</summary>
    public string? By { get; set; } = "DESC";

    /// <summary>排序方向是否為 ASC(供 service 層判斷用)。</summary>
    public bool IsAsc => string.Equals(By, "ASC", StringComparison.OrdinalIgnoreCase);
}

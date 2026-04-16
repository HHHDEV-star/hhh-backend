using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Website;

/// <summary>
/// 建商列表回應：分頁資料 + 全域統計摘要。
/// </summary>
/// <remarks>
/// 繼承 <see cref="PagedResponse{T}"/> 直接攤平 Items / Total / Page / PageSize / TotalPages,
/// 另多一個 <see cref="Summary"/> 欄位放全域統計(不受查詢條件影響)。
/// </remarks>
public class BuilderListResponse : PagedResponse<BuilderListItem>
{
    /// <summary>列表上方的統計摘要</summary>
    public BuilderListSummary Summary { get; set; } = new();
}

using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Platform.Admins;

/// <summary>
/// 管理者列表查詢條件(對應舊版 admin.php 的分頁參數)。
/// Page / PageSize / Sort 由 <see cref="PagedRequest"/> 提供,
/// 排序白名單:id / account / name / email / createTime / isActive。
/// </summary>
/// <remarks>
/// 這個 feature 預設排序方向為 ASC(不同於 <see cref="PagedRequest"/> 的 DESC),
/// 所以 override <see cref="By"/> 的預設值。
/// </remarks>
public class AdminListRequest : PagedRequest
{
    /// <inheritdoc/>
    public new string? By { get; set; } = "ASC";
}

namespace hhh.api.contracts.Common;

/// <summary>
/// 通用分頁查詢(無額外 filter 的列表共用)。
/// 繼承 <see cref="PagedRequest"/>,擁有 Page / PageSize / Sort / By。
/// 若列表需要額外 filter(如 keyword / dateRange),
/// 請建立自己的 XxxListQuery 繼承 <see cref="PagedRequest"/> 即可。
/// </summary>
public class ListQuery : PagedRequest
{
}

using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Users;

/// <summary>
/// 會員列表查詢條件(對應舊版 _users.php 的分頁參數)。
/// Page / PageSize / Sort / By 由 <see cref="PagedRequest"/> 提供,
/// 排序白名單:id / uname / email / name / regdate / lastLogin。
/// </summary>
public class UserListRequest : PagedRequest
{
}

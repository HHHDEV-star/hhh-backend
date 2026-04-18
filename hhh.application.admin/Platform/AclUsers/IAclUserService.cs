using hhh.api.contracts.admin.Platform.AclUsers;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;

namespace hhh.application.admin.Platform.AclUsers;

/// <summary>後台帳號(ACL)管理服務</summary>
/// <remarks>
/// 對應舊版 PHP:hhh-backstage/controllers/System.php → pure()
///             → Acl_lib::get_users / create_user / update_user
/// 後台 view:hhh-backstage/.../event/system_users.php
/// 資料來源：hhh_backstage.acl_users
/// </remarks>
public interface IAclUserService
{
    /// <summary>取得帳號列表（分頁）</summary>
    Task<PagedResponse<AclUserListItem>> GetListAsync(
        AclUserListQuery query, CancellationToken ct = default);

    /// <summary>新增帳號</summary>
    Task<OperationResult<int>> CreateAsync(
        CreateAclUserRequest request, CancellationToken ct = default);

    /// <summary>編輯帳號</summary>
    Task<OperationResult> UpdateAsync(
        int id, UpdateAclUserRequest request, CancellationToken ct = default);
}

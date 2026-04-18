using hhh.api.contracts.admin.Platform.AclMenuGroups;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;

namespace hhh.application.admin.Platform.AclMenuGroups;

/// <summary>ACL 目錄群組管理服務</summary>
/// <remarks>
/// 對應舊版 PHP:System.php → pure()
///             → Acl_lib::get_menu_path_group / create_menu_path_group / update_menu_path_group
/// 資料來源：hhh_backstage.acl_menu_group
/// </remarks>
public interface IAclMenuGroupService
{
    /// <summary>取得目錄群組列表（分頁）</summary>
    Task<PagedResponse<AclMenuGroupListItem>> GetListAsync(
        AclMenuGroupListQuery query, CancellationToken ct = default);

    /// <summary>新增目錄群組</summary>
    Task<OperationResult<int>> CreateAsync(
        CreateAclMenuGroupRequest request, CancellationToken ct = default);

    /// <summary>編輯目錄群組</summary>
    Task<OperationResult> UpdateAsync(
        int id, UpdateAclMenuGroupRequest request, CancellationToken ct = default);
}

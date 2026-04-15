using hhh.api.contracts.admin.Platform.AclMenuPaths;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;

namespace hhh.application.admin.Platform.AclMenuPaths;

/// <summary>ACL 目錄功能管理服務</summary>
/// <remarks>
/// 對應舊版 PHP:System.php → pure()
///             → Acl_lib::get_menus_path / create_menu_path / update_menu_path
///             → Acl_lib::get_select_projects / get_select_menu_group
/// 資料來源：hhh_backstage.acl_menu_path
/// </remarks>
public interface IAclMenuPathService
{
    /// <summary>取得目錄功能列表（分頁）</summary>
    Task<PagedResponse<AclMenuPathListItem>> GetListAsync(
        ListQuery query, CancellationToken ct = default);

    /// <summary>新增目錄功能</summary>
    Task<OperationResult<int>> CreateAsync(
        CreateAclMenuPathRequest request, CancellationToken ct = default);

    /// <summary>編輯目錄功能</summary>
    Task<OperationResult> UpdateAsync(
        int id, UpdateAclMenuPathRequest request, CancellationToken ct = default);

    /// <summary>取得專案下拉選單</summary>
    Task<List<SelectOption>> GetProjectOptionsAsync(CancellationToken ct = default);

    /// <summary>取得群組下拉選單</summary>
    Task<List<SelectOption>> GetMenuGroupOptionsAsync(CancellationToken ct = default);
}

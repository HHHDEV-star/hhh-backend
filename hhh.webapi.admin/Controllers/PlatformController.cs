using System.Security.Claims;
using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Platform.AclMenuGroups;
using hhh.api.contracts.admin.Platform.AclMenuPaths;
using hhh.api.contracts.admin.Platform.AclUsers;
using hhh.api.contracts.admin.Platform.Admins;
using hhh.api.contracts.admin.Platform.OperationLogs;
using hhh.api.contracts.admin.Platform.SystemLogs;
using hhh.application.admin.Platform.AclMenuGroups;
using hhh.application.admin.Platform.AclMenuPaths;
using hhh.application.admin.Platform.AclUsers;
using hhh.application.admin.Platform.Admins;
using hhh.application.admin.Platform.OperationLogs;
using hhh.application.admin.Platform.SystemLogs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hhh.webapi.admin.Controllers;

/// <summary>
/// 平台管理
/// </summary>
/// <remarks>
/// 後台「平台管理」業務類別下所有 endpoint 集中於此 controller。
/// 包含後台帳號管理 + 操作紀錄查詢。
///
/// 對應舊版 PHP:
///  - 後台帳號:/backend/admin.php、admin_edit.php、admin_password.php
///  - 操作紀錄:/backend/_hoplog.php、_hoplog_edit.php
/// </remarks>
[Route("api/platform")]
[Authorize]
[Tags("Platform")]
public class PlatformController : ApiControllerBase
{
    private readonly IAdminService _adminService;
    private readonly IOperationLogService _operationLogService;
    private readonly IAclUserService _aclUserService;
    private readonly ISystemLogService _systemLogService;
    private readonly IAclMenuGroupService _aclMenuGroupService;
    private readonly IAclMenuPathService _aclMenuPathService;

    public PlatformController(
        IAdminService adminService,
        IOperationLogService operationLogService,
        IAclUserService aclUserService,
        ISystemLogService systemLogService,
        IAclMenuGroupService aclMenuGroupService,
        IAclMenuPathService aclMenuPathService)
    {
        _adminService = adminService;
        _operationLogService = operationLogService;
        _aclUserService = aclUserService;
        _systemLogService = systemLogService;
        _aclMenuGroupService = aclMenuGroupService;
        _aclMenuPathService = aclMenuPathService;
    }

    // =========================================================================
    // 後台帳號 (admins)
    // =========================================================================

    /// <summary>取得管理者分頁列表</summary>
    /// <remarks>對應舊版 /backend/admin.php。排序白名單:id, account, name, email, createTime, isActive。</remarks>
    [HttpGet("admins/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<AdminListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAdminList(
        [FromQuery] AdminListRequest request,
        CancellationToken cancellationToken)
    {
        var data = await _adminService.GetListAsync(request, cancellationToken);
        return Ok(ApiResponse<PagedResponse<AdminListItem>>.Success(data));
    }

    /// <summary>取得目前登入的管理者(/me)</summary>
    /// <remarks>
    /// 對應舊版 /backend/admin_password.php 的資料讀取部分。
    /// 從 JWT NameIdentifier claim 取得 admin id。
    /// </remarks>
    [HttpGet("admins/me")]
    [ProducesResponseType(typeof(ApiResponse<AdminDetailResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCurrentAdmin(CancellationToken cancellationToken)
    {
        if (GetCurrentAdminId() is not { } adminId)
        {
            return StatusCode(StatusCodes.Status401Unauthorized,
                ApiResponse.Error(StatusCodes.Status401Unauthorized, "無法取得目前登入者"));
        }

        var admin = await _adminService.GetByIdAsync(adminId, cancellationToken);
        if (admin is null)
        {
            return StatusCode(StatusCodes.Status404NotFound,
                ApiResponse.Error(StatusCodes.Status404NotFound, "找不到管理者"));
        }

        return Ok(ApiResponse<AdminDetailResponse>.Success(admin));
    }

    /// <summary>取得單一管理者</summary>
    /// <remarks>對應舊版 /backend/admin_edit.php?id={id} (GET)。</remarks>
    [HttpGet("admins/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<AdminDetailResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAdminById(uint id, CancellationToken cancellationToken)
    {
        var admin = await _adminService.GetByIdAsync(id, cancellationToken);
        if (admin is null)
        {
            return StatusCode(StatusCodes.Status404NotFound,
                ApiResponse.Error(StatusCodes.Status404NotFound, "找不到管理者"));
        }

        return Ok(ApiResponse<AdminDetailResponse>.Success(admin));
    }

    /// <summary>新增管理者</summary>
    /// <remarks>對應舊版 /backend/admin_edit.php (POST 無 id 分支)。</remarks>
    [HttpPost("admins")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateAdmin(
        [FromBody] CreateAdminRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _adminService.CreateAsync(request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return StatusCode(
            StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { id = result.Data }, result.Message));
    }

    /// <summary>更新管理者</summary>
    /// <remarks>
    /// 對應舊版 /backend/admin_edit.php (POST 帶 id 分支)。
    /// 密碼欄位選填:null 或空字串代表不更新密碼。account 不可修改。
    /// </remarks>
    [HttpPut("admins/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAdmin(
        uint id,
        [FromBody] UpdateAdminRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _adminService.UpdateAsync(id, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { id = result.Data }, result.Message));
    }

    /// <summary>更新目前登入管理者的 profile(含密碼)</summary>
    /// <remarks>
    /// 對應舊版 /backend/admin_password.php。
    /// 只能改自己的 name / email / tel / pwd;不能改 account / allow_page / is_active。
    /// </remarks>
    [HttpPut("admins/me")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateCurrentAdmin(
        [FromBody] UpdateAdminProfileRequest request,
        CancellationToken cancellationToken)
    {
        if (GetCurrentAdminId() is not { } adminId)
        {
            return StatusCode(StatusCodes.Status401Unauthorized,
                ApiResponse.Error(StatusCodes.Status401Unauthorized, "無法取得目前登入者"));
        }

        var result = await _adminService.UpdateProfileAsync(adminId, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { id = result.Data }, result.Message));
    }

    // =========================================================================
    // 操作紀錄 (operation-logs) - read only
    // =========================================================================

    /// <summary>取得操作紀錄分頁列表</summary>
    /// <remarks>
    /// 對應舊版 /backend/_hoplog.php。
    /// 支援 q / uname / action / pageName / from / to / page / pageSize / sort / by。
    /// 預設依 creatTime DESC 排序。排序白名單:id, creatTime, uname, pageName, action。
    /// </remarks>
    [HttpGet("operation-logs/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<OperationLogListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOperationLogList(
        [FromQuery] OperationLogListRequest request,
        CancellationToken cancellationToken)
    {
        var data = await _operationLogService.GetListAsync(request, cancellationToken);
        return Ok(ApiResponse<PagedResponse<OperationLogListItem>>.Success(data));
    }

    /// <summary>取得單筆操作紀錄詳情(含 sqlcmd 全欄位)</summary>
    /// <remarks>對應舊版 /backend/_hoplog_edit.php?id={id} (GET)。</remarks>
    [HttpGet("operation-logs/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<OperationLogDetailResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetOperationLogById(uint id, CancellationToken cancellationToken)
    {
        var detail = await _operationLogService.GetByIdAsync(id, cancellationToken);
        if (detail is null)
        {
            return StatusCode(StatusCodes.Status404NotFound,
                ApiResponse.Error(StatusCodes.Status404NotFound, "找不到操作紀錄"));
        }

        return Ok(ApiResponse<OperationLogDetailResponse>.Success(detail));
    }

    // =========================================================================
    // 後台帳號 ACL (acl-users)
    // =========================================================================

    /// <summary>取得 ACL 帳號分頁列表</summary>
    /// <remarks>
    /// 對應舊版 PHP:hhh-backstage/controllers/System.php → pure() → users_list
    ///             → Acl_lib::get_users()
    /// 資料來源：hhh_backstage.acl_users
    /// </remarks>
    [HttpGet("acl-users/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<AclUserListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAclUserList(
        [FromQuery] AclUserListQuery query,
        CancellationToken cancellationToken)
    {
        var data = await _aclUserService.GetListAsync(query, cancellationToken);
        return Ok(ApiResponse<PagedResponse<AclUserListItem>>.Success(data));
    }

    /// <summary>新增 ACL 帳號</summary>
    /// <remarks>
    /// 對應舊版 PHP:System.php → pure() → user_create → Acl_lib::create_user()
    /// </remarks>
    [HttpPost("acl-users")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateAclUser(
        [FromBody] CreateAclUserRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _aclUserService.CreateAsync(request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return StatusCode(
            StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { id = result.Data }, result.Message));
    }

    /// <summary>編輯 ACL 帳號</summary>
    /// <remarks>
    /// 對應舊版 PHP:System.php → pure() → user_update → Acl_lib::update_user()
    /// 密碼選填：空或 null 表示不更新密碼。account 不可修改。
    /// </remarks>
    [HttpPut("acl-users/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAclUser(
        int id,
        [FromBody] UpdateAclUserRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _aclUserService.UpdateAsync(id, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(null!, result.Message));
    }

    // =========================================================================
    // 系統 API Log (system-logs) - read only
    // =========================================================================

    /// <summary>取得系統 API Log 分頁列表</summary>
    /// <remarks>
    /// 對應舊版 PHP:system/v1/System.php → backend_logs_get()
    ///             → System_model::get_backend_logs()
    /// 資料來源：hhh_api.rest_backend_logs
    /// 支援 account / apiUri / apiMethod / startDate / endDate / page / pageSize / sort / by。
    /// 排序白名單：id, time, account, method。
    /// </remarks>
    [HttpGet("system-logs/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<SystemLogListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSystemLogList(
        [FromQuery] SystemLogListRequest request,
        CancellationToken cancellationToken)
    {
        var data = await _systemLogService.GetListAsync(request, cancellationToken);
        return Ok(ApiResponse<PagedResponse<SystemLogListItem>>.Success(data));
    }

    // =========================================================================
    // ACL 目錄群組 (acl-menu-groups)
    // =========================================================================

    /// <summary>取得目錄群組分頁列表</summary>
    /// <remarks>
    /// 對應舊版 PHP:System.php → pure() → menu_path_group_list
    ///             → Acl_lib::get_menu_path_group()
    /// 資料來源：hhh_backstage.acl_menu_group，預設依 sort_num ASC 排序。
    /// </remarks>
    [HttpGet("acl-menu-groups/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<AclMenuGroupListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAclMenuGroupList(
        [FromQuery] AclMenuGroupListQuery query,
        CancellationToken cancellationToken)
    {
        var data = await _aclMenuGroupService.GetListAsync(query, cancellationToken);
        return Ok(ApiResponse<PagedResponse<AclMenuGroupListItem>>.Success(data));
    }

    /// <summary>新增目錄群組</summary>
    /// <remarks>
    /// 對應舊版 PHP:System.php → pure() → menu_path_group_create
    ///             → Acl_lib::create_menu_path_group()
    /// </remarks>
    [HttpPost("acl-menu-groups")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAclMenuGroup(
        [FromBody] CreateAclMenuGroupRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _aclMenuGroupService.CreateAsync(request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return StatusCode(
            StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { id = result.Data }, result.Message));
    }

    /// <summary>編輯目錄群組</summary>
    /// <remarks>
    /// 對應舊版 PHP:System.php → pure() → menu_path_group_update
    ///             → Acl_lib::update_menu_path_group()
    /// </remarks>
    [HttpPut("acl-menu-groups/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAclMenuGroup(
        int id,
        [FromBody] UpdateAclMenuGroupRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _aclMenuGroupService.UpdateAsync(id, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(null!, result.Message));
    }

    // =========================================================================
    // ACL 目錄功能 (acl-menu-paths)
    // =========================================================================

    /// <summary>取得目錄功能分頁列表</summary>
    /// <remarks>
    /// 對應舊版 PHP:System.php → pure() → menus_path_list
    ///             → Acl_lib::get_menus_path()
    /// 資料來源：hhh_backstage.acl_menu_path，預設依 sort_num ASC 排序。
    /// </remarks>
    [HttpGet("acl-menu-paths/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<AclMenuPathListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAclMenuPathList(
        [FromQuery] AclMenuPathListQuery query,
        CancellationToken cancellationToken)
    {
        var data = await _aclMenuPathService.GetListAsync(query, cancellationToken);
        return Ok(ApiResponse<PagedResponse<AclMenuPathListItem>>.Success(data));
    }

    /// <summary>新增目錄功能</summary>
    /// <remarks>
    /// 對應舊版 PHP:System.php → menu_path_create → Acl_lib::create_menu_path()
    /// path 需唯一，重複回傳 409。
    /// </remarks>
    [HttpPost("acl-menu-paths")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateAclMenuPath(
        [FromBody] CreateAclMenuPathRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _aclMenuPathService.CreateAsync(request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return StatusCode(
            StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { id = result.Data }, result.Message));
    }

    /// <summary>編輯目錄功能</summary>
    /// <remarks>
    /// 對應舊版 PHP:System.php → menu_path_update → Acl_lib::update_menu_path()
    /// </remarks>
    [HttpPut("acl-menu-paths/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAclMenuPath(
        int id,
        [FromBody] UpdateAclMenuPathRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _aclMenuPathService.UpdateAsync(id, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(null!, result.Message));
    }

    /// <summary>取得專案下拉選單</summary>
    /// <remarks>
    /// 對應舊版 PHP:System.php → select_projects → Acl_lib::get_select_projects()
    /// 回傳 [{value, text}] 供前端 DropDownList 使用。
    /// </remarks>
    [HttpGet("acl-menu-paths/select-projects")]
    [ProducesResponseType(typeof(ApiResponse<List<SelectOption>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSelectProjects(CancellationToken cancellationToken)
    {
        var data = await _aclMenuPathService.GetProjectOptionsAsync(cancellationToken);
        return Ok(ApiResponse<List<SelectOption>>.Success(data));
    }

    /// <summary>取得群組下拉選單</summary>
    /// <remarks>
    /// 對應舊版 PHP:System.php → select_menu_group → Acl_lib::get_select_menu_group()
    /// 回傳 [{value, text}] 供前端 DropDownList 使用。
    /// </remarks>
    [HttpGet("acl-menu-paths/select-menu-groups")]
    [ProducesResponseType(typeof(ApiResponse<List<SelectOption>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSelectMenuGroups(CancellationToken cancellationToken)
    {
        var data = await _aclMenuPathService.GetMenuGroupOptionsAsync(cancellationToken);
        return Ok(ApiResponse<List<SelectOption>>.Success(data));
    }

    // =========================================================================
    // Helpers
    // =========================================================================

    /// <summary>
    /// 從 JWT claim 取得目前登入管理者的 Id。
    /// AuthService 登入時把 admin.Id 放進 ClaimTypes.NameIdentifier。
    /// </summary>
    private uint? GetCurrentAdminId()
    {
        var nameId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return uint.TryParse(nameId, out var id) ? id : null;
    }
}

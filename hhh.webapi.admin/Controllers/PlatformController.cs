using System.Security.Claims;
using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Platform.Admins;
using hhh.api.contracts.admin.Platform.OperationLogs;
using hhh.application.admin.Platform.Admins;
using hhh.application.admin.Platform.OperationLogs;
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

    public PlatformController(
        IAdminService adminService,
        IOperationLogService operationLogService)
    {
        _adminService = adminService;
        _operationLogService = operationLogService;
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

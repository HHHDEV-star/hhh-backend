using System.Security.Claims;
using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Admins;
using hhh.application.admin.Admins;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hhh.webapi.admin.Controllers;

[Route("api/[controller]")]
[Authorize]
public class AdminsController : ApiControllerBase
{
    private readonly IAdminService _adminService;

    public AdminsController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    /// <summary>
    /// 取得管理者分頁列表
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/admin.php
    /// 支援 page / pageSize / sort / by 查詢參數。
    /// 排序白名單：id, account, name, email, createTime, isActive。
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<AdminListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetList(
        [FromQuery] AdminListRequest request,
        CancellationToken cancellationToken)
    {
        var data = await _adminService.GetListAsync(request, cancellationToken);
        return Ok(ApiResponse<PagedResponse<AdminListItem>>.Success(data));
    }

    /// <summary>
    /// 取得目前登入的管理者（/me）
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/admin_password.php 的資料讀取部分
    /// （原 PHP 使用 $_SESSION["admin"]["id"]，本 API 從 JWT claim 取得）
    /// </remarks>
    [HttpGet("me")]
    [ProducesResponseType(typeof(ApiResponse<AdminDetailResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMe(CancellationToken cancellationToken)
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

    /// <summary>
    /// 取得單一管理者
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/admin_edit.php?id={id} （GET 模式）
    /// </remarks>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<AdminDetailResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        uint id,
        CancellationToken cancellationToken)
    {
        var admin = await _adminService.GetByIdAsync(id, cancellationToken);
        if (admin is null)
        {
            return StatusCode(StatusCodes.Status404NotFound,
                ApiResponse.Error(StatusCodes.Status404NotFound, "找不到管理者"));
        }

        return Ok(ApiResponse<AdminDetailResponse>.Success(admin));
    }

    /// <summary>
    /// 新增管理者
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/admin_edit.php （POST 無 id 分支）
    /// 成功時回傳 HTTP 201 Created。
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create(
        [FromBody] CreateAdminRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _adminService.CreateAsync(request, cancellationToken);
        if (!result.IsSuccess)
        {
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        }

        return StatusCode(
            StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { id = result.Data }, result.Message));
    }

    /// <summary>
    /// 更新管理者
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/admin_edit.php （POST 帶 id 分支）
    /// 密碼欄位為選填：null 或空字串代表不更新密碼。
    /// 帳號 (account) 不可修改，與原表單顯示為唯讀一致。
    /// </remarks>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        uint id,
        [FromBody] UpdateAdminRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _adminService.UpdateAsync(id, request, cancellationToken);
        if (!result.IsSuccess)
        {
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        }

        return Ok(ApiResponse<object>.Success(
            new { id = result.Data }, result.Message));
    }

    /// <summary>
    /// 更新目前登入管理者的 profile（含密碼）
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/admin_password.php
    /// 只能改自己的 name / email / tel / pwd；不能改 account、allow_page、is_active。
    /// </remarks>
    [HttpPut("me")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateMe(
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
        {
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        }

        return Ok(ApiResponse<object>.Success(
            new { id = result.Data }, result.Message));
    }

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

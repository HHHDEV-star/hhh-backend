using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Users;
using hhh.application.admin.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hhh.webapi.admin.Controllers;

[Route("api/[controller]")]
[Authorize]
public class UsersController : ApiControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// 取得會員分頁列表
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_users.php
    /// 支援 page / pageSize / sort / by 查詢參數。
    /// 排序白名單:id, uname, email, name, regdate, lastLogin。
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<UserListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetList(
        [FromQuery] UserListRequest request,
        CancellationToken cancellationToken)
    {
        var data = await _userService.GetListAsync(request, cancellationToken);
        return Ok(ApiResponse<PagedResponse<UserListItem>>.Success(data));
    }

    /// <summary>
    /// 取得單一會員
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_users_edit.php?id_name=uid&amp;id={id} (GET 模式)
    /// </remarks>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<UserDetailResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        uint id,
        CancellationToken cancellationToken)
    {
        var user = await _userService.GetByIdAsync(id, cancellationToken);
        if (user is null)
        {
            return StatusCode(StatusCodes.Status404NotFound,
                ApiResponse.Error(StatusCodes.Status404NotFound, "找不到會員"));
        }

        return Ok(ApiResponse<UserDetailResponse>.Success(user));
    }

    /// <summary>
    /// 新增會員
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_users_edit.php (POST 無 id 分支)
    /// 注意:原 PHP 的新增表單缺少 uname 欄位、實際 INSERT 會因 NOT NULL 失敗;
    /// 本 API 補上 Account (uname) 欄位讓新增真正可用。
    /// 成功時回傳 HTTP 201 Created,body 為 ApiResponse&lt;{ id }&gt;。
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create(
        [FromBody] CreateUserRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _userService.CreateAsync(request, cancellationToken);
        if (!result.IsSuccess)
        {
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        }

        return StatusCode(
            StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { id = result.Data }, result.Message));
    }

    /// <summary>
    /// 更新會員
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_users_edit.php (POST 帶 id 分支)
    /// 密碼欄位為選填:null 或空字串代表不更新密碼(原 PHP 的預期行為)。
    /// 帳號 (uname) 不可修改,與原表單顯示為唯讀一致。
    /// </remarks>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Update(
        uint id,
        [FromBody] UpdateUserRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _userService.UpdateAsync(id, request, cancellationToken);
        if (!result.IsSuccess)
        {
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        }

        return Ok(ApiResponse<object>.Success(
            new { id = result.Data }, result.Message));
    }
}

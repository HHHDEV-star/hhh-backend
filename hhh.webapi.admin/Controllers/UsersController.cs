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
    /// 排序白名單：id, uname, email, name, regdate, lastLogin。
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<UserListResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetList(
        [FromQuery] UserListRequest request,
        CancellationToken cancellationToken)
    {
        var data = await _userService.GetListAsync(request, cancellationToken);
        return Ok(ApiResponse<UserListResponse>.Success(data));
    }
}

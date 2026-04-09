using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Auth;
using hhh.application.admin.Auth;
using Microsoft.AspNetCore.Mvc;

namespace hhh.webapi.admin.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// 管理後台登入
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/action/login.php
    /// 成功回傳 JWT Access Token 與登入者基本資料。
    /// </remarks>
    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResponse<LoginResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _authService.LoginAsync(request, cancellationToken);

        if (!result.IsSuccess)
        {
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        }

        return Ok(ApiResponse<LoginResponse>.Success(result.Data!, result.Message));
    }
}

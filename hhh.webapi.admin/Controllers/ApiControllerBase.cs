using hhh.api.contracts.Common;
using Microsoft.AspNetCore.Mvc;

namespace hhh.webapi.admin.Controllers;

/// <summary>
/// 所有 API Controller 的共同基底。
/// 把通用的 [ApiController]、[Produces] 和四種錯誤回應（400/401/403/500）統一掛在這層，
/// 衍生 controller 只需要宣告自己的路由與每個 action 的成功型別即可。
/// </summary>
[ApiController]
[Produces("application/json")]
[ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status403Forbidden)]
[ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status500InternalServerError)]
public abstract class ApiControllerBase : ControllerBase
{
}

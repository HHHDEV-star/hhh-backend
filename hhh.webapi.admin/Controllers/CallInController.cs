using hhh.api.contracts.Common;
using hhh.api.contracts.admin.CallIns;
using hhh.application.admin.CallIns;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hhh.webapi.admin.Controllers;

/// <summary>
/// 0809 來電管理 API
/// （對應舊版 PHP: hhh-api/.../third/v1/Callin.php）。
///
/// 兩個端點：
///   - GET  /api/call-ins        → 來電資料列表
///   - POST /api/call-ins        → 批次新增 0809 來電資料
/// </summary>
[Route("api/call-ins")]
[Authorize]
[Tags("CallIn")]
public class CallInController : ApiControllerBase
{
    private readonly ICallinDataService _callinDataService;

    public CallInController(ICallinDataService callinDataService)
    {
        _callinDataService = callinDataService;
    }

    /// <summary>取得 0809 來電資料列表（分頁）</summary>
    /// <remarks>
    /// 對應舊版 PHP: Callin/callin_get（9.2）
    /// 排序: activity_time DESC, designer_title DESC, callin_time ASC。
    ///
    /// 支援查詢條件:
    ///  - StartDate / EndDate:依 activity_time 篩選（含當日）
    ///  - CallinType:話單類型精確比對
    ///  - Phone:來電號碼模糊比對
    ///  - Extension:分機（對應 users_sn）精確比對
    ///  - Blacklist:true=僅黑名單、false=僅非黑名單、null=全部
    ///    （黑名單清單來自 appsettings.json:CallinBlacklist）
    /// </remarks>
    [HttpGet("list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<CallinDataListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetList(
        [FromQuery] CallinDataListQuery query,
        CancellationToken cancellationToken)
    {
        var data = await _callinDataService.GetListAsync(query, cancellationToken);
        return Ok(ApiResponse<PagedResponse<CallinDataListItem>>.Success(data));
    }

    /// <summary>批次新增 0809 來電資料</summary>
    /// <remarks>
    /// 對應舊版 PHP: Callin/callin_post（9.1）
    /// 以 activity_time + designer_title + callin_time + phone 四欄位做重複檢查,
    /// 僅新增不存在的紀錄。
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<BatchCreateCallinResult>), StatusCodes.Status201Created)]
    public async Task<IActionResult> BatchCreate(
        [FromBody] List<CallinDataItemRequest> items,
        CancellationToken cancellationToken)
    {
        var result = await _callinDataService.BatchCreateAsync(items, cancellationToken);

        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return StatusCode(
            StatusCodes.Status201Created,
            ApiResponse<BatchCreateCallinResult>.Created(result.Data!, result.Message));
    }
}

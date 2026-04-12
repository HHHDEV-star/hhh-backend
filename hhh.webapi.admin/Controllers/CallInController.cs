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

    /// <summary>取得 0809 來電資料列表</summary>
    /// <remarks>
    /// 對應舊版 PHP: Callin/callin_get（9.2）
    /// 全量回傳,不分頁。排序: activity_time DESC, designer_title DESC, callin_time ASC。
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<CallinDataListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetList(CancellationToken cancellationToken)
    {
        var data = await _callinDataService.GetListAsync(cancellationToken);
        return Ok(ApiResponse<List<CallinDataListItem>>.Success(data));
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

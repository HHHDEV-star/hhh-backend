using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Rss;
using hhh.application.admin.Rss.Yahoo;
using hhh.application.admin.Rss.Msn;
using hhh.application.admin.Rss.LineToday;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hhh.webapi.admin.Controllers;

/// <summary>
/// RSS
/// </summary>
/// <remarks>
/// 後台 RSS 排程管理。三個子資源:Yahoo / MSN / LineToday,
/// 各自對應一張獨立排程表。
///
/// 對應舊版:
///  - PHP:hhh-api/.../third/v1/Rss.php
///  - 後台 view:rss_yahoo.php / rss_msn.php / rss_linetoday.php
/// </remarks>
[Route("api/rss")]
[Authorize]
[Tags("Rss")]
public class RssController : ApiControllerBase
{
    private readonly IRssYahooService _yahooService;
    private readonly IRssMsnService _msnService;
    private readonly IRssLineTodayService _lineTodayService;

    public RssController(
        IRssYahooService yahooService,
        IRssMsnService msnService,
        IRssLineTodayService lineTodayService)
    {
        _yahooService = yahooService;
        _msnService = msnService;
        _lineTodayService = lineTodayService;
    }

    // =========================================================================
    // Yahoo
    // =========================================================================

    /// <summary>取得 Yahoo RSS 排程列表</summary>
    [HttpGet("yahoo")]
    [ProducesResponseType(typeof(ApiResponse<List<RssScheduleItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetYahooList(CancellationToken cancellationToken)
    {
        var data = await _yahooService.GetListAsync(cancellationToken);
        return Ok(ApiResponse<List<RssScheduleItem>>.Success(data));
    }

    /// <summary>新增 Yahoo RSS 排程</summary>
    [HttpPost("yahoo")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateYahoo(
        [FromBody] RssScheduleRequest request, CancellationToken cancellationToken)
    {
        var result = await _yahooService.CreateAsync(request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return StatusCode(StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { id = result.Data }, result.Message));
    }

    /// <summary>更新 Yahoo RSS 排程</summary>
    [HttpPut("yahoo/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateYahoo(
        uint id, [FromBody] RssScheduleRequest request, CancellationToken cancellationToken)
    {
        var result = await _yahooService.UpdateAsync(id, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { id = result.Data }, result.Message));
    }

    // =========================================================================
    // MSN
    // =========================================================================

    /// <summary>取得 MSN RSS 排程列表</summary>
    [HttpGet("msn")]
    [ProducesResponseType(typeof(ApiResponse<List<RssScheduleItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMsnList(CancellationToken cancellationToken)
    {
        var data = await _msnService.GetListAsync(cancellationToken);
        return Ok(ApiResponse<List<RssScheduleItem>>.Success(data));
    }

    /// <summary>新增 MSN RSS 排程</summary>
    [HttpPost("msn")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateMsn(
        [FromBody] RssScheduleRequest request, CancellationToken cancellationToken)
    {
        var result = await _msnService.CreateAsync(request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return StatusCode(StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { id = result.Data }, result.Message));
    }

    /// <summary>更新 MSN RSS 排程</summary>
    [HttpPut("msn/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateMsn(
        uint id, [FromBody] RssScheduleRequest request, CancellationToken cancellationToken)
    {
        var result = await _msnService.UpdateAsync(id, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { id = result.Data }, result.Message));
    }

    // =========================================================================
    // LineToday
    // =========================================================================

    /// <summary>取得 LineToday RSS 排程列表(最新 30 筆)</summary>
    [HttpGet("line-today")]
    [ProducesResponseType(typeof(ApiResponse<List<RssLineTodayItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLineTodayList(CancellationToken cancellationToken)
    {
        var data = await _lineTodayService.GetListAsync(cancellationToken);
        return Ok(ApiResponse<List<RssLineTodayItem>>.Success(data));
    }

    /// <summary>新增 LineToday RSS 排程</summary>
    [HttpPost("line-today")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateLineToday(
        [FromBody] RssLineTodayRequest request, CancellationToken cancellationToken)
    {
        var result = await _lineTodayService.CreateAsync(request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return StatusCode(StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { id = result.Data }, result.Message));
    }

    /// <summary>更新 LineToday RSS 排程</summary>
    [HttpPut("line-today/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateLineToday(
        uint id, [FromBody] RssLineTodayRequest request, CancellationToken cancellationToken)
    {
        var result = await _lineTodayService.UpdateAsync(id, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { id = result.Data }, result.Message));
    }
}

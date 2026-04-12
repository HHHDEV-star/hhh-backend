using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Rss;
using hhh.application.admin.Rss.LineToday;
using hhh.application.admin.Rss.Msn;
using hhh.application.admin.Rss.Transfer;
using hhh.application.admin.Rss.Yahoo;
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
///  - 後台 view:rss_yahoo.php / rss_msn.php / rss_linetoday.php / rss_lists.php
///  - Transfer:hhh-api/.../third/v1/Transfer.php → logs_get
/// </remarks>
[Route("api/rss")]
[Authorize]
[Tags("Rss")]
public class RssController : ApiControllerBase
{
    private readonly IRssYahooService _yahooService;
    private readonly IRssMsnService _msnService;
    private readonly IRssLineTodayService _lineTodayService;
    private readonly IRssTransferService _transferService;

    public RssController(
        IRssYahooService yahooService,
        IRssMsnService msnService,
        IRssLineTodayService lineTodayService,
        IRssTransferService transferService)
    {
        _yahooService = yahooService;
        _msnService = msnService;
        _lineTodayService = lineTodayService;
        _transferService = transferService;
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

    // =========================================================================
    // Transfer Logs (transfer-logs) — 轉接紀錄
    // =========================================================================

    /// <summary>取得 RSS 轉接紀錄</summary>
    /// <remarks>
    /// 對應舊版 PHP:Transfer/logs_get → transfer_model::read()
    /// 後台 view:rss_lists.php。至少帶一個日期參數才查(防全撈)。
    /// type 會從英文轉成中文(brand→廠商 等),url 由 type + num 計算。
    /// </remarks>
    [HttpGet("transfer-logs")]
    [ProducesResponseType(typeof(ApiResponse<List<RssTransferLogItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTransferLogs(
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        CancellationToken cancellationToken)
    {
        var data = await _transferService.GetLogsAsync(startDate, endDate, cancellationToken);
        return Ok(ApiResponse<List<RssTransferLogItem>>.Success(data));
    }

    /// <summary>取得 RSS 轉接統計</summary>
    /// <remarks>
    /// 對應舊版 PHP:Transfer/statistics_get → transfer_model::statistics()
    /// 後台 view:rss_statistics.php。GROUP BY type, num + COUNT(*)。
    /// type 轉中文,url 由 type + num 計算。可選帶日期區間。
    /// </remarks>
    [HttpGet("transfer-statistics")]
    [ProducesResponseType(typeof(ApiResponse<List<RssTransferStatItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTransferStatistics(
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        CancellationToken cancellationToken)
    {
        var data = await _transferService.GetStatisticsAsync(startDate, endDate, cancellationToken);
        return Ok(ApiResponse<List<RssTransferStatItem>>.Success(data));
    }
}

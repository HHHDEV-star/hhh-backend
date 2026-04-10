using hhh.api.contracts.Common;
using hhh.api.contracts.admin.CalculatorRequests;
using hhh.application.admin.CalculatorRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hhh.webapi.admin.Controllers;

[Route("api/calculator-requests")]
[Authorize]
public class CalculatorRequestsController : ApiControllerBase
{
    private readonly ICalculatorRequestService _calculatorRequestService;

    public CalculatorRequestsController(ICalculatorRequestService calculatorRequestService)
    {
        _calculatorRequestService = calculatorRequestService;
    }

    /// <summary>
    /// 取得裝修計算機需求列表
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP:hhh-api/application/controllers/third/v1/Calculator.php → requestindex_get
    /// 後台 view:hhh-backstage/backend/views/backend/event/calculator_request.php
    ///
    /// 支援查詢參數:
    ///  - startDate / endDate:依 create_time 過濾(yyyy-MM-dd,自動補時分秒)
    ///  - keyword:關鍵字搜尋
    ///      - 09 開頭視為手機,長度 10 會自動轉成 0912-345-678 後 LIKE phone
    ///      - 否則跨欄位 LIKE name / email / city / source_web / ca_type / h_class /
    ///        utm_source / utm_medium / utm_campaign / area
    ///      - 「無 / 同意 / 不同意」會額外比對 marketing_consent (2 / 1 / 0)
    ///
    /// 注意:目前沿用舊功能採「全量回傳 + 前端 Kendo Grid 自行分頁」,
    /// 不做 server-side paging。回傳會同時帶 searchCount(篩選後)與 allCount(全表)。
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<CalculatorRequestListResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetList(
        [FromQuery] CalculatorRequestListQuery query,
        CancellationToken cancellationToken)
    {
        var data = await _calculatorRequestService.GetListAsync(query, cancellationToken);
        return Ok(ApiResponse<CalculatorRequestListResponse>.Success(data));
    }
}

using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Brokers.Calculators;
using hhh.api.contracts.admin.Brokers.CalculatorRequests;
using hhh.api.contracts.admin.Brokers.Renovations;
using hhh.application.admin.Brokers.Calculators;
using hhh.application.admin.Brokers.CalculatorRequests;
using hhh.application.admin.Brokers.Renovations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hhh.webapi.admin.Controllers.Brokers;

/// <summary>
/// 經紀人
/// </summary>
/// <remarks>
/// 後台「經紀人」業務類別下所有 endpoint 集中於此 controller。
/// 服務各自獨立 (per-resource service),controller 只負責 dispatch。
///
/// 對應舊版後台 view:
///  - 裝修計算機:hhh-backstage/backend/views/backend/event/calculator.php
///  - 裝修需求預算單:hhh-backstage/backend/views/backend/event/calculator_request.php
///  - 裝修需求單:hhh-backstage/backend/views/backend/event/renovation.php
/// </remarks>
[Route("api/brokers")]
[Authorize]
[Tags("Brokers")]
public class BrokersController : ApiControllerBase
{
    private readonly ICalculatorService _calculatorService;
    private readonly ICalculatorRequestService _calculatorRequestService;
    private readonly IRenovationService _renovationService;

    public BrokersController(
        ICalculatorService calculatorService,
        ICalculatorRequestService calculatorRequestService,
        IRenovationService renovationService)
    {
        _calculatorService = calculatorService;
        _calculatorRequestService = calculatorRequestService;
        _renovationService = renovationService;
    }

    // -------------------------------------------------------------------------
    // 裝修計算機
    // -------------------------------------------------------------------------

    /// <summary>
    /// 取得裝修計算機列表
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP:hhh-api/.../third/v1/Calculator.php → index_get
    /// 業務規則:只回傳 contact_status='Y'(同意被聯繫)的紀錄,依 id DESC 排序、
    /// 全量回傳給前端 Kendo Grid 自行分頁。
    /// </remarks>
    [HttpGet("calculators")]
    [ProducesResponseType(typeof(ApiResponse<List<CalculatorListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCalculators(CancellationToken cancellationToken)
    {
        var data = await _calculatorService.GetListAsync(cancellationToken);
        return Ok(ApiResponse<List<CalculatorListItem>>.Success(data));
    }

    // -------------------------------------------------------------------------
    // 裝修需求預算單
    // -------------------------------------------------------------------------

    /// <summary>
    /// 取得裝修需求預算單列表
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP:hhh-api/.../third/v1/Calculator.php → requestindex_get
    ///
    /// 支援查詢參數:
    ///  - startDate / endDate:依 create_time 過濾(yyyy-MM-dd,自動補時分秒)
    ///  - keyword:關鍵字搜尋
    ///      - 09 開頭視為手機,長度 10 會自動轉成 0912-345-678 後 LIKE phone
    ///      - 否則跨欄位 LIKE name / email / city / source_web / ca_type / h_class /
    ///        utm_source / utm_medium / utm_campaign / area
    ///      - 「無 / 同意 / 不同意」會額外比對 marketing_consent (2 / 1 / 0)
    ///
    /// 注意:沿用舊功能採「全量回傳 + 前端 Kendo Grid 自行分頁」,
    /// 不做 server-side paging。回傳會同時帶 searchCount(篩選後)與 allCount(全表)。
    /// </remarks>
    [HttpGet("calculator-requests")]
    [ProducesResponseType(typeof(ApiResponse<CalculatorRequestListResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCalculatorRequests(
        [FromQuery] CalculatorRequestListQuery query,
        CancellationToken cancellationToken)
    {
        var data = await _calculatorRequestService.GetListAsync(query, cancellationToken);
        return Ok(ApiResponse<CalculatorRequestListResponse>.Success(data));
    }

    // -------------------------------------------------------------------------
    // 裝修需求單
    // -------------------------------------------------------------------------

    /// <summary>
    /// 取得裝修需求單列表
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP:hhh-api/.../third/v1/Renovation.php → index_get
    /// 業務規則:
    ///  - 從 renovation_reuqest 全量讀取,依 id DESC 排序
    ///  - 子查詢帶出 deco_record.company_name (僅當 bldsno != 0 才有值)
    ///  - site_lists 會把 DB 內 JSON 字串解析後以結構化方式回傳
    ///  - 沒有任何查詢條件、沒有 server-side paging,前端 Kendo Grid 自行分頁
    ///
    /// 注意:後端資料表為 legacy 拼錯的 renovation_reuqest,但 .NET 公開路由
    /// 與 DTO 都用乾淨命名 renovations / Renovation,不擴散拼錯。
    /// </remarks>
    [HttpGet("renovations")]
    [ProducesResponseType(typeof(ApiResponse<List<RenovationListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRenovations(CancellationToken cancellationToken)
    {
        var data = await _renovationService.GetListAsync(cancellationToken);
        return Ok(ApiResponse<List<RenovationListItem>>.Success(data));
    }
}

using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Agents;
using hhh.api.contracts.admin.Brokers.Calculators;
using hhh.api.contracts.admin.Brokers.CalculatorRequests;
using hhh.api.contracts.admin.Brokers.Decos;
using hhh.api.contracts.admin.Brokers.Renovations;
using hhh.application.admin.Agents;
using hhh.application.admin.Brokers.Calculators;
using hhh.application.admin.Brokers.CalculatorRequests;
using hhh.application.admin.Brokers.Decos;
using hhh.application.admin.Brokers.Renovations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hhh.webapi.admin.Controllers;

/// <summary>
/// 經紀人
/// </summary>
/// <remarks>
/// 後台「經紀人」業務類別下所有 endpoint 集中於此 controller。
/// 服務各自獨立 (per-resource service),controller 只負責 dispatch。
///
/// 對應舊版後台 view:
///  - 幸福經紀人:hhh-backstage/backend/views/backend/event/agent_list.php
///  - 裝修計算機:hhh-backstage/backend/views/backend/event/calculator.php
///  - 裝修需求預算單:hhh-backstage/backend/views/backend/event/calculator_request.php
///  - 裝修需求單:hhh-backstage/backend/views/backend/event/renovation.php
///  - 軟裝需求單:hhh-backstage/backend/views/backend/event/deco.php
/// </remarks>
[Route("api/brokers")]
[Authorize]
[Tags("Brokers")]
public class BrokersController : ApiControllerBase
{
    private readonly ICalculatorService _calculatorService;
    private readonly ICalculatorRequestService _calculatorRequestService;
    private readonly IRenovationService _renovationService;
    private readonly IDecoRequestService _decoRequestService;
    private readonly IAgentService _agentService;

    public BrokersController(
        ICalculatorService calculatorService,
        ICalculatorRequestService calculatorRequestService,
        IRenovationService renovationService,
        IDecoRequestService decoRequestService,
        IAgentService agentService)
    {
        _calculatorService = calculatorService;
        _calculatorRequestService = calculatorRequestService;
        _renovationService = renovationService;
        _decoRequestService = decoRequestService;
        _agentService = agentService;
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
    [HttpGet("calculators/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<CalculatorListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCalculatorList([FromQuery] ListQuery query, CancellationToken cancellationToken)
    {
        var data = await _calculatorService.GetListAsync(query, cancellationToken);
        return Ok(ApiResponse<PagedResponse<CalculatorListItem>>.Success(data));
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
    [HttpGet("calculator-requests/list")]
    [ProducesResponseType(typeof(ApiResponse<CalculatorRequestListResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCalculatorRequestList(
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
    [HttpGet("renovations/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<RenovationListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRenovationList([FromQuery] ListQuery query, CancellationToken cancellationToken)
    {
        var data = await _renovationService.GetListAsync(query, cancellationToken);
        return Ok(ApiResponse<PagedResponse<RenovationListItem>>.Success(data));
    }

    // -------------------------------------------------------------------------
    // 軟裝需求單 (deco_request)
    // -------------------------------------------------------------------------

    /// <summary>取得軟裝需求單列表（分頁）</summary>
    /// <remarks>
    /// 對應舊版 PHP:hhh-api/.../third/v2/Renovation.php → deco_get
    /// 業務規則:只回傳 payment_status='N' AND is_delete='N' 的資料;
    /// type 為空或 "全部" 時不過濾分類;排序固定 seq DESC。
    /// </remarks>
    [HttpGet("deco-requests/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<DecoRequestListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDecoRequestList(
        [FromQuery] DecoRequestListQuery query,
        CancellationToken cancellationToken)
    {
        var data = await _decoRequestService.GetListAsync(query, cancellationToken);
        return Ok(ApiResponse<PagedResponse<DecoRequestListItem>>.Success(data));
    }

    /// <summary>取得軟裝需求單明細(單筆)</summary>
    [HttpGet("deco-requests/{seq:int}")]
    [ProducesResponseType(typeof(ApiResponse<DecoRequestDetailResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDecoRequest(int seq, CancellationToken cancellationToken)
    {
        var result = await _decoRequestService.GetByIdAsync(seq, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<DecoRequestDetailResponse>.Success(result.Data!));
    }

    /// <summary>新增軟裝需求單</summary>
    /// <remarks>
    /// 對應舊版 PHP:Renovation.php → deco_by_backstage_post
    /// GUID 由後端自動產生,send_status 預設 'Y'。
    /// </remarks>
    [HttpPost("deco-requests")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateDecoRequest(
        [FromBody] CreateDecoRequestRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _decoRequestService.CreateAsync(request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return StatusCode(
            StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { seq = result.Data }, result.Message));
    }

    /// <summary>更新軟裝需求單</summary>
    /// <remarks>
    /// 對應舊版 PHP:Renovation.php → deco_by_backstage_put
    /// </remarks>
    [HttpPut("deco-requests/{seq:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateDecoRequest(
        int seq,
        [FromBody] UpdateDecoRequestRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _decoRequestService.UpdateAsync(seq, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(null!, result.Message));
    }

    /// <summary>批次軟刪除軟裝需求單</summary>
    /// <remarks>
    /// 對應舊版 PHP:Renovation.php → delete_put(將 is_delete 設為 'Y')。
    /// </remarks>
    [HttpDelete("deco-requests")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> BatchDeleteDecoRequests(
        [FromBody] BatchDeleteDecoRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _decoRequestService.BatchSoftDeleteAsync(request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(null!, result.Message));
    }

    /// <summary>取得軟裝需求單的附件清單</summary>
    /// <remarks>
    /// 對應舊版 PHP:Renovation.php → deco_files_get
    /// </remarks>
    [HttpGet("deco-requests/{seq:int}/files")]
    [ProducesResponseType(typeof(ApiResponse<List<DecoRequestFileItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDecoRequestFiles(int seq, CancellationToken cancellationToken)
    {
        var data = await _decoRequestService.GetFilesAsync(seq, cancellationToken);
        return Ok(ApiResponse<List<DecoRequestFileItem>>.Success(data));
    }

    /// <summary>批次新增軟裝需求單附件</summary>
    /// <remarks>
    /// 對應舊版 PHP:Renovation.php → deco_files_post
    /// 此 API 僅建立 DB 關聯紀錄,檔案上傳由前端或其他上傳端處理。
    /// </remarks>
    [HttpPost("deco-requests/{seq:int}/files")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateDecoRequestFiles(
        int seq,
        [FromBody] CreateDecoRequestFilesRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _decoRequestService.CreateFilesAsync(seq, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(null!, result.Message));
    }

    /// <summary>批次刪除軟裝需求單附件</summary>
    /// <remarks>
    /// 對應舊版 PHP:Renovation.php → deco_files_delete(硬刪除)。
    /// </remarks>
    [HttpDelete("deco-request-files")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteDecoRequestFiles(
        [FromBody] BatchDeleteDecoRequestFiles request,
        CancellationToken cancellationToken)
    {
        var result = await _decoRequestService.DeleteFilesAsync(request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(null!, result.Message));
    }

    /// <summary>付款通知(更新金額並標記已付款)</summary>
    /// <remarks>
    /// 對應舊版 PHP:Renovation.php → set_price_post
    /// 舊版會寄 email 給客戶(CC 經紀人 + 技術主管),.NET 尚未整合 SMTP,
    /// 目前僅更新 DB + 寫 _hoplog 紀錄,待 SMTP 接通後補送 email。
    /// </remarks>
    [HttpPost("deco-requests/{seq:int}/set-price")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SetDecoPrice(
        int seq,
        [FromBody] SetDecoPriceRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _decoRequestService.SetPriceAsync(seq, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(null!, result.Message));
    }

    // -------------------------------------------------------------------------
    // 幸福經紀人
    // -------------------------------------------------------------------------

    /// <summary>取得經紀人分頁列表</summary>
    /// <remarks>
    /// 對應舊版 PHP:hhh-api/.../base/v1/Agent.php → lists_get
    /// 支援 startDate / endDate（依 date_added 篩選）、keyword（手機/多欄位模糊搜尋）。
    /// 分頁參數來自 ListQuery（page / pageSize）。
    /// </remarks>
    [HttpGet("agents/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<AgentListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAgentList(
        [FromQuery] AgentQuery query,
        [FromQuery] ListQuery listQuery,
        CancellationToken cancellationToken)
    {
        var data = await _agentService.GetListAsync(query, listQuery, cancellationToken);
        return Ok(ApiResponse<PagedResponse<AgentListItem>>.Success(data));
    }

    /// <summary>軟刪除經紀人</summary>
    /// <remarks>
    /// 對應舊版 PHP:Agent.php → lists_delete（設定 is_del = 1）。
    /// </remarks>
    [HttpDelete("agents/{agentId:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAgent(uint agentId, CancellationToken cancellationToken)
    {
        var result = await _agentService.DeleteAsync(agentId, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(null!, result.Message));
    }

    /// <summary>取得經紀人附件列表</summary>
    /// <remarks>
    /// 對應舊版 PHP:Agent.php → files_get。
    /// </remarks>
    [HttpGet("agents/{agentId:int}/files")]
    [ProducesResponseType(typeof(ApiResponse<List<AgentFileListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAgentFiles(uint agentId, CancellationToken cancellationToken)
    {
        var data = await _agentService.GetFilesAsync(agentId, cancellationToken);
        return Ok(ApiResponse<List<AgentFileListItem>>.Success(data));
    }

    /// <summary>軟刪除經紀人附件</summary>
    /// <remarks>
    /// 對應舊版 PHP:Agent.php → files_delete（設定 is_del = 1）。
    /// </remarks>
    [HttpDelete("agent-files/{fileId:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAgentFile(uint fileId, CancellationToken cancellationToken)
    {
        var result = await _agentService.DeleteFileAsync(fileId, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(null!, result.Message));
    }

    /// <summary>取得經紀人表單詳情（單筆）</summary>
    /// <remarks>
    /// 對應舊版 PHP:Agent.php → form_get。
    /// JSON 欄位（need_item, family, need_style, need_update_array, agent_where）
    /// 會自動解碼為結構化物件。
    /// </remarks>
    [HttpGet("agents/{agentId:int}")]
    [ProducesResponseType(typeof(ApiResponse<AgentDetailResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAgent(uint agentId, CancellationToken cancellationToken)
    {
        var result = await _agentService.GetByIdAsync(agentId, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<AgentDetailResponse>.Success(result.Data!));
    }

    /// <summary>新增經紀人表單</summary>
    /// <remarks>
    /// 對應舊版 PHP:Agent.php → form_post → Agent_model::insert。
    /// 複合欄位（NeedItem, Family, NeedStyle 等）以結構化 JSON 傳入，
    /// 後端自動序列化存入 DB。
    /// </remarks>
    [HttpPost("agents")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAgent(
        [FromBody] CreateAgentRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _agentService.CreateAsync(request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return StatusCode(
            StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { agentId = result.Data }, result.Message));
    }

    /// <summary>更新經紀人表單</summary>
    /// <remarks>
    /// 對應舊版 PHP:Agent.php → form_put → Agent_model::update。
    /// </remarks>
    [HttpPut("agents/{agentId:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAgent(
        uint agentId,
        [FromBody] UpdateAgentRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _agentService.UpdateAsync(agentId, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(null!, result.Message));
    }
}

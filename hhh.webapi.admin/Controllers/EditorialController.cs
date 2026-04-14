using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Editorial.Cases;
using hhh.api.contracts.admin.Editorial.Columns;
using hhh.application.admin.Editorial.Cases;
using hhh.application.admin.Editorial.Columns;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hhh.webapi.admin.Controllers;

/// <summary>
/// 編輯部
/// </summary>
/// <remarks>
/// 後台「編輯部」業務類別下所有 endpoint 集中於此 controller。
/// 服務各自獨立 (per-resource service),controller 只負責 dispatch。
///
/// 對應舊版:
///  - 個案:hhh-api/.../base/v1/Cases.php、hhh-backstage/.../event/case_lists.php
///  - 專欄:hhh-api/.../base/v1/Column.php、hhh-backstage/.../event/column_lists.php
///
/// 注意:Cases 跟 Designers (舊 XOOPS _hcase.php 路線)共用同一張 _hcase 表,
/// 但是兩條不同工作流(編輯部 vs 設計師管理)。詳見各 service 註解。
/// </remarks>
[Route("api/editorial")]
[Authorize]
[Tags("Editorial")]
public class EditorialController : ApiControllerBase
{
    private readonly IEditorialCaseService _editorialCaseService;
    private readonly IEditorialColumnService _editorialColumnService;

    public EditorialController(
        IEditorialCaseService editorialCaseService,
        IEditorialColumnService editorialColumnService)
    {
        _editorialCaseService = editorialCaseService;
        _editorialColumnService = editorialColumnService;
    }

    // =========================================================================
    // 個案 (cases)
    // =========================================================================

    /// <summary>取得個案列表(分頁)</summary>
    /// <remarks>
    /// 對應舊版 PHP:Cases.php → index_get(no id) → case_model::get_case_lists()
    /// 依 sdate DESC、hcase_id DESC 排序。
    /// 列表會 JOIN _hdesigner 帶出 designerTitle。
    /// </remarks>
    [HttpGet("cases")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<EditorialCaseListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCaseList([FromQuery] ListQuery query, CancellationToken cancellationToken)
    {
        var data = await _editorialCaseService.GetListAsync(query, cancellationToken);
        return Ok(ApiResponse<PagedResponse<EditorialCaseListItem>>.Success(data));
    }

    /// <summary>取得單一個案完整資料</summary>
    /// <remarks>
    /// 對應舊版 PHP:Cases.php → index_get(with id) → case_model::get_case_info($id)
    /// 舊版只回 12 個編輯欄位,本 API 為 REST 一致性回完整 case(列表 17 + 編輯 12 = 29 欄)。
    /// </remarks>
    [HttpGet("cases/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<EditorialCaseDetailResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCaseById(uint id, CancellationToken cancellationToken)
    {
        var detail = await _editorialCaseService.GetByIdAsync(id, cancellationToken);
        if (detail is null)
        {
            return StatusCode(StatusCodes.Status404NotFound,
                ApiResponse.Error(StatusCodes.Status404NotFound, "找不到個案"));
        }

        return Ok(ApiResponse<EditorialCaseDetailResponse>.Success(detail));
    }

    /// <summary>新增個案</summary>
    /// <remarks>
    /// 對應舊版 PHP:Cases.php → index_post → case_model::insert_case_data()
    /// 舊版必填:onoff、caption、type、style、condition。
    /// 寫入時系統自動設定:recommend=0、corder=0、is_send=0、auto_count_fee=0、
    /// creat_time=now、tag_datetime=now、sdate(空白時)=今天。
    ///
    /// 注意:舊版是 batch(POST 帶 request[]),本 API 改成 single record(REST 標準)。
    /// 前端如果需要批次新增多筆,自行 loop 呼叫即可。
    /// </remarks>
    [HttpPost("cases")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateCase(
        [FromBody] CreateEditorialCaseRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _editorialCaseService.CreateAsync(request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return StatusCode(
            StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { id = result.Data }, result.Message));
    }

    /// <summary>更新個案</summary>
    /// <remarks>
    /// 對應舊版 PHP:Cases.php → index_put → case_model::update_case_data()
    /// 舊版必填:sdate、onoff、caption、type、style、condition、hcase_id(本 API 走 URL)。
    ///
    /// 注意:
    ///  1. 舊版會強制把 recommend 重設為 0(看起來像 bug,但保留行為避免破壞編輯部既有流程)
    ///  2. 不會動到 corder / is_send / auto_count_fee / creat_time / tag_datetime
    ///  3. 舊版是 batch(PUT 帶 request[]),本 API 改成 single record
    /// </remarks>
    [HttpPut("cases/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCase(
        uint id,
        [FromBody] UpdateEditorialCaseRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _editorialCaseService.UpdateAsync(id, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { id = result.Data }, result.Message));
    }

    // =========================================================================
    // 專欄 (columns)
    // =========================================================================

    /// <summary>取得專欄列表(分頁)</summary>
    /// <remarks>
    /// 對應舊版 PHP:Column.php → index_get(no id) → column_model::get_column_lists()
    /// 依 sdate DESC、hcolumn_id DESC 排序。
    /// </remarks>
    [HttpGet("columns")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<EditorialColumnListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetColumnList([FromQuery] ListQuery query, CancellationToken cancellationToken)
    {
        var data = await _editorialColumnService.GetListAsync(query, cancellationToken);
        return Ok(ApiResponse<PagedResponse<EditorialColumnListItem>>.Success(data));
    }

    /// <summary>取得單一專欄完整資料</summary>
    /// <remarks>
    /// 對應舊版 PHP:Column.php → index_get(with id) → column_model::get_column_page_content($id)
    /// 舊版只回 page_content 一個欄位,本 API 為 REST 一致性回完整 column。
    /// </remarks>
    [HttpGet("columns/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<EditorialColumnDetailResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetColumnById(uint id, CancellationToken cancellationToken)
    {
        var detail = await _editorialColumnService.GetByIdAsync(id, cancellationToken);
        if (detail is null)
        {
            return StatusCode(StatusCodes.Status404NotFound,
                ApiResponse.Error(StatusCodes.Status404NotFound, "找不到專欄"));
        }

        return Ok(ApiResponse<EditorialColumnDetailResponse>.Success(detail));
    }

    /// <summary>新增專欄</summary>
    /// <remarks>
    /// 對應舊版 PHP:Column.php → index_post → column_model::insert_column_data()
    /// 舊版必填:onoff、ctitle、ctype、cdesc、extend_str。
    /// 寫入時系統自動設定:recommend=0、is_send=0、creat_time=now、tag_datetime=now、
    /// sdate(空白時)=今天。
    ///
    /// 注意:舊版是 batch(POST 帶 request[]),本 API 改成 single record。
    /// </remarks>
    [HttpPost("columns")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateColumn(
        [FromBody] CreateEditorialColumnRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _editorialColumnService.CreateAsync(request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return StatusCode(
            StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { id = result.Data }, result.Message));
    }

    /// <summary>更新專欄</summary>
    /// <remarks>
    /// 對應舊版 PHP:Column.php → index_put → column_model::update_column_data()
    /// 舊版必填:onoff、hcolumn_id(本 API hcolumn_id 走 URL,所以 body 內僅 onoff 必填)。
    /// 舊版 PUT 比 POST 寬鬆很多 — 大部分欄位都可選填。
    ///
    /// 注意:
    ///  1. 舊版會強制把 recommend 重設為 0(跟 Cases 同一個 legacy bug 行為,保留)
    ///  2. 舊版是 batch(PUT 帶 request[]),本 API 改成 single record
    /// </remarks>
    [HttpPut("columns/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateColumn(
        uint id,
        [FromBody] UpdateEditorialColumnRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _editorialColumnService.UpdateAsync(id, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { id = result.Data }, result.Message));
    }
}

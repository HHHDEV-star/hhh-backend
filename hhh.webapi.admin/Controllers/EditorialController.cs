using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Editorial.Cases;
using hhh.api.contracts.admin.Editorial.Columns;
using hhh.api.contracts.admin.Editorial.Topics;
using hhh.application.admin.Editorial.Cases;
using hhh.application.admin.Editorial.Columns;
using hhh.application.admin.Editorial.Topics;
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
    private readonly ICaseImageService _caseImageService;
    private readonly IEditorialColumnService _editorialColumnService;
    private readonly IHtopicService _htopicService;

    public EditorialController(
        IEditorialCaseService editorialCaseService,
        ICaseImageService caseImageService,
        IEditorialColumnService editorialColumnService,
        IHtopicService htopicService)
    {
        _editorialCaseService = editorialCaseService;
        _caseImageService = caseImageService;
        _editorialColumnService = editorialColumnService;
        _htopicService = htopicService;
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
    [HttpGet("cases/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<EditorialCaseListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCaseList([FromQuery] EditorialCaseListQuery query, CancellationToken cancellationToken)
    {
        var data = await _editorialCaseService.GetListAsync(query, cancellationToken);
        return Ok(ApiResponse<PagedResponse<EditorialCaseListItem>>.Success(data));
    }

    /// <summary>取得個案下拉選單（僅上線中，不分頁）</summary>
    /// <remarks>
    /// 供關聯選擇使用（如首頁區塊設定、RSS 排程、SEO 管理等需要選擇個案的場景）。
    /// 僅回傳 onoff=1 的個案，依 sdate DESC 排序。
    /// 可選 keyword 做標題即時過濾（前端 combo-box 輸入時觸發）。
    /// </remarks>
    [HttpGet("cases/select-list")]
    [ProducesResponseType(typeof(ApiResponse<List<CaseSelectItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCaseSelectList(
        [FromQuery] string? keyword, CancellationToken cancellationToken)
    {
        var data = await _editorialCaseService.GetSelectListAsync(keyword, cancellationToken);
        return Ok(ApiResponse<List<CaseSelectItem>>.Success(data));
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
    // 個案圖片 (case-images)
    // =========================================================================

    /// <summary>取得個案圖片列表</summary>
    /// <remarks>
    /// 對應舊版 PHP: Cases/image GET → case_model::get_case_imgs()
    /// 依 order ASC 排序。
    /// </remarks>
    [HttpGet("cases/{hcaseId:int}/images/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<CaseImageListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCaseImageList(
        uint hcaseId, [FromQuery] PagedRequest query, CancellationToken cancellationToken)
    {
        var data = await _caseImageService.GetListAsync(hcaseId, query, cancellationToken);
        return Ok(ApiResponse<PagedResponse<CaseImageListItem>>.Success(data));
    }

    /// <summary>新增個案圖片</summary>
    /// <remarks>
    /// 對應舊版 PHP: Cases/image POST → case_model::insert_case_imgs()
    /// 前端先上傳圖片到圖片伺服器取得 URL，再呼叫此端點建立紀錄。
    /// </remarks>
    [HttpPost("cases/{hcaseId:int}/images")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateCaseImage(
        uint hcaseId,
        [FromBody] CreateCaseImageRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _caseImageService.CreateAsync(hcaseId, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return StatusCode(StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { id = result.Data }, result.Message));
    }

    /// <summary>更新個案圖片資訊</summary>
    /// <remarks>
    /// 對應舊版 PHP: Cases/image PUT → case_model::update_case_imgs()
    /// 可更新標題/描述/排序/Tag1-5/平面圖/3D示意圖。
    /// </remarks>
    [HttpPut("cases/{hcaseId:int}/images/{imgId:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCaseImage(
        uint hcaseId, uint imgId,
        [FromBody] UpdateCaseImageRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _caseImageService.UpdateAsync(imgId, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }

    /// <summary>刪除個案圖片</summary>
    /// <remarks>
    /// 對應舊版 PHP: Cases/image DELETE
    /// 硬刪除。
    /// </remarks>
    [HttpDelete("cases/{hcaseId:int}/images/{imgId:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCaseImage(
        uint hcaseId, uint imgId, CancellationToken cancellationToken)
    {
        var result = await _caseImageService.DeleteAsync(imgId, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }

    /// <summary>設定個案封面圖</summary>
    /// <remarks>
    /// 對應舊版 PHP: Cases/image PUT (cover)
    /// 將指定圖片設為封面，同時更新 _hcase.cover 欄位。
    /// </remarks>
    [HttpPost("cases/{hcaseId:int}/images/set-cover")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SetCaseCover(
        uint hcaseId,
        [FromBody] SetCaseCoverRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _caseImageService.SetCoverAsync(hcaseId, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }

    /// <summary>重新排序個案圖片</summary>
    /// <remarks>
    /// 對應舊版 PHP: Cases/image_order PUT
    /// 依目前 order 排序後，重新編號為連續序號 1, 2, 3...
    /// </remarks>
    [HttpPost("cases/{hcaseId:int}/images/reorder")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ReorderCaseImages(
        uint hcaseId, CancellationToken cancellationToken)
    {
        var result = await _caseImageService.ReorderAsync(hcaseId, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }

    // =========================================================================
    // 專欄 (columns)
    // =========================================================================

    /// <summary>取得專欄列表(分頁)</summary>
    /// <remarks>
    /// 對應舊版 PHP:Column.php → index_get(no id) → column_model::get_column_lists()
    /// 依 sdate DESC、hcolumn_id DESC 排序。
    /// </remarks>
    [HttpGet("columns/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<EditorialColumnListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetColumnList([FromQuery] EditorialColumnListQuery query, CancellationToken cancellationToken)
    {
        var data = await _editorialColumnService.GetListAsync(query, cancellationToken);
        return Ok(ApiResponse<PagedResponse<EditorialColumnListItem>>.Success(data));
    }

    /// <summary>取得專欄下拉選單（僅上線中，不分頁）</summary>
    /// <remarks>
    /// 供關聯選擇使用（如 RSS 排程、首頁區塊設定等需要選擇專欄的場景）。
    /// 僅回傳 onoff=1 的專欄，依 sdate DESC 排序。
    /// 可選 keyword 做標題即時過濾、ctype 做類別篩選。
    /// </remarks>
    [HttpGet("columns/select-list")]
    [ProducesResponseType(typeof(ApiResponse<List<ColumnSelectItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetColumnSelectList(
        [FromQuery] string? keyword, [FromQuery] string? ctype, CancellationToken cancellationToken)
    {
        var data = await _editorialColumnService.GetSelectListAsync(keyword, ctype, cancellationToken);
        return Ok(ApiResponse<List<ColumnSelectItem>>.Success(data));
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

    // =========================================================================
    // 主題 (topics)
    // =========================================================================

    /// <summary>取得主題列表(分頁)</summary>
    /// <remarks>
    /// 對應舊版 PHP:hhh-admin/_htopic.php 列表頁
    /// 支援關鍵字搜尋(q):可搜 ID、名稱、主題敘述。
    /// 預設依 htopic_id DESC 排序。
    /// </remarks>
    [HttpGet("topics/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<HtopicListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTopicList(
        [FromQuery] HtopicListQuery query, CancellationToken cancellationToken)
    {
        var data = await _htopicService.GetListAsync(query, cancellationToken);
        return Ok(ApiResponse<PagedResponse<HtopicListItem>>.Success(data));
    }

    /// <summary>取得單一主題完整資料</summary>
    /// <remarks>
    /// 對應舊版 PHP:hhh-admin/_htopic_edit.php 編輯頁（讀取模式）
    /// </remarks>
    [HttpGet("topics/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<HtopicDetailResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTopicById(uint id, CancellationToken cancellationToken)
    {
        var detail = await _htopicService.GetByIdAsync(id, cancellationToken);
        if (detail is null)
        {
            return StatusCode(StatusCodes.Status404NotFound,
                ApiResponse.Error(StatusCodes.Status404NotFound, "找不到主題"));
        }

        return Ok(ApiResponse<HtopicDetailResponse>.Success(detail));
    }

    /// <summary>新增主題</summary>
    /// <remarks>
    /// 對應舊版 PHP:hhh-admin/_htopic_edit.php POST（id 為空時新增）
    /// 必填:title、onoff。
    /// </remarks>
    [HttpPost("topics")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateTopic(
        [FromBody] CreateHtopicRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _htopicService.CreateAsync(request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return StatusCode(
            StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { id = result.Data }, result.Message));
    }

    /// <summary>更新主題</summary>
    /// <remarks>
    /// 對應舊版 PHP:hhh-admin/_htopic_edit.php POST（id 非空時更新）
    /// 寫入時自動更新 update_time。
    /// </remarks>
    [HttpPut("topics/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateTopic(
        uint id,
        [FromBody] UpdateHtopicRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _htopicService.UpdateAsync(id, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { id = result.Data }, result.Message));
    }

    /// <summary>刪除主題</summary>
    /// <remarks>
    /// 對應舊版 PHP:hhh-admin/_htopic.php?delete_id=xxx（hard delete）
    /// </remarks>
    [HttpDelete("topics/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTopic(uint id, CancellationToken cancellationToken)
    {
        var result = await _htopicService.DeleteAsync(id, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }
}

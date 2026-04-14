using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Main.Execute;
using hhh.api.contracts.admin.Main.Search;
using hhh.application.admin.Main.Execute;
using hhh.application.admin.Main.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hhh.webapi.admin.Controllers.Main;

/// <summary>
/// Main
/// </summary>
/// <remarks>
/// 後台「Main」業務類別下所有 endpoint 集中於此 controller。
///
/// 對應舊版:
///  - Search:hhh-api/.../base/v1/Search.php、hhh-backstage/.../event/keyword.php + searchall.php
///  - Execute:hhh-api/.../base/v1/Execute.php、hhh-backstage/.../event/execute.php
/// </remarks>
[Route("api/main")]
[Authorize]
[Tags("Main")]
public class MainController : ApiControllerBase
{
    private readonly ISearchService _searchService;
    private readonly IExecuteFormService _executeFormService;

    public MainController(
        ISearchService searchService,
        IExecuteFormService executeFormService)
    {
        _searchService = searchService;
        _executeFormService = executeFormService;
    }

    // =========================================================================
    // Search (search)
    // =========================================================================

    /// <summary>取得搜尋標籤</summary>
    /// <remarks>
    /// 對應舊版 PHP:Search/keyword_get → search_model::get_keyword_lists()
    /// 從 site_setup.all_search_tag 讀取,逗號分隔後回傳字串陣列。
    /// </remarks>
    [HttpGet("search/keywords")]
    [ProducesResponseType(typeof(ApiResponse<List<string>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetSearchKeywordTags(CancellationToken cancellationToken)
    {
        var data = await _searchService.GetKeywordTagsAsync(cancellationToken);
        return Ok(ApiResponse<List<string>>.Success(data));
    }

    /// <summary>取得熱門關鍵字</summary>
    /// <remarks>
    /// 對應舊版 PHP:Search/hot_keyword_get → search_model::get_search_history_lists()
    /// GROUP BY keyword,依搜尋次數 DESC 排序。
    /// </remarks>
    [HttpGet("search/hot-keywords")]
    [ProducesResponseType(typeof(ApiResponse<List<SearchKeywordItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetHotKeywords(CancellationToken cancellationToken)
    {
        var data = await _searchService.GetHotKeywordsAsync(cancellationToken);
        return Ok(ApiResponse<List<SearchKeywordItem>>.Success(data));
    }

    /// <summary>取得自動完成資料</summary>
    /// <remarks>
    /// 對應舊版 PHP:Search/autocomplete_get → search_model::get_autocomplete_data()
    /// 合併 _hdesigner(onoff=1)的 title/name + _hbrand(onoff=1)的 title。
    /// </remarks>
    [HttpGet("search/autocomplete")]
    [ProducesResponseType(typeof(ApiResponse<List<AutocompleteItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAutocomplete(CancellationToken cancellationToken)
    {
        var data = await _searchService.GetAutocompleteAsync(cancellationToken);
        return Ok(ApiResponse<List<AutocompleteItem>>.Success(data));
    }

    /// <summary>後台搜尋(跨 6 種類型)</summary>
    /// <remarks>
    /// 對應舊版 PHP:Search/lists_back_get → searchall_model::get_xxx_lists()
    /// type 必填(case / column / designer / video / brand / product),
    /// keyword 選填(跨欄位 OR LIKE)。
    /// 回傳統一格式的 id + title(+ subTitle for designer)。
    /// </remarks>
    [HttpGet("search/backend")]
    [ProducesResponseType(typeof(ApiResponse<List<SearchBackendResultItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> SearchBackend(
        [FromQuery] SearchBackendQuery query,
        CancellationToken cancellationToken)
    {
        var data = await _searchService.SearchBackendAsync(query, cancellationToken);
        return Ok(ApiResponse<List<SearchBackendResultItem>>.Success(data));
    }

    // =========================================================================
    // Execute (execute-forms) — 執行表單
    // =========================================================================

    /// <summary>取得執行表單列表</summary>
    /// <remarks>對應舊版 Execute/index_get(no exf_id)。篩 is_delete='N',exf_id DESC。</remarks>
    [HttpGet("execute-forms/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<ExecuteFormListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetExecuteFormList([FromQuery] ListQuery query, CancellationToken cancellationToken)
    {
        var data = await _executeFormService.GetListAsync(query, cancellationToken);
        return Ok(ApiResponse<PagedResponse<ExecuteFormListItem>>.Success(data));
    }

    /// <summary>取得單一執行表單</summary>
    /// <remarks>對應舊版 Execute/index_get(with exf_id)。</remarks>
    [HttpGet("execute-forms/{exfId:int}")]
    [ProducesResponseType(typeof(ApiResponse<ExecuteFormListItem>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetExecuteFormById(uint exfId, CancellationToken cancellationToken)
    {
        var detail = await _executeFormService.GetByIdAsync(exfId, cancellationToken);
        if (detail is null)
        {
            return StatusCode(StatusCodes.Status404NotFound,
                ApiResponse.Error(StatusCodes.Status404NotFound, "找不到執行表單"));
        }

        return Ok(ApiResponse<ExecuteFormListItem>.Success(detail));
    }

    /// <summary>新增執行表單</summary>
    /// <remarks>
    /// 對應舊版 Execute/index_post。
    /// 舊版必填:num、company、designer、contract_time、sales_man、is_close、detail_status。
    /// 舊版是 batch,本 API 改成 single record。
    /// </remarks>
    [HttpPost("execute-forms")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateExecuteForm(
        [FromBody] CreateExecuteFormRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _executeFormService.CreateAsync(request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return StatusCode(StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { exfId = result.Data }, result.Message));
    }

    /// <summary>更新執行表單</summary>
    /// <remarks>
    /// 對應舊版 Execute/index_put。
    /// 舊版是 batch,本 API 改成 single record。所有欄位選填(有給才更新)。
    /// </remarks>
    [HttpPut("execute-forms/{exfId:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateExecuteForm(
        uint exfId,
        [FromBody] UpdateExecuteFormRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _executeFormService.UpdateAsync(exfId, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { exfId = result.Data }, result.Message));
    }

    /// <summary>刪除執行表單(軟刪除 is_delete='Y')</summary>
    /// <remarks>對應舊版 Execute/delete_put。</remarks>
    [HttpDelete("execute-forms/{exfId:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteExecuteForm(uint exfId, CancellationToken cancellationToken)
    {
        var result = await _executeFormService.SoftDeleteAsync(exfId, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }
}

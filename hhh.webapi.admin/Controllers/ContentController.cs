using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Content.Hpublishes;
using hhh.api.contracts.admin.Content.Htopic2s;
using hhh.application.admin.Content.Hpublishes;
using hhh.application.admin.Content.Htopic2s;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hhh.webapi.admin.Controllers;

/// <summary>
/// 內容
/// </summary>
/// <remarks>
/// 後台「內容」業務類別下所有 endpoint 集中於此 controller。
/// 包含出版品 + 議題 2。
///
/// 對應舊版 PHP:
///  - 出版:/backend/_hpublish.php、_hpublish_edit.php
///  - 議題2:/backend/_htopic2.php、_htopic2_edit.php
/// </remarks>
[Route("api/content")]
[Authorize]
[Tags("Content")]
public class ContentController : ApiControllerBase
{
    private readonly IHpublishService _hpublishService;
    private readonly IHtopic2Service _htopic2Service;

    public ContentController(
        IHpublishService hpublishService,
        IHtopic2Service htopic2Service)
    {
        _hpublishService = hpublishService;
        _htopic2Service = htopic2Service;
    }

    // =========================================================================
    // 出版 (hpublishes)
    // =========================================================================

    /// <summary>取得出版分頁列表</summary>
    /// <remarks>
    /// 對應舊版 /backend/_hpublish.php。
    /// q 跨欄位:id / title / author / type / desc。
    /// 排序白名單:id, title, author, type, pdate, viewed, recommend。
    /// </remarks>
    [HttpGet("hpublishes/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<HpublishListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetHpublishList(
        [FromQuery] HpublishListRequest request,
        CancellationToken cancellationToken)
    {
        var data = await _hpublishService.GetListAsync(request, cancellationToken);
        return Ok(ApiResponse<PagedResponse<HpublishListItem>>.Success(data));
    }

    /// <summary>取得單一出版完整資料</summary>
    /// <remarks>對應舊版 /backend/_hpublish_edit.php?id={id} (GET)。</remarks>
    [HttpGet("hpublishes/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<HpublishDetailResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetHpublishById(uint id, CancellationToken cancellationToken)
    {
        var detail = await _hpublishService.GetByIdAsync(id, cancellationToken);
        if (detail is null)
        {
            return StatusCode(StatusCodes.Status404NotFound,
                ApiResponse.Error(StatusCodes.Status404NotFound, "找不到出版資料"));
        }

        return Ok(ApiResponse<HpublishDetailResponse>.Success(detail));
    }

    /// <summary>新增出版</summary>
    /// <remarks>對應舊版 /backend/_hpublish_edit.php (POST 無 id 分支)。</remarks>
    [HttpPost("hpublishes")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateHpublish(
        [FromBody] CreateHpublishRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _hpublishService.CreateAsync(request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return StatusCode(
            StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { id = result.Data }, result.Message));
    }

    /// <summary>更新出版</summary>
    /// <remarks>對應舊版 /backend/_hpublish_edit.php (POST 帶 id 分支)。viewed 不會被覆寫,保留原始前台累計值。</remarks>
    [HttpPut("hpublishes/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateHpublish(
        uint id,
        [FromBody] UpdateHpublishRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _hpublishService.UpdateAsync(id, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { id = result.Data }, result.Message));
    }

    /// <summary>刪除出版</summary>
    /// <remarks>對應舊版 /backend/_hpublish.php?delete_id={id}。</remarks>
    [HttpDelete("hpublishes/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteHpublish(uint id, CancellationToken cancellationToken)
    {
        var result = await _hpublishService.DeleteAsync(id, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }

    // =========================================================================
    // 議題 2 (htopic2s)
    // =========================================================================

    /// <summary>取得議題 2 分頁列表</summary>
    /// <remarks>對應舊版 /backend/_htopic2.php。q / onoff 過濾。排序白名單:id, title, onoff。</remarks>
    [HttpGet("htopic2s/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<Htopic2ListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetHtopic2List(
        [FromQuery] Htopic2ListRequest request,
        CancellationToken cancellationToken)
    {
        var data = await _htopic2Service.GetListAsync(request, cancellationToken);
        return Ok(ApiResponse<PagedResponse<Htopic2ListItem>>.Success(data));
    }

    /// <summary>取得單一議題 2 完整資料</summary>
    /// <remarks>對應舊版 /backend/_htopic2_edit.php?id={id} (GET)。</remarks>
    [HttpGet("htopic2s/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<Htopic2DetailResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetHtopic2ById(uint id, CancellationToken cancellationToken)
    {
        var detail = await _htopic2Service.GetByIdAsync(id, cancellationToken);
        if (detail is null)
        {
            return StatusCode(StatusCodes.Status404NotFound,
                ApiResponse.Error(StatusCodes.Status404NotFound, "找不到議題 2 資料"));
        }

        return Ok(ApiResponse<Htopic2DetailResponse>.Success(detail));
    }

    /// <summary>新增議題 2</summary>
    /// <remarks>對應舊版 /backend/_htopic2_edit.php (POST 無 id 分支)。</remarks>
    [HttpPost("htopic2s")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateHtopic2(
        [FromBody] CreateHtopic2Request request,
        CancellationToken cancellationToken)
    {
        var result = await _htopic2Service.CreateAsync(request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return StatusCode(
            StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { id = result.Data }, result.Message));
    }

    /// <summary>更新議題 2</summary>
    /// <remarks>對應舊版 /backend/_htopic2_edit.php (POST 帶 id 分支)。</remarks>
    [HttpPut("htopic2s/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateHtopic2(
        uint id,
        [FromBody] UpdateHtopic2Request request,
        CancellationToken cancellationToken)
    {
        var result = await _htopic2Service.UpdateAsync(id, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { id = result.Data }, result.Message));
    }

    /// <summary>刪除議題 2</summary>
    /// <remarks>舊版 PHP 沒有 delete 分支,這裡為 REST 完整性補上。</remarks>
    [HttpDelete("htopic2s/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteHtopic2(uint id, CancellationToken cancellationToken)
    {
        var result = await _htopic2Service.DeleteAsync(id, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }
}

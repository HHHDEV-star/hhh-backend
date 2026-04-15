using System.Security.Claims;
using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Tags;
using hhh.application.admin.Tags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hhh.webapi.admin.Controllers;

/// <summary>
/// Tags
/// </summary>
/// <remarks>
/// 後台標籤管理,跨 4 種資源(個案 / 專欄 / 影音 / 圖庫)統一管理 tag 欄位。
///
/// 對應舊版 PHP:hhh-api/.../third/v1/Tag.php(tag_model)
/// </remarks>
[Route("api/tags")]
[Authorize]
[Tags("Tags")]
public class TagsController : ApiControllerBase
{
    private readonly ITagService _tagService;

    public TagsController(ITagService tagService)
    {
        _tagService = tagService;
    }

    // =========================================================================
    // 個案標籤 (hcases)
    // =========================================================================

    /// <summary>取得個案標籤列表</summary>
    /// <remarks>至少帶一個條件(hdesignerId 或 searchTag),否則回空陣列(防全撈)。</remarks>
    [HttpGet("hcases/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<TagHcaseItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetHcaseTags(
        [FromQuery] uint? hdesignerId,
        [FromQuery] string? searchTag,
        [FromQuery] ListQuery query,
        CancellationToken cancellationToken)
    {
        var data = await _tagService.GetHcaseTagsAsync(hdesignerId, searchTag, query, cancellationToken);
        return Ok(ApiResponse<PagedResponse<TagHcaseItem>>.Success(data));
    }

    /// <summary>更新個案標籤</summary>
    [HttpPut("hcases/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateHcaseTag(
        uint id, [FromBody] UpdateTagRequest request, CancellationToken cancellationToken)
    {
        var result = await _tagService.UpdateHcaseTagAsync(id, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }

    // =========================================================================
    // 專欄標籤 (hcolumns)
    // =========================================================================

    /// <summary>取得專欄標籤列表</summary>
    /// <remarks>至少帶一個條件(ctype / ctitle / 日期區間 / searchTag),否則回空陣列。</remarks>
    [HttpGet("hcolumns/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<TagHcolumnItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetHcolumnTags(
        [FromQuery] string? ctype,
        [FromQuery] string? ctitle,
        [FromQuery] DateOnly? startDate,
        [FromQuery] DateOnly? endDate,
        [FromQuery] string? searchTag,
        [FromQuery] ListQuery query,
        CancellationToken cancellationToken)
    {
        var data = await _tagService.GetHcolumnTagsAsync(ctype, ctitle, startDate, endDate, searchTag, query, cancellationToken);
        return Ok(ApiResponse<PagedResponse<TagHcolumnItem>>.Success(data));
    }

    /// <summary>更新專欄標籤</summary>
    [HttpPut("hcolumns/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateHcolumnTag(
        uint id, [FromBody] UpdateTagRequest request, CancellationToken cancellationToken)
    {
        var result = await _tagService.UpdateHcolumnTagAsync(id, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }

    // =========================================================================
    // 影音標籤 (hvideos)
    // =========================================================================

    /// <summary>取得影音標籤列表</summary>
    /// <remarks>至少帶一個條件(hdesignerId / title / 日期區間 / searchTag),否則回空陣列。</remarks>
    [HttpGet("hvideos/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<TagHvideoItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetHvideoTags(
        [FromQuery] uint? hdesignerId,
        [FromQuery] string? title,
        [FromQuery] DateOnly? startDate,
        [FromQuery] DateOnly? endDate,
        [FromQuery] string? searchTag,
        [FromQuery] ListQuery query,
        CancellationToken cancellationToken)
    {
        var data = await _tagService.GetHvideoTagsAsync(hdesignerId, title, startDate, endDate, searchTag, query, cancellationToken);
        return Ok(ApiResponse<PagedResponse<TagHvideoItem>>.Success(data));
    }

    /// <summary>更新影音標籤</summary>
    [HttpPut("hvideos/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateHvideoTag(
        uint id, [FromBody] UpdateTagRequest request, CancellationToken cancellationToken)
    {
        var result = await _tagService.UpdateHvideoTagAsync(id, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }

    // =========================================================================
    // 圖庫標籤 (images)
    // =========================================================================

    /// <summary>取得圖庫標籤列表</summary>
    /// <remarks>至少帶 hcaseId 或 searchTag,否則回空陣列。</remarks>
    [HttpGet("images/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<TagImageItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetImageTags(
        [FromQuery] uint? hcaseId,
        [FromQuery] string? searchTag,
        [FromQuery] ListQuery query,
        CancellationToken cancellationToken)
    {
        var data = await _tagService.GetImageTagsAsync(hcaseId, searchTag, query, cancellationToken);
        return Ok(ApiResponse<PagedResponse<TagImageItem>>.Success(data));
    }

    /// <summary>更新圖庫標籤(tag1-tag5 + title)</summary>
    /// <remarks>操作者 email 自動從 JWT claim 取得,寫入 tag_man。</remarks>
    [HttpPut("images/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateImageTag(
        uint id, [FromBody] UpdateImageTagRequest request, CancellationToken cancellationToken)
    {
        var email = User.FindFirstValue("email");
        var result = await _tagService.UpdateImageTagAsync(id, request, email, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }
}

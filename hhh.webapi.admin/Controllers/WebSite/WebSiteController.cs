using hhh.api.contracts.Common;
using hhh.api.contracts.admin.WebSite.DecoImages;
using hhh.api.contracts.admin.WebSite.DecoRecords;
using hhh.application.admin.WebSite.DecoImages;
using hhh.application.admin.WebSite.DecoRecords;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hhh.webapi.admin.Controllers.WebSite;

/// <summary>
/// WebSite
/// </summary>
/// <remarks>
/// 後台「WebSite」業務類別下所有 endpoint 集中於此 controller。
///
/// 對應舊版:
///  - 查證照:hhh-api/.../base/v1/Deco.php → backend_get / backend_put、hhh-backstage/.../event/decoquery.php
///  - 查證照圖片審核:hhh-api/.../base/v1/Deco.php → images_get / images_onoff_put、hhh-backstage/.../event/deco_images.php
/// </remarks>
[Route("api/website")]
[Authorize]
[Tags("WebSite")]
public class WebSiteController : ApiControllerBase
{
    private readonly IDecoRecordService _decoRecordService;
    private readonly IDecoImageService _decoImageService;

    public WebSiteController(
        IDecoRecordService decoRecordService,
        IDecoImageService decoImageService)
    {
        _decoRecordService = decoRecordService;
        _decoImageService = decoImageService;
    }

    // =========================================================================
    // 查證照 (deco-records)
    // =========================================================================

    /// <summary>取得查證照列表(全量,register_number DESC)</summary>
    /// <remarks>
    /// 對應舊版 PHP:Deco/backend_get → deco_model::get_deco_lists_backend()。
    /// SELECT FROM deco_record ORDER BY register_number DESC,無 paging。
    /// </remarks>
    [HttpGet("deco-records")]
    [ProducesResponseType(typeof(ApiResponse<List<DecoRecordListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDecoRecordList(CancellationToken cancellationToken)
    {
        var data = await _decoRecordService.GetListAsync(cancellationToken);
        return Ok(ApiResponse<List<DecoRecordListItem>>.Success(data));
    }

    /// <summary>更新查證照紀錄</summary>
    /// <remarks>
    /// 對應舊版 PHP:Deco/backend_put → deco_model::update_deco_record_backend()。
    /// 舊版是 batch(PUT 帶 request[]),本 API 改成 single record(bldsno 走 URL)。
    /// 可編輯欄位:register_number / service_phone / phone / cellphone / website /
    /// lineid / street / district / hdesigner_id / email / onoff。
    /// </remarks>
    [HttpPut("deco-records/{bldsno:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateDecoRecord(
        int bldsno,
        [FromBody] UpdateDecoRecordRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _decoRecordService.UpdateAsync(bldsno, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }

    // =========================================================================
    // 查證照圖片審核 (deco-images)
    // =========================================================================

    /// <summary>取得查證照圖片審核列表</summary>
    /// <remarks>
    /// 對應舊版 PHP:Deco/images_get → deco_model::get_deco_img_list()。
    /// JOIN deco_record 帶出 register_number / company_name / company_ceo。
    /// ORDER BY onoff ASC(未審核在前), bldsno, sort。
    /// </remarks>
    [HttpGet("deco-images")]
    [ProducesResponseType(typeof(ApiResponse<List<DecoImageListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDecoImageList(CancellationToken cancellationToken)
    {
        var data = await _decoImageService.GetListAsync(cancellationToken);
        return Ok(ApiResponse<List<DecoImageListItem>>.Success(data));
    }

    /// <summary>更新查證照圖片審核狀態</summary>
    /// <remarks>
    /// 對應舊版 PHP:Deco/images_onoff_put → deco_model::set_deco_img_onoff()。
    /// 舊版是 batch,本 API 改成 single record(id 走 URL)。
    /// onoff=true → 通過(Y),onoff=false → 不通過(N)。
    /// </remarks>
    [HttpPut("deco-images/{id:int}/onoff")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateDecoImageOnoff(
        uint id,
        [FromBody] UpdateDecoImageOnoffRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _decoImageService.UpdateOnoffAsync(id, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }
}

using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Advertise.Ads;
using hhh.application.admin.Advertise.Ads;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hhh.webapi.admin.Controllers;

/// <summary>
/// Advertise
/// </summary>
/// <remarks>
/// 後台「Advertise」業務類別下所有 endpoint 集中於此 controller。
///
/// 對應舊版:
///  - 廣告管理:hhh-api/.../base/v1/Homepage.php → had_* 系列、hhh-backstage/.../event/had.php
/// </remarks>
[Route("api/advertise")]
[Authorize]
[Tags("Advertise")]
public class AdvertiseController : ApiControllerBase
{
    private readonly IAdService _adService;

    public AdvertiseController(IAdService adService)
    {
        _adService = adService;
    }

    // =========================================================================
    // 廣告 (ads)
    // =========================================================================

    /// <summary>取得廣告列表</summary>
    /// <remarks>
    /// 對應舊版 Homepage/had_get → homepage_model::get_ad_all_lists()。
    /// 可選帶 type 篩選廣告類型。ORDER BY adid DESC。
    /// </remarks>
    [HttpGet("ads")]
    [ProducesResponseType(typeof(ApiResponse<List<AdListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAdList(
        [FromQuery] string? type, CancellationToken cancellationToken)
    {
        var data = await _adService.GetListAsync(type, cancellationToken);
        return Ok(ApiResponse<List<AdListItem>>.Success(data));
    }

    /// <summary>新增廣告</summary>
    /// <remarks>
    /// 對應舊版 Homepage/had_post → homepage_model::ins_had()。
    /// 舊版必填:adtype、adhref、onoff、start_time、end_time。
    /// 舊版是 batch,本 API 改成 single record。
    /// </remarks>
    [HttpPost("ads")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAd(
        [FromBody] CreateAdRequest request, CancellationToken cancellationToken)
    {
        var result = await _adService.CreateAsync(request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return StatusCode(StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { adid = result.Data }, result.Message));
    }

    /// <summary>更新廣告</summary>
    /// <remarks>
    /// 對應舊版 Homepage/had_put → homepage_model::upd_had()。
    /// 舊版必填:adid(URL)、adtype、adhref、onoff、start_time、end_time。
    /// 舊版是 batch,本 API 改成 single record。
    /// </remarks>
    [HttpPut("ads/{adid:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAd(
        uint adid, [FromBody] UpdateAdRequest request, CancellationToken cancellationToken)
    {
        var result = await _adService.UpdateAsync(adid, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { adid = result.Data }, result.Message));
    }

    /// <summary>刪除廣告</summary>
    /// <remarks>
    /// 對應舊版 Homepage/had_delete → homepage_model::del_had()。
    /// 舊版是 batch,本 API 改成 single record(adid 走 URL)。Hard delete。
    /// </remarks>
    [HttpDelete("ads/{adid:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAd(uint adid, CancellationToken cancellationToken)
    {
        var result = await _adService.DeleteAsync(adid, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }

    /// <summary>更新廣告圖片</summary>
    /// <remarks>
    /// 對應舊版 Homepage/had_logo_put → homepage_model::upd_had_logo()。
    /// 四個圖片欄位:adlogo(桌機) / adlogo_mobile(手機) / adlogo_webp / adlogo_mobile_webp,
    /// 加上 logo_icon。有給才更新。
    /// 舊版是 batch,本 API 改成 single record。
    /// </remarks>
    [HttpPut("ads/{adid:int}/logo")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAdLogo(
        uint adid, [FromBody] UpdateAdLogoRequest request, CancellationToken cancellationToken)
    {
        var result = await _adService.UpdateLogoAsync(adid, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }
}

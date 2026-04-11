using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Awards.Hawards;
using hhh.api.contracts.admin.Awards.Hcontests;
using hhh.api.contracts.admin.Awards.Hprizes;
using hhh.application.admin.Awards.Hawards;
using hhh.application.admin.Awards.Hcontests;
using hhh.application.admin.Awards.Hprizes;
using hhh.infrastructure.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hhh.webapi.admin.Controllers;

/// <summary>
/// 競賽獎項
/// </summary>
/// <remarks>
/// 後台「競賽獎項」業務類別下所有 endpoint 集中於此 controller。
/// 包含獲獎紀錄 + 獎品 + 競賽報名。
///
/// 對應舊版 PHP:
///  - 獲獎紀錄:/backend/_hawards.php、_hawards_edit.php
///  - 獎品:/backend/_hprize.php、_hprize_edit.php
///  - 競賽報名:/backend/_hcontest.php、_hcontest_edit.php
/// </remarks>
[Route("api/awards")]
[Authorize]
[Tags("Awards")]
public class AwardsController : ApiControllerBase
{
    private const string HprizeUploadFolder = "hprize";

    private readonly IHawardService _hawardService;
    private readonly IHprizeService _hprizeService;
    private readonly IHcontestService _hcontestService;
    private readonly IImageUploadService _imageUploadService;

    public AwardsController(
        IHawardService hawardService,
        IHprizeService hprizeService,
        IHcontestService hcontestService,
        IImageUploadService imageUploadService)
    {
        _hawardService = hawardService;
        _hprizeService = hprizeService;
        _hcontestService = hcontestService;
        _imageUploadService = imageUploadService;
    }

    // =========================================================================
    // 獲獎紀錄 (hawards)
    // =========================================================================

    /// <summary>取得得獎記錄分頁列表</summary>
    /// <remarks>
    /// 對應舊版 /backend/_hawards.php。
    /// q 跨欄位:hawards_id / awards_name / hdesigner_id / hcase_id / 設計師 name / 個案 caption。
    /// 排序白名單:id, awardsName, hdesignerId, hcaseId, onoff。
    /// </remarks>
    [HttpGet("hawards")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<HawardListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetHawardList(
        [FromQuery] HawardListRequest request,
        CancellationToken cancellationToken)
    {
        var data = await _hawardService.GetListAsync(request, cancellationToken);
        return Ok(ApiResponse<PagedResponse<HawardListItem>>.Success(data));
    }

    /// <summary>取得單一得獎記錄完整資料</summary>
    /// <remarks>對應舊版 /backend/_hawards_edit.php?id={id} (GET)。</remarks>
    [HttpGet("hawards/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<HawardDetailResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetHawardById(uint id, CancellationToken cancellationToken)
    {
        var haward = await _hawardService.GetByIdAsync(id, cancellationToken);
        if (haward is null)
        {
            return StatusCode(StatusCodes.Status404NotFound,
                ApiResponse.Error(StatusCodes.Status404NotFound, "找不到得獎記錄"));
        }

        return Ok(ApiResponse<HawardDetailResponse>.Success(haward));
    }

    /// <summary>新增得獎記錄</summary>
    /// <remarks>對應舊版 /backend/_hawards_edit.php (POST 無 id 分支)。會驗證 logo 白名單 / 設計師 / 個案 FK。</remarks>
    [HttpPost("hawards")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateHaward(
        [FromBody] CreateHawardRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _hawardService.CreateAsync(request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return StatusCode(
            StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { id = result.Data }, result.Message));
    }

    /// <summary>更新得獎記錄</summary>
    /// <remarks>對應舊版 /backend/_hawards_edit.php (POST 帶 id 分支)。</remarks>
    [HttpPut("hawards/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateHaward(
        uint id,
        [FromBody] UpdateHawardRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _hawardService.UpdateAsync(id, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { id = result.Data }, result.Message));
    }

    /// <summary>刪除得獎記錄</summary>
    /// <remarks>舊版 PHP UI 沒有刪除按鈕,新 API 補上。</remarks>
    [HttpDelete("hawards/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteHaward(uint id, CancellationToken cancellationToken)
    {
        var result = await _hawardService.DeleteAsync(id, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }

    // =========================================================================
    // 獎品 (hprizes) - 含 logo 上傳
    // =========================================================================

    /// <summary>取得獎品分頁列表</summary>
    /// <remarks>對應舊版 /backend/_hprize.php。q 跨欄位:hprize_id / title / desc。排序白名單:id, title, creatTime。</remarks>
    [HttpGet("hprizes")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<HprizeListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetHprizeList(
        [FromQuery] HprizeListRequest request,
        CancellationToken cancellationToken)
    {
        var data = await _hprizeService.GetListAsync(request, cancellationToken);
        return Ok(ApiResponse<PagedResponse<HprizeListItem>>.Success(data));
    }

    /// <summary>取得單一獎品完整資料</summary>
    /// <remarks>對應舊版 /backend/_hprize_edit.php?id={id} (GET)。</remarks>
    [HttpGet("hprizes/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<HprizeDetailResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetHprizeById(uint id, CancellationToken cancellationToken)
    {
        var hprize = await _hprizeService.GetByIdAsync(id, cancellationToken);
        if (hprize is null)
        {
            return StatusCode(StatusCodes.Status404NotFound,
                ApiResponse.Error(StatusCodes.Status404NotFound, "找不到獎品"));
        }

        return Ok(ApiResponse<HprizeDetailResponse>.Success(hprize));
    }

    /// <summary>新增獎品(multipart/form-data)</summary>
    /// <remarks>
    /// 對應舊版 /backend/_hprize_edit.php (POST 無 id 分支)。
    /// 表單欄位:title、desc、logo(檔案)。logo 必填,接受 png / jpg / jpeg / gif / webp。
    /// </remarks>
    [HttpPost("hprizes")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateHprize(
        [FromForm] CreateHprizeRequest request,
        IFormFile? logo,
        CancellationToken cancellationToken)
    {
        if (logo is null || logo.Length == 0)
        {
            return StatusCode(StatusCodes.Status400BadRequest,
                ApiResponse.Error(400, "logo 檔案必填"));
        }

        ImageUploadResult uploaded;
        try
        {
            await using var stream = logo.OpenReadStream();
            uploaded = await _imageUploadService.UploadImageAsync(
                stream,
                logo.FileName,
                logo.Length,
                HprizeUploadFolder,
                cancellationToken);
        }
        catch (ImageUploadException ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest,
                ApiResponse.Error(400, ex.Message));
        }

        var result = await _hprizeService.CreateAsync(request, uploaded, cancellationToken);
        if (!result.IsSuccess)
        {
            // DB 寫入失敗:把剛剛上傳的檔案刪掉避免孤兒
            _imageUploadService.Delete(uploaded.RelativePath);
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        }

        return StatusCode(
            StatusCodes.Status201Created,
            ApiResponse<object>.Created(
                new { id = result.Data, logo = uploaded.PublicUrl },
                result.Message));
    }

    /// <summary>更新獎品(multipart/form-data)</summary>
    /// <remarks>
    /// 對應舊版 /backend/_hprize_edit.php (POST 帶 id 分支)。
    /// 表單欄位:title、desc、logo(檔案,選填)。
    /// 未上傳 logo 時保留原檔;上傳新 logo 時會自動刪除舊檔(若舊檔是本機管理的)。
    /// </remarks>
    [HttpPut("hprizes/{id:int}")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateHprize(
        uint id,
        [FromForm] UpdateHprizeRequest request,
        IFormFile? logo,
        CancellationToken cancellationToken)
    {
        ImageUploadResult? uploaded = null;
        if (logo is not null && logo.Length > 0)
        {
            try
            {
                await using var stream = logo.OpenReadStream();
                uploaded = await _imageUploadService.UploadImageAsync(
                    stream,
                    logo.FileName,
                    logo.Length,
                    HprizeUploadFolder,
                    cancellationToken);
            }
            catch (ImageUploadException ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    ApiResponse.Error(400, ex.Message));
            }
        }

        var result = await _hprizeService.UpdateAsync(id, request, uploaded, cancellationToken);
        if (!result.IsSuccess)
        {
            if (uploaded is not null)
            {
                _imageUploadService.Delete(uploaded.RelativePath);
            }
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        }

        if (!string.IsNullOrEmpty(result.OldLogoRelativePath))
        {
            _imageUploadService.Delete(result.OldLogoRelativePath);
        }

        return Ok(ApiResponse<object>.Success(
            new { id = result.Data, logo = uploaded?.PublicUrl },
            result.Message));
    }

    /// <summary>刪除獎品</summary>
    /// <remarks>對應舊版 /backend/_hprize.php?delete_id={id}。會一併刪除本機管理的 logo 檔案。</remarks>
    [HttpDelete("hprizes/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteHprize(uint id, CancellationToken cancellationToken)
    {
        var result = await _hprizeService.DeleteAsync(id, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        if (!string.IsNullOrEmpty(result.OldLogoRelativePath))
        {
            _imageUploadService.Delete(result.OldLogoRelativePath);
        }

        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }

    // =========================================================================
    // 競賽報名 (hcontests)
    // =========================================================================

    /// <summary>取得競賽報名分頁列表</summary>
    /// <remarks>
    /// 對應舊版 /backend/_hcontest.php。
    /// 支援 q / year / classType / finalist。
    /// q 跨欄位:contest_id / class_type / year / c1 / c2 / c3 / c9。
    /// 排序白名單:id, year, classType, applytime, finalist, wp。
    /// </remarks>
    [HttpGet("hcontests")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<HcontestListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetHcontestList(
        [FromQuery] HcontestListRequest request,
        CancellationToken cancellationToken)
    {
        var data = await _hcontestService.GetListAsync(request, cancellationToken);
        return Ok(ApiResponse<PagedResponse<HcontestListItem>>.Success(data));
    }

    /// <summary>取得單筆競賽報名完整資料</summary>
    /// <remarks>對應舊版 /backend/_hcontest_edit.php?id={id} (GET)。</remarks>
    [HttpGet("hcontests/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<HcontestDetailResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetHcontestById(uint id, CancellationToken cancellationToken)
    {
        var detail = await _hcontestService.GetByIdAsync(id, cancellationToken);
        if (detail is null)
        {
            return StatusCode(StatusCodes.Status404NotFound,
                ApiResponse.Error(StatusCodes.Status404NotFound, "找不到競賽報名資料"));
        }

        return Ok(ApiResponse<HcontestDetailResponse>.Success(detail));
    }

    /// <summary>新增競賽報名</summary>
    /// <remarks>對應舊版 /backend/_hcontest_edit.php (POST 無 id 分支)。</remarks>
    [HttpPost("hcontests")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateHcontest(
        [FromBody] CreateHcontestRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _hcontestService.CreateAsync(request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return StatusCode(
            StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { id = result.Data }, result.Message));
    }

    /// <summary>更新競賽報名</summary>
    /// <remarks>對應舊版 /backend/_hcontest_edit.php (POST 帶 id 分支)。applytime 沒傳時保留原值。</remarks>
    [HttpPut("hcontests/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateHcontest(
        uint id,
        [FromBody] UpdateHcontestRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _hcontestService.UpdateAsync(id, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { id = result.Data }, result.Message));
    }

    /// <summary>刪除競賽報名</summary>
    /// <remarks>舊版 PHP 沒有 delete 分支,這裡為 REST 完整性補上。</remarks>
    [HttpDelete("hcontests/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteHcontest(uint id, CancellationToken cancellationToken)
    {
        var result = await _hcontestService.DeleteAsync(id, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }
}

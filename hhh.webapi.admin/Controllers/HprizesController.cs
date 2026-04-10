using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Hprizes;
using hhh.application.admin.Hprizes;
using hhh.infrastructure.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hhh.webapi.admin.Controllers;

[Route("api/[controller]")]
[Authorize]
public class HprizesController : ApiControllerBase
{
    private const string UploadFolder = "hprize";

    private readonly IHprizeService _hprizeService;
    private readonly IImageUploadService _imageUploadService;

    public HprizesController(
        IHprizeService hprizeService,
        IImageUploadService imageUploadService)
    {
        _hprizeService = hprizeService;
        _imageUploadService = imageUploadService;
    }

    /// <summary>
    /// 取得獎品分頁列表
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_hprize.php
    /// 支援 q / page / pageSize / sort / by 查詢參數。
    /// q：跨欄位 LIKE 搜尋（hprize_id / title / desc）。
    /// 排序白名單：id, title, creatTime。
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<HprizeListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetList(
        [FromQuery] HprizeListRequest request,
        CancellationToken cancellationToken)
    {
        var data = await _hprizeService.GetListAsync(request, cancellationToken);
        return Ok(ApiResponse<PagedResponse<HprizeListItem>>.Success(data));
    }

    /// <summary>
    /// 取得單一獎品完整資料
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_hprize_edit.php?id={id} （GET 模式）
    /// </remarks>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<HprizeDetailResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        uint id,
        CancellationToken cancellationToken)
    {
        var hprize = await _hprizeService.GetByIdAsync(id, cancellationToken);
        if (hprize is null)
        {
            return StatusCode(StatusCodes.Status404NotFound,
                ApiResponse.Error(StatusCodes.Status404NotFound, "找不到獎品"));
        }

        return Ok(ApiResponse<HprizeDetailResponse>.Success(hprize));
    }

    /// <summary>
    /// 新增獎品（multipart/form-data）
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_hprize_edit.php （POST 無 id 分支）
    /// 表單欄位：title、desc、logo（檔案）。
    /// logo 必填，接受 png / jpg / jpeg / gif / webp。
    /// 成功時回傳 HTTP 201 Created。
    /// </remarks>
    [HttpPost]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(
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
                UploadFolder,
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
            // DB 寫入失敗：把剛剛上傳的檔案刪掉避免孤兒
            _imageUploadService.Delete(uploaded.RelativePath);
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        }

        return StatusCode(
            StatusCodes.Status201Created,
            ApiResponse<object>.Created(
                new { id = result.Data, logo = uploaded.PublicUrl },
                result.Message));
    }

    /// <summary>
    /// 更新獎品（multipart/form-data）
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_hprize_edit.php （POST 帶 id 分支）
    /// 表單欄位：title、desc、logo（檔案，選填）。
    /// 未上傳 logo 時保留原檔；上傳新 logo 時會自動刪除舊檔（若舊檔是本機管理的）。
    /// </remarks>
    [HttpPut("{id:int}")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
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
                    UploadFolder,
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
            // DB 更新失敗：把剛剛上傳的新檔回滾（若有）
            if (uploaded is not null)
            {
                _imageUploadService.Delete(uploaded.RelativePath);
            }
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        }

        // 成功：如果有新 logo，service 會把舊檔相對路徑放在 OldLogoRelativePath，清理之
        if (!string.IsNullOrEmpty(result.OldLogoRelativePath))
        {
            _imageUploadService.Delete(result.OldLogoRelativePath);
        }

        return Ok(ApiResponse<object>.Success(
            new { id = result.Data, logo = uploaded?.PublicUrl },
            result.Message));
    }

    /// <summary>
    /// 刪除獎品
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_hprize.php?delete_id={id}
    /// 會一併刪除本機管理的 logo 檔案（若存在）。
    /// </remarks>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        uint id,
        CancellationToken cancellationToken)
    {
        var result = await _hprizeService.DeleteAsync(id, cancellationToken);
        if (!result.IsSuccess)
        {
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        }

        if (!string.IsNullOrEmpty(result.OldLogoRelativePath))
        {
            _imageUploadService.Delete(result.OldLogoRelativePath);
        }

        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }
}

using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Common;
using hhh.infrastructure.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hhh.webapi.admin.Controllers;

/// <summary>
/// 通用檔案上傳 API
/// </summary>
/// <remarks>
/// 前端獨立調用：上傳圖片 → 取得 CDN URL → 再呼叫各業務 API 傳入 URL。
/// 對應舊版 PHP 前端直接呼叫 images.hhh.com.tw/images/water_upload 的流程。
/// </remarks>
[Route("api/upload")]
[Authorize]
[Tags("Upload")]
public class UploadController : ApiControllerBase
{
    private readonly IImageUploadService _uploadService;

    public UploadController(IImageUploadService uploadService)
    {
        _uploadService = uploadService;
    }

    /// <summary>上傳單張圖片</summary>
    /// <remarks>
    /// 使用 multipart/form-data 上傳。
    /// folder 參數指定存放子目錄（如 hcase、hcolumn、hdesigner、hprize、ad 等），
    /// 用來在 S3 / 本機磁碟分類管理檔案。
    ///
    /// 回傳的 url 可直接用於後續業務 API（如新增個案圖片、更新 SEO 圖片等）。
    /// </remarks>
    /// <param name="file">圖片檔案（支援 png/jpg/jpeg/gif/webp，上限 10MB）</param>
    /// <param name="folder">存放目錄名稱（例：hcase、hcolumn、hdesigner、ad）</param>
    [HttpPost("image")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(ApiResponse<UploadResult>), StatusCodes.Status200OK)]
    [RequestSizeLimit(10 * 1024 * 1024)] // 10 MB
    public async Task<IActionResult> UploadImage(
        IFormFile file,
        [FromQuery] string folder,
        CancellationToken cancellationToken)
    {
        if (file is null || file.Length == 0)
            return BadRequest(ApiResponse.Error(400, "請選擇要上傳的檔案"));

        if (string.IsNullOrWhiteSpace(folder))
            return BadRequest(ApiResponse.Error(400, "請指定 folder 參數"));

        try
        {
            await using var stream = file.OpenReadStream();
            var result = await _uploadService.UploadImageAsync(
                stream,
                file.FileName,
                file.Length,
                folder,
                cancellationToken);

            return Ok(ApiResponse<UploadResult>.Success(new UploadResult
            {
                Url = result.PublicUrl,
                Path = result.RelativePath,
                OriginalFileName = result.OriginalFileName,
                SizeBytes = result.SizeBytes,
            }));
        }
        catch (ImageUploadException ex)
        {
            return BadRequest(ApiResponse.Error(400, ex.Message));
        }
    }

    /// <summary>批次上傳多張圖片</summary>
    /// <remarks>
    /// 一次可上傳多張，每張獨立處理。任一張失敗不影響其他張。
    /// 回傳陣列順序與上傳順序一致。
    /// </remarks>
    /// <param name="files">圖片檔案（多選）</param>
    /// <param name="folder">存放目錄名稱</param>
    [HttpPost("images")]
    [Consumes("multipart/form-data")]
    [ProducesResponseType(typeof(ApiResponse<List<UploadResult>>), StatusCodes.Status200OK)]
    [RequestSizeLimit(50 * 1024 * 1024)] // 50 MB（多檔合計）
    public async Task<IActionResult> UploadImages(
        List<IFormFile> files,
        [FromQuery] string folder,
        CancellationToken cancellationToken)
    {
        if (files is not { Count: > 0 })
            return BadRequest(ApiResponse.Error(400, "請選擇要上傳的檔案"));

        if (string.IsNullOrWhiteSpace(folder))
            return BadRequest(ApiResponse.Error(400, "請指定 folder 參數"));

        var results = new List<UploadResult>();

        foreach (var file in files)
        {
            if (file.Length == 0) continue;

            try
            {
                await using var stream = file.OpenReadStream();
                var result = await _uploadService.UploadImageAsync(
                    stream,
                    file.FileName,
                    file.Length,
                    folder,
                    cancellationToken);

                results.Add(new UploadResult
                {
                    Url = result.PublicUrl,
                    Path = result.RelativePath,
                    OriginalFileName = result.OriginalFileName,
                    SizeBytes = result.SizeBytes,
                });
            }
            catch (ImageUploadException ex)
            {
                // 單檔失敗記錄錯誤但不中斷
                results.Add(new UploadResult
                {
                    OriginalFileName = file.FileName,
                    Url = $"error: {ex.Message}",
                });
            }
        }

        return Ok(ApiResponse<List<UploadResult>>.Success(results));
    }

    /// <summary>刪除已上傳的檔案</summary>
    /// <remarks>
    /// 傳入上傳時回傳的 path（S3 Key 或本機相對路徑）。
    /// 檔案不存在時不會報錯（idempotent）。
    /// </remarks>
    /// <param name="path">上傳時回傳的 path 值</param>
    [HttpDelete("image")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    public IActionResult DeleteImage([FromQuery] string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            return BadRequest(ApiResponse.Error(400, "請指定 path 參數"));

        var deleted = _uploadService.Delete(path);
        return Ok(ApiResponse<object>.Success(
            new { deleted },
            deleted ? "檔案已刪除" : "檔案不存在或已刪除"));
    }
}

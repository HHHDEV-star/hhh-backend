using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Website;
using hhh.api.contracts.admin.WebSite.DecoImages;
using hhh.api.contracts.admin.WebSite.DecoRecords;
using hhh.application.admin.Website;
using hhh.application.admin.WebSite.DecoImages;
using hhh.application.admin.WebSite.DecoRecords;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using hhh.infrastructure.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hhh.webapi.admin.Controllers;

/// <summary>
/// 網站管理 API — 建商 / 建案 / 建案圖片
/// （對應舊版 PHP: hhh-api/.../base/v1/Builder.php）。
///
/// 三個子資源集中在同一個 controller（參考 RssController / DesignersController 模式）：
///   - builders          → 建商 thin CRUD（直接戳 XoopsContext）
///   - builder-products  → 建案 CRUD（委派 IBuilderProductService）
///   - builder-products/{productId}/images → 建案圖片（委派同 service）
/// </summary>
[Route("api/website")]
[Authorize]
[Tags("Website")]
public class WebSiteController : ApiControllerBase
{
    private const string BuilderPageName = "建商";

    private readonly XoopsContext _db;
    private readonly IBuilderProductService _builderProductService;
    private readonly IOperationLogWriter _logWriter;
    private readonly IDecoRecordService _decoRecordService;
    private readonly IDecoImageService _decoImageService;

    public WebSiteController(
        XoopsContext db,
        IBuilderProductService builderProductService,
        IOperationLogWriter logWriter,
        IDecoRecordService decoRecordService,
        IDecoImageService decoImageService)
    {
        _db = db;
        _builderProductService = builderProductService;
        _logWriter = logWriter;
        _decoRecordService = decoRecordService;
        _decoImageService = decoImageService;
    }

    // =========================================================================
    // Builders 建商（thin CRUD，直接戳 XoopsContext）
    // =========================================================================

    /// <summary>取得建商列表</summary>
    /// <remarks>
    /// 對應舊版 PHP: Builder/lists_get（13.01）
    /// 小資料集,不分頁。
    /// </remarks>
    [HttpGet("builders")]
    [ProducesResponseType(typeof(ApiResponse<List<BuilderListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBuilderList(CancellationToken cancellationToken)
    {
        var data = await _db.Builders
            .AsNoTracking()
            .OrderByDescending(b => b.BuilderId)
            .Select(b => new BuilderListItem
            {
                Id = b.BuilderId,
                Logo = b.Logo,
                Title = b.Title,
                Onoff = b.Onoff,
                Phone = b.Phone,
                Email = b.Email,
                Address = b.Address,
                CreatTime = b.CreatTime,
            })
            .ToListAsync(cancellationToken);

        return Ok(ApiResponse<List<BuilderListItem>>.Success(data));
    }

    /// <summary>取得單筆建商完整資料</summary>
    [HttpGet("builders/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<BuilderDetailResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBuilderById(
        uint id, CancellationToken cancellationToken)
    {
        var detail = await _db.Builders
            .AsNoTracking()
            .Where(b => b.BuilderId == id)
            .Select(b => new BuilderDetailResponse
            {
                Id = b.BuilderId,
                Logo = b.Logo,
                Logo2 = b.Logo2,
                Title = b.Title,
                SubCompany = b.SubCompany,
                ServicePhone = b.ServicePhone,
                Phone = b.Phone,
                Address = b.Address,
                Website = b.Website,
                Fbpageurl = b.Fbpageurl,
                Email = b.Email,
                Intro = b.Intro,
                History = b.History,
                Desc = b.Desc,
                Gchoice = b.Gchoice,
                HvideoId = b.HvideoId,
                Recommend = b.Recommend,
                Border = b.Border,
                Onoff = b.Onoff,
                CreatTime = b.CreatTime,
                Vr360Id = b.Vr360Id,
                Clicks = b.Clicks,
                BackgroundMobile = b.BackgroundMobile,
                IsSend = b.IsSend,
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (detail is null)
        {
            return StatusCode(StatusCodes.Status404NotFound,
                ApiResponse.Error(StatusCodes.Status404NotFound, "找不到建商"));
        }

        return Ok(ApiResponse<BuilderDetailResponse>.Success(detail));
    }

    /// <summary>新增建商</summary>
    /// <remarks>
    /// 對應舊版 PHP: Builder/lists_post（13.02）
    /// </remarks>
    [HttpPost("builders")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateBuilder(
        [FromBody] CreateBuilderRequest request,
        CancellationToken cancellationToken)
    {
        var entity = new Builder
        {
            Title = request.Title,
            Logo = request.Logo ?? string.Empty,
            Logo2 = string.Empty,
            SubCompany = null,
            ServicePhone = string.Empty,
            Phone = request.Phone ?? string.Empty,
            Address = request.Address ?? string.Empty,
            Website = string.Empty,
            Fbpageurl = string.Empty,
            Email = request.Email ?? string.Empty,
            Intro = request.Intro ?? string.Empty,
            History = string.Empty,
            Desc = request.Desc ?? string.Empty,
            Gchoice = string.Empty,
            HvideoId = 0,
            Recommend = 0,
            Border = 0,
            Onoff = request.Onoff,
            CreatTime = DateTime.Now,
            Vr360Id = string.Empty,
            Clicks = 0,
            BackgroundMobile = null,
            IsSend = 0,
        };

        _db.Builders.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            BuilderPageName,
            OperationAction.Create,
            $"新增建商 id={entity.BuilderId} 名稱={request.Title}",
            cancellationToken: cancellationToken);

        return StatusCode(
            StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { id = entity.BuilderId }, "新增成功"));
    }

    /// <summary>修改建商</summary>
    /// <remarks>
    /// 對應舊版 PHP: Builder/lists_put（13.03）
    /// </remarks>
    [HttpPut("builders/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBuilder(
        uint id,
        [FromBody] UpdateBuilderRequest request,
        CancellationToken cancellationToken)
    {
        var entity = await _db.Builders
            .FirstOrDefaultAsync(b => b.BuilderId == id, cancellationToken);

        if (entity is null)
        {
            return StatusCode(StatusCodes.Status404NotFound,
                ApiResponse.Error(StatusCodes.Status404NotFound, "找不到建商"));
        }

        entity.Title = request.Title;
        entity.Logo = request.Logo ?? string.Empty;
        entity.Onoff = request.Onoff;
        entity.Email = request.Email ?? string.Empty;
        entity.Phone = request.Phone ?? string.Empty;
        entity.Intro = request.Intro ?? string.Empty;
        entity.Desc = request.Desc ?? string.Empty;
        entity.Address = request.Address ?? string.Empty;

        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            BuilderPageName,
            OperationAction.Update,
            $"修改建商 id={id} 名稱={request.Title}",
            cancellationToken: cancellationToken);

        return Ok(ApiResponse<object>.Success(new { id }, "修改成功"));
    }

    /// <summary>刪除建商</summary>
    /// <remarks>
    /// 對應舊版 PHP: Builder/lists_delete（13.04）
    /// </remarks>
    [HttpDelete("builders/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBuilder(
        uint id,
        CancellationToken cancellationToken)
    {
        var entity = await _db.Builders
            .FirstOrDefaultAsync(b => b.BuilderId == id, cancellationToken);

        if (entity is null)
        {
            return StatusCode(StatusCodes.Status404NotFound,
                ApiResponse.Error(StatusCodes.Status404NotFound, "找不到建商"));
        }

        var oldTitle = entity.Title;

        _db.Builders.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            BuilderPageName,
            OperationAction.Delete,
            $"刪除建商 id={id} 名稱={oldTitle}",
            cancellationToken: cancellationToken);

        return Ok(ApiResponse<object>.Success(new { }, "刪除成功"));
    }

    /// <summary>建商下拉選單</summary>
    /// <remarks>
    /// 對應舊版 PHP: Builder/dropdown_get（13.13）
    /// 格式: value=builder_id, name="id-title"
    /// </remarks>
    [HttpGet("builders/dropdown")]
    [ProducesResponseType(typeof(ApiResponse<List<BuilderDropdownItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBuilderDropdown(CancellationToken cancellationToken)
    {
        var data = await _db.Builders
            .AsNoTracking()
            .Select(b => new BuilderDropdownItem
            {
                Value = b.BuilderId,
                Name = $"{b.BuilderId}-{b.Title}",
            })
            .ToListAsync(cancellationToken);

        return Ok(ApiResponse<List<BuilderDropdownItem>>.Success(data));
    }

    // =========================================================================
    // BuilderProducts 建案（委派 IBuilderProductService）
    // =========================================================================

    /// <summary>取得建案分頁列表</summary>
    /// <remarks>
    /// 對應舊版 PHP: Builder/product_get（13.05）
    /// 支援 q / builderId / onoff / page / pageSize / sort / by 查詢參數。
    /// 排序白名單: id, name, builderId, city, onoff, updateTime。
    /// </remarks>
    [HttpGet("builder-products")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<BuilderProductListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBuilderProductList(
        [FromQuery] BuilderProductListRequest request,
        CancellationToken cancellationToken)
    {
        var data = await _builderProductService.GetListAsync(request, cancellationToken);
        return Ok(ApiResponse<PagedResponse<BuilderProductListItem>>.Success(data));
    }

    /// <summary>取得單筆建案完整資料</summary>
    [HttpGet("builder-products/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<BuilderProductDetailResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBuilderProductById(
        uint id, CancellationToken cancellationToken)
    {
        var detail = await _builderProductService.GetByIdAsync(id, cancellationToken);
        if (detail is null)
        {
            return StatusCode(StatusCodes.Status404NotFound,
                ApiResponse.Error(StatusCodes.Status404NotFound, "找不到建案"));
        }

        return Ok(ApiResponse<BuilderProductDetailResponse>.Success(detail));
    }

    /// <summary>新增建案</summary>
    /// <remarks>
    /// 對應舊版 PHP: Builder/product_post（13.06）
    /// 會連動更新 _hcolumn / _hvideo 的 builder_product_id。
    /// </remarks>
    [HttpPost("builder-products")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateBuilderProduct(
        [FromBody] CreateBuilderProductRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _builderProductService.CreateAsync(request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return StatusCode(
            StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { id = result.Data }, result.Message));
    }

    /// <summary>修改建案</summary>
    /// <remarks>
    /// 對應舊版 PHP: Builder/product_put（13.07）
    /// 會重新連動 _hcolumn / _hvideo。
    /// </remarks>
    [HttpPut("builder-products/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBuilderProduct(
        uint id,
        [FromBody] UpdateBuilderProductRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _builderProductService.UpdateAsync(id, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { id = result.Data }, result.Message));
    }

    /// <summary>刪除建案</summary>
    /// <remarks>
    /// 對應舊版 PHP: Builder/product_delete（13.08）
    /// </remarks>
    [HttpDelete("builder-products/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBuilderProduct(
        uint id, CancellationToken cancellationToken)
    {
        var result = await _builderProductService.DeleteAsync(id, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }

    /// <summary>建案下拉選單</summary>
    /// <remarks>
    /// 對應舊版 PHP: Builder/lists_dropdown_get（13.14）
    /// 僅回傳 onoff=1 的建案。
    /// </remarks>
    [HttpGet("builder-products/dropdown")]
    [ProducesResponseType(typeof(ApiResponse<List<BuilderProductDropdownItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBuilderProductDropdown(CancellationToken cancellationToken)
    {
        var data = await _builderProductService.GetDropdownAsync(cancellationToken);
        return Ok(ApiResponse<List<BuilderProductDropdownItem>>.Success(data));
    }

    // =========================================================================
    // BuilderProductImages 建案圖片（委派 IBuilderProductService）
    // =========================================================================

    /// <summary>取得指定建案的圖片列表</summary>
    /// <remarks>
    /// 對應舊版 PHP: Builder/product_img_get（13.09）
    /// 排序: is_cover DESC, order_no ASC。
    /// </remarks>
    [HttpGet("builder-products/{productId:int}/images")]
    [ProducesResponseType(typeof(ApiResponse<List<BuilderProductImageListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBuilderProductImages(
        uint productId, CancellationToken cancellationToken)
    {
        var data = await _builderProductService.GetImagesAsync(productId, cancellationToken);
        return Ok(ApiResponse<List<BuilderProductImageListItem>>.Success(data));
    }

    /// <summary>新增建案圖片</summary>
    /// <remarks>
    /// 對應舊版 PHP: Builder/product_img_post（13.10）
    /// 同步更新 builder_product.update_time。
    /// </remarks>
    [HttpPost("builder-products/{productId:int}/images")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateBuilderProductImage(
        uint productId,
        [FromBody] CreateBuilderProductImageRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _builderProductService.CreateImageAsync(
            productId, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return StatusCode(
            StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { id = result.Data }, result.Message));
    }

    /// <summary>修改建案圖片</summary>
    /// <remarks>
    /// 對應舊版 PHP: Builder/product_img_put（13.11）
    /// 同步更新 builder_product.update_time。
    /// </remarks>
    [HttpPut("builder-products/{productId:int}/images/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateBuilderProductImage(
        uint productId,
        uint id,
        [FromBody] UpdateBuilderProductImageRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _builderProductService.UpdateImageAsync(
            productId, id, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { id = result.Data }, result.Message));
    }

    /// <summary>刪除建案圖片</summary>
    /// <remarks>
    /// 對應舊版 PHP: Builder/product_img_delete（13.12）
    /// 同步更新 builder_product.update_time。
    /// </remarks>
    [HttpDelete("builder-products/{productId:int}/images/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBuilderProductImage(
        uint productId,
        uint id,
        CancellationToken cancellationToken)
    {
        var result = await _builderProductService.DeleteImageAsync(
            productId, id, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }

    /// <summary>設定建案封面</summary>
    /// <remarks>
    /// 對應舊版 PHP: Builder/img_put（15.16）
    /// 更新 builder_product.cover + reset 所有圖片 is_cover + 設定指定圖片為封面。
    /// </remarks>
    [HttpPut("builder-products/{productId:int}/cover")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SetBuilderProductCover(
        uint productId,
        [FromBody] SetCoverRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _builderProductService.SetCoverAsync(
            productId, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { id = result.Data }, result.Message));
    }

    // =========================================================================
    // 查證照 (deco-records)
    // =========================================================================

    /// <summary>取得查證照列表(全量,register_number DESC)</summary>
    /// <remarks>對應舊版 PHP:Deco/backend_get → deco_model::get_deco_lists_backend()。</remarks>
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
    /// 舊版是 batch,本 API 改成 single record(bldsno 走 URL)。
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
    /// JOIN deco_record 帶出公司資料。ORDER BY onoff ASC(未審核在前)。
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

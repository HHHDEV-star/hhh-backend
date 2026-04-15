using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Marketing;
using hhh.application.admin.Marketing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hhh.webapi.admin.Controllers;

/// <summary>
/// 行銷管理 API — 專欄 SEO / 個案 SEO / 產品 SEO
/// （對應舊版 PHP: Column.php + Cases.php + Product.php 的 seo_get / seo_put / seoimage_put）
/// </summary>
[Route("api/marketing")]
[Authorize]
[Tags("Marketing")]
public class MarketingController : ApiControllerBase
{
    private readonly IColumnSeoService _columnSeoService;
    private readonly ICaseSeoService _caseSeoService;
    private readonly IProductSeoService _productSeoService;

    public MarketingController(
        IColumnSeoService columnSeoService,
        ICaseSeoService caseSeoService,
        IProductSeoService productSeoService)
    {
        _columnSeoService = columnSeoService;
        _caseSeoService = caseSeoService;
        _productSeoService = productSeoService;
    }

    // =========================================================================
    // 專欄 SEO (column-seo)
    // =========================================================================

    /// <summary>取得專欄 SEO 列表</summary>
    /// <remarks>
    /// 對應舊版 PHP: Column/seo_get（15.11）
    /// 全量回傳，排序: sdate DESC, hcolumn_id DESC。
    /// </remarks>
    [HttpGet("column-seo/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<ColumnSeoListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetColumnSeoList(
        [FromQuery] ListQuery query,
        CancellationToken cancellationToken)
    {
        var data = await _columnSeoService.GetListAsync(query, cancellationToken);
        return Ok(ApiResponse<PagedResponse<ColumnSeoListItem>>.Success(data));
    }

    /// <summary>批次更新專欄 SEO 標題/描述</summary>
    /// <remarks>
    /// 對應舊版 PHP: Column/seo_put（15.12）
    /// KendoGrid batch 模式，一次送出多筆更新。
    /// </remarks>
    [HttpPut("column-seo")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    public async Task<IActionResult> BatchUpdateColumnSeo(
        [FromBody] List<UpdateColumnSeoItem> items,
        CancellationToken cancellationToken)
    {
        var result = await _columnSeoService.BatchUpdateAsync(items, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }

    /// <summary>批次更新專欄 SEO 圖片</summary>
    /// <remarks>
    /// 對應舊版 PHP: Column/image_put（seo_image 欄位）
    /// 圖片上傳完成後，前端呼叫此端點更新 seo_image URL。
    /// </remarks>
    [HttpPut("column-seo/images")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    public async Task<IActionResult> BatchUpdateColumnSeoImage(
        [FromBody] List<UpdateColumnSeoImageItem> items,
        CancellationToken cancellationToken)
    {
        var result = await _columnSeoService.BatchUpdateImageAsync(items, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }

    // =========================================================================
    // 個案 SEO (case-seo)
    // =========================================================================

    /// <summary>取得個案 SEO 列表（分頁）</summary>
    /// <remarks>
    /// 對應舊版 PHP: Cases/seo_get（14.10）
    /// 排序: sdate DESC, hcase_id DESC。
    /// </remarks>
    [HttpGet("case-seo/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<CaseSeoListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCaseSeoList(
        [FromQuery] ListQuery query,
        CancellationToken cancellationToken)
    {
        var data = await _caseSeoService.GetListAsync(query, cancellationToken);
        return Ok(ApiResponse<PagedResponse<CaseSeoListItem>>.Success(data));
    }

    /// <summary>批次更新個案 SEO 標題/描述</summary>
    /// <remarks>
    /// 對應舊版 PHP: Cases/seo_put（14.11）
    /// KendoGrid batch 模式，一次送出多筆更新。
    /// </remarks>
    [HttpPut("case-seo")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    public async Task<IActionResult> BatchUpdateCaseSeo(
        [FromBody] List<UpdateCaseSeoItem> items,
        CancellationToken cancellationToken)
    {
        var result = await _caseSeoService.BatchUpdateAsync(items, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }

    /// <summary>批次更新個案 SEO 圖片</summary>
    /// <remarks>
    /// 對應舊版 PHP: Cases/seoimage_put（seo_image 欄位）
    /// 圖片上傳完成後，前端呼叫此端點更新 seo_image URL。
    /// </remarks>
    [HttpPut("case-seo/images")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    public async Task<IActionResult> BatchUpdateCaseSeoImage(
        [FromBody] List<UpdateCaseSeoImageItem> items,
        CancellationToken cancellationToken)
    {
        var result = await _caseSeoService.BatchUpdateImageAsync(items, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }

    // =========================================================================
    // 產品 SEO (product-seo)
    // =========================================================================

    /// <summary>取得產品 SEO 列表</summary>
    /// <remarks>
    /// 對應舊版 PHP: Product/seo_get（16.06）
    /// 全量回傳，排序: id DESC。
    /// </remarks>
    [HttpGet("product-seo/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<ProductSeoListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductSeoList(
        [FromQuery] ListQuery query,
        CancellationToken cancellationToken)
    {
        var data = await _productSeoService.GetListAsync(query, cancellationToken);
        return Ok(ApiResponse<PagedResponse<ProductSeoListItem>>.Success(data));
    }

    /// <summary>批次更新產品 SEO 標題</summary>
    /// <remarks>
    /// 對應舊版 PHP: Product/seo_put（16.07）
    /// KendoGrid batch 模式，一次送出多筆更新。
    /// </remarks>
    [HttpPut("product-seo")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    public async Task<IActionResult> BatchUpdateProductSeo(
        [FromBody] List<UpdateProductSeoItem> items,
        CancellationToken cancellationToken)
    {
        var result = await _productSeoService.BatchUpdateAsync(items, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }

    /// <summary>批次更新產品 SEO 圖片</summary>
    /// <remarks>
    /// 對應舊版 PHP: Product/seoimage_put（16.08）
    /// 圖片上傳完成後，前端呼叫此端點更新 seo_image URL。
    /// </remarks>
    [HttpPut("product-seo/images")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    public async Task<IActionResult> BatchUpdateProductSeoImage(
        [FromBody] List<UpdateProductSeoImageItem> items,
        CancellationToken cancellationToken)
    {
        var result = await _productSeoService.BatchUpdateImageAsync(items, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }
}

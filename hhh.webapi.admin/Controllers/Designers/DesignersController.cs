using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Designers.Hcases;
using hhh.api.contracts.admin.Designers.Hdesigners;
using hhh.application.admin.Designers.Hcases;
using hhh.application.admin.Designers.Hdesigners;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hhh.webapi.admin.Controllers.Designers;

/// <summary>
/// 設計師
/// </summary>
/// <remarks>
/// 後台「設計師」業務類別下所有 endpoint 集中於此 controller。
/// 包含設計師資料 + 案例(個案屬於設計師作品集)。
///
/// 對應舊版 PHP:
///  - 設計師:/backend/_hdesigner.php、_hdesigner_edit.php、_hdesigner_sort.php、_hdesigner_mobile_sort.php
///  - 個案:/backend/_hcase.php、_hcase_edit.php、_hcase_sort.php
/// </remarks>
[Route("api/designers")]
[Authorize]
[Tags("Designers")]
public class DesignersController : ApiControllerBase
{
    private readonly IHdesignerService _hdesignerService;
    private readonly IHcaseService _hcaseService;

    public DesignersController(
        IHdesignerService hdesignerService,
        IHcaseService hcaseService)
    {
        _hdesignerService = hdesignerService;
        _hcaseService = hcaseService;
    }

    // =========================================================================
    // 設計師 (hdesigners)
    // =========================================================================

    /// <summary>取得設計師分頁列表</summary>
    /// <remarks>
    /// 對應舊版 /backend/_hdesigner.php。
    /// q 跨欄位:id / title / name / mail / website / phone / address。
    /// searchByIdOnly=true 時 q 僅比對 hdesigner_id 精準值。
    /// 排序白名單:id, title, name, dorder, mobileOrder, onoff, creatTime, updateTime。
    /// </remarks>
    [HttpGet("hdesigners")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<HdesignerListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetHdesignerList(
        [FromQuery] HdesignerListRequest request,
        CancellationToken cancellationToken)
    {
        var data = await _hdesignerService.GetListAsync(request, cancellationToken);
        return Ok(ApiResponse<PagedResponse<HdesignerListItem>>.Success(data));
    }

    /// <summary>取得單一設計師完整資料</summary>
    /// <remarks>對應舊版 /backend/_hdesigner_edit.php?id={id} (GET)。</remarks>
    [HttpGet("hdesigners/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<HdesignerDetailResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetHdesignerById(uint id, CancellationToken cancellationToken)
    {
        var hdesigner = await _hdesignerService.GetByIdAsync(id, cancellationToken);
        if (hdesigner is null)
        {
            return StatusCode(StatusCodes.Status404NotFound,
                ApiResponse.Error(StatusCodes.Status404NotFound, "找不到設計師"));
        }

        return Ok(ApiResponse<HdesignerDetailResponse>.Success(hdesigner));
    }

    /// <summary>新增設計師</summary>
    /// <remarks>
    /// 對應舊版 /backend/_hdesigner_edit.php (POST 無 id 分支)。
    /// 寫入時 idea 欄位會自動同步到 description。
    /// </remarks>
    [HttpPost("hdesigners")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateHdesigner(
        [FromBody] CreateHdesignerRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _hdesignerService.CreateAsync(request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return StatusCode(
            StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { id = result.Data }, result.Message));
    }

    /// <summary>更新設計師</summary>
    /// <remarks>
    /// 對應舊版 /backend/_hdesigner_edit.php (POST 帶 id 分支)。
    /// 寫入時 idea 同步到 description、update_time 設為現在時間。
    /// 排序欄位 (dorder / mobile_order) 不在此端點修改,請改用 sort-order / mobile-sort-order。
    /// </remarks>
    [HttpPut("hdesigners/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateHdesigner(
        uint id,
        [FromBody] UpdateHdesignerRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _hdesignerService.UpdateAsync(id, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { id = result.Data }, result.Message));
    }

    /// <summary>批次更新桌機版排序</summary>
    /// <remarks>對應舊版 /backend/_hdesigner_sort.php。只會更新 onoff=1 的設計師。</remarks>
    [HttpPut("hdesigners/sort-order")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateHdesignerSortOrder(
        [FromBody] UpdateHdesignerSortOrderRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _hdesignerService.UpdateSortOrderAsync(request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }

    /// <summary>批次更新手機版排序</summary>
    /// <remarks>對應舊版 /backend/_hdesigner_mobile_sort.php。</remarks>
    [HttpPut("hdesigners/mobile-sort-order")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateHdesignerMobileSortOrder(
        [FromBody] UpdateHdesignerSortOrderRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _hdesignerService.UpdateMobileSortOrderAsync(request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }

    // =========================================================================
    // 個案 (hcases)
    // =========================================================================

    /// <summary>取得個案分頁列表</summary>
    /// <remarks>
    /// 對應舊版 /backend/_hcase.php。
    /// q 跨欄位 LIKE:id / caption / location / style / type / 設計師 title / 設計師 name。
    /// hdesignerId:只看某位設計師底下的個案。
    /// 排序白名單:id, caption, hdesignerId, viewed, corder, creatTime, updateTime, onoff, sdate, recommend。
    /// </remarks>
    [HttpGet("hcases")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<HcaseListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetHcaseList(
        [FromQuery] HcaseListRequest request,
        CancellationToken cancellationToken)
    {
        var data = await _hcaseService.GetListAsync(request, cancellationToken);
        return Ok(ApiResponse<PagedResponse<HcaseListItem>>.Success(data));
    }

    /// <summary>取得單一個案完整資料</summary>
    /// <remarks>對應舊版 /backend/_hcase_edit.php?id={id} (GET)。回傳包含 JOIN 的設計師 title / name。</remarks>
    [HttpGet("hcases/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<HcaseDetailResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetHcaseById(uint id, CancellationToken cancellationToken)
    {
        var hcase = await _hcaseService.GetByIdAsync(id, cancellationToken);
        if (hcase is null)
        {
            return StatusCode(StatusCodes.Status404NotFound,
                ApiResponse.Error(StatusCodes.Status404NotFound, "找不到個案"));
        }

        return Ok(ApiResponse<HcaseDetailResponse>.Success(hcase));
    }

    /// <summary>新增個案</summary>
    /// <remarks>
    /// 對應舊版 /backend/_hcase_edit.php (POST 無 id 分支)。
    /// 寫入時自動合併 style/style2/type/condition 為 tag 欄位,並設定 tag_datetime / creat_time / update_time。
    /// </remarks>
    [HttpPost("hcases")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateHcase(
        [FromBody] CreateHcaseRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _hcaseService.CreateAsync(request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return StatusCode(
            StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { id = result.Data }, result.Message));
    }

    /// <summary>更新個案</summary>
    /// <remarks>
    /// 對應舊版 /backend/_hcase_edit.php (POST 帶 id 分支)。
    /// 寫入時:重新合併 tag、若 fee 變動自動把 auto_count_fee 設為 false、更新 update_time。
    /// 排序欄位 corder 不在此端點修改,請改用 hcases/sort-order。
    /// </remarks>
    [HttpPut("hcases/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateHcase(
        uint id,
        [FromBody] UpdateHcaseRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _hcaseService.UpdateAsync(id, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { id = result.Data }, result.Message));
    }

    /// <summary>批次更新某設計師底下的個案排序</summary>
    /// <remarks>
    /// 對應舊版 /backend/_hcase_sort.php。
    /// Body 帶 hdesignerId + featured 與 normal 兩個陣列:
    ///  - featured:首六區個案,corder = position - 1000(負值)
    ///  - normal:一般區個案,corder 一律設為 0
    /// 同時把該設計師的 _hdesigner.update_time 更新為現在時間。
    /// 跨設計師的 id 會被忽略。
    /// </remarks>
    [HttpPut("hcases/sort-order")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateHcaseSortOrder(
        [FromBody] UpdateHcaseSortOrderRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _hcaseService.UpdateSortOrderAsync(request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }
}

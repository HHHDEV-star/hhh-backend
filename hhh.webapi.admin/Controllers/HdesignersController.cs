using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Hdesigners;
using hhh.application.admin.Hdesigners;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hhh.webapi.admin.Controllers;

[Route("api/[controller]")]
[Authorize]
public class HdesignersController : ApiControllerBase
{
    private readonly IHdesignerService _hdesignerService;

    public HdesignersController(IHdesignerService hdesignerService)
    {
        _hdesignerService = hdesignerService;
    }

    /// <summary>
    /// 取得設計師分頁列表
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_hdesigner.php
    /// 支援 q / searchByIdOnly / page / pageSize / sort / by 查詢參數。
    /// q：跨欄位 LIKE 搜尋（id / title / name / mail / website / phone / address）。
    /// searchByIdOnly=true 時 q 僅比對 hdesigner_id 精準值。
    /// 排序白名單：id, title, name, dorder, mobileOrder, onoff, creatTime, updateTime。
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<HdesignerListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetList(
        [FromQuery] HdesignerListRequest request,
        CancellationToken cancellationToken)
    {
        var data = await _hdesignerService.GetListAsync(request, cancellationToken);
        return Ok(ApiResponse<PagedResponse<HdesignerListItem>>.Success(data));
    }

    /// <summary>
    /// 取得單一設計師完整資料
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_hdesigner_edit.php?id={id} （GET 模式）
    /// </remarks>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<HdesignerDetailResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        uint id,
        CancellationToken cancellationToken)
    {
        var hdesigner = await _hdesignerService.GetByIdAsync(id, cancellationToken);
        if (hdesigner is null)
        {
            return StatusCode(StatusCodes.Status404NotFound,
                ApiResponse.Error(StatusCodes.Status404NotFound, "找不到設計師"));
        }

        return Ok(ApiResponse<HdesignerDetailResponse>.Success(hdesigner));
    }

    /// <summary>
    /// 新增設計師
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_hdesigner_edit.php （POST 無 id 分支）
    /// 寫入時 idea 欄位會自動同步到 description。
    /// 成功時回傳 HTTP 201 Created。
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(
        [FromBody] CreateHdesignerRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _hdesignerService.CreateAsync(request, cancellationToken);
        if (!result.IsSuccess)
        {
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        }

        return StatusCode(
            StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { id = result.Data }, result.Message));
    }

    /// <summary>
    /// 更新設計師
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_hdesigner_edit.php （POST 帶 id 分支）
    /// 寫入時 idea 欄位會自動同步到 description、update_time 設為現在時間。
    /// 排序欄位（dorder / mobile_order）不在此端點修改，請改用 /sort-order 或 /mobile-sort-order。
    /// </remarks>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        uint id,
        [FromBody] UpdateHdesignerRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _hdesignerService.UpdateAsync(id, request, cancellationToken);
        if (!result.IsSuccess)
        {
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        }

        return Ok(ApiResponse<object>.Success(
            new { id = result.Data }, result.Message));
    }

    /// <summary>
    /// 批次更新桌機版排序
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_hdesigner_sort.php
    /// 前端拖拉後一次送上 { items: [{ id, order }] }。
    /// 只會更新 onoff=1 的設計師，關閉狀態將被忽略。
    /// </remarks>
    [HttpPut("sort-order")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateSortOrder(
        [FromBody] UpdateHdesignerSortOrderRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _hdesignerService.UpdateSortOrderAsync(request, cancellationToken);
        if (!result.IsSuccess)
        {
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        }

        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }

    /// <summary>
    /// 批次更新手機版排序
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_hdesigner_mobile_sort.php
    /// 前端拖拉後一次送上 { items: [{ id, order }] }。
    /// </remarks>
    [HttpPut("mobile-sort-order")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateMobileSortOrder(
        [FromBody] UpdateHdesignerSortOrderRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _hdesignerService.UpdateMobileSortOrderAsync(request, cancellationToken);
        if (!result.IsSuccess)
        {
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        }

        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }
}

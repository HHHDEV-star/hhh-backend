using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Hawards;
using hhh.application.admin.Hawards;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hhh.webapi.admin.Controllers;

[Route("api/[controller]")]
[Authorize]
public class HawardsController : ApiControllerBase
{
    private readonly IHawardService _hawardService;

    public HawardsController(IHawardService hawardService)
    {
        _hawardService = hawardService;
    }

    /// <summary>
    /// 取得得獎記錄分頁列表
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_hawards.php
    /// 支援 q / hdesignerId / hcaseId / page / pageSize / sort / by 查詢參數。
    /// q：跨欄位 LIKE 搜尋（hawards_id / awards_name / hdesigner_id / hcase_id / designer name / case caption）。
    /// 排序白名單：id, awardsName, hdesignerId, hcaseId, onoff。
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<HawardListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetList(
        [FromQuery] HawardListRequest request,
        CancellationToken cancellationToken)
    {
        var data = await _hawardService.GetListAsync(request, cancellationToken);
        return Ok(ApiResponse<PagedResponse<HawardListItem>>.Success(data));
    }

    /// <summary>
    /// 取得單一得獎記錄完整資料
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_hawards_edit.php?id={id} （GET 模式）
    /// </remarks>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<HawardDetailResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        uint id,
        CancellationToken cancellationToken)
    {
        var haward = await _hawardService.GetByIdAsync(id, cancellationToken);
        if (haward is null)
        {
            return StatusCode(StatusCodes.Status404NotFound,
                ApiResponse.Error(StatusCodes.Status404NotFound, "找不到得獎記錄"));
        }

        return Ok(ApiResponse<HawardDetailResponse>.Success(haward));
    }

    /// <summary>
    /// 新增得獎記錄
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_hawards_edit.php （POST 無 id 分支）
    /// 會驗證 logo 白名單、設計師 / 個案 FK 是否存在。
    /// 成功時回傳 HTTP 201 Created。
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(
        [FromBody] CreateHawardRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _hawardService.CreateAsync(request, cancellationToken);
        if (!result.IsSuccess)
        {
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        }

        return StatusCode(
            StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { id = result.Data }, result.Message));
    }

    /// <summary>
    /// 更新得獎記錄
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_hawards_edit.php （POST 帶 id 分支）
    /// 會驗證 logo 白名單、設計師 / 個案 FK 是否存在。
    /// </remarks>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        uint id,
        [FromBody] UpdateHawardRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _hawardService.UpdateAsync(id, request, cancellationToken);
        if (!result.IsSuccess)
        {
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        }

        return Ok(ApiResponse<object>.Success(
            new { id = result.Data }, result.Message));
    }

    /// <summary>
    /// 刪除得獎記錄
    /// </summary>
    /// <remarks>
    /// 舊版 PHP UI 沒有刪除按鈕，新 API 補上標準 DELETE 動作。
    /// </remarks>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        uint id,
        CancellationToken cancellationToken)
    {
        var result = await _hawardService.DeleteAsync(id, cancellationToken);
        if (!result.IsSuccess)
        {
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        }

        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }
}

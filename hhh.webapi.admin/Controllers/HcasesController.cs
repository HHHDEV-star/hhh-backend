using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Hcases;
using hhh.application.admin.Hcases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hhh.webapi.admin.Controllers;

[Route("api/[controller]")]
[Authorize]
public class HcasesController : ApiControllerBase
{
    private readonly IHcaseService _hcaseService;

    public HcasesController(IHcaseService hcaseService)
    {
        _hcaseService = hcaseService;
    }

    /// <summary>
    /// 取得個案分頁列表
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_hcase.php
    /// 支援 q / hdesignerId / page / pageSize / sort / by 查詢參數。
    /// q：跨欄位 LIKE 搜尋（id / caption / location / style / type / 設計師 title / 設計師 name）。
    /// hdesignerId：只看某位設計師底下的個案。
    /// 排序白名單：id, caption, hdesignerId, viewed, corder, creatTime, updateTime, onoff, sdate, recommend。
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<HcaseListResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetList(
        [FromQuery] HcaseListRequest request,
        CancellationToken cancellationToken)
    {
        var data = await _hcaseService.GetListAsync(request, cancellationToken);
        return Ok(ApiResponse<HcaseListResponse>.Success(data));
    }

    /// <summary>
    /// 取得單一個案完整資料
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_hcase_edit.php?id={id} （GET 模式）
    /// 回傳包含 JOIN 的設計師 title / name。
    /// </remarks>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<HcaseDetailResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        uint id,
        CancellationToken cancellationToken)
    {
        var hcase = await _hcaseService.GetByIdAsync(id, cancellationToken);
        if (hcase is null)
        {
            return StatusCode(StatusCodes.Status404NotFound,
                ApiResponse.Error(StatusCodes.Status404NotFound, "找不到個案"));
        }

        return Ok(ApiResponse<HcaseDetailResponse>.Success(hcase));
    }

    /// <summary>
    /// 新增個案
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_hcase_edit.php （POST 無 id 分支）
    /// 寫入時會自動合併 style/style2/type/condition 為 tag 欄位，並設定 tag_datetime / creat_time / update_time。
    /// 成功時回傳 HTTP 201 Created。
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create(
        [FromBody] CreateHcaseRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _hcaseService.CreateAsync(request, cancellationToken);
        if (!result.IsSuccess)
        {
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        }

        return StatusCode(
            StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { id = result.HcaseId }, result.Message));
    }

    /// <summary>
    /// 更新個案
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_hcase_edit.php （POST 帶 id 分支）
    /// 寫入時會：
    ///  - 重新合併 style/style2/type/condition 為 tag、更新 tag_datetime
    ///  - 若 fee 與 DB 現值不同，自動把 auto_count_fee 設為 false
    ///  - 更新 update_time
    /// 排序欄位 corder 不在此端點修改，請改用 /sort-order。
    /// </remarks>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        uint id,
        [FromBody] UpdateHcaseRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _hcaseService.UpdateAsync(id, request, cancellationToken);
        if (!result.IsSuccess)
        {
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        }

        return Ok(ApiResponse<object>.Success(
            new { id = result.HcaseId }, result.Message));
    }

    /// <summary>
    /// 批次更新某設計師底下的個案排序
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_hcase_sort.php
    /// Body 需帶 hdesignerId + featured 與 normal 兩個陣列：
    ///  - featured 陣列：首六區個案；第 N 個會寫入 corder = N - 1000（負值）
    ///  - normal 陣列：一般區個案；corder 會一律設為 0
    /// 同時會把該設計師的 _hdesigner.update_time 更新為現在時間。
    /// 只會更新屬於指定設計師的個案，跨設計師的 id 會被忽略。
    /// </remarks>
    [HttpPut("sort-order")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateSortOrder(
        [FromBody] UpdateHcaseSortOrderRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _hcaseService.UpdateSortOrderAsync(request, cancellationToken);
        if (!result.IsSuccess)
        {
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        }

        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }
}

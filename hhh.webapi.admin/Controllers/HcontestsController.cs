using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Hcontests;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using hhh.infrastructure.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hhh.webapi.admin.Controllers;

/// <summary>
/// 競賽報名管理 API(對應舊版 /backend/_hcontest.php、_hcontest_edit.php)。
/// 純 thin CRUD,沒有業務邏輯,直接在 controller 戳 XoopsContext。
///
/// 備註:c1~c13 欄位 schema 暫時原樣保留(c4/c13 業務意義未釐清),
/// 待業務單位確認後再 rename。
/// </summary>
[Route("api/[controller]")]
[Authorize]
public class HcontestsController : ApiControllerBase
{
    private const string PageName = "競賽報名";

    private readonly XoopsContext _db;
    private readonly IOperationLogWriter _logWriter;

    public HcontestsController(XoopsContext db, IOperationLogWriter logWriter)
    {
        _db = db;
        _logWriter = logWriter;
    }

    /// <summary>
    /// 取得競賽報名分頁列表
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_hcontest.php
    /// 支援 q / year / classType / finalist / page / pageSize / sort / by 查詢參數。
    /// q 跨欄位:contest_id / class_type / year / c1 / c2 / c3 / c9。
    /// 排序白名單:id, year, classType, applytime, finalist, wp。
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<HcontestListResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetList(
        [FromQuery] HcontestListRequest request,
        CancellationToken cancellationToken)
    {
        var query = _db.Hcontests.AsNoTracking();

        // 關鍵字搜尋 ----------------------------------------------------------
        if (!string.IsNullOrWhiteSpace(request.Q))
        {
            var q = request.Q.Trim();
            var like = $"%{q}%";
            query = query.Where(h =>
                EF.Functions.Like(h.ContestId.ToString(), like) ||
                EF.Functions.Like(h.ClassType, like) ||
                EF.Functions.Like(h.Year.ToString(), like) ||
                EF.Functions.Like(h.C1, like) ||
                EF.Functions.Like(h.C2, like) ||
                EF.Functions.Like(h.C3, like) ||
                EF.Functions.Like(h.C9, like));
        }

        // 精準過濾 ------------------------------------------------------------
        if (request.Year is { } year)
        {
            query = query.Where(h => h.Year == year);
        }

        if (!string.IsNullOrWhiteSpace(request.ClassType))
        {
            var ct = request.ClassType.Trim();
            query = query.Where(h => h.ClassType == ct);
        }

        if (request.Finalist is { } finalist)
        {
            byte flag = (byte)(finalist ? 1 : 0);
            query = query.Where(h => h.Finalist == flag);
        }

        var total = await query.LongCountAsync(cancellationToken);

        var ordered = ApplyOrdering(query, request.Sort, request.By);

        var items = await ordered
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(h => new HcontestListItem
            {
                Id = h.ContestId,
                Year = h.Year,
                C1 = h.C1,
                C2 = h.C2,
                C3 = h.C3,
                C9 = h.C9,
                An = h.An,
                Finalist = h.Finalist == 1,
            })
            .ToListAsync(cancellationToken);

        var response = new HcontestListResponse
        {
            Items = items,
            Total = total,
            Page = request.Page,
            PageSize = request.PageSize,
        };

        return Ok(ApiResponse<HcontestListResponse>.Success(response));
    }

    /// <summary>
    /// 取得單筆競賽報名完整資料
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_hcontest_edit.php?id={id} (GET 模式)
    /// </remarks>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<HcontestDetailResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        uint id,
        CancellationToken cancellationToken)
    {
        var detail = await _db.Hcontests
            .AsNoTracking()
            .Where(h => h.ContestId == id)
            .Select(h => new HcontestDetailResponse
            {
                Id = h.ContestId,
                Uid = h.Uid,
                Year = h.Year,
                ClassType = h.ClassType,
                C1 = h.C1,
                C2 = h.C2,
                C3 = h.C3,
                C4 = h.C4,
                C5 = h.C5,
                C6 = h.C6,
                C7 = h.C7,
                C8 = h.C8,
                C9 = h.C9,
                C10 = h.C10,
                C11 = h.C11,
                C12 = h.C12,
                C13 = h.C13,
                Applytime = h.Applytime,
                Pay = h.Pay,
                An = h.An,
                Score = h.Score,
                Wp = h.Wp,
                WpDetail = h.WpDetail,
                Finalist = h.Finalist == 1,
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (detail is null)
        {
            return StatusCode(StatusCodes.Status404NotFound,
                ApiResponse.Error(StatusCodes.Status404NotFound, "找不到競賽報名資料"));
        }

        return Ok(ApiResponse<HcontestDetailResponse>.Success(detail));
    }

    /// <summary>
    /// 新增競賽報名
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_hcontest_edit.php (POST 無 id 分支)
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(
        [FromBody] CreateHcontestRequest request,
        CancellationToken cancellationToken)
    {
        var entity = new Hcontest
        {
            Uid = request.Uid,
            Year = request.Year,
            ClassType = request.ClassType ?? string.Empty,
            C1 = request.C1 ?? string.Empty,
            C2 = request.C2 ?? string.Empty,
            C3 = request.C3 ?? string.Empty,
            C4 = request.C4 ?? string.Empty,
            C5 = request.C5 ?? string.Empty,
            C6 = request.C6 ?? string.Empty,
            C7 = request.C7 ?? string.Empty,
            C8 = request.C8 ?? string.Empty,
            C9 = request.C9 ?? string.Empty,
            C10 = request.C10 ?? string.Empty,
            C11 = request.C11 ?? string.Empty,
            C12 = request.C12 ?? string.Empty,
            C13 = request.C13 ?? string.Empty,
            Applytime = request.Applytime ?? DateTime.Now,
            Pay = request.Pay ?? string.Empty,
            An = request.An ?? string.Empty,
            Score = request.Score ?? string.Empty,
            Wp = request.Wp,
            WpDetail = request.WpDetail ?? string.Empty,
            Finalist = (byte)(request.Finalist ? 1 : 0),
        };

        _db.Hcontests.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Create,
            $"新增競賽報名 id={entity.ContestId} 年份={request.Year} 報名者={request.C2}",
            cancellationToken: cancellationToken);

        return StatusCode(
            StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { id = entity.ContestId }, "新增成功"));
    }

    /// <summary>
    /// 更新競賽報名
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_hcontest_edit.php (POST 帶 id 分支)
    /// </remarks>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        uint id,
        [FromBody] UpdateHcontestRequest request,
        CancellationToken cancellationToken)
    {
        var entity = await _db.Hcontests
            .FirstOrDefaultAsync(h => h.ContestId == id, cancellationToken);

        if (entity is null)
        {
            return StatusCode(StatusCodes.Status404NotFound,
                ApiResponse.Error(StatusCodes.Status404NotFound, "找不到競賽報名資料"));
        }

        entity.Uid = request.Uid;
        entity.Year = request.Year;
        entity.ClassType = request.ClassType ?? string.Empty;
        entity.C1 = request.C1 ?? string.Empty;
        entity.C2 = request.C2 ?? string.Empty;
        entity.C3 = request.C3 ?? string.Empty;
        entity.C4 = request.C4 ?? string.Empty;
        entity.C5 = request.C5 ?? string.Empty;
        entity.C6 = request.C6 ?? string.Empty;
        entity.C7 = request.C7 ?? string.Empty;
        entity.C8 = request.C8 ?? string.Empty;
        entity.C9 = request.C9 ?? string.Empty;
        entity.C10 = request.C10 ?? string.Empty;
        entity.C11 = request.C11 ?? string.Empty;
        entity.C12 = request.C12 ?? string.Empty;
        entity.C13 = request.C13 ?? string.Empty;
        // applytime 若沒傳就保留原值(不覆寫成 Now)
        if (request.Applytime is { } newTime)
        {
            entity.Applytime = newTime;
        }
        entity.Pay = request.Pay ?? string.Empty;
        entity.An = request.An ?? string.Empty;
        entity.Score = request.Score ?? string.Empty;
        entity.Wp = request.Wp;
        entity.WpDetail = request.WpDetail ?? string.Empty;
        entity.Finalist = (byte)(request.Finalist ? 1 : 0);

        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Update,
            $"修改競賽報名 id={id} 年份={request.Year} 報名者={request.C2}",
            cancellationToken: cancellationToken);

        return Ok(ApiResponse<object>.Success(new { id }, "修改成功"));
    }

    /// <summary>
    /// 刪除競賽報名
    /// </summary>
    /// <remarks>
    /// 舊版 PHP 沒有 delete 分支,這裡為 REST 完整性補上。
    /// </remarks>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        uint id,
        CancellationToken cancellationToken)
    {
        var entity = await _db.Hcontests
            .FirstOrDefaultAsync(h => h.ContestId == id, cancellationToken);

        if (entity is null)
        {
            return StatusCode(StatusCodes.Status404NotFound,
                ApiResponse.Error(StatusCodes.Status404NotFound, "找不到競賽報名資料"));
        }

        var oldYear = entity.Year;
        var oldName = entity.C2;

        _db.Hcontests.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Delete,
            $"刪除競賽報名 id={id} 年份={oldYear} 報名者={oldName}",
            cancellationToken: cancellationToken);

        return Ok(ApiResponse<object>.Success(new { }, "刪除成功"));
    }

    // -------------------------------------------------------------------
    // Helpers
    // -------------------------------------------------------------------

    /// <summary>
    /// 排序白名單:未列出的欄位會 fallback 到 ContestId。
    /// 不信任 client 直接組 SQL ORDER BY 片段,避免 injection。
    /// </summary>
    private static IOrderedQueryable<Hcontest> ApplyOrdering(
        IQueryable<Hcontest> query,
        string? sort,
        string? by)
    {
        var isAsc = string.Equals(by, "ASC", StringComparison.OrdinalIgnoreCase);
        var key = sort?.ToLowerInvariant();

        return key switch
        {
            "year" => isAsc ? query.OrderBy(h => h.Year) : query.OrderByDescending(h => h.Year),
            "classtype" => isAsc ? query.OrderBy(h => h.ClassType) : query.OrderByDescending(h => h.ClassType),
            "applytime" => isAsc ? query.OrderBy(h => h.Applytime) : query.OrderByDescending(h => h.Applytime),
            "finalist" => isAsc ? query.OrderBy(h => h.Finalist) : query.OrderByDescending(h => h.Finalist),
            "wp" => isAsc ? query.OrderBy(h => h.Wp) : query.OrderByDescending(h => h.Wp),
            _ => isAsc ? query.OrderBy(h => h.ContestId) : query.OrderByDescending(h => h.ContestId),
        };
    }
}

using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Htopic2s;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using hhh.infrastructure.Extensions;
using hhh.infrastructure.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hhh.webapi.admin.Controllers;

/// <summary>
/// 議題 2 管理 API(對應舊版 /backend/_htopic2.php、_htopic2_edit.php)。
/// 純 thin CRUD,沒有業務邏輯,因此不在 application.admin 層建 service,
/// 直接在 controller 戳 XoopsContext(符合 CLAUDE.md 的 pragmatic 原則)。
///
/// 備註:舊 PHP 的 _htopic2 與 _htopic 業務差異不明(MIGRATION_PLAN §1.3),
/// 這裡先照 schema 原樣遷移,後續若確認是死碼再整批刪除。
/// </summary>
[Route("api/[controller]")]
[Authorize]
public class Htopic2sController : ApiControllerBase
{
    private const string PageName = "議題2";

    private readonly XoopsContext _db;
    private readonly IOperationLogWriter _logWriter;

    public Htopic2sController(XoopsContext db, IOperationLogWriter logWriter)
    {
        _db = db;
        _logWriter = logWriter;
    }

    /// <summary>
    /// 取得議題 2 分頁列表
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_htopic2.php
    /// 支援 q / onoff / page / pageSize / sort / by 查詢參數。
    /// 排序白名單:id, title, onoff。
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<Htopic2ListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetList(
        [FromQuery] Htopic2ListRequest request,
        CancellationToken cancellationToken)
    {
        var query = _db.Htopic2s.AsNoTracking();

        // 關鍵字搜尋 ----------------------------------------------------------
        if (!string.IsNullOrWhiteSpace(request.Q))
        {
            var q = request.Q.Trim();
            var like = $"%{q}%";
            query = query.Where(h =>
                EF.Functions.Like(h.Id.ToString(), like) ||
                EF.Functions.Like(h.Title, like) ||
                EF.Functions.Like(h.Desc, like));
        }

        // 上線狀態過濾 --------------------------------------------------------
        if (request.Onoff is { } onoff)
        {
            byte flag = (byte)(onoff ? 1 : 0);
            query = query.Where(h => h.Onoff == flag);
        }

        // 排序白名單 ----------------------------------------------------------
        var ordered = ApplyOrdering(query, request.Sort, request.By);

        var response = await ordered
            .Select(h => new Htopic2ListItem
            {
                Id = h.Id,
                Title = h.Title,
                Desc = h.Desc,
                Logo = h.Logo,
                StrarrHdesignerId = h.StrarrHdesignerId,
                StrarrHcaseId = h.StrarrHcaseId,
                StrarrHvideoId = h.StrarrHvideoId,
                StrarrHcolumnId = h.StrarrHcolumnId,
                Onoff = h.Onoff == 1,
            })
            .ToPagedResponseAsync(request.Page, request.PageSize, cancellationToken);

        return Ok(ApiResponse<PagedResponse<Htopic2ListItem>>.Success(response));
    }

    /// <summary>
    /// 取得單一議題 2 完整資料
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_htopic2_edit.php?id={id} (GET 模式)
    /// </remarks>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<Htopic2DetailResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        uint id,
        CancellationToken cancellationToken)
    {
        var detail = await _db.Htopic2s
            .AsNoTracking()
            .Where(h => h.Id == id)
            .Select(h => new Htopic2DetailResponse
            {
                Id = h.Id,
                Title = h.Title,
                Desc = h.Desc,
                Logo = h.Logo,
                StrarrHdesignerId = h.StrarrHdesignerId,
                StrarrHcaseId = h.StrarrHcaseId,
                StrarrHvideoId = h.StrarrHvideoId,
                StrarrHcolumnId = h.StrarrHcolumnId,
                Onoff = h.Onoff == 1,
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (detail is null)
        {
            return StatusCode(StatusCodes.Status404NotFound,
                ApiResponse.Error(StatusCodes.Status404NotFound, "找不到議題 2 資料"));
        }

        return Ok(ApiResponse<Htopic2DetailResponse>.Success(detail));
    }

    /// <summary>
    /// 新增議題 2
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_htopic2_edit.php (POST 無 id 分支)
    /// 成功時回傳 HTTP 201 Created。
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(
        [FromBody] CreateHtopic2Request request,
        CancellationToken cancellationToken)
    {
        var entity = new Htopic2
        {
            Title = request.Title,
            Desc = request.Desc ?? string.Empty,
            Logo = request.Logo ?? string.Empty,
            StrarrHdesignerId = request.StrarrHdesignerId ?? string.Empty,
            StrarrHcaseId = request.StrarrHcaseId ?? string.Empty,
            StrarrHvideoId = request.StrarrHvideoId ?? string.Empty,
            StrarrHcolumnId = request.StrarrHcolumnId ?? string.Empty,
            Onoff = (byte)(request.Onoff ? 1 : 0),
        };

        _db.Htopic2s.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Create,
            $"新增議題2 id={entity.Id} 標題={request.Title}",
            cancellationToken: cancellationToken);

        return StatusCode(
            StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { id = entity.Id }, "新增成功"));
    }

    /// <summary>
    /// 更新議題 2
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_htopic2_edit.php (POST 帶 id 分支)
    /// </remarks>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        uint id,
        [FromBody] UpdateHtopic2Request request,
        CancellationToken cancellationToken)
    {
        var entity = await _db.Htopic2s
            .FirstOrDefaultAsync(h => h.Id == id, cancellationToken);

        if (entity is null)
        {
            return StatusCode(StatusCodes.Status404NotFound,
                ApiResponse.Error(StatusCodes.Status404NotFound, "找不到議題 2 資料"));
        }

        entity.Title = request.Title;
        entity.Desc = request.Desc ?? string.Empty;
        entity.Logo = request.Logo ?? string.Empty;
        entity.StrarrHdesignerId = request.StrarrHdesignerId ?? string.Empty;
        entity.StrarrHcaseId = request.StrarrHcaseId ?? string.Empty;
        entity.StrarrHvideoId = request.StrarrHvideoId ?? string.Empty;
        entity.StrarrHcolumnId = request.StrarrHcolumnId ?? string.Empty;
        entity.Onoff = (byte)(request.Onoff ? 1 : 0);

        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Update,
            $"修改議題2 id={id} 標題={request.Title}",
            cancellationToken: cancellationToken);

        return Ok(ApiResponse<object>.Success(new { id }, "修改成功"));
    }

    /// <summary>
    /// 刪除議題 2
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
        var entity = await _db.Htopic2s
            .FirstOrDefaultAsync(h => h.Id == id, cancellationToken);

        if (entity is null)
        {
            return StatusCode(StatusCodes.Status404NotFound,
                ApiResponse.Error(StatusCodes.Status404NotFound, "找不到議題 2 資料"));
        }

        var oldTitle = entity.Title;

        _db.Htopic2s.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Delete,
            $"刪除議題2 id={id} 標題={oldTitle}",
            cancellationToken: cancellationToken);

        return Ok(ApiResponse<object>.Success(new { }, "刪除成功"));
    }

    // -------------------------------------------------------------------
    // Helpers
    // -------------------------------------------------------------------

    /// <summary>
    /// 排序白名單:未列出的欄位會 fallback 到 Id。
    /// 不信任 client 直接組 SQL ORDER BY 片段,避免 injection。
    /// </summary>
    private static IOrderedQueryable<Htopic2> ApplyOrdering(
        IQueryable<Htopic2> query,
        string? sort,
        string? by)
    {
        var isAsc = string.Equals(by, "ASC", StringComparison.OrdinalIgnoreCase);
        var key = sort?.ToLowerInvariant();

        return key switch
        {
            "title" => isAsc ? query.OrderBy(h => h.Title) : query.OrderByDescending(h => h.Title),
            "onoff" => isAsc ? query.OrderBy(h => h.Onoff) : query.OrderByDescending(h => h.Onoff),
            _ => isAsc ? query.OrderBy(h => h.Id) : query.OrderByDescending(h => h.Id),
        };
    }
}

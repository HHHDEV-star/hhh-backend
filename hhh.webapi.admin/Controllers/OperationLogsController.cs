using hhh.api.contracts.Common;
using hhh.api.contracts.admin.OperationLogs;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hhh.webapi.admin.Controllers;

/// <summary>
/// 操作紀錄查詢 API(對應舊版 /backend/_hoplog.php)。
/// 純讀取端點,沒有業務邏輯,因此採 thin controller 直接戳 XoopsContext,
/// 不在 application.admin 層建 service(符合 CLAUDE.md 的 pragmatic 原則)。
/// </summary>
[Route("api/operation-logs")]
[Authorize]
public class OperationLogsController : ApiControllerBase
{
    private readonly XoopsContext _db;

    public OperationLogsController(XoopsContext db)
    {
        _db = db;
    }

    /// <summary>
    /// 取得操作紀錄分頁列表
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_hoplog.php
    /// 支援 q / uname / action / pageName / from / to / page / pageSize / sort / by 查詢參數。
    /// 預設依 creatTime DESC 排序(最新的在最上面)。
    /// 排序白名單:id, creatTime, uname, pageName, action。
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<OperationLogListResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetList(
        [FromQuery] OperationLogListRequest request,
        CancellationToken cancellationToken)
    {
        var query = _db.Hoplogs.AsNoTracking();

        // 關鍵字搜尋 ----------------------------------------------------------
        if (!string.IsNullOrWhiteSpace(request.Q))
        {
            var q = request.Q.Trim();
            var like = $"%{q}%";
            query = query.Where(h =>
                EF.Functions.Like(h.Uname, like) ||
                (h.PageName != null && EF.Functions.Like(h.PageName, like)) ||
                EF.Functions.Like(h.Opdesc, like) ||
                (h.Ip != null && EF.Functions.Like(h.Ip, like)));
        }

        // 精準欄位過濾 --------------------------------------------------------
        if (!string.IsNullOrWhiteSpace(request.Uname))
        {
            var uname = request.Uname.Trim();
            query = query.Where(h => h.Uname == uname);
        }

        if (!string.IsNullOrWhiteSpace(request.Action))
        {
            var action = request.Action.Trim();
            query = query.Where(h => h.Action == action);
        }

        if (!string.IsNullOrWhiteSpace(request.PageName))
        {
            var pageName = request.PageName.Trim();
            query = query.Where(h => h.PageName == pageName);
        }

        // 時間區間 ------------------------------------------------------------
        if (request.From is { } from)
        {
            query = query.Where(h => h.CreatTime >= from);
        }

        if (request.To is { } to)
        {
            query = query.Where(h => h.CreatTime <= to);
        }

        var total = await query.LongCountAsync(cancellationToken);

        // 排序白名單 ----------------------------------------------------------
        query = ApplyOrdering(query, request.Sort, request.By);

        var items = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(h => new OperationLogListItem
            {
                Id = h.Id,
                Uid = h.Uid,
                Uname = h.Uname,
                PageName = h.PageName,
                Action = h.Action,
                Opdesc = h.Opdesc,
                Ip = h.Ip,
                CreatTime = h.CreatTime,
            })
            .ToListAsync(cancellationToken);

        var response = new OperationLogListResponse
        {
            Items = items,
            Total = total,
            Page = request.Page,
            PageSize = request.PageSize,
        };

        return Ok(ApiResponse<OperationLogListResponse>.Success(response));
    }

    /// <summary>
    /// 取得單筆操作紀錄詳情(含 sqlcmd 全欄位)
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_hoplog_edit.php?id={id} (GET 模式)
    /// </remarks>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<OperationLogDetailResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        uint id,
        CancellationToken cancellationToken)
    {
        var detail = await _db.Hoplogs
            .AsNoTracking()
            .Where(h => h.Id == id)
            .Select(h => new OperationLogDetailResponse
            {
                Id = h.Id,
                Uid = h.Uid,
                Uname = h.Uname,
                PageName = h.PageName,
                Action = h.Action,
                Opdesc = h.Opdesc,
                Sqlcmd = h.Sqlcmd,
                Ip = h.Ip,
                CreatTime = h.CreatTime,
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (detail is null)
        {
            return StatusCode(StatusCodes.Status404NotFound,
                ApiResponse.Error(StatusCodes.Status404NotFound, "找不到操作紀錄"));
        }

        return Ok(ApiResponse<OperationLogDetailResponse>.Success(detail));
    }

    // -------------------------------------------------------------------
    // Helpers
    // -------------------------------------------------------------------

    /// <summary>
    /// 排序白名單:未列出的欄位會 fallback 到 CreatTime。
    /// 不信任 client 直接組 SQL ORDER BY 片段,避免 injection。
    /// </summary>
    private static IOrderedQueryable<Hoplog> ApplyOrdering(
        IQueryable<Hoplog> query,
        string? sort,
        string? by)
    {
        var isAsc = string.Equals(by, "ASC", StringComparison.OrdinalIgnoreCase);
        var key = sort?.ToLowerInvariant();

        return key switch
        {
            "id" => isAsc ? query.OrderBy(h => h.Id) : query.OrderByDescending(h => h.Id),
            "uname" => isAsc ? query.OrderBy(h => h.Uname) : query.OrderByDescending(h => h.Uname),
            "pagename" => isAsc ? query.OrderBy(h => h.PageName) : query.OrderByDescending(h => h.PageName),
            "action" => isAsc ? query.OrderBy(h => h.Action) : query.OrderByDescending(h => h.Action),
            _ => isAsc ? query.OrderBy(h => h.CreatTime) : query.OrderByDescending(h => h.CreatTime),
        };
    }
}

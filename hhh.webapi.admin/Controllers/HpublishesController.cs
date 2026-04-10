using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Hpublishes;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using hhh.infrastructure.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hhh.webapi.admin.Controllers;

/// <summary>
/// 出版管理 API(對應舊版 /backend/_hpublish.php、_hpublish_edit.php)。
/// 純 thin CRUD,沒有業務邏輯,因此不在 application.admin 層建 service,
/// 直接在 controller 戳 XoopsContext(符合 CLAUDE.md 的 pragmatic 原則)。
/// </summary>
[Route("api/[controller]")]
[Authorize]
public class HpublishesController : ApiControllerBase
{
    private const string PageName = "出版";

    private readonly XoopsContext _db;
    private readonly IOperationLogWriter _logWriter;

    public HpublishesController(XoopsContext db, IOperationLogWriter logWriter)
    {
        _db = db;
        _logWriter = logWriter;
    }

    /// <summary>
    /// 取得出版分頁列表
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_hpublish.php
    /// 支援 q / type / page / pageSize / sort / by 查詢參數。
    /// q:跨欄位 LIKE 搜尋(id / title / author / type / desc)。
    /// 排序白名單:id, title, author, type, pdate, viewed, recommend。
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<HpublishListResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetList(
        [FromQuery] HpublishListRequest request,
        CancellationToken cancellationToken)
    {
        var query = _db.Hpublishes.AsNoTracking();

        // 關鍵字搜尋 ----------------------------------------------------------
        if (!string.IsNullOrWhiteSpace(request.Q))
        {
            var q = request.Q.Trim();
            var like = $"%{q}%";
            query = query.Where(h =>
                EF.Functions.Like(h.HpublishId.ToString(), like) ||
                EF.Functions.Like(h.Title, like) ||
                EF.Functions.Like(h.Author, like) ||
                EF.Functions.Like(h.Type, like) ||
                EF.Functions.Like(h.Desc, like));
        }

        // 書籍類別精準過濾 ----------------------------------------------------
        if (!string.IsNullOrWhiteSpace(request.Type))
        {
            var type = request.Type.Trim();
            query = query.Where(h => h.Type == type);
        }

        var total = await query.LongCountAsync(cancellationToken);

        // 排序白名單 ----------------------------------------------------------
        var ordered = ApplyOrdering(query, request.Sort, request.By);

        var items = await ordered
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(h => new HpublishListItem
            {
                Id = h.HpublishId,
                Type = h.Type,
                Logo = h.Logo,
                Title = h.Title,
                Author = h.Author,
                Pdate = h.Pdate,
                Viewed = h.Viewed,
                Recommend = h.Recommend,
            })
            .ToListAsync(cancellationToken);

        var response = new HpublishListResponse
        {
            Items = items,
            Total = total,
            Page = request.Page,
            PageSize = request.PageSize,
        };

        return Ok(ApiResponse<HpublishListResponse>.Success(response));
    }

    /// <summary>
    /// 取得單一出版完整資料
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_hpublish_edit.php?id={id} (GET 模式)
    /// </remarks>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<HpublishDetailResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        uint id,
        CancellationToken cancellationToken)
    {
        var detail = await _db.Hpublishes
            .AsNoTracking()
            .Where(h => h.HpublishId == id)
            .Select(h => new HpublishDetailResponse
            {
                Id = h.HpublishId,
                Type = h.Type,
                Logo = h.Logo,
                Title = h.Title,
                Author = h.Author,
                Pdate = h.Pdate,
                Desc = h.Desc,
                Editor = h.Editor,
                Viewed = h.Viewed,
                Recommend = h.Recommend,
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (detail is null)
        {
            return StatusCode(StatusCodes.Status404NotFound,
                ApiResponse.Error(StatusCodes.Status404NotFound, "找不到出版資料"));
        }

        return Ok(ApiResponse<HpublishDetailResponse>.Success(detail));
    }

    /// <summary>
    /// 新增出版
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_hpublish_edit.php (POST 無 id 分支)
    /// 成功時回傳 HTTP 201 Created。
    /// </remarks>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create(
        [FromBody] CreateHpublishRequest request,
        CancellationToken cancellationToken)
    {
        var entity = new Hpublish
        {
            Type = request.Type,
            Logo = request.Logo ?? string.Empty,
            Title = request.Title,
            Author = request.Author,
            Pdate = request.Pdate,
            Desc = request.Desc ?? string.Empty,
            Editor = request.Editor ?? string.Empty,
            Recommend = request.Recommend,
            // viewed 系統累計,建立時固定 0
            Viewed = 0,
        };

        _db.Hpublishes.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Create,
            $"新增出版 id={entity.HpublishId} 書名={request.Title} 作者={request.Author}",
            cancellationToken: cancellationToken);

        return StatusCode(
            StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { id = entity.HpublishId }, "新增成功"));
    }

    /// <summary>
    /// 更新出版
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_hpublish_edit.php (POST 帶 id 分支)
    /// viewed 不會被覆寫,保留原始前台累計值。
    /// </remarks>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        uint id,
        [FromBody] UpdateHpublishRequest request,
        CancellationToken cancellationToken)
    {
        var entity = await _db.Hpublishes
            .FirstOrDefaultAsync(h => h.HpublishId == id, cancellationToken);

        if (entity is null)
        {
            return StatusCode(StatusCodes.Status404NotFound,
                ApiResponse.Error(StatusCodes.Status404NotFound, "找不到出版資料"));
        }

        entity.Type = request.Type;
        entity.Logo = request.Logo ?? string.Empty;
        entity.Title = request.Title;
        entity.Author = request.Author;
        entity.Pdate = request.Pdate;
        entity.Desc = request.Desc ?? string.Empty;
        entity.Editor = request.Editor ?? string.Empty;
        entity.Recommend = request.Recommend;
        // Viewed 維持原值(前台累計),不接受 API 覆寫

        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Update,
            $"修改出版 id={id} 書名={request.Title} 作者={request.Author}",
            cancellationToken: cancellationToken);

        return Ok(ApiResponse<object>.Success(new { id }, "修改成功"));
    }

    /// <summary>
    /// 刪除出版
    /// </summary>
    /// <remarks>
    /// 對應舊版 PHP: /backend/_hpublish.php?delete_id={id}
    /// </remarks>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        uint id,
        CancellationToken cancellationToken)
    {
        var entity = await _db.Hpublishes
            .FirstOrDefaultAsync(h => h.HpublishId == id, cancellationToken);

        if (entity is null)
        {
            return StatusCode(StatusCodes.Status404NotFound,
                ApiResponse.Error(StatusCodes.Status404NotFound, "找不到出版資料"));
        }

        var oldTitle = entity.Title;
        var oldAuthor = entity.Author;

        _db.Hpublishes.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Delete,
            $"刪除出版 id={id} 書名={oldTitle} 作者={oldAuthor}",
            cancellationToken: cancellationToken);

        return Ok(ApiResponse<object>.Success(new { }, "刪除成功"));
    }

    // -------------------------------------------------------------------
    // Helpers
    // -------------------------------------------------------------------

    /// <summary>
    /// 排序白名單:未列出的欄位會 fallback 到 HpublishId。
    /// 不信任 client 直接組 SQL ORDER BY 片段,避免 injection。
    /// </summary>
    private static IOrderedQueryable<Hpublish> ApplyOrdering(
        IQueryable<Hpublish> query,
        string? sort,
        string? by)
    {
        var isAsc = string.Equals(by, "ASC", StringComparison.OrdinalIgnoreCase);
        var key = sort?.ToLowerInvariant();

        return key switch
        {
            "title" => isAsc ? query.OrderBy(h => h.Title) : query.OrderByDescending(h => h.Title),
            "author" => isAsc ? query.OrderBy(h => h.Author) : query.OrderByDescending(h => h.Author),
            "type" => isAsc ? query.OrderBy(h => h.Type) : query.OrderByDescending(h => h.Type),
            "pdate" => isAsc ? query.OrderBy(h => h.Pdate) : query.OrderByDescending(h => h.Pdate),
            "viewed" => isAsc ? query.OrderBy(h => h.Viewed) : query.OrderByDescending(h => h.Viewed),
            "recommend" => isAsc ? query.OrderBy(h => h.Recommend) : query.OrderByDescending(h => h.Recommend),
            _ => isAsc ? query.OrderBy(h => h.HpublishId) : query.OrderByDescending(h => h.HpublishId),
        };
    }
}

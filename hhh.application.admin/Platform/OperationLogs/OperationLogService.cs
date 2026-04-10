using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Platform.OperationLogs;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using hhh.infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Platform.OperationLogs;

public class OperationLogService : IOperationLogService
{
    private readonly XoopsContext _db;

    public OperationLogService(XoopsContext db)
    {
        _db = db;
    }

    public async Task<PagedResponse<OperationLogListItem>> GetListAsync(
        OperationLogListRequest request,
        CancellationToken cancellationToken = default)
    {
        var query = _db.Hoplogs.AsNoTracking();

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

        if (request.From is { } from)
        {
            query = query.Where(h => h.CreatTime >= from);
        }

        if (request.To is { } to)
        {
            query = query.Where(h => h.CreatTime <= to);
        }

        var ordered = ApplyOrdering(query, request.Sort, request.By);

        return await ordered
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
            .ToPagedResponseAsync(request.Page, request.PageSize, cancellationToken);
    }

    public async Task<OperationLogDetailResponse?> GetByIdAsync(
        uint id,
        CancellationToken cancellationToken = default)
    {
        return await _db.Hoplogs
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
    }

    /// <summary>
    /// 排序白名單:未列出的欄位會 fallback 到 CreatTime DESC。
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

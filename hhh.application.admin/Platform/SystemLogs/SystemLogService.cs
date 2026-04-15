using hhh.api.contracts.admin.Platform.SystemLogs;
using hhh.api.contracts.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Platform.SystemLogs;

public class SystemLogService : ISystemLogService
{
    private readonly HhhApiContext _db;

    public SystemLogService(HhhApiContext db) => _db = db;

    // 排序白名單
    private static readonly HashSet<string> SortWhitelist = new(StringComparer.OrdinalIgnoreCase)
    {
        "id", "time", "account", "method"
    };

    // 允許的 HTTP method 值
    private static readonly HashSet<string> AllowedMethods = new(StringComparer.OrdinalIgnoreCase)
    {
        "get", "put", "post"
    };

    public async Task<PagedResponse<SystemLogListItem>> GetListAsync(
        SystemLogListRequest request, CancellationToken ct = default)
    {
        var query = _db.RestBackendLogs.AsNoTracking();

        // 帳號精準篩選
        if (!string.IsNullOrWhiteSpace(request.Account))
        {
            var account = request.Account.Trim();
            query = query.Where(l => l.Account == account);
        }

        // API URI 模糊篩選
        if (!string.IsNullOrWhiteSpace(request.ApiUri))
        {
            var like = $"%{request.ApiUri.Trim()}%";
            query = query.Where(l => EF.Functions.Like(l.Uri, like));
        }

        // 傳送方式精準篩選（僅接受 get/put/post）
        if (!string.IsNullOrWhiteSpace(request.ApiMethod)
            && AllowedMethods.Contains(request.ApiMethod.Trim()))
        {
            var method = request.ApiMethod.Trim();
            query = query.Where(l => l.Method == method);
        }

        // 時間區間
        if (request.StartDate.HasValue)
            query = query.Where(l => l.Time >= request.StartDate.Value);

        if (request.EndDate.HasValue)
            query = query.Where(l => l.Time <= request.EndDate.Value);

        // 排序
        var sortField = SortWhitelist.Contains(request.Sort ?? "")
            ? request.Sort! : "id";

        query = (sortField.ToLowerInvariant(), request.IsAsc) switch
        {
            ("time", true) => query.OrderBy(l => l.Time),
            ("time", false) => query.OrderByDescending(l => l.Time),
            ("account", true) => query.OrderBy(l => l.Account),
            ("account", false) => query.OrderByDescending(l => l.Account),
            ("method", true) => query.OrderBy(l => l.Method),
            ("method", false) => query.OrderByDescending(l => l.Method),
            (_, true) => query.OrderBy(l => l.Id),
            (_, false) => query.OrderByDescending(l => l.Id),
        };

        return await query
            .Select(l => new SystemLogListItem
            {
                Id = l.Id,
                Account = l.Account,
                Uri = l.Uri,
                Method = l.Method,
                Params = l.Params,
                ApiKey = l.ApiKey,
                IpAddress = l.IpAddress,
                Time = l.Time,
                Rtime = l.Rtime,
                Authorized = l.Authorized,
                ResponseCode = l.ResponseCode,
            })
            .ToPagedResponseAsync(request.Page, request.PageSize, ct);
    }
}

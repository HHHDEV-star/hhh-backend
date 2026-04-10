using hhh.api.contracts.admin.Users;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Users;

public class UserService : IUserService
{
    private readonly XoopsContext _db;

    public UserService(XoopsContext db)
    {
        _db = db;
    }

    public async Task<UserListResponse> GetListAsync(
        UserListRequest request,
        CancellationToken cancellationToken = default)
    {
        // 基底查詢：對應舊 PHP 的 SELECT * FROM _users WHERE 1=1
        // 目前沒有搜尋條件，保留 AsQueryable 以便之後加 filter
        var query = _db.Users.AsNoTracking();

        var total = await query.LongCountAsync(cancellationToken);

        var ordered = ApplyOrdering(query, request.Sort, request.By);

        var items = await ordered
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(u => new UserListItem
            {
                Id = u.Uid,
                Account = u.Uname,
                Email = u.Email,
                Name = u.Name,
                Tel = u.UserIntrest,
                Address = u.UserFrom,
            })
            .ToListAsync(cancellationToken);

        return new UserListResponse
        {
            Items = items,
            Total = total,
            Page = request.Page,
            PageSize = request.PageSize,
        };
    }

    /// <summary>
    /// 排序白名單：未列出的欄位會 fallback 到 Uid。
    /// 不信任 client 直接組 SQL ORDER BY 片段，避免 injection。
    /// </summary>
    private static IOrderedQueryable<User> ApplyOrdering(
        IQueryable<User> query,
        string? sort,
        string? by)
    {
        var isAsc = string.Equals(by, "ASC", StringComparison.OrdinalIgnoreCase);
        var key = sort?.ToLowerInvariant();

        return key switch
        {
            "uname" => isAsc ? query.OrderBy(u => u.Uname) : query.OrderByDescending(u => u.Uname),
            "email" => isAsc ? query.OrderBy(u => u.Email) : query.OrderByDescending(u => u.Email),
            "name" => isAsc ? query.OrderBy(u => u.Name) : query.OrderByDescending(u => u.Name),
            "regdate" => isAsc ? query.OrderBy(u => u.UserRegdate) : query.OrderByDescending(u => u.UserRegdate),
            "lastlogin" => isAsc ? query.OrderBy(u => u.LastLogin) : query.OrderByDescending(u => u.LastLogin),
            _ => isAsc ? query.OrderBy(u => u.Uid) : query.OrderByDescending(u => u.Uid),
        };
    }
}

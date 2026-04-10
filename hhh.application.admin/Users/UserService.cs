using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Users;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using hhh.infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Users;

public class UserService : IUserService
{
    private readonly XoopsContext _db;

    public UserService(XoopsContext db)
    {
        _db = db;
    }

    public async Task<PagedResponse<UserListItem>> GetListAsync(
        UserListRequest request,
        CancellationToken cancellationToken = default)
    {
        // 基底查詢:對應舊 PHP 的 SELECT * FROM _users WHERE 1=1
        // 目前沒有搜尋條件,保留 AsQueryable 以便之後加 filter
        var query = _db.Users.AsNoTracking();

        var ordered = ApplyOrdering(query, request.Sort, request.By);

        return await ordered
            .Select(u => new UserListItem
            {
                Id = u.Uid,
                Account = u.Uname,
                Email = u.Email,
                Name = u.Name,
                Tel = u.UserIntrest,
                Address = u.UserFrom,
            })
            .ToPagedResponseAsync(request.Page, request.PageSize, cancellationToken);
    }

    public async Task<UserDetailResponse?> GetByIdAsync(
        uint id,
        CancellationToken cancellationToken = default)
    {
        return await _db.Users
            .AsNoTracking()
            .Where(u => u.Uid == id)
            .Select(u => new UserDetailResponse
            {
                Id = u.Uid,
                Account = u.Uname,
                Name = u.Name,
                Email = u.Email,
                Tel = u.UserIntrest,
                Address = u.UserFrom,
                RegisteredAt = u.UserRegdateDatetime,
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<OperationResult<uint>> CreateAsync(
        CreateUserRequest request,
        CancellationToken cancellationToken = default)
    {
        // 帳號 / 信箱唯一性檢查(對應 _users 表上的 uname / email unique index)
        var accountTaken = await _db.Users
            .AnyAsync(u => u.Uname == request.Account, cancellationToken);
        if (accountTaken)
            return OperationResult<uint>.Conflict("帳號已被使用");

        var emailTaken = await _db.Users
            .AnyAsync(u => u.Email == request.Email, cancellationToken);
        if (emailTaken)
            return OperationResult<uint>.Conflict("信箱已被使用");

        // 許多欄位是 NOT NULL 但沒有 DB 預設值,這裡以 Xoops 慣例填空字串 / 0,
        // 讓 INSERT 不會因為缺欄位失敗。之後若前端需要更多欄位再擴充。
        var now = DateTime.UtcNow;
        var unixNow = (uint)DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        var user = new User
        {
            Guid = Guid.NewGuid(),
            Active = "1",
            Sex = "N",
            Birthday = DateOnly.FromDateTime(DateTime.UnixEpoch),
            Uname = request.Account,
            Name = request.Name,
            Email = request.Email,
            Pass = request.Password, // 注意:相容舊系統,明文存。改 BCrypt 需同步修改 AuthService
            Url = string.Empty,
            UserAvatar = string.Empty,
            UserRegdate = unixNow,
            UserRegdateDatetime = now,
            UserIcq = string.Empty,
            UserFrom = request.Address ?? string.Empty,
            UserViewemail = 0,
            Actkey = string.Empty,
            UserAim = string.Empty,
            UserYim = string.Empty,
            UserMsnm = string.Empty,
            Theme = string.Empty,
            Umode = string.Empty,
            NotifyMethod = false,
            UserOcc = string.Empty,
            UserIntrest = request.Tel ?? string.Empty,
            ForumBlock = "N",
            LastLoginDatetime = DateTime.UnixEpoch,
            UpdateTime = now,
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync(cancellationToken);

        return OperationResult<uint>.Created(user.Uid);
    }

    public async Task<OperationResult<uint>> UpdateAsync(
        uint id,
        UpdateUserRequest request,
        CancellationToken cancellationToken = default)
    {
        var user = await _db.Users
            .FirstOrDefaultAsync(u => u.Uid == id, cancellationToken);

        if (user is null)
            return OperationResult<uint>.NotFound("找不到會員");

        // 信箱唯一檢查(排除自己)
        var emailTaken = await _db.Users
            .AnyAsync(u => u.Email == request.Email && u.Uid != id, cancellationToken);
        if (emailTaken)
            return OperationResult<uint>.Conflict("信箱已被使用");

        user.Name = request.Name;
        user.Email = request.Email;
        user.UserIntrest = request.Tel ?? string.Empty;
        user.UserFrom = request.Address ?? string.Empty;
        user.UpdateTime = DateTime.UtcNow;

        // 只有 client 有送密碼才更新(對應原 PHP 的預期行為,
        // 原 PHP 因 $exclude_keyword 寫成 "pwd" 但 input name="pass" 而 bug)
        if (!string.IsNullOrEmpty(request.Password))
        {
            user.Pass = request.Password; // 注意:相容舊系統
        }

        await _db.SaveChangesAsync(cancellationToken);
        return OperationResult<uint>.Ok(id, "更新成功");
    }

    /// <summary>
    /// 排序白名單:未列出的欄位會 fallback 到 Uid。
    /// 不信任 client 直接組 SQL ORDER BY 片段,避免 injection。
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

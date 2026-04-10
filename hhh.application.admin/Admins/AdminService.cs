using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Admins;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using hhh.infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Admins;

public class AdminService : IAdminService
{
    private readonly XoopsContext _db;

    public AdminService(XoopsContext db)
    {
        _db = db;
    }

    public async Task<PagedResponse<AdminListItem>> GetListAsync(
        AdminListRequest request,
        CancellationToken cancellationToken = default)
    {
        // 對應舊 PHP:SELECT * FROM admin WHERE id>0
        var query = _db.Admins.AsNoTracking();

        var ordered = ApplyOrdering(query, request.Sort, request.By);

        return await ordered
            .Select(a => new AdminListItem
            {
                Id = a.Id,
                Account = a.Account,
                Name = a.Name ?? string.Empty,
                Email = a.Email ?? string.Empty,
                Tel = a.Tel ?? string.Empty,
                CreateTime = a.CreateTime,
                IsActive = a.IsActive == 1,
            })
            .ToPagedResponseAsync(request.Page, request.PageSize, cancellationToken);
    }

    public async Task<AdminDetailResponse?> GetByIdAsync(
        uint id,
        CancellationToken cancellationToken = default)
    {
        var admin = await _db.Admins
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        if (admin is null)
            return null;

        return new AdminDetailResponse
        {
            Id = admin.Id,
            Account = admin.Account,
            Name = admin.Name ?? string.Empty,
            Email = admin.Email ?? string.Empty,
            Tel = admin.Tel ?? string.Empty,
            CreateTime = admin.CreateTime,
            AllowPage = ParseAllowPage(admin.AllowPage),
            IsActive = admin.IsActive == 1,
        };
    }

    public async Task<OperationResult<uint>> CreateAsync(
        CreateAdminRequest request,
        CancellationToken cancellationToken = default)
    {
        // 帳號唯一性檢查(對應 admin.account unique index)
        var accountTaken = await _db.Admins
            .AnyAsync(a => a.Account == request.Account, cancellationToken);
        if (accountTaken)
            return OperationResult<uint>.Conflict("帳號已被使用");

        var admin = new Admin
        {
            Account = request.Account,
            Pwd = request.Pwd, // 注意:相容舊系統,明文存。改 BCrypt 需同步修改 AuthService
            Name = request.Name,
            Email = request.Email,
            Tel = request.Tel,
            AllowPage = JoinAllowPage(request.AllowPage),
            IsActive = (sbyte)(request.IsActive ? 1 : 0),
            CreateTime = DateTime.UtcNow,
        };

        _db.Admins.Add(admin);
        await _db.SaveChangesAsync(cancellationToken);

        return OperationResult<uint>.Created(admin.Id);
    }

    public async Task<OperationResult<uint>> UpdateAsync(
        uint id,
        UpdateAdminRequest request,
        CancellationToken cancellationToken = default)
    {
        var admin = await _db.Admins
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        if (admin is null)
            return OperationResult<uint>.NotFound("找不到管理者");

        admin.Name = request.Name;
        admin.Email = request.Email;
        admin.Tel = request.Tel;
        admin.AllowPage = JoinAllowPage(request.AllowPage);
        admin.IsActive = (sbyte)(request.IsActive ? 1 : 0);

        // 只有 client 有送密碼才更新(對應原 PHP $exclude_keyword 的預期行為)
        if (!string.IsNullOrEmpty(request.Pwd))
        {
            admin.Pwd = request.Pwd; // 注意:相容舊系統,明文存
        }

        await _db.SaveChangesAsync(cancellationToken);
        return OperationResult<uint>.Ok(id, "更新成功");
    }

    public async Task<OperationResult<uint>> UpdateProfileAsync(
        uint id,
        UpdateAdminProfileRequest request,
        CancellationToken cancellationToken = default)
    {
        // 對應舊版 admin_password.php:本人改自己的 name/email/tel/pwd
        var admin = await _db.Admins
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        if (admin is null)
            return OperationResult<uint>.NotFound("找不到管理者");

        admin.Name = request.Name;
        admin.Email = request.Email;
        admin.Tel = request.Tel;

        if (!string.IsNullOrEmpty(request.Pwd))
        {
            admin.Pwd = request.Pwd;
        }

        await _db.SaveChangesAsync(cancellationToken);
        return OperationResult<uint>.Ok(id, "更新成功");
    }

    /// <summary>
    /// 排序白名單:未列出的欄位會 fallback 到 Id。
    /// 不信任 client 直接組 SQL ORDER BY 片段,避免 injection。
    /// </summary>
    private static IOrderedQueryable<Admin> ApplyOrdering(
        IQueryable<Admin> query,
        string? sort,
        string? by)
    {
        var isAsc = string.Equals(by, "ASC", StringComparison.OrdinalIgnoreCase);
        var key = sort?.ToLowerInvariant();

        return key switch
        {
            "account" => isAsc ? query.OrderBy(a => a.Account) : query.OrderByDescending(a => a.Account),
            "name" => isAsc ? query.OrderBy(a => a.Name) : query.OrderByDescending(a => a.Name),
            "email" => isAsc ? query.OrderBy(a => a.Email) : query.OrderByDescending(a => a.Email),
            "createtime" => isAsc ? query.OrderBy(a => a.CreateTime) : query.OrderByDescending(a => a.CreateTime),
            "isactive" => isAsc ? query.OrderBy(a => a.IsActive) : query.OrderByDescending(a => a.IsActive),
            _ => isAsc ? query.OrderBy(a => a.Id) : query.OrderByDescending(a => a.Id),
        };
    }

    /// <summary>
    /// allow_page 在 DB 是逗號分隔字串;轉成陣列供前端使用。
    /// </summary>
    private static IReadOnlyList<string> ParseAllowPage(string? raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
            return Array.Empty<string>();

        return raw.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    }

    /// <summary>
    /// 前端傳入字串陣列,儲存時合併為逗號分隔字串。
    /// 空陣列 / null 都視為空字串,與 PHP 表單「沒勾選 = 空值」行為一致。
    /// </summary>
    private static string JoinAllowPage(IReadOnlyList<string>? pages)
    {
        if (pages is null || pages.Count == 0)
            return string.Empty;

        return string.Join(',', pages.Where(p => !string.IsNullOrWhiteSpace(p)));
    }
}

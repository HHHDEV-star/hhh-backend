using System.Security.Cryptography;
using System.Text;
using hhh.api.contracts.admin.Platform.AclUsers;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.HHHBackstage;
using hhh.infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Platform.AclUsers;

public class AclUserService : IAclUserService
{
    private readonly HHHBackstageContext _db;

    public AclUserService(HHHBackstageContext db) => _db = db;

    public async Task<PagedResponse<AclUserListItem>> GetListAsync(
        ListQuery query, CancellationToken ct = default)
    {
        return await _db.AclUsers
            .AsNoTracking()
            .OrderByDescending(u => u.Id)
            .Select(u => new AclUserListItem
            {
                Id = u.Id,
                Name = u.Name,
                Account = u.Account,
                Email = u.Email,
                Position = u.Position,
                IsDel = u.IsDel,
                IsRemote = u.IsRemote,
                IsExecuteSales = u.IsExecuteSales,
                CreateDate = u.CreateDate,
                UpdateDate = u.UpdateDate,
            })
            .ToPagedResponseAsync(query.Page, query.PageSize, ct);
    }

    public async Task<OperationResult<int>> CreateAsync(
        CreateAclUserRequest request, CancellationToken ct = default)
    {
        // 檢查帳號唯一
        var exists = await _db.AclUsers
            .AnyAsync(u => u.Account == request.Account, ct);
        if (exists)
            return OperationResult<int>.Conflict("account 已存在");

        var entity = new AclUser
        {
            Name = request.Name,
            Account = request.Account,
            Email = request.Email ?? string.Empty,
            // 注意：舊版使用 MD5 雜湊密碼，此處保持相容
            Pwd = Md5Hash(request.Pwd),
            IsDel = request.IsDel,
            IsRemote = request.IsRemote,
            IsExecuteSales = request.IsExecuteSales,
            Position = request.Position ?? "無",
            CreateDate = DateTime.Now,
        };

        _db.AclUsers.Add(entity);
        await _db.SaveChangesAsync(ct);

        return OperationResult<int>.Created(entity.Id, "新增成功");
    }

    public async Task<OperationResult> UpdateAsync(
        int id, UpdateAclUserRequest request, CancellationToken ct = default)
    {
        var entity = await _db.AclUsers
            .FirstOrDefaultAsync(u => u.Id == id, ct);
        if (entity is null)
            return OperationResult.NotFound("找不到帳號");

        entity.Name = request.Name;
        entity.Email = request.Email ?? entity.Email;
        entity.IsDel = request.IsDel;
        entity.IsRemote = request.IsRemote;
        entity.IsExecuteSales = request.IsExecuteSales;
        entity.Position = request.Position ?? entity.Position;
        entity.UpdateDate = DateTime.Now;

        // 密碼有值才更新
        if (!string.IsNullOrEmpty(request.Pwd))
            entity.Pwd = Md5Hash(request.Pwd);

        await _db.SaveChangesAsync(ct);

        return OperationResult.Ok("編輯成功");
    }

    /// <summary>MD5 雜湊（與舊版 PHP md5() 相容）</summary>
    private static string Md5Hash(string input)
    {
        var bytes = MD5.HashData(Encoding.UTF8.GetBytes(input));
        return Convert.ToHexString(bytes).ToLowerInvariant();
    }
}

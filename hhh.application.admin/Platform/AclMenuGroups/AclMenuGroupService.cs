using hhh.api.contracts.admin.Platform.AclMenuGroups;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.HHHBackstage;
using hhh.infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Platform.AclMenuGroups;

public class AclMenuGroupService : IAclMenuGroupService
{
    private readonly HHHBackstageContext _db;

    public AclMenuGroupService(HHHBackstageContext db) => _db = db;

    public async Task<PagedResponse<AclMenuGroupListItem>> GetListAsync(
        ListQuery query, CancellationToken ct = default)
    {
        return await _db.AclMenuGroups
            .AsNoTracking()
            .OrderBy(g => g.SortNum)
            .Select(g => new AclMenuGroupListItem
            {
                Id = g.Id,
                Icon = g.Icon,
                Name = g.Name,
                SortNum = g.SortNum,
                IsShow = g.IsShow,
                CreateDate = g.CreateDate,
                UpdateDate = g.UpdateDate,
            })
            .ToPagedResponseAsync(query.Page, query.PageSize, ct);
    }

    public async Task<OperationResult<int>> CreateAsync(
        CreateAclMenuGroupRequest request, CancellationToken ct = default)
    {
        var entity = new AclMenuGroup
        {
            Icon = request.Icon,
            Name = request.Name,
            SortNum = request.SortNum,
            IsShow = request.IsShow,
            CreateDate = DateTime.Now,
        };

        _db.AclMenuGroups.Add(entity);
        await _db.SaveChangesAsync(ct);

        return OperationResult<int>.Created(entity.Id, "新增成功");
    }

    public async Task<OperationResult> UpdateAsync(
        int id, UpdateAclMenuGroupRequest request, CancellationToken ct = default)
    {
        var entity = await _db.AclMenuGroups
            .FirstOrDefaultAsync(g => g.Id == id, ct);
        if (entity is null)
            return OperationResult.NotFound("找不到目錄群組");

        entity.Icon = request.Icon;
        entity.Name = request.Name;
        entity.SortNum = request.SortNum;
        entity.IsShow = request.IsShow;
        entity.UpdateDate = DateTime.Now;

        await _db.SaveChangesAsync(ct);

        return OperationResult.Ok("編輯成功");
    }
}

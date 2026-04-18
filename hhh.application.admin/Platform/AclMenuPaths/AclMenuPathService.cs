using hhh.api.contracts.admin.Platform.AclMenuPaths;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.HHHBackstage;
using hhh.infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Platform.AclMenuPaths;

public class AclMenuPathService : IAclMenuPathService
{
    private readonly HHHBackstageContext _db;

    public AclMenuPathService(HHHBackstageContext db) => _db = db;

    public async Task<PagedResponse<AclMenuPathListItem>> GetListAsync(
        AclMenuPathListQuery query, CancellationToken ct = default)
    {
        var q = _db.AclMenuPaths.AsNoTracking().AsQueryable();

        // 關鍵字篩選（Name / Path）
        if (!string.IsNullOrWhiteSpace(query.Keyword))
        {
            var kw = $"%{query.Keyword}%";
            q = q.Where(p =>
                (p.Name != null && EF.Functions.Like(p.Name, kw)) ||
                (p.Path != null && EF.Functions.Like(p.Path, kw)));
        }

        // 群組篩選
        if (query.MenuGroupId.HasValue)
            q = q.Where(p => p.MenuGroupId == query.MenuGroupId.Value);

        // 是否顯示篩選
        if (!string.IsNullOrWhiteSpace(query.IsShow))
            q = q.Where(p => p.IsShow == query.IsShow);

        return await q
            .OrderBy(p => p.SortNum)
            .Select(p => new AclMenuPathListItem
            {
                Id = p.Id,
                MenuGroupId = p.MenuGroupId,
                ProjectId = p.ProjectId,
                Name = p.Name,
                Path = p.Path,
                SortNum = p.SortNum,
                IsShow = p.IsShow,
                CreateDate = p.CreateDate,
                UpdateDate = p.UpdateDate,
            })
            .ToPagedResponseAsync(query.Page, query.PageSize, ct);
    }

    public async Task<OperationResult<int>> CreateAsync(
        CreateAclMenuPathRequest request, CancellationToken ct = default)
    {
        // 檢查 path 唯一
        var exists = await _db.AclMenuPaths
            .AnyAsync(p => p.Path == request.Path, ct);
        if (exists)
            return OperationResult<int>.Conflict("path 已存在");

        var entity = new AclMenuPath
        {
            MenuGroupId = request.MenuGroupId,
            ProjectId = request.ProjectId,
            Name = request.Name,
            Path = request.Path,
            SortNum = request.SortNum,
            IsShow = request.IsShow,
            CreateDate = DateTime.Now,
        };

        _db.AclMenuPaths.Add(entity);
        await _db.SaveChangesAsync(ct);

        return OperationResult<int>.Created(entity.Id, "新增成功");
    }

    public async Task<OperationResult> UpdateAsync(
        int id, UpdateAclMenuPathRequest request, CancellationToken ct = default)
    {
        var entity = await _db.AclMenuPaths
            .FirstOrDefaultAsync(p => p.Id == id, ct);
        if (entity is null)
            return OperationResult.NotFound("找不到目錄功能");

        entity.MenuGroupId = request.MenuGroupId;
        entity.ProjectId = request.ProjectId;
        entity.Name = request.Name;
        entity.Path = request.Path;
        entity.SortNum = request.SortNum;
        entity.IsShow = request.IsShow;
        entity.UpdateDate = DateTime.Now;

        await _db.SaveChangesAsync(ct);

        return OperationResult.Ok("編輯成功");
    }

    public async Task<List<SelectOption>> GetProjectOptionsAsync(CancellationToken ct = default)
    {
        return await _db.AclProjects
            .AsNoTracking()
            .Select(p => new SelectOption
            {
                Value = p.Id,
                Text = p.Name,
            })
            .ToListAsync(ct);
    }

    public async Task<List<SelectOption>> GetMenuGroupOptionsAsync(CancellationToken ct = default)
    {
        return await _db.AclMenuGroups
            .AsNoTracking()
            .Select(g => new SelectOption
            {
                Value = g.Id,
                Text = g.Name ?? string.Empty,
            })
            .ToListAsync(ct);
    }
}

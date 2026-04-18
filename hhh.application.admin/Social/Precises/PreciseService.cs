using hhh.api.contracts.admin.Social.Precises;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using hhh.infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Social.Precises;

public class PreciseService : IPreciseService
{
    private static readonly HashSet<string> AllowedIdentities = new() { "designer", "supplier" };

    private readonly XoopsContext _db;

    public PreciseService(XoopsContext db) => _db = db;

    public async Task<PagedResponse<PreciseListItem>> GetListAsync(PreciseListQuery query, CancellationToken cancellationToken = default)
    {
        var q = _db.Precises.AsNoTracking().AsQueryable();

        // 關鍵字搜尋：姓名 / Email / 公司 / 手機
        if (!string.IsNullOrWhiteSpace(query.Keyword))
        {
            var like = $"%{query.Keyword.Trim()}%";
            q = q.Where(p =>
                EF.Functions.Like(p.Name, like) ||
                EF.Functions.Like(p.Email, like) ||
                EF.Functions.Like(p.Company, like) ||
                EF.Functions.Like(p.Mobile, like));
        }

        // 身份別篩選
        if (!string.IsNullOrWhiteSpace(query.Identity))
        {
            q = q.Where(p => p.Identity == query.Identity);
        }

        // 日期區間篩選（建立時間）
        if (query.DateFrom is { } dateFrom)
        {
            var from = dateFrom.ToDateTime(TimeOnly.MinValue);
            q = q.Where(p => p.CreateTime >= from);
        }
        if (query.DateTo is { } dateTo)
        {
            var to = dateTo.ToDateTime(TimeOnly.MaxValue);
            q = q.Where(p => p.CreateTime <= to);
        }

        return await q
            .OrderByDescending(p => p.Id)
            .Select(p => new PreciseListItem
            {
                Id = p.Id,
                Identity = p.Identity,
                Email = p.Email,
                Name = p.Name,
                Company = p.Company,
                Mobile = p.Mobile,
                CreateTime = p.CreateTime,
            })
            .ToPagedResponseAsync(query.Page, query.PageSize, cancellationToken);
    }

    public async Task<OperationResult<int>> CreateAsync(
        CreatePreciseRequest request, CancellationToken cancellationToken = default)
    {
        if (!AllowedIdentities.Contains(request.Identity))
            return OperationResult<int>.BadRequest("身分別資料不符(僅接受 designer 或 supplier)");

        var entity = new Precise
        {
            Identity = request.Identity,
            Email = request.Email,
            Name = request.Name,
            Company = request.Company,
            Mobile = request.Mobile,
            CreateTime = DateTime.UtcNow,
        };

        _db.Precises.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        return OperationResult<int>.Created(entity.Id);
    }
}

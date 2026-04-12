using hhh.api.contracts.admin.Social.Precises;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Social.Precises;

public class PreciseService : IPreciseService
{
    private static readonly HashSet<string> AllowedIdentities = new() { "designer", "supplier" };

    private readonly XoopsContext _db;

    public PreciseService(XoopsContext db) => _db = db;

    public async Task<List<PreciseListItem>> GetListAsync(CancellationToken cancellationToken = default)
    {
        return await _db.Precises
            .AsNoTracking()
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
            .ToListAsync(cancellationToken);
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

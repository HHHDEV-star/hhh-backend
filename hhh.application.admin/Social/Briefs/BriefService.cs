using hhh.api.contracts.admin.Social.Briefs;
using hhh.api.contracts.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Social.Briefs;

public class BriefService : IBriefService
{
    private readonly XoopsContext _db;

    public BriefService(XoopsContext db) => _db = db;

    public async Task<PagedResponse<BriefListItem>> GetListAsync(ListQuery query, CancellationToken cancellationToken = default)
    {
        return await _db.Briefs
            .AsNoTracking()
            .OrderByDescending(b => b.BriefId)
            .Select(b => new BriefListItem
            {
                BriefId = b.BriefId,
                Name = b.Name,
                Email = b.Email,
                Phone = b.Phone,
                Area = b.Area,
                Image = b.Image,
                Pin = b.Pin,
                Fee = b.Fee,
                CreateTime = b.CreateTime,
            })
            .ToPagedResponseAsync(query.Page, query.PageSize, cancellationToken);
    }
}

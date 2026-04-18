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

    public async Task<PagedResponse<BriefListItem>> GetListAsync(BriefListQuery query, CancellationToken cancellationToken = default)
    {
        var q = _db.Briefs.AsNoTracking().AsQueryable();

        // 關鍵字搜尋：姓名 / Email / 電話 / 地區
        if (!string.IsNullOrWhiteSpace(query.Keyword))
        {
            var like = $"%{query.Keyword.Trim()}%";
            q = q.Where(b =>
                EF.Functions.Like(b.Name, like) ||
                EF.Functions.Like(b.Email, like) ||
                EF.Functions.Like(b.Phone, like) ||
                EF.Functions.Like(b.Area, like));
        }

        // 日期區間篩選（建立時間）
        if (query.DateFrom is { } dateFrom)
        {
            var from = dateFrom.ToDateTime(TimeOnly.MinValue);
            q = q.Where(b => b.CreateTime >= from);
        }
        if (query.DateTo is { } dateTo)
        {
            var to = dateTo.ToDateTime(TimeOnly.MaxValue);
            q = q.Where(b => b.CreateTime <= to);
        }

        return await q
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

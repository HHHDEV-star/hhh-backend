using System.Text.RegularExpressions;
using hhh.api.contracts.admin.Social.HhhHps;
using hhh.api.contracts.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Social.HhhHps;

public partial class HhhHpService : IHhhHpService
{
    private readonly XoopsContext _db;

    public HhhHpService(XoopsContext db) => _db = db;

    public async Task<PagedResponse<HhhHpListItem>> GetListAsync(
        HhhHpListQuery query, CancellationToken ct = default)
    {
        // 對應舊�� PHP:Hp_model::requestget()
        var q = _db.HhhHps.AsNoTracking().AsQueryable();

        // 日期篩選
        if (query.StartDate is { } sd)
        {
            var startDateTime = sd.ToDateTime(TimeOnly.MinValue);
            q = q.Where(h => h.CreateTime >= startDateTime);
        }

        if (query.EndDate is { } ed)
        {
            var endDateTime = ed.ToDateTime(new TimeOnly(23, 59, 59));
            q = q.Where(h => h.CreateTime <= endDateTime);
        }

        // 關鍵字篩選
        var keyword = query.Keyword?.Trim();
        if (!string.IsNullOrEmpty(keyword))
        {
            if (keyword.StartsWith("09"))
            {
                // 手機號碼：格式化為 XXXX-XXX-XXX 後搜 phone
                if (keyword.Length == 10)
                    keyword = MobilePhoneRegex().Replace(keyword, "$1-$2-$3");

                q = q.Where(h => h.Phone != null && h.Phone.Contains(keyword));
            }
            else
            {
                // 舊 PHP 用 or_like 搜 name/email/city/region
                // 注意：舊版 or_like 會跳脫日期條件（bug），這裡正確地用 AND 包裹
                var kw = keyword;
                q = q.Where(h =>
                    (h.Name != null && h.Name.Contains(kw)) ||
                    (h.Email != null && h.Email.Contains(kw)) ||
                    (h.City != null && h.City.Contains(kw)) ||
                    (h.Region != null && h.Region.Contains(kw)));
            }
        }

        return await q
            .OrderByDescending(h => h.Id)
            .Select(h => new HhhHpListItem
            {
                Id = h.Id,
                Name = h.Name,
                Phone = h.Phone,
                Email = h.Email,
                City = h.City,
                Region = h.Region,
                HpBuilderId = h.HpBuilderId,
                IsRequest = h.IsRequest == 1,
                IsAgree = h.IsAgree == 1,
                CreateTime = h.CreateTime,
            })
            .ToPagedResponseAsync(query.Page, query.PageSize, ct);
    }

    /// <summary>手機號碼格式化：0912345678 → 0912-345-678</summary>
    [GeneratedRegex(@"(\d{4})(\d{3})(\d{3})")]
    private static partial Regex MobilePhoneRegex();
}

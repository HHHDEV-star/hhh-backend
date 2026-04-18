using System.Text.Json;
using hhh.api.contracts.admin.Brokers.Renovations;
using hhh.api.contracts.Common;
using hhh.infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Brokers.Renovations;

public class RenovationService : IRenovationService
{
    private readonly XoopsContext _db;

    public RenovationService(XoopsContext db)
    {
        _db = db;
    }

    public async Task<PagedResponse<RenovationListItem>> GetListAsync(RenovationListQuery query, CancellationToken cancellationToken = default)
    {
        // 對應舊 PHP:Renovation_model::get()
        // 為了能在 LINQ 投影裡同時拿到「原始 site_lists 字串」與其他欄位,
        // 先 query 到匿名型別,再在 in-memory 階段把 site_lists 解析成 JsonElement。
        var filtered = _db.RenovationReuqests.AsNoTracking().AsQueryable();

        // 關鍵字篩選（Name / Phone / Email）
        if (!string.IsNullOrWhiteSpace(query.Keyword))
        {
            var like = $"%{query.Keyword.Trim()}%";
            filtered = filtered.Where(r =>
                EF.Functions.Like(r.Name, like) ||
                EF.Functions.Like(r.Phone, like) ||
                EF.Functions.Like(r.Email, like));
        }

        // 類型篩選
        if (!string.IsNullOrWhiteSpace(query.Type))
            filtered = filtered.Where(r => r.Type == query.Type.Trim());

        // 建立日期範圍篩選
        if (query.DateFrom is { } dateFrom)
        {
            var from = dateFrom.ToDateTime(TimeOnly.MinValue);
            filtered = filtered.Where(r => r.Ctime >= from);
        }

        if (query.DateTo is { } dateTo)
        {
            var to = dateTo.ToDateTime(new TimeOnly(23, 59, 59));
            filtered = filtered.Where(r => r.Ctime <= to);
        }

        var baseQuery = from r in filtered
            orderby r.Id descending
            select new
            {
                r.Id,
                r.Ctime,
                r.Name,
                r.Phone,
                r.Email,
                r.IsFb,
                r.Sex,
                r.Area,
                r.Time,
                r.Type,
                r.Mode,
                r.Budget,
                r.Pin,
                r.Pattern,
                r.Style,
                SiteListsRaw = r.SiteLists,
                r.UtmSource,
                CompanyName = _db.DecoRecords
                    .Where(d => d.Bldsno == r.Bldsno && r.Bldsno != 0)
                    .Select(d => d.CompanyName)
                    .FirstOrDefault(),
            };

        var total = await baseQuery.LongCountAsync(cancellationToken);
        var rows = await baseQuery
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);

        var items = rows.Select(r => new RenovationListItem
        {
            Id = r.Id,
            Ctime = r.Ctime,
            Name = r.Name,
            Phone = r.Phone,
            Email = r.Email,
            IsFb = r.IsFb,
            Sex = r.Sex,
            Area = r.Area,
            Time = r.Time,
            Type = r.Type,
            Mode = r.Mode,
            Budget = r.Budget,
            Pin = r.Pin,
            Pattern = r.Pattern,
            Style = r.Style,
            SiteLists = ParseSiteLists(r.SiteListsRaw),
            CompanyName = r.CompanyName,
            UtmSource = r.UtmSource,
        }).ToList();

        return new PagedResponse<RenovationListItem>
        {
            Items = items,
            Total = total,
            Page = query.Page,
            PageSize = query.PageSize,
        };
    }

    /// <summary>
    /// 對應舊 PHP:json_decode($value['site_lists'])
    /// 解析失敗、null 或空字串都回傳 null。
    /// </summary>
    private static JsonElement? ParseSiteLists(string? raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
        {
            return null;
        }

        try
        {
            using var doc = JsonDocument.Parse(raw);
            // Clone() 讓 JsonElement 可以脫離 JsonDocument 生命週期被回傳
            return doc.RootElement.Clone();
        }
        catch (JsonException)
        {
            // 舊 DB 內可能有非 JSON 字串(legacy 髒資料),此時直接回傳 null,
            // 行為對應 PHP json_decode 失敗時回傳 NULL。
            return null;
        }
    }
}

using System.Text.Json;
using hhh.api.contracts.admin.Brokers.Renovations;
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

    public async Task<List<RenovationListItem>> GetListAsync(CancellationToken cancellationToken = default)
    {
        // 對應舊 PHP:Renovation_model::get()
        //   SELECT rr.*,
        //          (SELECT company_name FROM deco_record
        //           WHERE deco_record.bldsno = rr.bldsno AND rr.bldsno != 0) AS company_name
        //   FROM renovation_reuqest rr
        //   ORDER BY rr.id DESC
        //
        // 注意:當 rr.bldsno = 0 時整個子查詢取不到列,company_name 會是 NULL
        // (原 PHP SQL 的 "AND rr.bldsno != 0" 寫在子查詢內也是同樣效果)。
        //
        // 為了能在 LINQ 投影裡同時拿到「原始 site_lists 字串」與其他欄位,
        // 先 query 到匿名型別,再在 in-memory 階段把 site_lists 解析成 JsonElement。
        var rows = await (
            from r in _db.RenovationReuqests.AsNoTracking()
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
            })
            .ToListAsync(cancellationToken);

        return rows.Select(r => new RenovationListItem
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

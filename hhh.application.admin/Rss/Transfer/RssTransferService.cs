using hhh.api.contracts.admin.Rss;
using hhh.infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Rss.Transfer;

public class RssTransferService : IRssTransferService
{
    private const string Domain = "https://hhh.com.tw/";

    // 對應舊 PHP transfer_model 的 type / url mapping
    private static readonly Dictionary<string, string> TypeLabels = new()
    {
        ["brand"] = "廠商",
        ["case"] = "個案",
        ["column"] = "專欄",
        ["designer"] = "設計師",
    };

    private static readonly Dictionary<string, string> UrlPrefixes = new()
    {
        ["brand"] = Domain + "brand-index.php?brand_id=",
        ["case"] = Domain + "case-post.php?id=",
        ["column"] = Domain + "column-post.php?id=",
        ["designer"] = Domain + "designer-index.php?designer_id=",
    };

    private readonly XoopsContext _db;

    public RssTransferService(XoopsContext db) => _db = db;

    public async Task<List<RssTransferLogItem>> GetLogsAsync(
        DateTime? startDate, DateTime? endDate,
        CancellationToken cancellationToken = default)
    {
        // 對應舊 PHP:禁止無條件全撈,至少要帶日期
        if (startDate is null && endDate is null)
            return new List<RssTransferLogItem>();

        var q = _db.RssTransfers.AsNoTracking().AsQueryable();

        if (startDate is { } sd)
            q = q.Where(r => r.Datetime >= sd);

        if (endDate is { } ed)
            q = q.Where(r => r.Datetime <= ed);

        var rows = await q
            .OrderByDescending(r => r.Id)
            .ToListAsync(cancellationToken);

        // 在 in-memory 做 type → 中文 + url 計算(對應舊 PHP _set_rows_data)
        return rows.Select(r =>
        {
            var rawType = r.Type;
            return new RssTransferLogItem
            {
                Id = r.Id,
                Source = r.Source,
                Type = TypeLabels.GetValueOrDefault(rawType, rawType),
                Num = r.Num,
                Url = UrlPrefixes.TryGetValue(rawType, out var prefix)
                    ? prefix + r.Num
                    : string.Empty,
                Ip = r.Ip,
                Datetime = r.Datetime,
            };
        }).ToList();
    }

    public async Task<List<RssTransferStatItem>> GetStatisticsAsync(
        DateTime? startDate, DateTime? endDate,
        CancellationToken cancellationToken = default)
    {
        // 對應舊 PHP transfer_model::statistics():
        //   SELECT type, num, COUNT(*) as total FROM rss_transfer
        //   [WHERE datetime >= ? AND datetime <= ?]
        //   GROUP BY type, num ORDER BY type ASC, num ASC
        var q = _db.RssTransfers.AsNoTracking().AsQueryable();

        if (startDate is { } sd)
            q = q.Where(r => r.Datetime >= sd);

        if (endDate is { } ed)
            q = q.Where(r => r.Datetime <= ed);

        var grouped = await q
            .GroupBy(r => new { r.Type, r.Num })
            .Select(g => new { g.Key.Type, g.Key.Num, Total = g.Count() })
            .OrderBy(g => g.Type)
            .ThenBy(g => g.Num)
            .ToListAsync(cancellationToken);

        // in-memory: type → 中文 + url 計算
        return grouped.Select(g => new RssTransferStatItem
        {
            Type = TypeLabels.GetValueOrDefault(g.Type, g.Type),
            Num = g.Num,
            Total = g.Total,
            Url = UrlPrefixes.TryGetValue(g.Type, out var prefix)
                ? prefix + g.Num
                : string.Empty,
        }).ToList();
    }
}

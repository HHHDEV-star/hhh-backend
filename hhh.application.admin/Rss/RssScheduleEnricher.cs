using hhh.api.contracts.admin.Rss;
using hhh.infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Rss;

/// <summary>
/// RSS 排程共用 enrichment：將逗號分隔的 ID 字串展開為帶標題/封面的列表
/// </summary>
internal static class RssScheduleEnricher
{
    /// <summary>
    /// 將 RssScheduleItem 的 Hcolumn / Hcase CSV 展開為 Columns / Cases 列表
    /// </summary>
    public static async Task EnrichAsync(
        IReadOnlyList<RssScheduleItem> items, XoopsContext db, CancellationToken ct)
    {
        if (items.Count == 0) return;

        // 收集所有需要查詢的 ID
        var allColumnIds = new HashSet<uint>();
        var allCaseIds = new HashSet<uint>();

        foreach (var item in items)
        {
            foreach (var id in ParseIds(item.Hcolumn)) allColumnIds.Add(id);
            foreach (var id in ParseIds(item.Hcase)) allCaseIds.Add(id);
        }

        // 批次查詢專欄
        var columnMap = allColumnIds.Count > 0
            ? await db.Hcolumns.AsNoTracking()
                .Where(c => allColumnIds.Contains(c.HcolumnId))
                .Select(c => new RssScheduleRefItem
                {
                    Id = c.HcolumnId,
                    Title = c.Ctitle,
                    Cover = c.Clogo,
                    Onoff = c.Onoff == 1,
                })
                .ToDictionaryAsync(c => c.Id, ct)
            : new Dictionary<uint, RssScheduleRefItem>();

        // 批次查詢個案
        var caseMap = allCaseIds.Count > 0
            ? await db.Hcases.AsNoTracking()
                .Where(c => allCaseIds.Contains(c.HcaseId))
                .Select(c => new RssScheduleRefItem
                {
                    Id = c.HcaseId,
                    Title = c.Caption,
                    Cover = c.Cover,
                    Onoff = c.Onoff == 1,
                })
                .ToDictionaryAsync(c => c.Id, ct)
            : new Dictionary<uint, RssScheduleRefItem>();

        // 組裝到各 item
        foreach (var item in items)
        {
            item.Columns = ParseIds(item.Hcolumn)
                .Select(id => columnMap.TryGetValue(id, out var r) ? r : new RssScheduleRefItem { Id = id, Title = $"(找不到 #{id})" })
                .ToList();

            item.Cases = ParseIds(item.Hcase)
                .Select(id => caseMap.TryGetValue(id, out var r) ? r : new RssScheduleRefItem { Id = id, Title = $"(找不到 #{id})" })
                .ToList();
        }
    }

    private static List<uint> ParseIds(string? csv)
    {
        if (string.IsNullOrWhiteSpace(csv)) return [];

        var result = new List<uint>();
        foreach (var part in csv.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            if (uint.TryParse(part, out var id))
                result.Add(id);
        }
        return result;
    }
}

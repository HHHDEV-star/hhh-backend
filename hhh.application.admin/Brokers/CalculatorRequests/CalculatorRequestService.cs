using System.Text.RegularExpressions;
using hhh.api.contracts.admin.Brokers.CalculatorRequests;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Brokers.CalculatorRequests;

public class CalculatorRequestService : ICalculatorRequestService
{
    private readonly XoopsContext _db;

    public CalculatorRequestService(XoopsContext db)
    {
        _db = db;
    }

    public async Task<CalculatorRequestListResponse> GetListAsync(
        CalculatorRequestListQuery query,
        CancellationToken cancellationToken = default)
    {
        // 對應舊 PHP:Calculator_model::requestget
        //   SELECT *,(SELECT COUNT(id) FROM calculator_request) all_count
        //   FROM calculator_request
        //   WHERE create_time >= ? AND create_time <= ?
        //     AND <關鍵字>
        //   ORDER BY id DESC
        //
        // 注意:舊 PHP 在關鍵字分支用 or_like(),搭配前面 where 的日期條件會產生
        //      "date AND date OR keyword OR keyword..." 的 SQL,語意上等於日期被
        //      OR 掉,實質是個 bug。但前端 JS 搜尋關鍵字時會把日期清空,所以使用
        //      者沒踩到。這裡改成正確語意:日期 AND (關鍵字跨欄位 OR)。
        var q = _db.CalculatorRequests.AsNoTracking();

        if (query.StartDate is { } start)
        {
            var startOfDay = start.Date;
            q = q.Where(r => r.CreateTime >= startOfDay);
        }

        if (query.EndDate is { } end)
        {
            var endOfDay = end.Date.AddDays(1).AddTicks(-1);
            q = q.Where(r => r.CreateTime <= endOfDay);
        }

        if (!string.IsNullOrWhiteSpace(query.Keyword))
        {
            q = ApplyKeyword(q, query.Keyword.Trim());
        }

        var items = await q
            .OrderByDescending(r => r.Id)
            .Select(r => new CalculatorRequestListItem
            {
                Id = r.Id,
                CreateTime = r.CreateTime,
                Name = r.Name,
                Phone = r.Phone,
                Email = r.Email,
                City = r.City,
                HClass = r.HClass,
                Area = r.Area,
                CaType = r.CaType,
                SourceWeb = r.SourceWeb,
                MarketingConsent = r.MarketingConsent,
                UtmSource = r.UtmSource,
                UtmMedium = r.UtmMedium,
                UtmCampaign = r.UtmCampaign,
            })
            .ToListAsync(cancellationToken);

        var allCount = await _db.CalculatorRequests.CountAsync(cancellationToken);

        return new CalculatorRequestListResponse
        {
            Items = items,
            SearchCount = items.Count,
            AllCount = allCount,
        };
    }

    /// <summary>
    /// 套用關鍵字搜尋。
    /// 對應舊 PHP:Calculator_model::requestget 內的關鍵字分支。
    /// </summary>
    private static IQueryable<CalculatorRequest> ApplyKeyword(
        IQueryable<CalculatorRequest> query,
        string keyword)
    {
        // 1. 以 09 開頭視為手機號碼
        if (keyword.StartsWith("09", StringComparison.Ordinal))
        {
            // 長度 10 → 轉成 0912-345-678 格式 (對應舊 PHP preg_replace)
            if (keyword.Length == 10 && Regex.IsMatch(keyword, @"^\d{10}$"))
            {
                keyword = $"{keyword[..4]}-{keyword.Substring(4, 3)}-{keyword.Substring(7, 3)}";
            }

            var phoneLike = $"%{keyword}%";
            return query.Where(r => r.Phone != null && EF.Functions.Like(r.Phone, phoneLike));
        }

        // 2. 一般跨欄位 LIKE
        // 舊 PHP 還會針對「無 / 同意 / 不同意」對 marketing_consent 做 LIKE,
        // 這邊把它一起放進 OR 條件裡。
        var like = $"%{keyword}%";
        var marketingTarget = keyword switch
        {
            "無" => "2",
            "同意" => "1",
            "不同意" => "0",
            _ => null,
        };

        return query.Where(r =>
            (r.Name != null && EF.Functions.Like(r.Name, like)) ||
            (r.Email != null && EF.Functions.Like(r.Email, like)) ||
            (r.City != null && EF.Functions.Like(r.City, like)) ||
            (r.SourceWeb != null && EF.Functions.Like(r.SourceWeb, like)) ||
            (r.CaType != null && EF.Functions.Like(r.CaType, like)) ||
            (r.HClass != null && EF.Functions.Like(r.HClass, like)) ||
            (r.UtmSource != null && EF.Functions.Like(r.UtmSource, like)) ||
            (r.UtmMedium != null && EF.Functions.Like(r.UtmMedium, like)) ||
            (r.UtmCampaign != null && EF.Functions.Like(r.UtmCampaign, like)) ||
            (r.Area != null && EF.Functions.Like(r.Area, like)) ||
            (marketingTarget != null && r.MarketingConsent == marketingTarget));
    }
}

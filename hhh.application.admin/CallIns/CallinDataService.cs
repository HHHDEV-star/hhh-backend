using hhh.api.contracts.admin.CallIns;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using hhh.infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace hhh.application.admin.CallIns;

public class CallinDataService : ICallinDataService
{
    private readonly XoopsContext _db;
    private readonly HashSet<string> _blacklist;

    public CallinDataService(XoopsContext db, IConfiguration configuration)
    {
        _db = db;

        // 從 appsettings.json 讀取黑名單電話號碼（CallinBlacklist 陣列）
        _blacklist = configuration
            .GetSection("CallinBlacklist")
            .Get<string[]>()
            ?.ToHashSet(StringComparer.OrdinalIgnoreCase)
            ?? [];
    }

    /// <inheritdoc />
    public async Task<PagedResponse<CallinDataListItem>> GetListAsync(
        CallinDataListQuery query,
        CancellationToken cancellationToken = default)
    {
        var q = _db.CallinData.AsNoTracking();

        // 起訖日（依 activity_time 篩選，含當日）
        if (query.StartDate.HasValue)
            q = q.Where(c => c.ActivityTime >= query.StartDate.Value);

        if (query.EndDate.HasValue)
            q = q.Where(c => c.ActivityTime <= query.EndDate.Value);

        // 話單類型
        if (!string.IsNullOrWhiteSpace(query.CallinType))
            q = q.Where(c => c.CallinType == query.CallinType);

        // 來電號碼（模糊比對）
        if (!string.IsNullOrWhiteSpace(query.Phone))
        {
            var phone = query.Phone.Trim();
            q = q.Where(c => c.Phone.Contains(phone));
        }

        // 分機（精確比對 users_sn）
        if (!string.IsNullOrWhiteSpace(query.Extension))
        {
            var ext = query.Extension.Trim();
            q = q.Where(c => c.UsersSn == ext);
        }

        // 黑名單篩選（黑名單來源為 appsettings.json，需在 DB 端用 IN / NOT IN）
        if (query.Blacklist.HasValue && _blacklist.Count > 0)
        {
            if (query.Blacklist.Value)
                q = q.Where(c => _blacklist.Contains(c.Phone));
            else
                q = q.Where(c => !_blacklist.Contains(c.Phone));
        }
        // Blacklist == true 但黑名單為空：直接回傳空集合
        else if (query.Blacklist == true && _blacklist.Count == 0)
        {
            q = q.Where(_ => false);
        }

        var paged = await q
            .OrderByDescending(c => c.ActivityTime)
            .ThenByDescending(c => c.DesignerTitle)
            .ThenBy(c => c.CallinTime)
            .Select(c => new CallinDataListItem
            {
                Seq = c.Seq,
                UsersSn = c.UsersSn,
                DesignerTitle = c.DesignerTitle,
                ActivityTime = c.ActivityTime,
                CallinTime = c.CallinTime,
                CallinPeriod = c.CallinPeriod,
                CallinType = c.CallinType,
                Phone = c.Phone,
                CreateTime = c.CreateTime,
                SendMail = c.SendMail,
                SendTime = c.SendTime,
            })
            .ToPagedResponseAsync(query.Page, query.PageSize, cancellationToken);

        // 標註黑名單（僅對當頁資料）
        foreach (var item in paged.Items)
        {
            if (_blacklist.Contains(item.Phone))
                item.Blacklist = "黑名單";
        }

        return paged;
    }

    /// <inheritdoc />
    public async Task<OperationResult<BatchCreateCallinResult>> BatchCreateAsync(
        List<CallinDataItemRequest> items,
        CancellationToken cancellationToken = default)
    {
        if (items is not { Count: > 0 })
            return OperationResult<BatchCreateCallinResult>.BadRequest("請提供至少一筆來電資料");

        var insertedCount = 0;

        foreach (var item in items)
        {
            // 重複檢查（對齊 PHP: where activity_time + designer_title + callin_time + phone）
            var exists = await _db.CallinData.AnyAsync(c =>
                c.ActivityTime == item.ActivityTime &&
                c.DesignerTitle == item.DesignerTitle &&
                c.CallinTime == item.CallinTime &&
                c.Phone == item.Phone, cancellationToken);

            if (!exists)
            {
                _db.CallinData.Add(new CallinDatum
                {
                    UsersSn = item.UsersSn,
                    DesignerTitle = item.DesignerTitle,
                    ActivityTime = item.ActivityTime,
                    CallinTime = item.CallinTime,
                    CallinPeriod = item.CallinPeriod,
                    CallinType = item.CallinType,
                    Phone = item.Phone,
                    CreateTime = DateTime.Now,
                    SendMail = "N",
                });
                insertedCount++;
            }
        }

        await _db.SaveChangesAsync(cancellationToken);

        return OperationResult<BatchCreateCallinResult>.Created(
            new BatchCreateCallinResult
            {
                InsertedCount = insertedCount,
                TotalReceived = items.Count,
            },
            "建立成功");
    }
}

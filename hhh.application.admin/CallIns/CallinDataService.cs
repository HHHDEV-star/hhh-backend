using hhh.api.contracts.admin.CallIns;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
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
    public async Task<List<CallinDataListItem>> GetListAsync(
        CancellationToken cancellationToken = default)
    {
        var data = await _db.CallinData
            .AsNoTracking()
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
            .ToListAsync(cancellationToken);

        // 標註黑名單
        foreach (var item in data)
        {
            if (_blacklist.Contains(item.Phone))
                item.Blacklist = "黑名單";
        }

        return data;
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

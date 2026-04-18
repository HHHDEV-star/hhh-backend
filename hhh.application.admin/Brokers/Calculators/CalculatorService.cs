using hhh.api.contracts.admin.Brokers.Calculators;
using hhh.api.contracts.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Brokers.Calculators;

public class CalculatorService : ICalculatorService
{
    private readonly XoopsContext _db;

    public CalculatorService(XoopsContext db)
    {
        _db = db;
    }

    public async Task<PagedResponse<CalculatorListItem>> GetListAsync(CalculatorListQuery query, CancellationToken cancellationToken = default)
    {
        // 對應舊 PHP:Calculator_model::get()
        //   SELECT * FROM calculator WHERE contact_status = 'Y' ORDER BY id DESC
        var q = _db.Calculators
            .AsNoTracking()
            .Where(c => c.ContactStatus == "Y");

        // 關鍵字篩選（Name / Phone / Email）
        if (!string.IsNullOrWhiteSpace(query.Keyword))
        {
            var like = $"%{query.Keyword.Trim()}%";
            q = q.Where(c =>
                EF.Functions.Like(c.Name, like) ||
                EF.Functions.Like(c.Phone, like) ||
                EF.Functions.Like(c.Email, like));
        }

        // 建立日期範圍篩選
        if (query.DateFrom is { } dateFrom)
        {
            var from = dateFrom.ToDateTime(TimeOnly.MinValue);
            q = q.Where(c => c.CreateTime >= from);
        }

        if (query.DateTo is { } dateTo)
        {
            var to = dateTo.ToDateTime(new TimeOnly(23, 59, 59));
            q = q.Where(c => c.CreateTime <= to);
        }

        return await q
            .OrderByDescending(c => c.Id)
            .Select(c => new CalculatorListItem
            {
                Id = c.Id,
                CreateTime = c.CreateTime,
                Name = c.Name,
                Phone = c.Phone,
                Email = c.Email,
                IsFb = c.IsFb,
                ContactStatus = c.ContactStatus,
                Message = c.Message,
                HouseType = c.HouseType,
                Location = c.Location,
                HouseYear = c.HouseYear,
                Floor = c.Floor,
                Compartment = c.Compartment,
                Ceiling = c.Ceiling,
                ChangeArea = c.ChangeArea,
                Room = c.Room,
                Liveroom = c.Liveroom,
                Bathroom = c.Bathroom,
                Pin = c.Pin,
                Total = c.Total,
                LoanStatus = c.LoanStatus,
                StorageStatus = c.StorageStatus,
                WarehousingStatus = c.WarehousingStatus,
                RentHouseStatus = c.RentHouseStatus,
                Ip = c.Ip,
            })
            .ToPagedResponseAsync(query.Page, query.PageSize, cancellationToken);
    }
}

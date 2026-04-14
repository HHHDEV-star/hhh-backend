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

    public async Task<PagedResponse<CalculatorListItem>> GetListAsync(ListQuery query, CancellationToken cancellationToken = default)
    {
        // 對應舊 PHP:Calculator_model::get()
        //   SELECT * FROM calculator WHERE contact_status = 'Y' ORDER BY id DESC
        // 沿用舊功能採「全量回傳 + 前端 Kendo Grid 自行分頁」,
        // 不做 server-side paging、也沒有任何查詢條件。
        return await _db.Calculators
            .AsNoTracking()
            .Where(c => c.ContactStatus == "Y")
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

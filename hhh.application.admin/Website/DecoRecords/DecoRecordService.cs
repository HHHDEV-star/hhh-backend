using hhh.api.contracts.admin.WebSite.DecoRecords;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.WebSite.DecoRecords;

public class DecoRecordService : IDecoRecordService
{
    private readonly XoopsContext _db;

    public DecoRecordService(XoopsContext db) => _db = db;

    public async Task<PagedResponse<DecoRecordListItem>> GetListAsync(DecoRecordListQuery query, CancellationToken ct = default)
    {
        var q = _db.DecoRecords.AsNoTracking().AsQueryable();

        // 上線狀態篩選（不帶 = 全部）
        if (query.Onoff is { } onoff)
        {
            q = q.Where(d => d.Onoff == onoff);
        }

        // 關鍵字搜尋：模糊比對 Response 所有欄位
        if (!string.IsNullOrWhiteSpace(query.Keyword))
        {
            var like = $"%{query.Keyword.Trim()}%";
            q = q.Where(d =>
                EF.Functions.Like(d.RegisterNumber ?? "", like) ||
                EF.Functions.Like(d.CompanyName, like) ||
                EF.Functions.Like(d.Address ?? "", like) ||
                EF.Functions.Like(d.District ?? "", like) ||
                EF.Functions.Like(d.Street ?? "", like) ||
                EF.Functions.Like(d.Phone ?? "", like) ||
                EF.Functions.Like(d.Cellphone ?? "", like) ||
                EF.Functions.Like(d.ServicePhone ?? "", like) ||
                EF.Functions.Like(d.Email ?? "", like) ||
                EF.Functions.Like(d.Lineid ?? "", like) ||
                EF.Functions.Like(d.Website ?? "", like) ||
                EF.Functions.Like(d.Url ?? "", like));
        }

        return await q
            .OrderByDescending(d => d.RegisterNumber)
            .Select(d => new DecoRecordListItem
            {
                Bldsno = d.Bldsno,
                Url = d.Url,
                RegisterNumber = d.RegisterNumber,
                CompanyName = d.CompanyName,
                Address = d.Address,
                District = d.District,
                Street = d.Street,
                HdesignerId = d.HdesignerId,
                DataUpdateDate = d.DataUpdateDate,
                Phone = d.Phone,
                Cellphone = d.Cellphone,
                ServicePhone = d.ServicePhone,
                Email = d.Email,
                Lineid = d.Lineid,
                Website = d.Website,
                Onoff = d.Onoff == 1,
            })
            .ToPagedResponseAsync(query.Page, query.PageSize, ct);
    }

    public async Task<OperationResult> UpdateAsync(
        int bldsno, UpdateDecoRecordRequest request, CancellationToken ct = default)
    {
        var entity = await _db.DecoRecords.FirstOrDefaultAsync(d => d.Bldsno == bldsno, ct);
        if (entity is null)
            return OperationResult.NotFound("找不到登記資料");

        if (request.RegisterNumber is not null) entity.RegisterNumber = request.RegisterNumber;
        if (request.ServicePhone is not null) entity.ServicePhone = request.ServicePhone;
        if (request.Phone is not null) entity.Phone = request.Phone;
        if (request.Cellphone is not null) entity.Cellphone = request.Cellphone;
        if (request.Website is not null) entity.Website = request.Website;
        if (request.Lineid is not null) entity.Lineid = request.Lineid;
        if (request.Street is not null) entity.Street = request.Street;
        if (request.District is not null) entity.District = request.District;
        entity.HdesignerId = request.HdesignerId;
        if (request.Email is not null) entity.Email = request.Email;
        entity.Onoff = (sbyte)(request.Onoff ? 1 : 0);
        entity.DataUpdateDate = DateTime.UtcNow;

        await _db.SaveChangesAsync(ct);
        return OperationResult.Ok("修改成功");
    }
}

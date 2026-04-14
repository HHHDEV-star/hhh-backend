using hhh.api.contracts.admin.WebSite.DecoImages;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.WebSite.DecoImages;

public class DecoImageService : IDecoImageService
{
    private readonly XoopsContext _db;

    public DecoImageService(XoopsContext db) => _db = db;

    public async Task<PagedResponse<DecoImageListItem>> GetListAsync(ListQuery query, CancellationToken ct = default)
    {
        // 對應舊 PHP deco_model::get_deco_img_list():
        // JOIN deco_record 帶出 register_number / company_name / company_ceo
        // ORDER BY onoff ASC(未審核在前), bldsno ASC, sort ASC
        return await (
            from img in _db.DecoRecordImgs.AsNoTracking()
            join rec in _db.DecoRecords.AsNoTracking() on img.Bldsno equals (uint)rec.Bldsno into rj
            from rec in rj.DefaultIfEmpty()
            orderby img.Onoff, img.Bldsno, img.Sort
            select new DecoImageListItem
            {
                Id = img.Id,
                Bldsno = img.Bldsno,
                RegisterNumber = rec != null ? rec.RegisterNumber : null,
                CompanyName = rec != null ? rec.CompanyName : string.Empty,
                CompanyCeo = rec != null ? rec.CompanyCeo : null,
                ImgPath = img.ImgPath,
                Sort = img.Sort,
                CreateTime = img.CreateTime,
                UpdateTime = img.UpdateTime,
                Onoff = img.Onoff == "Y",
            })
            .ToPagedResponseAsync(query.Page, query.PageSize, ct);
    }

    public async Task<OperationResult> UpdateOnoffAsync(
        uint id, UpdateDecoImageOnoffRequest request, CancellationToken ct = default)
    {
        var entity = await _db.DecoRecordImgs.FirstOrDefaultAsync(i => i.Id == id, ct);
        if (entity is null)
            return OperationResult.NotFound("找不到圖片");

        entity.Onoff = request.Onoff ? "Y" : "N";
        entity.UpdateTime = DateTime.UtcNow;

        await _db.SaveChangesAsync(ct);
        return OperationResult.Ok("審核狀態更新成功");
    }
}

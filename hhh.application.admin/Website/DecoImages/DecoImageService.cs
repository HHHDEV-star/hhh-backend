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

    public async Task<PagedResponse<DecoImageListItem>> GetListAsync(DecoImageListQuery query, CancellationToken ct = default)
    {
        // 對應舊 PHP deco_model::get_deco_img_list():
        // JOIN deco_record 帶出 register_number / company_name / company_ceo
        // ORDER BY onoff ASC(未審核在前), bldsno ASC, sort ASC
        var baseQuery =
            from img in _db.DecoRecordImgs.AsNoTracking()
            join rec in _db.DecoRecords.AsNoTracking() on img.Bldsno equals (uint)rec.Bldsno into rj
            from rec in rj.DefaultIfEmpty()
            select new { img, rec };

        // 關鍵字篩選：模糊比對公司名稱 / 登記證號 / 負責人
        if (!string.IsNullOrWhiteSpace(query.Keyword))
        {
            var kw = $"%{query.Keyword.Trim()}%";
            baseQuery = baseQuery.Where(x =>
                (x.rec != null && EF.Functions.Like(x.rec.CompanyName, kw)) ||
                (x.rec != null && x.rec.RegisterNumber != null && EF.Functions.Like(x.rec.RegisterNumber, kw)) ||
                (x.rec != null && x.rec.CompanyCeo != null && EF.Functions.Like(x.rec.CompanyCeo, kw)));
        }

        // 審核狀態篩選（Y=通過 / N=未通過）
        if (!string.IsNullOrWhiteSpace(query.Onoff))
        {
            var onoff = query.Onoff.Trim().ToUpper();
            baseQuery = baseQuery.Where(x => x.img.Onoff == onoff);
        }

        return await baseQuery
            .OrderBy(x => x.img.Onoff)
            .ThenBy(x => x.img.Bldsno)
            .ThenBy(x => x.img.Sort)
            .Select(x => new DecoImageListItem
            {
                Id = x.img.Id,
                Bldsno = x.img.Bldsno,
                RegisterNumber = x.rec != null ? x.rec.RegisterNumber : null,
                CompanyName = x.rec != null ? x.rec.CompanyName : string.Empty,
                CompanyCeo = x.rec != null ? x.rec.CompanyCeo : null,
                ImgPath = x.img.ImgPath,
                Sort = x.img.Sort,
                CreateTime = x.img.CreateTime,
                UpdateTime = x.img.UpdateTime,
                Onoff = x.img.Onoff == "Y",
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

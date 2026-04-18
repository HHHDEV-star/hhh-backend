using hhh.api.contracts.admin.Advertise.Ads;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using hhh.infrastructure.Extensions;
using hhh.infrastructure.Logging;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Advertise.Ads;

public class AdService : IAdService
{
    private const string PageName = "廣告";

    private readonly XoopsContext _db;
    private readonly IOperationLogWriter _logWriter;

    public AdService(XoopsContext db, IOperationLogWriter logWriter)
    {
        _db = db;
        _logWriter = logWriter;
    }

    public async Task<PagedResponse<AdListItem>> GetListAsync(AdListQuery query, CancellationToken ct = default)
    {
        var q = _db.Hads.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(query.Type))
            q = q.Where(a => a.Adtype == query.Type.Trim());

        // 關鍵字篩選（Addesc / Keyword / Tabname）
        if (!string.IsNullOrWhiteSpace(query.Keyword))
        {
            var like = $"%{query.Keyword.Trim()}%";
            q = q.Where(a =>
                EF.Functions.Like(a.Addesc, like) ||
                (a.Keyword != null && EF.Functions.Like(a.Keyword, like)) ||
                EF.Functions.Like(a.Tabname, like));
        }

        // 上下架狀態篩選
        if (query.Onoff.HasValue)
            q = q.Where(a => a.Onoff == query.Onoff.Value);

        return await q
            .OrderByDescending(a => a.Adid)
            .Select(a => new AdListItem
            {
                Adid = a.Adid,
                Adtype = a.Adtype,
                Adlogo = a.Adlogo,
                AdlogoMobile = a.AdlogoMobile,
                AdlogoWebp = a.AdlogoWebp,
                AdlogoMobileWebp = a.AdlogoMobileWebp,
                LogoIcon = a.LogoIcon,
                Addesc = a.Addesc,
                Adlongdesc = a.Adlongdesc,
                Adhref = a.Adhref,
                AdhrefR = a.AdhrefR,
                Keyword = a.Keyword,
                Tabname = a.Tabname,
                Onoff = a.Onoff == 1,
                StartTime = a.StartTime,
                EndTime = a.EndTime,
                CreatTime = a.CreatTime,
                ClickCounter = a.ClickCounter,
                HdesignerId = a.HdesignerId,
                HbrandId = a.HbrandId,
                BuilderProductId = a.BuilderProductId,
                WaterMark = a.WaterMark,
                AltUse = a.AltUse,
                IndexChar1 = a.IndexChar1,
                IndexChar21 = a.IndexChar21,
                IndexChar22 = a.IndexChar22,
                IndexChar23 = a.IndexChar23,
            })
            .ToPagedResponseAsync(query.Page, query.PageSize, ct);
    }

    public async Task<OperationResult<uint>> CreateAsync(
        CreateAdRequest request, CancellationToken ct = default)
    {
        var entity = new Had
        {
            Adtype = request.Adtype,
            Adhref = request.Adhref,
            Onoff = (byte)(request.Onoff ? 1 : 0),
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            Addesc = request.Addesc ?? string.Empty,
            Adlongdesc = request.Adlongdesc ?? string.Empty,
            AdhrefR = request.AdhrefR,
            Keyword = request.Keyword,
            Tabname = request.Tabname ?? string.Empty,
            HdesignerId = request.HdesignerId,
            HbrandId = request.HbrandId,
            BuilderProductId = request.BuilderProductId,
            AltUse = request.AltUse ?? string.Empty,
            IndexChar1 = request.IndexChar1,
            IndexChar21 = request.IndexChar21,
            IndexChar22 = request.IndexChar22,
            IndexChar23 = request.IndexChar23,
            // 系統預設
            Adlogo = string.Empty,
            AdlogoWebp = string.Empty,
            AdlogoMobileWebp = string.Empty,
            CreatTime = DateTime.UtcNow,
            IsSend = "N",
        };

        _db.Hads.Add(entity);
        await _db.SaveChangesAsync(ct);

        await _logWriter.WriteAsync(
            PageName, OperationAction.Create,
            $"新增廣告 adid={entity.Adid} type={request.Adtype}",
            cancellationToken: ct);

        return OperationResult<uint>.Created(entity.Adid);
    }

    public async Task<OperationResult<uint>> UpdateAsync(
        uint adid, UpdateAdRequest request, CancellationToken ct = default)
    {
        var entity = await _db.Hads.FirstOrDefaultAsync(a => a.Adid == adid, ct);
        if (entity is null) return OperationResult<uint>.NotFound("找不到廣告");

        entity.Adtype = request.Adtype;
        entity.Adhref = request.Adhref;
        entity.Onoff = (byte)(request.Onoff ? 1 : 0);
        entity.StartTime = request.StartTime;
        entity.EndTime = request.EndTime;
        if (request.Addesc is not null) entity.Addesc = request.Addesc;
        if (request.Adlongdesc is not null) entity.Adlongdesc = request.Adlongdesc;
        entity.AdhrefR = request.AdhrefR;
        entity.Keyword = request.Keyword;
        if (request.Tabname is not null) entity.Tabname = request.Tabname;
        entity.HdesignerId = request.HdesignerId;
        entity.HbrandId = request.HbrandId;
        entity.BuilderProductId = request.BuilderProductId;
        entity.WaterMark = request.WaterMark;
        if (request.AltUse is not null) entity.AltUse = request.AltUse;
        entity.IndexChar1 = request.IndexChar1;
        entity.IndexChar21 = request.IndexChar21;
        entity.IndexChar22 = request.IndexChar22;
        entity.IndexChar23 = request.IndexChar23;

        await _db.SaveChangesAsync(ct);

        await _logWriter.WriteAsync(
            PageName, OperationAction.Update,
            $"修改廣告 adid={adid} type={request.Adtype}",
            cancellationToken: ct);

        return OperationResult<uint>.Ok(adid, "修改成功");
    }

    public async Task<OperationResult> DeleteAsync(uint adid, CancellationToken ct = default)
    {
        var entity = await _db.Hads.FirstOrDefaultAsync(a => a.Adid == adid, ct);
        if (entity is null) return OperationResult.NotFound("找不到廣告");

        _db.Hads.Remove(entity);
        await _db.SaveChangesAsync(ct);

        await _logWriter.WriteAsync(
            PageName, OperationAction.Delete,
            $"刪除廣告 adid={adid} type={entity.Adtype}",
            cancellationToken: ct);

        return OperationResult.Ok("刪除成功");
    }

    public async Task<OperationResult> UpdateLogoAsync(
        uint adid, UpdateAdLogoRequest request, CancellationToken ct = default)
    {
        var entity = await _db.Hads.FirstOrDefaultAsync(a => a.Adid == adid, ct);
        if (entity is null) return OperationResult.NotFound("找不到廣告");

        if (request.Adlogo is not null) entity.Adlogo = request.Adlogo;
        if (request.AdlogoMobile is not null) entity.AdlogoMobile = request.AdlogoMobile;
        if (request.AdlogoWebp is not null) entity.AdlogoWebp = request.AdlogoWebp;
        if (request.AdlogoMobileWebp is not null) entity.AdlogoMobileWebp = request.AdlogoMobileWebp;
        if (request.LogoIcon is not null) entity.LogoIcon = request.LogoIcon;

        await _db.SaveChangesAsync(ct);
        return OperationResult.Ok("圖片更新成功");
    }
}

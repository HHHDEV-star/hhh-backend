using hhh.api.contracts.admin.Editorial.Cases;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using hhh.infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Editorial.Cases;

public class CaseImageService : ICaseImageService
{
    private readonly XoopsContext _db;

    public CaseImageService(XoopsContext db) => _db = db;

    /// <inheritdoc />
    public async Task<PagedResponse<CaseImageListItem>> GetListAsync(
        uint hcaseId, PagedRequest query, CancellationToken ct = default)
    {
        // 對應舊 PHP case_model::get_case_imgs()
        // ORDER BY order ASC, hcase_img_id ASC
        return await _db.HcaseImgs
            .AsNoTracking()
            .Where(i => i.HcaseId == hcaseId)
            .OrderBy(i => i.Order)
            .ThenBy(i => i.HcaseImgId)
            .Select(i => new CaseImageListItem
            {
                HcaseImgId = i.HcaseImgId,
                HcaseId = i.HcaseId,
                Name = i.Name,
                Title = i.Title,
                Desc = i.Desc,
                Order = i.Order,
                Viewed = i.Viewed,
                Tag1 = i.Tag1,
                Tag2 = i.Tag2,
                Tag3 = i.Tag3,
                Tag4 = i.Tag4,
                Tag5 = i.Tag5,
                IsFlat = i.IsFlat == 1,
                IsHint = i.IsHint == 1,
                IsCover = i.IsCover,
                TagDatetime = i.TagDatetime,
            })
            .ToPagedResponseAsync(query.Page, query.PageSize, ct);
    }

    /// <inheritdoc />
    public async Task<OperationResult<uint>> CreateAsync(
        uint hcaseId, CreateCaseImageRequest request, CancellationToken ct = default)
    {
        // 確認個案存在
        var caseExists = await _db.Hcases.AnyAsync(c => c.HcaseId == hcaseId, ct);
        if (!caseExists)
            return OperationResult<uint>.NotFound("找不到個案");

        // 取得目前最大排序值
        var maxOrder = await _db.HcaseImgs
            .Where(i => i.HcaseId == hcaseId)
            .MaxAsync(i => (ushort?)i.Order, ct) ?? 0;

        var entity = new HcaseImg
        {
            HcaseId = hcaseId,
            Name = request.Name,
            Title = request.Title,
            Order = (ushort)(maxOrder + 1),
            TagDatetime = DateTime.UtcNow,
            ChangeName = "N",
        };

        _db.HcaseImgs.Add(entity);
        await _db.SaveChangesAsync(ct);

        return OperationResult<uint>.Created(entity.HcaseImgId, "圖片新增成功");
    }

    /// <inheritdoc />
    public async Task<OperationResult> UpdateAsync(
        uint hcaseImgId, UpdateCaseImageRequest request, CancellationToken ct = default)
    {
        var entity = await _db.HcaseImgs.FirstOrDefaultAsync(i => i.HcaseImgId == hcaseImgId, ct);
        if (entity is null)
            return OperationResult.NotFound("找不到圖片");

        if (request.Title is not null) entity.Title = request.Title;
        if (request.Desc is not null) entity.Desc = request.Desc;
        if (request.Order is { } order) entity.Order = order;
        if (request.Tag1 is not null) entity.Tag1 = request.Tag1;
        if (request.Tag2 is not null) entity.Tag2 = request.Tag2;
        if (request.Tag3 is not null) entity.Tag3 = request.Tag3;
        if (request.Tag4 is not null) entity.Tag4 = request.Tag4;
        if (request.Tag5 is not null) entity.Tag5 = request.Tag5;
        if (request.IsFlat is { } isFlat) entity.IsFlat = (byte)(isFlat ? 1 : 0);
        if (request.IsHint is { } isHint) entity.IsHint = (byte)(isHint ? 1 : 0);

        entity.TagDatetime = DateTime.UtcNow;

        await _db.SaveChangesAsync(ct);
        return OperationResult.Ok("圖片資訊更新成功");
    }

    /// <inheritdoc />
    public async Task<OperationResult> DeleteAsync(uint hcaseImgId, CancellationToken ct = default)
    {
        // 舊版為 hard delete
        var entity = await _db.HcaseImgs.FirstOrDefaultAsync(i => i.HcaseImgId == hcaseImgId, ct);
        if (entity is null)
            return OperationResult.NotFound("找不到圖片");

        _db.HcaseImgs.Remove(entity);
        await _db.SaveChangesAsync(ct);

        return OperationResult.Ok("圖片刪除成功");
    }

    /// <inheritdoc />
    public async Task<OperationResult> SetCoverAsync(
        uint hcaseId, SetCaseCoverRequest request, CancellationToken ct = default)
    {
        // 找到指定圖片
        var img = await _db.HcaseImgs
            .FirstOrDefaultAsync(i => i.HcaseImgId == request.HcaseImgId && i.HcaseId == hcaseId, ct);
        if (img is null)
            return OperationResult.NotFound("找不到圖片");

        // 舊版是把圖片 URL 寫入 _hcase.cover
        var hcase = await _db.Hcases.FirstOrDefaultAsync(c => c.HcaseId == hcaseId, ct);
        if (hcase is null)
            return OperationResult.NotFound("找不到個案");

        // 清除舊封面標記
        await _db.HcaseImgs
            .Where(i => i.HcaseId == hcaseId && i.IsCover)
            .ExecuteUpdateAsync(s => s.SetProperty(i => i.IsCover, false), ct);

        // 設定新封面
        img.IsCover = true;
        hcase.Cover = img.Name;

        await _db.SaveChangesAsync(ct);
        return OperationResult.Ok("封面設定成功");
    }

    /// <inheritdoc />
    public async Task<OperationResult> ReorderAsync(uint hcaseId, CancellationToken ct = default)
    {
        // 對應舊版 Cases/image_order PUT
        // 依目前 order 排序後，重新編號為連續序號 1, 2, 3...
        var images = await _db.HcaseImgs
            .Where(i => i.HcaseId == hcaseId)
            .OrderBy(i => i.Order)
            .ThenBy(i => i.HcaseImgId)
            .ToListAsync(ct);

        if (images.Count == 0)
            return OperationResult.Ok("沒有圖片需要排序");

        for (var idx = 0; idx < images.Count; idx++)
        {
            images[idx].Order = (ushort)(idx + 1);
        }

        await _db.SaveChangesAsync(ct);
        return OperationResult.Ok("圖片排序完成");
    }
}

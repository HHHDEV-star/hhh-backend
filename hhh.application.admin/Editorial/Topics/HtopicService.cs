using hhh.api.contracts.admin.Editorial.Topics;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using hhh.infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Editorial.Topics;

public class HtopicService : IHtopicService
{
    private readonly XoopsContext _db;

    public HtopicService(XoopsContext db) => _db = db;

    public async Task<PagedResponse<HtopicListItem>> GetListAsync(
        HtopicListQuery query, CancellationToken ct = default)
    {
        // 對應舊版 PHP:SELECT * FROM _htopic WHERE {query} ORDER BY {sort} {by}
        var q = _db.Htopics.AsNoTracking().AsQueryable();

        // 搜尋：htopic_id LIKE / title LIKE / desc LIKE
        if (!string.IsNullOrWhiteSpace(query.Q))
        {
            var keyword = query.Q.Trim();
            var like = $"%{keyword}%";

            // 嘗試解析為 ID 做精確匹配,同時也做 title/desc 模糊搜尋
            if (uint.TryParse(keyword, out var searchId))
            {
                q = q.Where(t =>
                    t.HtopicId == searchId ||
                    EF.Functions.Like(t.Title, like) ||
                    EF.Functions.Like(t.Desc, like));
            }
            else
            {
                q = q.Where(t =>
                    EF.Functions.Like(t.Title, like) ||
                    EF.Functions.Like(t.Desc, like));
            }
        }

        return await q
            .OrderByDescending(t => t.HtopicId)
            .Select(t => new HtopicListItem
            {
                HtopicId = t.HtopicId,
                Title = t.Title,
                Desc = t.Desc,
                Onoff = t.Onoff == 1,
                Logo = t.Logo,
                StrarrHcaseId = t.StrarrHcaseId,
                StrarrHcolumnId = t.StrarrHcolumnId,
                CreateTime = t.CreateTime,
                Viewed = t.Viewed,
            })
            .ToPagedResponseAsync(query.Page, query.PageSize, ct);
    }

    public async Task<HtopicDetailResponse?> GetByIdAsync(uint id, CancellationToken ct = default)
    {
        return await _db.Htopics
            .AsNoTracking()
            .Where(t => t.HtopicId == id)
            .Select(t => new HtopicDetailResponse
            {
                HtopicId = t.HtopicId,
                Title = t.Title,
                Desc = t.Desc,
                Logo = t.Logo,
                StrarrHcaseId = t.StrarrHcaseId,
                StrarrHcolumnId = t.StrarrHcolumnId,
                Viewed = t.Viewed,
                CreateTime = t.CreateTime,
                UpdateTime = t.UpdateTime,
                Onoff = t.Onoff == 1,
                IsSend = t.IsSend,
                SeoImage = t.SeoImage,
                SeoTitle = t.SeoTitle,
                SeoDescription = t.SeoDescription,
            })
            .FirstOrDefaultAsync(ct);
    }

    public async Task<OperationResult<uint>> CreateAsync(
        CreateHtopicRequest request, CancellationToken ct = default)
    {
        // 對應舊版 PHP:INSERT INTO _htopic (...) + update_time=NOW()
        var now = DateTime.UtcNow;
        var entity = new Htopic
        {
            Title = request.Title,
            Desc = request.Desc,
            Logo = request.Logo ?? string.Empty,
            SeoImage = request.SeoImage,
            SeoTitle = request.SeoTitle,
            SeoDescription = request.SeoDescription,
            StrarrHcaseId = request.StrarrHcaseId ?? string.Empty,
            StrarrHcolumnId = request.StrarrHcolumnId ?? string.Empty,
            Onoff = (byte)(request.Onoff ? 1 : 0),
            Viewed = 0,
            IsSend = false,
            CreateTime = now,
            UpdateTime = now,
        };

        _db.Htopics.Add(entity);
        await _db.SaveChangesAsync(ct);

        return OperationResult<uint>.Created(entity.HtopicId, "新增成功");
    }

    public async Task<OperationResult<uint>> UpdateAsync(
        uint id, UpdateHtopicRequest request, CancellationToken ct = default)
    {
        var entity = await _db.Htopics.FirstOrDefaultAsync(t => t.HtopicId == id, ct);
        if (entity is null)
            return OperationResult<uint>.NotFound("找不到主題");

        // 對應舊版 PHP:UPDATE _htopic SET update_time=NOW(), ... WHERE htopic_id='{id}'
        entity.Title = request.Title;
        entity.Desc = request.Desc;
        entity.Logo = request.Logo ?? string.Empty;
        entity.SeoImage = request.SeoImage;
        entity.SeoTitle = request.SeoTitle;
        entity.SeoDescription = request.SeoDescription;
        entity.StrarrHcaseId = request.StrarrHcaseId ?? string.Empty;
        entity.StrarrHcolumnId = request.StrarrHcolumnId ?? string.Empty;
        entity.Onoff = (byte)(request.Onoff ? 1 : 0);
        entity.UpdateTime = DateTime.UtcNow;

        await _db.SaveChangesAsync(ct);

        return OperationResult<uint>.Ok(id, "修改成功");
    }

    public async Task<OperationResult> DeleteAsync(uint id, CancellationToken ct = default)
    {
        // 對應舊版 PHP:DELETE FROM _htopic WHERE htopic_id='{delete_id}'（hard delete）
        var entity = await _db.Htopics.FirstOrDefaultAsync(t => t.HtopicId == id, ct);
        if (entity is null)
            return OperationResult.NotFound("找不到主題");

        _db.Htopics.Remove(entity);
        await _db.SaveChangesAsync(ct);

        return OperationResult.Ok("刪除成功");
    }
}

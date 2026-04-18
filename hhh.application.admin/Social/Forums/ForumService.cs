using hhh.api.contracts.admin.Social.Forums;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Social.Forums;

public class ForumService : IForumService
{
    private readonly XoopsContext _db;

    public ForumService(XoopsContext db) => _db = db;

    public async Task<PagedResponse<ForumArticleBackItem>> GetArticleBackListAsync(ForumArticleListQuery query, CancellationToken ct = default)
    {
        // 對應舊 PHP forum_model::get_article_for_back()
        // JOIN _users 帶出 uname / name / email,ORDER BY article_id DESC
        var q = from a in _db.ForumArticles.AsNoTracking()
                join u in _db.Users.AsNoTracking() on a.Uid equals (int)u.Uid into uj
                from u in uj.DefaultIfEmpty()
                select new { a, u };

        // 關鍵字搜尋：標題 / 發文者帳號 / 發文者 Email
        if (!string.IsNullOrWhiteSpace(query.Keyword))
        {
            var like = $"%{query.Keyword.Trim()}%";
            q = q.Where(x =>
                EF.Functions.Like(x.a.Title, like) ||
                (x.u != null && EF.Functions.Like(x.u.Uname, like)) ||
                (x.u != null && EF.Functions.Like(x.u.Email, like)));
        }

        // 分類篩選
        if (query.Category is { } category)
        {
            q = q.Where(x => x.a.Category == category);
        }

        // 置頂篩選
        if (query.IsTop is { } isTop)
        {
            var val = (sbyte)(isTop ? 1 : 0);
            q = q.Where(x => x.a.IsTop == val);
        }

        // 刪除狀態篩選
        if (query.IsDel is { } isDel)
        {
            var val = (sbyte)(isDel ? 1 : 0);
            q = q.Where(x => x.a.IsDel == val);
        }

        // 日期區間篩選（建立時間）
        if (query.DateFrom is { } dateFrom)
        {
            var from = dateFrom.ToDateTime(TimeOnly.MinValue);
            q = q.Where(x => x.a.DateAdded >= from);
        }
        if (query.DateTo is { } dateTo)
        {
            var to = dateTo.ToDateTime(TimeOnly.MaxValue);
            q = q.Where(x => x.a.DateAdded <= to);
        }

        return await q
            .OrderByDescending(x => x.a.ArticleId)
            .Select(x => new ForumArticleBackItem
            {
                ArticleId = x.a.ArticleId,
                Uid = x.a.Uid,
                Uname = x.u != null ? x.u.Uname : string.Empty,
                Name = x.u != null ? x.u.Name : string.Empty,
                Email = x.u != null ? x.u.Email : string.Empty,
                Category = x.a.Category,
                Title = x.a.Title,
                Description = x.a.Description,
                ReplyCount = x.a.ReplyCount,
                GoodCount = x.a.GoodCount,
                BadCount = x.a.BadCount,
                ReadCount = x.a.ReadCount,
                IsTop = x.a.IsTop == 1,
                IsDel = x.a.IsDel == 1,
                IsHidden = x.a.IsHidden == 1,
                SeoImage = x.a.SeoImage,
                DateCreated = x.a.DateAdded,
                DateModified = x.a.DateModified,
            })
            .ToPagedResponseAsync(query.Page, query.PageSize, ct);
    }

    public async Task<OperationResult> UpdateArticleAsync(
        int articleId, UpdateForumArticleRequest request, CancellationToken ct = default)
    {
        var entity = await _db.ForumArticles.FirstOrDefaultAsync(a => a.ArticleId == articleId, ct);
        if (entity is null) return OperationResult.NotFound("找不到文章");

        entity.IsTop = (sbyte)(request.IsTop ? 1 : 0);
        entity.IsDel = (sbyte)(request.IsDel ? 1 : 0);
        entity.ReadCount = request.ReadCount;
        if (request.SeoTitle is not null) entity.SeoTitle = request.SeoTitle;
        entity.DateModified = DateTime.UtcNow;

        await _db.SaveChangesAsync(ct);
        return OperationResult.Ok("文章修改成功");
    }

    public async Task<PagedResponse<ForumReplyBackItem>> GetReplyBackListAsync(int articleId, ForumReplyListQuery query, CancellationToken ct = default)
    {
        // 對應舊 PHP forum_model::get_article_reply_for_back()
        var q = from r in _db.ForumArticleReplies.AsNoTracking()
                where r.ArticleId == articleId
                join u in _db.Users.AsNoTracking() on r.Uid equals (int)u.Uid into uj
                from u in uj.DefaultIfEmpty()
                select new { r, u };

        // 關鍵字搜尋：帳號 / 回覆內容
        if (!string.IsNullOrWhiteSpace(query.Keyword))
        {
            var like = $"%{query.Keyword.Trim()}%";
            q = q.Where(x =>
                (x.u != null && EF.Functions.Like(x.u.Uname, like)) ||
                EF.Functions.Like(x.r.ReplyContent, like));
        }

        // 刪除狀態篩選
        if (query.IsDel is { } isDel)
        {
            var val = (sbyte)(isDel ? 1 : 0);
            q = q.Where(x => x.r.IsDel == val);
        }

        return await q
            .OrderByDescending(x => x.r.ArticleReplyId)
            .Select(x => new ForumReplyBackItem
            {
                ArticleReplyId = x.r.ArticleReplyId,
                ArticleId = x.r.ArticleId,
                Uid = x.r.Uid,
                Uname = x.u != null ? x.u.Uname : string.Empty,
                Name = x.u != null ? x.u.Name : string.Empty,
                Email = x.u != null ? x.u.Email : string.Empty,
                ReplyContent = x.r.ReplyContent,
                GoodCount = x.r.ReplyGoodCount,
                BadCount = x.r.ReplyBadCount,
                IsDel = x.r.IsDel == 1,
                DateCreated = x.r.DateAdded,
                DateModified = x.r.DateModified,
            })
            .ToPagedResponseAsync(query.Page, query.PageSize, ct);
    }

    public async Task<OperationResult> UpdateReplyAsync(
        int replyId, UpdateForumReplyRequest request, CancellationToken ct = default)
    {
        var entity = await _db.ForumArticleReplies.FirstOrDefaultAsync(r => r.ArticleReplyId == replyId, ct);
        if (entity is null) return OperationResult.NotFound("找不到回覆");

        entity.IsDel = (sbyte)(request.IsDel ? 1 : 0);
        entity.DateModified = DateTime.UtcNow;

        await _db.SaveChangesAsync(ct);
        return OperationResult.Ok("回覆修改成功");
    }

    public async Task<OperationResult> UpdateSeoImageAsync(
        int articleId, UpdateForumSeoImageRequest request, CancellationToken ct = default)
    {
        var entity = await _db.ForumArticles.FirstOrDefaultAsync(a => a.ArticleId == articleId, ct);
        if (entity is null) return OperationResult.NotFound("找不到文章");

        entity.SeoImage = request.SeoImage;
        await _db.SaveChangesAsync(ct);
        return OperationResult.Ok("SEO 圖片更新成功");
    }

    public async Task<ForumBlockListResponse> GetBlockListAsync(ForumBlockListQuery query, CancellationToken ct = default)
    {
        // 對應舊 PHP forum_model::get_block():
        // 預設只撈 forum_block='Y',可透過 status 切換全部/黑名單/非黑名單
        var q = _db.Users.AsNoTracking().AsQueryable();

        // 黑名單狀態篩選（預設只撈黑名單）
        if (string.Equals(query.Status, "all", StringComparison.OrdinalIgnoreCase))
        {
            // 全部會員，不過濾
        }
        else if (string.Equals(query.Status, "N", StringComparison.OrdinalIgnoreCase))
        {
            q = q.Where(u => u.ForumBlock == "N");
        }
        else
        {
            // 預設或 status=Y：只撈黑名單
            q = q.Where(u => u.ForumBlock == "Y");
        }

        // 帳號模糊搜尋
        if (!string.IsNullOrWhiteSpace(query.Uname))
        {
            var like = $"%{query.Uname.Trim()}%";
            q = q.Where(u => EF.Functions.Like(u.Uname, like));
        }

        // Email 模糊搜尋
        if (!string.IsNullOrWhiteSpace(query.Email))
        {
            var like = $"%{query.Email.Trim()}%";
            q = q.Where(u => EF.Functions.Like(u.Email, like));
        }

        var paged = await q.OrderByDescending(u => u.Uid)
            .Select(u => new ForumBlockItem
            {
                Uid = u.Uid,
                Name = u.Name,
                Uname = u.Uname,
                Email = u.Email,
                Phone = u.UserIntrest,
                RegisterDate = u.UserRegdateDatetime,
                LastLoginDate = u.LastLoginDatetime,
                Posts = u.Posts,
                ForumBlock = u.ForumBlock == "Y",
            }).ToPagedResponseAsync(query.Page, query.PageSize, ct);

        // 全域統計（不受查詢條件影響）
        var userTotal = await _db.Users.CountAsync(ct);
        var blockCount = await _db.Users.CountAsync(u => u.ForumBlock == "Y", ct);

        return new ForumBlockListResponse
        {
            Items = paged.Items,
            Total = paged.Total,
            Page = paged.Page,
            PageSize = paged.PageSize,
            Summary = new ForumBlockSummary
            {
                UserTotal = userTotal,
                BlockCount = blockCount,
            },
        };
    }

    public async Task<OperationResult> UpdateBlockAsync(
        uint uid, UpdateForumBlockRequest request, CancellationToken ct = default)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Uid == uid, ct);
        if (user is null) return OperationResult.NotFound("找不到使用者");

        user.ForumBlock = request.ForumBlock ? "Y" : "N";
        await _db.SaveChangesAsync(ct);
        return OperationResult.Ok(request.ForumBlock ? "已封鎖" : "已解除封鎖");
    }
}

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

    public async Task<PagedResponse<ForumArticleBackItem>> GetArticleBackListAsync(ListQuery query, CancellationToken ct = default)
    {
        // 對應舊 PHP forum_model::get_article_for_back()
        // JOIN _users 帶出 uname / email,ORDER BY article_id DESC
        return await (
            from a in _db.ForumArticles.AsNoTracking()
            join u in _db.Users.AsNoTracking() on a.Uid equals (int)u.Uid into uj
            from u in uj.DefaultIfEmpty()
            orderby a.ArticleId descending
            select new ForumArticleBackItem
            {
                ArticleId = a.ArticleId,
                Uid = a.Uid,
                Uname = u != null ? u.Uname : string.Empty,
                Email = u != null ? u.Email : string.Empty,
                Category = a.Category,
                Title = a.Title,
                ReplyCount = a.ReplyCount,
                IsTop = a.IsTop == 1,
                IsDel = a.IsDel == 1,
                ReadCount = a.ReadCount,
                SeoImage = a.SeoImage,
                DateCreated = a.DateAdded,
                DateModified = a.DateModified,
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

    public async Task<PagedResponse<ForumReplyBackItem>> GetReplyBackListAsync(int articleId, ListQuery query, CancellationToken ct = default)
    {
        // 對應舊 PHP forum_model::get_article_reply_for_back()
        return await (
            from r in _db.ForumArticleReplies.AsNoTracking()
            where r.ArticleId == articleId
            join u in _db.Users.AsNoTracking() on r.Uid equals (int)u.Uid into uj
            from u in uj.DefaultIfEmpty()
            orderby r.ArticleReplyId descending
            select new ForumReplyBackItem
            {
                ArticleReplyId = r.ArticleReplyId,
                ArticleId = r.ArticleId,
                Uid = r.Uid,
                Uname = u != null ? u.Uname : string.Empty,
                ReplyContent = r.ReplyContent,
                IsDel = r.IsDel == 1,
                DateCreated = r.DateAdded,
                DateModified = r.DateModified,
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

    public async Task<PagedResponse<ForumBlockItem>> GetBlockListAsync(string? uname, ListQuery query, CancellationToken ct = default)
    {
        // 對應舊 PHP forum_model::get_block():
        // 預設只撈 forum_block='Y',若有帶 uname 則額外 LIKE 搜尋
        var q = _db.Users.AsNoTracking()
            .Where(u => u.ForumBlock == "Y");

        if (!string.IsNullOrWhiteSpace(uname))
        {
            var like = $"%{uname.Trim()}%";
            q = q.Where(u => EF.Functions.Like(u.Uname, like));
        }

        return await q.OrderByDescending(u => u.Uid)
            .Select(u => new ForumBlockItem
            {
                Uid = u.Uid,
                Name = u.Name,
                Uname = u.Uname,
                Email = u.Email,
            }).ToPagedResponseAsync(query.Page, query.PageSize, ct);
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

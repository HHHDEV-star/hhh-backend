using hhh.api.contracts.admin.Social.Forums;
using hhh.application.admin.Common;

namespace hhh.application.admin.Social.Forums;

/// <summary>
/// 討論區後台管理服務
/// </summary>
/// <remarks>
/// 對應舊版 PHP:hhh-api/.../base/v1/Forum.php(forum_model)
/// 後台 view:forum.php(文章/回覆管理) + forum_block.php(黑名單)
/// </remarks>
public interface IForumService
{
    Task<List<ForumArticleBackItem>> GetArticleBackListAsync(CancellationToken ct = default);
    Task<OperationResult> UpdateArticleAsync(int articleId, UpdateForumArticleRequest request, CancellationToken ct = default);
    Task<List<ForumReplyBackItem>> GetReplyBackListAsync(int articleId, CancellationToken ct = default);
    Task<OperationResult> UpdateReplyAsync(int replyId, UpdateForumReplyRequest request, CancellationToken ct = default);
    Task<OperationResult> UpdateSeoImageAsync(int articleId, UpdateForumSeoImageRequest request, CancellationToken ct = default);
    Task<List<ForumBlockItem>> GetBlockListAsync(string? uname, CancellationToken ct = default);
    Task<OperationResult> UpdateBlockAsync(uint uid, UpdateForumBlockRequest request, CancellationToken ct = default);
}

using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Social.Briefs;
using hhh.api.contracts.admin.Social.Decorations;
using hhh.api.contracts.admin.Social.Forums;
using hhh.application.admin.Social.Briefs;
using hhh.application.admin.Social.Decorations;
using hhh.application.admin.Social.Forums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hhh.webapi.admin.Controllers;

/// <summary>
/// Social
/// </summary>
/// <remarks>
/// 後台「Social」業務類別下所有 endpoint 集中於此 controller。
///
/// 對應舊版:
///  - 屋主上傳名片:hhh-api/.../third/v1/Events.php → brief_get
///  - 全室裝修收名單:hhh-api/.../third/v1/Decoration.php → lists_get / index_post
///  - 討論區:hhh-api/.../base/v1/Forum.php → 後台管理 endpoints
/// </remarks>
[Route("api/social")]
[Authorize]
[Tags("Social")]
public class SocialController : ApiControllerBase
{
    private readonly IBriefService _briefService;
    private readonly IDecorationService _decorationService;
    private readonly IForumService _forumService;

    public SocialController(
        IBriefService briefService,
        IDecorationService decorationService,
        IForumService forumService)
    {
        _briefService = briefService;
        _decorationService = decorationService;
        _forumService = forumService;
    }

    // =========================================================================
    // 屋主上傳名片領好康 (briefs)
    // =========================================================================

    /// <summary>取得名片列表(全量,brief_id DESC)</summary>
    /// <remarks>
    /// 對應舊版 PHP:Events/brief_get → events_model::brief_lists()
    /// SELECT * FROM brief ORDER BY brief_id DESC,無 paging、無 filter。
    /// </remarks>
    [HttpGet("briefs")]
    [ProducesResponseType(typeof(ApiResponse<List<BriefListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBriefList(CancellationToken cancellationToken)
    {
        var data = await _briefService.GetListAsync(cancellationToken);
        return Ok(ApiResponse<List<BriefListItem>>.Success(data));
    }

    // =========================================================================
    // 全室裝修收名單 (decorations)
    // =========================================================================

    /// <summary>取得全室裝修收名單列表(全量,id DESC)</summary>
    /// <remarks>
    /// 對應舊版 PHP:Decoration/lists_get → decoration_model::lists()
    /// SELECT * FROM decoration ORDER BY id DESC,無 paging、無 filter。
    /// </remarks>
    [HttpGet("decorations")]
    [ProducesResponseType(typeof(ApiResponse<List<DecorationListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDecorationList(CancellationToken cancellationToken)
    {
        var data = await _decorationService.GetListAsync(cancellationToken);
        return Ok(ApiResponse<List<DecorationListItem>>.Success(data));
    }

    /// <summary>新增全室裝修收名單</summary>
    /// <remarks>
    /// 對應舊版 PHP:Decoration/index_post → decoration_model::insert()
    /// 舊版必填:email、name、phone、area、type、pin。
    /// type 僅接受:預售屋 / 新屋 / 中古屋。
    /// pin 僅接受:10 坪以下 / 11~20 坪 / 21~30 坪 / 31~40 坪 / 41~50 坪 / 51 坪以上。
    /// </remarks>
    [HttpPost("decorations")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateDecoration(
        [FromBody] CreateDecorationRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _decorationService.CreateAsync(request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return StatusCode(StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { id = result.Data }, result.Message));
    }

    // =========================================================================
    // 討論區 - 文章管理 (forum-articles)
    // =========================================================================

    /// <summary>取得後台文章列表</summary>
    /// <remarks>對應舊版 Forum/article_back_list_get → forum_model::get_article_for_back()。JOIN _users 帶 uname/email。</remarks>
    [HttpGet("forum-articles")]
    [ProducesResponseType(typeof(ApiResponse<List<ForumArticleBackItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetForumArticleList(CancellationToken cancellationToken)
    {
        var data = await _forumService.GetArticleBackListAsync(cancellationToken);
        return Ok(ApiResponse<List<ForumArticleBackItem>>.Success(data));
    }

    /// <summary>後台編輯文章(置頂/刪除/閱讀數/SEO)</summary>
    /// <remarks>
    /// 對應舊版 Forum/article_edit_put → forum_model::upd_article()。
    /// 舊版是 batch,本 API 改成 single record。
    /// </remarks>
    [HttpPut("forum-articles/{articleId:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateForumArticle(
        int articleId, [FromBody] UpdateForumArticleRequest request, CancellationToken cancellationToken)
    {
        var result = await _forumService.UpdateArticleAsync(articleId, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }

    /// <summary>更新文章 SEO 圖片</summary>
    /// <remarks>對應舊版 Forum/seoimage_put → forum_model::update_forum_seo_image()。舊版是 batch。</remarks>
    [HttpPut("forum-articles/{articleId:int}/seo-image")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateForumSeoImage(
        int articleId, [FromBody] UpdateForumSeoImageRequest request, CancellationToken cancellationToken)
    {
        var result = await _forumService.UpdateSeoImageAsync(articleId, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }

    // =========================================================================
    // 討論區 - 回覆管理 (forum-replies)
    // =========================================================================

    /// <summary>取得某文章的後台回覆列表</summary>
    /// <remarks>對應舊版 Forum/article_reply_back_list_get → forum_model::get_article_reply_for_back()。必帶 articleId。</remarks>
    [HttpGet("forum-articles/{articleId:int}/replies")]
    [ProducesResponseType(typeof(ApiResponse<List<ForumReplyBackItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetForumReplyList(int articleId, CancellationToken cancellationToken)
    {
        var data = await _forumService.GetReplyBackListAsync(articleId, cancellationToken);
        return Ok(ApiResponse<List<ForumReplyBackItem>>.Success(data));
    }

    /// <summary>後台編輯回覆(刪除/恢復)</summary>
    /// <remarks>對應舊版 Forum/article_reply_edit_put → forum_model::upd_article_reply()。舊版是 batch。</remarks>
    [HttpPut("forum-replies/{replyId:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateForumReply(
        int replyId, [FromBody] UpdateForumReplyRequest request, CancellationToken cancellationToken)
    {
        var result = await _forumService.UpdateReplyAsync(replyId, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }

    // =========================================================================
    // 討論區 - 黑名單 (forum-blocks)
    // =========================================================================

    /// <summary>取得討論區黑名單列表</summary>
    /// <remarks>對應舊版 Forum/block_get → forum_model::get_block()。可選 uname 模糊搜尋。</remarks>
    [HttpGet("forum-blocks")]
    [ProducesResponseType(typeof(ApiResponse<List<ForumBlockItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetForumBlockList(
        [FromQuery] string? uname, CancellationToken cancellationToken)
    {
        var data = await _forumService.GetBlockListAsync(uname, cancellationToken);
        return Ok(ApiResponse<List<ForumBlockItem>>.Success(data));
    }

    /// <summary>設定/解除討論區黑名單</summary>
    /// <remarks>
    /// 對應舊版 Forum/block_put → forum_model::set_block()。
    /// 舊版是 batch,本 API 改成 single record(uid 走 URL)。
    /// forumBlock=true → 封鎖(Y),forumBlock=false → 解除(N)。
    /// </remarks>
    [HttpPut("forum-blocks/{uid:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateForumBlock(
        uint uid, [FromBody] UpdateForumBlockRequest request, CancellationToken cancellationToken)
    {
        var result = await _forumService.UpdateBlockAsync(uid, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }
}

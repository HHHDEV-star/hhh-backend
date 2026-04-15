using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Social.Briefs;
using hhh.api.contracts.admin.Social.Decorations;
using hhh.api.contracts.admin.Social.Forums;
using hhh.api.contracts.admin.Social.Precises;
using hhh.api.contracts.admin.Social.Products;
using hhh.application.admin.Social.Briefs;
using hhh.application.admin.Social.Decorations;
using hhh.application.admin.Social.Forums;
using hhh.application.admin.Social.Precises;
using hhh.application.admin.Social.Products;
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
///  - 精準名單白皮書:hhh-api/.../third/v1/Precise.php
///  - 產品:hhh-api/.../base/v1/Product.php
/// </remarks>
[Route("api/social")]
[Authorize]
[Tags("Social")]
public class SocialController : ApiControllerBase
{
    private readonly IBriefService _briefService;
    private readonly IDecorationService _decorationService;
    private readonly IForumService _forumService;
    private readonly IPreciseService _preciseService;
    private readonly IProductService _productService;

    public SocialController(
        IBriefService briefService,
        IDecorationService decorationService,
        IForumService forumService,
        IPreciseService preciseService,
        IProductService productService)
    {
        _briefService = briefService;
        _decorationService = decorationService;
        _forumService = forumService;
        _preciseService = preciseService;
        _productService = productService;
    }

    // =========================================================================
    // 屋主上傳名片領好康 (briefs)
    // =========================================================================

    /// <summary>取得名片列表(全量,brief_id DESC)</summary>
    /// <remarks>
    /// 對應舊版 PHP:Events/brief_get → events_model::brief_lists()
    /// SELECT * FROM brief ORDER BY brief_id DESC,無 paging、無 filter。
    /// </remarks>
    [HttpGet("briefs/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<BriefListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBriefList([FromQuery] ListQuery query, CancellationToken cancellationToken)
    {
        var data = await _briefService.GetListAsync(query, cancellationToken);
        return Ok(ApiResponse<PagedResponse<BriefListItem>>.Success(data));
    }

    // =========================================================================
    // 全室裝修收名單 (decorations)
    // =========================================================================

    /// <summary>取得全室裝修收名單列表(全量,id DESC)</summary>
    /// <remarks>
    /// 對應舊版 PHP:Decoration/lists_get → decoration_model::lists()
    /// SELECT * FROM decoration ORDER BY id DESC,無 paging、無 filter。
    /// </remarks>
    [HttpGet("decorations/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<DecorationListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetDecorationList([FromQuery] ListQuery query, CancellationToken cancellationToken)
    {
        var data = await _decorationService.GetListAsync(query, cancellationToken);
        return Ok(ApiResponse<PagedResponse<DecorationListItem>>.Success(data));
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
    [HttpGet("forum-articles/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<ForumArticleBackItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetForumArticleList([FromQuery] ListQuery query, CancellationToken cancellationToken)
    {
        var data = await _forumService.GetArticleBackListAsync(query, cancellationToken);
        return Ok(ApiResponse<PagedResponse<ForumArticleBackItem>>.Success(data));
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
    [HttpGet("forum-articles/{articleId:int}/replies/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<ForumReplyBackItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetForumReplyList(
        int articleId, [FromQuery] ListQuery query, CancellationToken cancellationToken)
    {
        var data = await _forumService.GetReplyBackListAsync(articleId, query, cancellationToken);
        return Ok(ApiResponse<PagedResponse<ForumReplyBackItem>>.Success(data));
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
    [HttpGet("forum-blocks/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<ForumBlockItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetForumBlockList(
        [FromQuery] string? uname, [FromQuery] ListQuery query, CancellationToken cancellationToken)
    {
        var data = await _forumService.GetBlockListAsync(uname, query, cancellationToken);
        return Ok(ApiResponse<PagedResponse<ForumBlockItem>>.Success(data));
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

    // =========================================================================
    // 精準名單白皮書 (precises)
    // =========================================================================

    /// <summary>取得精準名單列表(全量,id DESC)</summary>
    /// <remarks>對應舊版 PHP:Precise/lists_get → precise_model::lists()。</remarks>
    [HttpGet("precises/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<PreciseListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPreciseList([FromQuery] ListQuery query, CancellationToken cancellationToken)
    {
        var data = await _preciseService.GetListAsync(query, cancellationToken);
        return Ok(ApiResponse<PagedResponse<PreciseListItem>>.Success(data));
    }

    /// <summary>新增精準名單</summary>
    /// <remarks>
    /// 對應舊版 PHP:Precise/index_post → precise_model::insert()。
    /// 舊版必填:identity、email、name、company、mobile。
    /// identity 僅接受:designer / supplier。
    /// </remarks>
    [HttpPost("precises")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreatePrecise(
        [FromBody] CreatePreciseRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _preciseService.CreateAsync(request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return StatusCode(StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { id = result.Data }, result.Message));
    }

    // =========================================================================
    // 產品 (products)
    // =========================================================================

    /// <summary>取得產品後台列表</summary>
    /// <remarks>對應舊版 Product/index_get → product_model::get_product_lists()。全量 id DESC。</remarks>
    [HttpGet("products/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<ProductListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductList([FromQuery] ListQuery query, CancellationToken cancellationToken)
    {
        var data = await _productService.GetListAsync(query, cancellationToken);
        return Ok(ApiResponse<PagedResponse<ProductListItem>>.Success(data));
    }

    /// <summary>更新產品(上下架/分類)</summary>
    /// <remarks>
    /// 對應舊版 Product/index_put → product_model::update_product_data()。
    /// 舊版必填:onoff、cate1、cate2。舊版是 batch,本 API 改成 single record。
    /// </remarks>
    [HttpPut("products/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProduct(
        uint id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var result = await _productService.UpdateAsync(id, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }

    /// <summary>取得產品 SEO 列表</summary>
    /// <remarks>對應舊版 Product/seo_get → product_model::get_product_lists_seo()。</remarks>
    [HttpGet("products/seo/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<ProductSeoItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductSeoList(
        [FromQuery] ListQuery query, CancellationToken cancellationToken)
    {
        var data = await _productService.GetSeoListAsync(query, cancellationToken);
        return Ok(ApiResponse<PagedResponse<ProductSeoItem>>.Success(data));
    }

    /// <summary>更新產品 SEO 圖片</summary>
    /// <remarks>對應舊版 Product/seoimage_put → product_model::update_product_seo_image()。舊版是 batch。</remarks>
    [HttpPut("products/{id:int}/seo-image")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProductSeoImage(
        uint id, [FromBody] UpdateProductSeoImageRequest request, CancellationToken cancellationToken)
    {
        var result = await _productService.UpdateSeoImageAsync(id, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }
}

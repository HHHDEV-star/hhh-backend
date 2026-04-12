namespace hhh.api.contracts.admin.Social.Forums;

/// <summary>
/// 後台編輯文章請求(article_id 走 URL)
/// (對應舊版 Forum/article_edit_put → forum_model::upd_article)
/// </summary>
public class UpdateForumArticleRequest
{
    /// <summary>是否置頂(0/1)</summary>
    public bool IsTop { get; set; }
    /// <summary>是否刪除(0/1)</summary>
    public bool IsDel { get; set; }
    /// <summary>閱讀數</summary>
    public int ReadCount { get; set; }
    /// <summary>SEO 標題</summary>
    public string? SeoTitle { get; set; }
}

namespace hhh.api.contracts.admin.Social.Forums;

/// <summary>
/// 後台編輯回覆請求(article_reply_id 走 URL)
/// (對應舊版 Forum/article_reply_edit_put → forum_model::upd_article_reply)
/// </summary>
public class UpdateForumReplyRequest
{
    /// <summary>是否刪除(0/1)</summary>
    public bool IsDel { get; set; }
}

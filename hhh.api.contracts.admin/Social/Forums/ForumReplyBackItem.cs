namespace hhh.api.contracts.admin.Social.Forums;

/// <summary>後台回覆列表項目(對應 forum_model::get_article_reply_for_back)</summary>
public class ForumReplyBackItem
{
    public int ArticleReplyId { get; set; }
    public int ArticleId { get; set; }
    public int Uid { get; set; }
    public string Uname { get; set; } = string.Empty;
    public string ReplyContent { get; set; } = string.Empty;
    public bool IsDel { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
}

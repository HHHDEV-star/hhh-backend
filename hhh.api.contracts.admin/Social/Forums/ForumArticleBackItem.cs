namespace hhh.api.contracts.admin.Social.Forums;

/// <summary>後台文章列表項目(對應 forum_model::get_article_for_back)</summary>
public class ForumArticleBackItem
{
    public int ArticleId { get; set; }
    public int Uid { get; set; }
    public string Uname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Category { get; set; }
    public string Title { get; set; } = string.Empty;
    public int ReplyCount { get; set; }
    public bool IsTop { get; set; }
    public bool IsDel { get; set; }
    public int ReadCount { get; set; }
    public string? SeoImage { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
}

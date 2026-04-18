namespace hhh.api.contracts.admin.Social.Forums;

/// <summary>後台文章列表項目(對應 forum_model::get_article_for_back)</summary>
public class ForumArticleBackItem
{
    /// <summary>文章編號</summary>
    public int ArticleId { get; set; }

    /// <summary>發文者 UID</summary>
    public int Uid { get; set; }

    /// <summary>發文者帳號</summary>
    public string Uname { get; set; } = string.Empty;

    /// <summary>發文者姓名</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>發文者 Email</summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>文章分類</summary>
    public int Category { get; set; }

    /// <summary>標題</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>簡介</summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>回覆數</summary>
    public int ReplyCount { get; set; }

    /// <summary>按讚數</summary>
    public int GoodCount { get; set; }

    /// <summary>倒讚數</summary>
    public int BadCount { get; set; }

    /// <summary>閱讀次數</summary>
    public int ReadCount { get; set; }

    /// <summary>是否置頂</summary>
    public bool IsTop { get; set; }

    /// <summary>是否刪除</summary>
    public bool IsDel { get; set; }

    /// <summary>是否自行隱藏</summary>
    public bool IsHidden { get; set; }

    /// <summary>SEO 圖片</summary>
    public string? SeoImage { get; set; }

    /// <summary>建立時間</summary>
    public DateTime DateCreated { get; set; }

    /// <summary>修改時間</summary>
    public DateTime DateModified { get; set; }
}

namespace hhh.api.contracts.admin.Social.Forums;

/// <summary>討論區黑名單項目(對應 forum_model::get_block)</summary>
public class ForumBlockItem
{
    public uint Uid { get; set; }

    /// <summary>姓名</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>帳號</summary>
    public string Uname { get; set; } = string.Empty;

    /// <summary>Email</summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>聯絡電話</summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>註冊時間</summary>
    public DateTime RegisterDate { get; set; }

    /// <summary>最後登入時間</summary>
    public DateTime LastLoginDate { get; set; }

    /// <summary>發文數</summary>
    public uint Posts { get; set; }

    /// <summary>是否為黑名單</summary>
    public bool ForumBlock { get; set; }
}

namespace hhh.api.contracts.admin.Social.Forums;

/// <summary>
/// 討論區黑名單統計摘要（全域統計，不受查詢條件影響）
/// </summary>
public class ForumBlockSummary
{
    /// <summary>會員總數</summary>
    public int UserTotal { get; set; }

    /// <summary>黑名單人數（forum_block = 'Y'）</summary>
    public int BlockCount { get; set; }
}

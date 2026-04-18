using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Social.Forums;

/// <summary>
/// 討論區黑名單列表回應：分頁資料 + 統計摘要
/// </summary>
public class ForumBlockListResponse : PagedResponse<ForumBlockItem>
{
    /// <summary>列表上方的統計摘要</summary>
    public ForumBlockSummary Summary { get; set; } = new();
}

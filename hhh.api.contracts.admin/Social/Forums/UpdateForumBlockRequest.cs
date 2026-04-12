using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Social.Forums;

/// <summary>
/// 更新黑名單狀態請求(uid 走 URL)
/// (對應舊版 Forum/block_put → forum_model::set_block)
/// </summary>
public class UpdateForumBlockRequest
{
    /// <summary>是否封鎖(true=封鎖 Y / false=解除 N)</summary>
    [Required]
    public bool ForumBlock { get; set; }
}

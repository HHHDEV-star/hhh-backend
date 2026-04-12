namespace hhh.api.contracts.admin.Social.Forums;

/// <summary>討論區黑名單項目(對應 forum_model::get_block)</summary>
public class ForumBlockItem
{
    public uint Uid { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Uname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

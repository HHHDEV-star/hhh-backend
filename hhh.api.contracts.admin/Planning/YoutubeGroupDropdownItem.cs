namespace hhh.api.contracts.admin.Planning;

/// <summary>YouTube 群組下拉選單項目</summary>
public class YoutubeGroupDropdownItem
{
    /// <summary>群組 ID</summary>
    public uint Gid { get; set; }

    /// <summary>群組名稱</summary>
    public string Name { get; set; } = string.Empty;
}

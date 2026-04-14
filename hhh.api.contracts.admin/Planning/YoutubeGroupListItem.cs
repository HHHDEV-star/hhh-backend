namespace hhh.api.contracts.admin.Planning;

/// <summary>YouTube 群組管理列表項目</summary>
public class YoutubeGroupListItem
{
    /// <summary>群組 ID</summary>
    public uint Gid { get; set; }

    /// <summary>群組名稱(代號)</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>是否啟用 (Y/N)</summary>
    public string Onoff { get; set; } = string.Empty;

    /// <summary>建立時間</summary>
    public DateTime CreateTime { get; set; }
}

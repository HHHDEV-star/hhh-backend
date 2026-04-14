namespace hhh.api.contracts.admin.Planning;

/// <summary>YouTube 群組明細列表項目（JOIN youtube_list + youtube_group）</summary>
public class YoutubeGroupDetailListItem
{
    /// <summary>明細 PK</summary>
    public uint Id { get; set; }

    /// <summary>群組 ID</summary>
    public uint Gid { get; set; }

    /// <summary>群組名稱</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>影片 ID</summary>
    public uint Yid { get; set; }

    /// <summary>影片名稱</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>排序</summary>
    public byte Sort { get; set; }

    /// <summary>是否開啟 (Y/N)</summary>
    public string Onoff { get; set; } = string.Empty;

    /// <summary>建立時間</summary>
    public DateTime CreateTime { get; set; }
}

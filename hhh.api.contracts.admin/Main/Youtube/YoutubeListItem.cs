namespace hhh.api.contracts.admin.Main.Youtube;

/// <summary>
/// Youtube 影片列表項目
/// (對應舊版 hhh-api/.../third/v1/Youtube.php → index_get → youtube_model::get_youtube_list())
/// </summary>
public class YoutubeListItem
{
    /// <summary>PK</summary>
    public uint Yid { get; set; }

    /// <summary>設計師 ID</summary>
    public uint HdesignerId { get; set; }

    /// <summary>廠商 ID</summary>
    public uint HbrandId { get; set; }

    /// <summary>建商 ID</summary>
    public int BuilderId { get; set; }

    /// <summary>頻道編號</summary>
    public string ChannelId { get; set; } = string.Empty;

    /// <summary>影片標題</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>影片敘述</summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>影片縮圖 URL</summary>
    public string YoutubeImg { get; set; } = string.Empty;

    /// <summary>Youtube 影片 ID</summary>
    public string YoutubeVideoId { get; set; } = string.Empty;

    /// <summary>影片發布時間</summary>
    public DateTime PublishedTime { get; set; }

    /// <summary>建立時間</summary>
    public DateTime CreateTime { get; set; }

    /// <summary>更新時間</summary>
    public DateTime UpdateTime { get; set; }

    /// <summary>是否開啟</summary>
    public bool Onoff { get; set; }
}

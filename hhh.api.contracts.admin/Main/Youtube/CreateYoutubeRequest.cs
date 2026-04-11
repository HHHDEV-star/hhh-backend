using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Main.Youtube;

/// <summary>
/// 新增 Youtube 影片請求
/// (對應舊版 Youtube.php → index_post)
/// 舊版必填:hdesigner_id, hbrand_id, builder_id, channel_id, title, description
/// </summary>
public class CreateYoutubeRequest
{
    /// <summary>設計師 ID</summary>
    [Required]
    public uint HdesignerId { get; set; }

    /// <summary>廠商 ID</summary>
    [Required]
    public uint HbrandId { get; set; }

    /// <summary>建商 ID</summary>
    [Required]
    public int BuilderId { get; set; }

    /// <summary>頻道編號</summary>
    [Required]
    [StringLength(50)]
    public string ChannelId { get; set; } = string.Empty;

    /// <summary>影片標題</summary>
    [Required]
    [StringLength(60)]
    public string Title { get; set; } = string.Empty;

    /// <summary>影片敘述</summary>
    [Required]
    public string Description { get; set; } = string.Empty;

    /// <summary>影片發布時間</summary>
    public DateTime? PublishedTime { get; set; }

    /// <summary>Youtube 影片 ID</summary>
    [StringLength(11)]
    public string? YoutubeVideoId { get; set; }

    /// <summary>影片縮圖 URL</summary>
    [StringLength(60)]
    public string? YoutubeImg { get; set; }
}

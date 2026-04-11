using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Main.Youtube;

/// <summary>
/// 更新 Youtube 影片請求(yid 走 URL)
/// (對應舊版 Youtube.php → index_put)
/// 舊版必填:yid(URL), onoff
/// </summary>
public class UpdateYoutubeRequest
{
    /// <summary>是否開啟(必填)</summary>
    [Required]
    public bool Onoff { get; set; }

    /// <summary>設計師 ID</summary>
    public uint HdesignerId { get; set; }

    /// <summary>廠商 ID</summary>
    public uint HbrandId { get; set; }

    /// <summary>建商 ID</summary>
    public int BuilderId { get; set; }

    /// <summary>頻道編號</summary>
    [StringLength(50)]
    public string? ChannelId { get; set; }

    /// <summary>影片標題</summary>
    [StringLength(60)]
    public string? Title { get; set; }

    /// <summary>影片敘述</summary>
    public string? Description { get; set; }
}

using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Main.Youtube;

/// <summary>
/// YouTube 影片列表查詢參數
/// （對應舊版 PHP: Youtube/index_get）
/// </summary>
public class YoutubeListQuery : PagedRequest
{
    /// <summary>關鍵字搜尋（Title / YoutubeVideoId）</summary>
    public string? Keyword { get; set; }

    /// <summary>上下架狀態（"Y" / "N"）</summary>
    public string? Onoff { get; set; }

    /// <summary>頻道 ID</summary>
    public string? ChannelId { get; set; }
}

using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Planning;

/// <summary>
/// YouTube 群組明細列表查詢參數
/// （對應舊版 PHP: Program/group_detail_get）
/// </summary>
public class YoutubeGroupDetailListQuery : PagedRequest
{
    /// <summary>群組 ID</summary>
    public uint? Gid { get; set; }

    /// <summary>關鍵字搜尋（影片 Title）</summary>
    public string? Keyword { get; set; }

    /// <summary>上下架狀態（"Y" / "N"）</summary>
    public string? Onoff { get; set; }
}

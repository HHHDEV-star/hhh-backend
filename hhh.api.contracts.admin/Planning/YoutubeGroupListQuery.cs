using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Planning;

/// <summary>
/// YouTube 群組列表查詢參數
/// （對應舊版 PHP: Program/group_get）
/// </summary>
public class YoutubeGroupListQuery : PagedRequest
{
    /// <summary>關鍵字搜尋（Name）</summary>
    public string? Keyword { get; set; }

    /// <summary>上下架狀態（"Y" / "N"）</summary>
    public string? Onoff { get; set; }
}

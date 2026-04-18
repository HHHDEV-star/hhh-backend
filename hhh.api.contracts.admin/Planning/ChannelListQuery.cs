using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Planning;

/// <summary>
/// 頻道列表查詢參數
/// （對應舊版 PHP: _hprog_chan.php）
/// </summary>
public class ChannelListQuery : PagedRequest
{
    /// <summary>關鍵字搜尋（Cname / CnameS）</summary>
    public string? Keyword { get; set; }

    /// <summary>上下架狀態</summary>
    public short? Onoff { get; set; }
}

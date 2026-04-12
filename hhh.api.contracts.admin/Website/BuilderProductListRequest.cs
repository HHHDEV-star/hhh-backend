using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Website;

/// <summary>建案列表查詢參數</summary>
public class BuilderProductListRequest : PagedRequest
{
    /// <summary>關鍵字搜尋(name, builder title)</summary>
    public string? Q { get; set; }

    /// <summary>依建商 ID 過濾</summary>
    public uint? BuilderId { get; set; }

    /// <summary>依上線狀態過濾</summary>
    public sbyte? Onoff { get; set; }
}

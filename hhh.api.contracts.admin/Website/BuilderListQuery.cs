using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Website;

/// <summary>
/// 建商列表查詢參數
/// </summary>
public class BuilderListQuery : PagedRequest
{
    /// <summary>
    /// 上線狀態篩選(0=關, 1=開)。不帶則不過濾(全部)。
    /// </summary>
    public byte? Onoff { get; set; }

    /// <summary>
    /// 關鍵字搜尋:跨欄位 LIKE(公司名稱 / 電話 / 地址 / Email / 建案名稱)
    /// </summary>
    public string? Keyword { get; set; }
}

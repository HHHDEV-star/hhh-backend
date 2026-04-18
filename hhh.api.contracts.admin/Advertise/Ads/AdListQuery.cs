using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Advertise.Ads;

/// <summary>廣告列表查詢參數</summary>
public class AdListQuery : PagedRequest
{
    /// <summary>廣告類型篩選</summary>
    public string? Type { get; set; }

    /// <summary>關鍵字（模糊比對 Addesc / Keyword / Tabname）</summary>
    public string? Keyword { get; set; }

    /// <summary>上下架狀態（0=下架, 1=上架）</summary>
    public byte? Onoff { get; set; }
}

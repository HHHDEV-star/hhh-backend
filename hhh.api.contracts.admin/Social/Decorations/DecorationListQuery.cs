using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Social.Decorations;

/// <summary>全室裝修收名單列表查詢條件</summary>
public class DecorationListQuery : PagedRequest
{
    /// <summary>模糊比對姓名 / Email / 電話 / 地區</summary>
    public string? Keyword { get; set; }

    /// <summary>房屋類型篩選（預售屋/新屋/中古屋）</summary>
    public string? Type { get; set; }

    /// <summary>建立時間起始日</summary>
    public DateOnly? DateFrom { get; set; }

    /// <summary>建立時間結束日</summary>
    public DateOnly? DateTo { get; set; }
}

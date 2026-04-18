using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Social.Briefs;

/// <summary>名片列表查詢條件</summary>
public class BriefListQuery : PagedRequest
{
    /// <summary>模糊比對姓名 / Email / 電話 / 地區</summary>
    public string? Keyword { get; set; }

    /// <summary>建立時間起始日</summary>
    public DateOnly? DateFrom { get; set; }

    /// <summary>建立時間結束日</summary>
    public DateOnly? DateTo { get; set; }
}

using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Social.Precises;

/// <summary>精準名單列表查詢條件</summary>
public class PreciseListQuery : PagedRequest
{
    /// <summary>模糊比對姓名 / Email / 公司 / 手機</summary>
    public string? Keyword { get; set; }

    /// <summary>身份別篩選（designer/supplier）</summary>
    public string? Identity { get; set; }

    /// <summary>建立時間起始日</summary>
    public DateOnly? DateFrom { get; set; }

    /// <summary>建立時間結束日</summary>
    public DateOnly? DateTo { get; set; }
}

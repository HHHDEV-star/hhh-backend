using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Social.HhhHps;

/// <summary>
/// HHH HP 列表查詢參數
/// （對應舊版 PHP:Calculator/requesthpindex_get → Hp_model::requestget()）
/// </summary>
public class HhhHpListQuery : PagedRequest
{
    /// <summary>起始日期（依 create_time 篩選）</summary>
    public DateOnly? StartDate { get; set; }

    /// <summary>結束日期（依 create_time 篩選）</summary>
    public DateOnly? EndDate { get; set; }

    /// <summary>關鍵字（手機號碼以 09 開頭搜手機欄位,其餘搜姓名/郵件/縣市/區域）</summary>
    public string? Keyword { get; set; }
}

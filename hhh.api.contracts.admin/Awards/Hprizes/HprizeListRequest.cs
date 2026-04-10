using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Awards.Hprizes;

/// <summary>
/// 獎品列表查詢條件(對應舊版 _hprize.php 搜尋 / 分頁參數)。
/// Page / PageSize / Sort / By 由 <see cref="PagedRequest"/> 提供,
/// 排序白名單:id / title / creatTime。
/// </summary>
public class HprizeListRequest : PagedRequest
{
    /// <summary>
    /// 關鍵字搜尋,同時比對 hprize_id / title / desc
    /// </summary>
    public string? Q { get; set; }
}

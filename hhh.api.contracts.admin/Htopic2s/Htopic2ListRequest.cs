using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Htopic2s;

/// <summary>
/// 議題 2 列表查詢條件(對應舊版 _htopic2.php 分頁參數)。
/// 舊 PHP 沒有做關鍵字搜尋,這裡仍保留 Q 方便前端統一。
/// Page / PageSize / Sort / By 由 <see cref="PagedRequest"/> 提供,
/// 排序白名單:id / title / onoff。
/// </summary>
public class Htopic2ListRequest : PagedRequest
{
    /// <summary>關鍵字搜尋,同時比對 id / title / desc</summary>
    public string? Q { get; set; }

    /// <summary>
    /// 上線狀態過濾。null = 全部,true = 只要 Onoff=1,false = 只要 Onoff=0
    /// </summary>
    public bool? Onoff { get; set; }
}

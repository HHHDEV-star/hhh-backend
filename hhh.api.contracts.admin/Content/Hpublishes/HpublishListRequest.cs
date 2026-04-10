using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Content.Hpublishes;

/// <summary>
/// 出版列表查詢條件(對應舊版 _hpublish.php 分頁參數)。
/// 舊 PHP 沒有做關鍵字搜尋,這裡仍保留 Q 方便前端統一。
/// Page / PageSize / Sort / By 由 <see cref="PagedRequest"/> 提供,
/// 排序白名單:id / title / author / type / pdate / viewed / recommend。
/// </summary>
public class HpublishListRequest : PagedRequest
{
    /// <summary>
    /// 關鍵字搜尋,同時比對 hpublish_id / title / author / type / desc
    /// </summary>
    public string? Q { get; set; }

    /// <summary>
    /// 精準過濾書籍類別(_hpublish.type)。為空則不過濾。
    /// </summary>
    public string? Type { get; set; }
}

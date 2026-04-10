using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Designers.Hdesigners;

/// <summary>
/// 設計師列表查詢條件(對應舊版 _hdesigner.php 的搜尋 / 分頁參數)。
/// Page / PageSize / Sort / By 由 <see cref="PagedRequest"/> 提供,
/// 排序白名單:id / title / name / dorder / mobileOrder / createTime / updateTime / onoff。
/// </summary>
public class HdesignerListRequest : PagedRequest
{
    /// <summary>
    /// 關鍵字搜尋,同時比對 hdesigner_id / title / name / mail / website / phone / address
    /// </summary>
    public string? Q { get; set; }

    /// <summary>
    /// 僅以 hdesigner_id 精確比對(對應舊 PHP 的「只查設計師ID」按鈕)。
    /// 預設 false = 走模糊搜尋。
    /// </summary>
    public bool SearchByIdOnly { get; set; }
}

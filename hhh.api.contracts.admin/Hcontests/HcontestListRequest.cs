using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Hcontests;

/// <summary>
/// 競賽報名列表查詢條件(對應舊版 _hcontest.php 搜尋 / 分頁參數)。
/// Page / PageSize / Sort / By 由 <see cref="PagedRequest"/> 提供,
/// 排序白名單:id / year / classType / applytime / finalist / wp。
/// </summary>
public class HcontestListRequest : PagedRequest
{
    /// <summary>
    /// 關鍵字搜尋。舊 PHP 跨欄位:
    /// contest_id / class_type / year / c1(組別2) / c2(報名者) / c3(公司/學校) / c9(作品)
    /// </summary>
    public string? Q { get; set; }

    /// <summary>年份過濾</summary>
    public ushort? Year { get; set; }

    /// <summary>組別1(class_type)過濾</summary>
    public string? ClassType { get; set; }

    /// <summary>是否入圍:null = 全部,true = 只要入圍,false = 只要未入圍</summary>
    public bool? Finalist { get; set; }
}

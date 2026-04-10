using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Hawards;

/// <summary>
/// 得獎記錄列表查詢條件(對應舊版 _hawards.php 搜尋 / 分頁參數)。
/// Page / PageSize / Sort / By 由 <see cref="PagedRequest"/> 提供,
/// 排序白名單:id / awardsName / hdesignerId / hcaseId / onoff。
/// </summary>
public class HawardListRequest : PagedRequest
{
    /// <summary>
    /// 關鍵字搜尋,同時比對 hawards_id / awards_name / hdesigner_id / hcase_id
    /// </summary>
    public string? Q { get; set; }

    /// <summary>設計師 ID 精準篩選(可選)</summary>
    public uint? HdesignerId { get; set; }

    /// <summary>個案 ID 精準篩選(可選)</summary>
    public uint? HcaseId { get; set; }
}

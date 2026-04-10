using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Hcases;

/// <summary>
/// 個案列表查詢參數（對應舊版 _hcase.php）
/// </summary>
public class HcaseListRequest
{
    /// <summary>
    /// 關鍵字：跨欄位 LIKE 搜尋
    /// （hcase_id / caption / location / style / type / 設計師 title / 設計師 name）
    /// </summary>
    public string? Q { get; set; }

    /// <summary>指定設計師 ID（可選，用於只看某位設計師的作品）</summary>
    public uint? HdesignerId { get; set; }

    [Range(1, int.MaxValue)]
    public int Page { get; set; } = 1;

    [Range(1, 100)]
    public int PageSize { get; set; } = 15;

    /// <summary>
    /// 排序欄位（白名單）：id, caption, hdesignerId, viewed, corder,
    /// creatTime, updateTime, onoff, sdate, recommend
    /// </summary>
    public string? Sort { get; set; } = "id";

    /// <summary>ASC / DESC</summary>
    public string? By { get; set; } = "DESC";
}

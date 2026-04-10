using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Hcases;

/// <summary>
/// 批次更新某設計師底下的個案排序（PUT /api/hcases/sort-order）
/// </summary>
/// <remarks>
/// 對應舊版 _hcase_sort.php。
/// 舊 PHP 的 UI 分成兩個拖拉區塊：
///  - 首六區（featured）：corder 存成「位置 - 1000」的負值
///  - 一般區（normal）：corder 一律設為 0
/// 操作完成後還會順便把該設計師的 _hdesigner.update_time 更新為 now
/// （對應 PHP 註解「2024/05/21 為了設計師更新時間」）。
/// </remarks>
public class UpdateHcaseSortOrderRequest
{
    /// <summary>排序所屬的設計師 ID</summary>
    [Required(ErrorMessage = "設計師 ID 必填")]
    public uint HdesignerId { get; set; }

    /// <summary>
    /// 首六區個案（負值 corder）；第 N 個對應 corder = N - 1000。
    /// 可以為空陣列。
    /// </summary>
    public List<HcaseSortItem> Featured { get; set; } = new();

    /// <summary>
    /// 一般區個案（corder = 0）。可以為空陣列。
    /// </summary>
    public List<HcaseSortItem> Normal { get; set; } = new();
}

/// <summary>
/// 單一個案排序項目
/// </summary>
public class HcaseSortItem
{
    /// <summary>個案 ID</summary>
    [Required]
    public uint Id { get; set; }

    /// <summary>
    /// 位置（從 1 開始）。只有 Featured 陣列會使用此欄位計算 corder；
    /// Normal 陣列會忽略此值（一律寫 0）。
    /// </summary>
    public int Position { get; set; }
}

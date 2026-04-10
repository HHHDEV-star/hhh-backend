using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Designers.Hdesigners;

/// <summary>
/// 批次更新設計師排序請求。
/// 同時用於 /api/hdesigners/sort-order（桌機版 dorder）
/// 與 /api/hdesigners/mobile-sort-order（手機版 mobile_order）。
/// </summary>
/// <remarks>
/// 對應舊版 _hdesigner_sort.php / _hdesigner_mobile_sort.php：
/// 前端拖拉後一次送上排好的 { id, order } 陣列。
/// </remarks>
public class UpdateHdesignerSortOrderRequest
{
    /// <summary>排序項目清單；必須至少 1 筆</summary>
    [Required(ErrorMessage = "排序項目不得為空")]
    [MinLength(1, ErrorMessage = "排序項目至少 1 筆")]
    public List<HdesignerSortItem> Items { get; set; } = new();
}

/// <summary>
/// 單一排序項目
/// </summary>
public class HdesignerSortItem
{
    /// <summary>設計師 ID</summary>
    [Required]
    public uint Id { get; set; }

    /// <summary>新的排序值（從 1 開始遞增）</summary>
    [Required]
    [Range(0, int.MaxValue, ErrorMessage = "排序值不得為負")]
    public int Order { get; set; }
}

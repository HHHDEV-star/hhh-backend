using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Admins;

/// <summary>
/// 管理者列表查詢條件（對應舊版 admin.php 的分頁參數）
/// </summary>
public class AdminListRequest
{
    /// <summary>頁碼（從 1 開始）</summary>
    [Range(1, int.MaxValue, ErrorMessage = "頁碼必須大於等於 1")]
    public int Page { get; set; } = 1;

    /// <summary>每頁筆數（1 ~ 100）</summary>
    [Range(1, 100, ErrorMessage = "每頁筆數必須在 1 ~ 100 之間")]
    public int PageSize { get; set; } = 15;

    /// <summary>
    /// 排序欄位。允許值：id / account / name / email / createTime / isActive
    /// 其他值會 fallback 到 id
    /// </summary>
    public string? Sort { get; set; } = "id";

    /// <summary>排序方向：ASC / DESC（大小寫不拘）</summary>
    public string? By { get; set; } = "ASC";
}

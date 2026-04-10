using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Hdesigners;

/// <summary>
/// 設計師列表查詢條件（對應舊版 _hdesigner.php 的搜尋 / 分頁參數）
/// </summary>
public class HdesignerListRequest
{
    /// <summary>
    /// 關鍵字搜尋，同時比對 hdesigner_id / title / name / mail / website / phone / address
    /// </summary>
    public string? Q { get; set; }

    /// <summary>
    /// 僅以 hdesigner_id 精確比對（對應舊 PHP 的「只查設計師ID」按鈕）。
    /// 預設 false = 走模糊搜尋。
    /// </summary>
    public bool SearchByIdOnly { get; set; }

    /// <summary>頁碼（從 1 開始）</summary>
    [Range(1, int.MaxValue, ErrorMessage = "頁碼必須大於等於 1")]
    public int Page { get; set; } = 1;

    /// <summary>每頁筆數（1 ~ 100）</summary>
    [Range(1, 100, ErrorMessage = "每頁筆數必須在 1 ~ 100 之間")]
    public int PageSize { get; set; } = 15;

    /// <summary>
    /// 排序欄位。允許值：id / title / name / dorder / mobileOrder / createTime / updateTime / onoff
    /// 其他值會 fallback 到 id
    /// </summary>
    public string? Sort { get; set; } = "id";

    /// <summary>排序方向：ASC / DESC（大小寫不拘）</summary>
    public string? By { get; set; } = "DESC";
}

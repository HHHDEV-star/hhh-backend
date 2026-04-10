using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Hprizes;

/// <summary>
/// 獎品列表查詢條件（對應舊版 _hprize.php 搜尋 / 分頁參數）
/// </summary>
public class HprizeListRequest
{
    /// <summary>
    /// 關鍵字搜尋，同時比對 hprize_id / title / desc
    /// </summary>
    public string? Q { get; set; }

    /// <summary>頁碼（從 1 開始）</summary>
    [Range(1, int.MaxValue, ErrorMessage = "頁碼必須大於等於 1")]
    public int Page { get; set; } = 1;

    /// <summary>每頁筆數（1 ~ 100）</summary>
    [Range(1, 100, ErrorMessage = "每頁筆數必須在 1 ~ 100 之間")]
    public int PageSize { get; set; } = 15;

    /// <summary>
    /// 排序欄位。允許值：id / title / creatTime
    /// 其他值會 fallback 到 id
    /// </summary>
    public string? Sort { get; set; } = "id";

    /// <summary>排序方向：ASC / DESC（大小寫不拘）</summary>
    public string? By { get; set; } = "DESC";
}

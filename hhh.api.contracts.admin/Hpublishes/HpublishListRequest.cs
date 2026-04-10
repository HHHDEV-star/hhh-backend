using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Hpublishes;

/// <summary>
/// 出版列表查詢條件（對應舊版 _hpublish.php 分頁參數）。
/// 舊 PHP 沒有做關鍵字搜尋，這裡仍保留 Q 方便前端統一。
/// </summary>
public class HpublishListRequest
{
    /// <summary>
    /// 關鍵字搜尋，同時比對 hpublish_id / title / author / type / desc
    /// </summary>
    public string? Q { get; set; }

    /// <summary>
    /// 精準過濾書籍類別（_hpublish.type）。為空則不過濾。
    /// </summary>
    public string? Type { get; set; }

    /// <summary>頁碼（從 1 開始）</summary>
    [Range(1, int.MaxValue, ErrorMessage = "頁碼必須大於等於 1")]
    public int Page { get; set; } = 1;

    /// <summary>每頁筆數（1 ~ 100）</summary>
    [Range(1, 100, ErrorMessage = "每頁筆數必須在 1 ~ 100 之間")]
    public int PageSize { get; set; } = 15;

    /// <summary>
    /// 排序欄位。允許值：id / title / author / type / pdate / viewed / recommend
    /// 其他值會 fallback 到 id
    /// </summary>
    public string? Sort { get; set; } = "id";

    /// <summary>排序方向：ASC / DESC（大小寫不拘）</summary>
    public string? By { get; set; } = "DESC";
}

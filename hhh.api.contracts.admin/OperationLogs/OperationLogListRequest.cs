using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.OperationLogs;

/// <summary>
/// 操作紀錄列表查詢條件（對應舊版 _hoplog.php 搜尋 / 分頁參數）
/// </summary>
public class OperationLogListRequest
{
    /// <summary>
    /// 關鍵字搜尋：同時比對 uname / page_name / opdesc / ip
    /// </summary>
    public string? Q { get; set; }

    /// <summary>操作人員帳號名稱精準篩選（對應 uname 欄位）</summary>
    public string? Uname { get; set; }

    /// <summary>動作分類篩選：新增 / 修改 / 刪除 / 置換</summary>
    public string? Action { get; set; }

    /// <summary>功能頁面名稱篩選</summary>
    public string? PageName { get; set; }

    /// <summary>起始時間（含）</summary>
    public DateTime? From { get; set; }

    /// <summary>結束時間（含）</summary>
    public DateTime? To { get; set; }

    /// <summary>頁碼（從 1 開始）</summary>
    [Range(1, int.MaxValue, ErrorMessage = "頁碼必須大於等於 1")]
    public int Page { get; set; } = 1;

    /// <summary>每頁筆數（1 ~ 100）</summary>
    [Range(1, 100, ErrorMessage = "每頁筆數必須在 1 ~ 100 之間")]
    public int PageSize { get; set; } = 15;

    /// <summary>
    /// 排序欄位。允許值：id / creatTime / uname / pageName / action
    /// 其他值會 fallback 到 creatTime
    /// </summary>
    public string? Sort { get; set; } = "creatTime";

    /// <summary>排序方向:ASC / DESC(大小寫不拘)。預設 DESC,符合「最新在最上面」。</summary>
    public string? By { get; set; } = "DESC";
}

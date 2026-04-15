using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Editorial.Topics;

/// <summary>
/// 主題列表查詢參數
/// （對應舊版 PHP:_htopic.php 列表頁）
/// </summary>
public class HtopicListQuery : PagedRequest
{
    /// <summary>關鍵字搜尋（可輸入 ID、名稱、主題敘述）</summary>
    public string? Q { get; set; }
}

using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Platform.OperationLogs;

/// <summary>
/// 操作紀錄列表查詢條件(對應舊版 _hoplog.php 搜尋 / 分頁參數)。
/// Page / PageSize / Sort / By 由 <see cref="PagedRequest"/> 提供,
/// 排序白名單:id / creatTime / uname / pageName / action,
/// 未指定時 fallback 到 creatTime(最新在最上面)。
/// </summary>
public class OperationLogListRequest : PagedRequest
{
    /// <summary>
    /// 關鍵字搜尋:同時比對 uname / page_name / opdesc / ip
    /// </summary>
    public string? Q { get; set; }

    /// <summary>操作人員帳號名稱精準篩選(對應 uname 欄位)</summary>
    public string? Uname { get; set; }

    /// <summary>動作分類篩選:新增 / 修改 / 刪除 / 置換</summary>
    public string? Action { get; set; }

    /// <summary>功能頁面名稱篩選</summary>
    public string? PageName { get; set; }

    /// <summary>起始時間(含)</summary>
    public DateTime? From { get; set; }

    /// <summary>結束時間(含)</summary>
    public DateTime? To { get; set; }
}

namespace hhh.infrastructure.Logging;

/// <summary>
/// 寫入 _hoplog 的介面，對應舊 PHP 的 `_save_log()`。
/// 所有後台 CUD 服務都應該在成功寫入業務資料後呼叫這個介面，
/// 否則 audit trail 會斷掉。
/// </summary>
public interface IOperationLogWriter
{
    /// <summary>
    /// 寫一筆操作紀錄。
    /// 實作為 best-effort：寫入失敗會 log 但 **不會** 丟回例外，
    /// 以避免 audit 故障影響到主要業務流程（比照舊 PHP 的實作）。
    /// </summary>
    /// <param name="pageName">功能頁面名稱（例如「設計師獲獎」、「案例」），對應 _hoplog.page_name。</param>
    /// <param name="action">動作分類。</param>
    /// <param name="opdesc">人類可讀的操作描述，例如「新增 id=123」或欄位差異。</param>
    /// <param name="sqlcmd">舊欄位（原本存 raw SQL）。.NET 版本通常留空字串即可。</param>
    /// <param name="cancellationToken">取消 token。</param>
    Task WriteAsync(
        string pageName,
        OperationAction action,
        string opdesc,
        string sqlcmd = "",
        CancellationToken cancellationToken = default);
}

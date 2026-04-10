namespace hhh.api.contracts.admin.Platform.OperationLogs;

/// <summary>
/// 操作紀錄列表項目（對應 _hoplog 單列,不含完整 sqlcmd 以避免 payload 過大)
/// </summary>
public class OperationLogListItem
{
    /// <summary>主鍵</summary>
    public uint Id { get; set; }

    /// <summary>操作人員 id</summary>
    public uint Uid { get; set; }

    /// <summary>操作人員顯示名稱(對應舊 PHP $_SESSION["admin"]["name"])</summary>
    public string Uname { get; set; } = string.Empty;

    /// <summary>功能頁面名稱,例如「設計師獲獎」</summary>
    public string? PageName { get; set; }

    /// <summary>動作:新增 / 修改 / 刪除 / 置換</summary>
    public string? Action { get; set; }

    /// <summary>操作描述(人類可讀的 diff 或 id)</summary>
    public string Opdesc { get; set; } = string.Empty;

    /// <summary>客戶端 IP</summary>
    public string? Ip { get; set; }

    /// <summary>建立時間</summary>
    public DateTime CreatTime { get; set; }
}

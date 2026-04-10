namespace hhh.api.contracts.admin.OperationLogs;

/// <summary>
/// 操作紀錄詳情(對應 _hoplog 單列全部欄位,含 sqlcmd)
/// </summary>
public class OperationLogDetailResponse
{
    /// <summary>主鍵</summary>
    public uint Id { get; set; }

    /// <summary>操作人員 id</summary>
    public uint Uid { get; set; }

    /// <summary>操作人員顯示名稱</summary>
    public string Uname { get; set; } = string.Empty;

    /// <summary>功能頁面名稱</summary>
    public string? PageName { get; set; }

    /// <summary>動作:新增 / 修改 / 刪除 / 置換</summary>
    public string? Action { get; set; }

    /// <summary>操作描述</summary>
    public string Opdesc { get; set; } = string.Empty;

    /// <summary>
    /// 舊欄位(原本存 raw SQL)。.NET 版本通常為空字串,
    /// 舊資料才會有內容。保留欄位以便查詢歷史紀錄。
    /// </summary>
    public string Sqlcmd { get; set; } = string.Empty;

    /// <summary>客戶端 IP</summary>
    public string? Ip { get; set; }

    /// <summary>建立時間</summary>
    public DateTime CreatTime { get; set; }
}

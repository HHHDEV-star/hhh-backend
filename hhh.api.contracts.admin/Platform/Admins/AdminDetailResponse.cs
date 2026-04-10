namespace hhh.api.contracts.admin.Platform.Admins;

/// <summary>
/// 管理者詳細資料（對應舊版 admin_edit.php 讀取分支）
/// </summary>
/// <remarks>
/// pwd 欄位永遠不包含在 API 回應中，避免密碼洩漏。
/// allow_page 在 DB 是逗號分隔字串，API 轉成字串陣列。
/// </remarks>
public class AdminDetailResponse
{
    /// <summary>管理者 ID</summary>
    public uint Id { get; set; }

    /// <summary>帳號</summary>
    public string Account { get; set; } = string.Empty;

    /// <summary>名稱</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>E-mail</summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>聯絡電話</summary>
    public string Tel { get; set; } = string.Empty;

    /// <summary>建立時間</summary>
    public DateTime CreateTime { get; set; }

    /// <summary>允許存取的頁面 key 列表（對應 admin.allow_page 欄位）</summary>
    public IReadOnlyList<string> AllowPage { get; set; } = Array.Empty<string>();

    /// <summary>是否啟用</summary>
    public bool IsActive { get; set; }
}

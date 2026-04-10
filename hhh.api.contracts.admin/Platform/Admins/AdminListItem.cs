namespace hhh.api.contracts.admin.Platform.Admins;

/// <summary>
/// 管理者列表單筆項目
/// </summary>
/// <remarks>
/// 對應舊版 admin 資料表，不含 pwd 欄位（密碼永遠不回傳）。
/// </remarks>
public class AdminListItem
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

    /// <summary>是否啟用</summary>
    public bool IsActive { get; set; }
}

namespace hhh.api.contracts.admin.Members.Users;

/// <summary>
/// 會員列表單筆項目
/// </summary>
/// <remarks>
/// 對應舊版 _users 資料表欄位；JSON 名稱改用語意化命名，
/// 與底層欄位 user_intrest / user_from 這類歷史命名脫鉤。
/// </remarks>
public class UserListItem
{
    /// <summary>會員 ID（uid）</summary>
    public uint Id { get; set; }

    /// <summary>帳號（uname）</summary>
    public string Account { get; set; } = string.Empty;

    /// <summary>E-mail</summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>姓名</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>聯絡電話（對應 user_intrest 欄位）</summary>
    public string Tel { get; set; } = string.Empty;

    /// <summary>地址（對應 user_from 欄位）</summary>
    public string Address { get; set; } = string.Empty;
}

using System;

namespace hhh.api.contracts.admin.Members.Users;

/// <summary>
/// 單一會員詳細資料（對應舊版 _users_edit.php 的 GET 模式）
/// </summary>
/// <remarks>
/// 只回傳後台編輯頁面會用到的欄位，不暴露 pass、token 等敏感資訊。
/// </remarks>
public class UserDetailResponse
{
    /// <summary>會員 ID（uid）</summary>
    public uint Id { get; set; }

    /// <summary>帳號（uname，建立後不可修改）</summary>
    public string Account { get; set; } = string.Empty;

    /// <summary>姓名</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>E-mail</summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>聯絡電話（對應 user_intrest 欄位）</summary>
    public string Tel { get; set; } = string.Empty;

    /// <summary>地址（對應 user_from 欄位）</summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>註冊時間（對應 user_regdate_datetime 欄位）</summary>
    public DateTime? RegisteredAt { get; set; }
}

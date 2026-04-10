using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Users;

/// <summary>
/// 更新會員請求（PUT /api/users/{id}）
/// </summary>
/// <remarks>
/// 對應舊版 _users_edit.php 的更新分支。
/// 帳號（uname）不可修改，與原 PHP 顯示為唯讀一致。
/// 密碼為選填：空字串或 null 代表不更新密碼，只有非空才會覆寫 pass 欄位。
/// （原 PHP 作者 `$exclude_keyword` 寫成 `pwd` 但表單 input 是 `pass`，導致空密碼會誤蓋原密碼，這裡實作作者的原始意圖。）
/// </remarks>
public class UpdateUserRequest
{
    /// <summary>姓名</summary>
    [Required(ErrorMessage = "姓名必填")]
    [StringLength(60)]
    public string Name { get; set; } = string.Empty;

    /// <summary>密碼（選填；空字串或 null 代表維持原密碼）</summary>
    [StringLength(32, ErrorMessage = "密碼長度不得超過 32")]
    public string? Password { get; set; }

    /// <summary>E-mail</summary>
    [Required(ErrorMessage = "E-mail 必填")]
    [EmailAddress(ErrorMessage = "E-mail 格式錯誤")]
    [StringLength(128)]
    public string Email { get; set; } = string.Empty;

    /// <summary>聯絡電話（對應 user_intrest 欄位）</summary>
    [StringLength(150)]
    public string? Tel { get; set; }

    /// <summary>地址（對應 user_from 欄位）</summary>
    [StringLength(100)]
    public string? Address { get; set; }
}

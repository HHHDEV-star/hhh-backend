using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Users;

/// <summary>
/// 新增會員請求（POST /api/users）
/// </summary>
/// <remarks>
/// 對應舊版 _users_edit.php 的新增分支。原 PHP 表單沒有 uname 欄位、實際新增會失敗；
/// 本 API 補上 Account (uname) 欄位，要求前端明確提供。
/// </remarks>
public class CreateUserRequest
{
    /// <summary>帳號（對應 uname，唯一）</summary>
    [Required(ErrorMessage = "帳號必填")]
    [StringLength(128, MinimumLength = 1, ErrorMessage = "帳號長度不得超過 128")]
    public string Account { get; set; } = string.Empty;

    /// <summary>密碼（對應 pass 欄位，明文儲存以相容舊系統）</summary>
    [Required(ErrorMessage = "密碼必填")]
    [StringLength(32, MinimumLength = 1, ErrorMessage = "密碼長度不得超過 32")]
    public string Password { get; set; } = string.Empty;

    /// <summary>姓名</summary>
    [Required(ErrorMessage = "姓名必填")]
    [StringLength(60)]
    public string Name { get; set; } = string.Empty;

    /// <summary>E-mail（唯一）</summary>
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

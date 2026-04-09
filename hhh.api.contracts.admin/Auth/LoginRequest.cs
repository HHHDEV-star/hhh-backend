using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Auth;

/// <summary>
/// 管理後台登入請求
/// </summary>
public class LoginRequest
{
    /// <summary>帳號</summary>
    [Required(ErrorMessage = "帳號為必填")]
    [StringLength(20, ErrorMessage = "帳號長度不可超過 20 字")]
    public string Account { get; set; } = null!;

    /// <summary>密碼</summary>
    [Required(ErrorMessage = "密碼為必填")]
    [StringLength(40, ErrorMessage = "密碼長度不可超過 40 字")]
    public string Pwd { get; set; } = null!;
}

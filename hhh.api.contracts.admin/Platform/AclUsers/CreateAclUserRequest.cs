using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Platform.AclUsers;

/// <summary>
/// 新增後台帳號(ACL)
/// （對應舊版 PHP:System.php → user_create → Acl_lib::create_user）
/// </summary>
public class CreateAclUserRequest
{
    /// <summary>名稱</summary>
    [Required(ErrorMessage = "name 不得為空")]
    [StringLength(45)]
    public string Name { get; set; } = null!;

    /// <summary>帳號</summary>
    [Required(ErrorMessage = "account 不得為空")]
    [StringLength(45)]
    public string Account { get; set; } = null!;

    /// <summary>電子郵件</summary>
    [StringLength(50)]
    public string? Email { get; set; }

    /// <summary>密碼（明文,後端 MD5）</summary>
    [Required(ErrorMessage = "pwd 不得為空")]
    [MinLength(7, ErrorMessage = "密碼必須大於 6 字元")]
    public string Pwd { get; set; } = null!;

    /// <summary>是否刪除（0/1）</summary>
    [Required]
    [RegularExpression("^[01]$", ErrorMessage = "is_del 僅接受 0 或 1")]
    public string IsDel { get; set; } = "0";

    /// <summary>是否可遠端（0/1）</summary>
    [Required]
    [RegularExpression("^[01]$", ErrorMessage = "is_remote 僅接受 0 或 1")]
    public string IsRemote { get; set; } = "0";

    /// <summary>是否為業務（0/1）</summary>
    [Required]
    [RegularExpression("^[01]$", ErrorMessage = "is_execute_sales 僅接受 0 或 1")]
    public string IsExecuteSales { get; set; } = "0";

    /// <summary>部門（職位）</summary>
    [StringLength(50)]
    public string? Position { get; set; }
}

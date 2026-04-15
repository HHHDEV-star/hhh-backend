using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Platform.AclUsers;

/// <summary>
/// 編輯後台帳號(ACL)
/// （對應舊版 PHP:System.php → user_update → Acl_lib::update_user）
/// 密碼選填：空或 null 表示不更新密碼。account 不可修改。
/// </summary>
public class UpdateAclUserRequest
{
    /// <summary>名稱</summary>
    [Required(ErrorMessage = "name 不得為空")]
    [StringLength(45)]
    public string Name { get; set; } = null!;

    /// <summary>電子郵件</summary>
    [StringLength(50)]
    public string? Email { get; set; }

    /// <summary>密碼（明文,後端 MD5）。空或 null 表示不更新。</summary>
    public string? Pwd { get; set; }

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

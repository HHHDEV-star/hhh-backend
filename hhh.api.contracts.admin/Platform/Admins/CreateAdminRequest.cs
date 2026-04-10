using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Platform.Admins;

/// <summary>
/// 新增管理者請求（POST /api/admins）
/// </summary>
/// <remarks>
/// 對應舊版 admin_edit.php 的新增分支。
/// 密碼沿用舊系統為明文儲存（admin.pwd varchar(40)）；改用 BCrypt 需同步調整 AuthService。
/// </remarks>
public class CreateAdminRequest
{
    /// <summary>帳號（唯一）</summary>
    [Required(ErrorMessage = "帳號必填")]
    [StringLength(20, MinimumLength = 1, ErrorMessage = "帳號長度不得超過 20")]
    public string Account { get; set; } = string.Empty;

    /// <summary>密碼（必填，明文儲存以相容舊系統）</summary>
    [Required(ErrorMessage = "密碼必填")]
    [StringLength(40, MinimumLength = 1, ErrorMessage = "密碼長度不得超過 40")]
    public string Pwd { get; set; } = string.Empty;

    /// <summary>名稱</summary>
    [StringLength(40)]
    public string? Name { get; set; }

    /// <summary>E-mail</summary>
    [EmailAddress(ErrorMessage = "E-mail 格式錯誤")]
    [StringLength(200)]
    public string? Email { get; set; }

    /// <summary>聯絡電話</summary>
    [StringLength(20)]
    public string? Tel { get; set; }

    /// <summary>
    /// 允許存取的頁面 key 列表；儲存時會以逗號分隔寫入 admin.allow_page 欄位。
    /// 對應舊版表單的 allow_page[] checkbox 群組。
    /// </summary>
    public IReadOnlyList<string>? AllowPage { get; set; }

    /// <summary>是否啟用（對應 admin.is_active，true = 1、false = 0）</summary>
    public bool IsActive { get; set; } = true;
}

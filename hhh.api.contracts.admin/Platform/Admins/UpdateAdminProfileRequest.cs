using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Platform.Admins;

/// <summary>
/// 更新自己的管理者 profile（PUT /api/admins/me）
/// </summary>
/// <remarks>
/// 對應舊版 admin_password.php：已登入管理者修改自己的 name / email / tel / pwd。
/// 帳號、allow_page、is_active 不在此端點修改（避免本人自己停權 / 自己改權限）。
/// 密碼為選填：空字串或 null 代表維持原密碼。
/// </remarks>
public class UpdateAdminProfileRequest
{
    /// <summary>密碼（選填；空字串或 null 代表維持原密碼）</summary>
    [StringLength(40, ErrorMessage = "密碼長度不得超過 40")]
    public string? Pwd { get; set; }

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
}

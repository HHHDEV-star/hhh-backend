using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Platform.Admins;

/// <summary>
/// 更新管理者請求（PUT /api/admins/{id}）
/// </summary>
/// <remarks>
/// 對應舊版 admin_edit.php 的更新分支。
/// 帳號（account）不可修改，與原表單顯示一致。
/// 密碼為選填：空字串或 null 代表不更新密碼，只有非空才會覆寫 pwd 欄位
/// （對應原 PHP 的 $exclude_keyword 排除 pwd 行為）。
/// </remarks>
public class UpdateAdminRequest
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

    /// <summary>允許存取的頁面 key 列表（儲存為逗號分隔字串）</summary>
    public IReadOnlyList<string>? AllowPage { get; set; }

    /// <summary>是否啟用</summary>
    public bool IsActive { get; set; }
}

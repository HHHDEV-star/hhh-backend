using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Platform.AclMenuGroups;

/// <summary>
/// 編輯 ACL 目錄群組
/// （對應舊版 PHP:System.php → menu_path_group_update → Acl_lib::update_menu_path_group）
/// </summary>
public class UpdateAclMenuGroupRequest
{
    /// <summary>顯示圖標</summary>
    [StringLength(50)]
    public string? Icon { get; set; }

    /// <summary>目錄群組名稱</summary>
    [Required(ErrorMessage = "name 不得為空")]
    [StringLength(45)]
    public string Name { get; set; } = null!;

    /// <summary>群組排序</summary>
    [Required(ErrorMessage = "sort_num 不得為空")]
    public int SortNum { get; set; }

    /// <summary>是否顯示在目錄（0/1）</summary>
    [Required]
    [RegularExpression("^[01]$", ErrorMessage = "is_show 僅接受 0 或 1")]
    public string IsShow { get; set; } = "1";
}

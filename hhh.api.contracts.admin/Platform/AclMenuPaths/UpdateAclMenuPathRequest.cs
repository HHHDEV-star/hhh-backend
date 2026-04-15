using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Platform.AclMenuPaths;

/// <summary>
/// 編輯 ACL 目錄功能
/// （對應舊版 PHP:System.php → menu_path_update → Acl_lib::update_menu_path）
/// </summary>
public class UpdateAclMenuPathRequest
{
    /// <summary>所屬群組編號</summary>
    [Required(ErrorMessage = "menu_group_id 不得為空")]
    public int MenuGroupId { get; set; }

    /// <summary>所屬專案編號</summary>
    [Required(ErrorMessage = "project_id 不得為空")]
    public int ProjectId { get; set; }

    /// <summary>功能名稱</summary>
    [Required(ErrorMessage = "name 不得為空")]
    [StringLength(45)]
    public string Name { get; set; } = null!;

    /// <summary>功能路徑</summary>
    [Required(ErrorMessage = "path 不得為空")]
    [StringLength(100)]
    public string Path { get; set; } = null!;

    /// <summary>功能排序</summary>
    [Required(ErrorMessage = "sort_num 不得為空")]
    public int SortNum { get; set; }

    /// <summary>是否顯示在目錄（0/1）</summary>
    [Required]
    [RegularExpression("^[01]$", ErrorMessage = "is_show 僅接受 0 或 1")]
    public string IsShow { get; set; } = "1";
}

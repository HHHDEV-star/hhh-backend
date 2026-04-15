namespace hhh.api.contracts.admin.Platform.AclMenuPaths;

/// <summary>
/// ACL 目錄功能列表項目
/// （對應舊版 PHP:System.php → pure() → menus_path_list
///             → Acl_lib::get_menus_path()）
/// 資料來源：hhh_backstage.acl_menu_path
/// </summary>
public class AclMenuPathListItem
{
    /// <summary>主鍵</summary>
    public int Id { get; set; }

    /// <summary>所屬群組編號</summary>
    public int MenuGroupId { get; set; }

    /// <summary>所屬專案編號</summary>
    public int ProjectId { get; set; }

    /// <summary>功能名稱</summary>
    public string? Name { get; set; }

    /// <summary>功能路徑</summary>
    public string? Path { get; set; }

    /// <summary>功能排序</summary>
    public int? SortNum { get; set; }

    /// <summary>是否顯示在目錄（0=否,1=是）</summary>
    public string IsShow { get; set; } = null!;

    /// <summary>建立時間</summary>
    public DateTime? CreateDate { get; set; }

    /// <summary>編輯時間</summary>
    public DateTime? UpdateDate { get; set; }
}

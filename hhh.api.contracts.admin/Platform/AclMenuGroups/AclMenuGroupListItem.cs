namespace hhh.api.contracts.admin.Platform.AclMenuGroups;

/// <summary>
/// ACL 目錄群組列表項目
/// （對應舊版 PHP:System.php → pure() → menu_path_group_list
///             → Acl_lib::get_menu_path_group()）
/// 資料來源：hhh_backstage.acl_menu_group
/// </summary>
public class AclMenuGroupListItem
{
    /// <summary>主鍵</summary>
    public int Id { get; set; }

    /// <summary>顯示圖標</summary>
    public string? Icon { get; set; }

    /// <summary>目錄群組名稱</summary>
    public string? Name { get; set; }

    /// <summary>群組排序</summary>
    public int? SortNum { get; set; }

    /// <summary>是否顯示在目錄（0=否,1=是）</summary>
    public string IsShow { get; set; } = null!;

    /// <summary>建立時間</summary>
    public DateTime? CreateDate { get; set; }

    /// <summary>編輯時間</summary>
    public DateTime? UpdateDate { get; set; }
}

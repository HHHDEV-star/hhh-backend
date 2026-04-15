namespace hhh.api.contracts.admin.Platform.AclUsers;

/// <summary>
/// 後台帳號(ACL)列表項目
/// （對應舊版 PHP:hhh-backstage/event/system_users.php Kendo Grid 欄位）
/// 資料來源：hhh_backstage.acl_users
/// </summary>
public class AclUserListItem
{
    /// <summary>編號</summary>
    public int Id { get; set; }

    /// <summary>名稱</summary>
    public string? Name { get; set; }

    /// <summary>帳號</summary>
    public string Account { get; set; } = null!;

    /// <summary>電子郵件</summary>
    public string Email { get; set; } = null!;

    /// <summary>部門（職位）</summary>
    public string? Position { get; set; }

    /// <summary>是否刪除（0=否,1=是）</summary>
    public string IsDel { get; set; } = null!;

    /// <summary>是否可遠端（0=否,1=可）</summary>
    public string IsRemote { get; set; } = null!;

    /// <summary>是否為業務（0=否,1=是）</summary>
    public string IsExecuteSales { get; set; } = null!;

    /// <summary>建立時間</summary>
    public DateTime? CreateDate { get; set; }

    /// <summary>編輯時間</summary>
    public DateTime? UpdateDate { get; set; }
}

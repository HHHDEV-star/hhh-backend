namespace hhh.api.contracts.admin.Website.Contacts;

/// <summary>
/// 聯絡我們列表項目
/// (對應舊版 Contact/index_get → contact_model::get)
/// </summary>
public class ContactListItem
{
    public uint Id { get; set; }

    /// <summary>建立時間</summary>
    public DateTime CreateTime { get; set; }

    /// <summary>姓名</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>公司名稱</summary>
    public string Company { get; set; } = string.Empty;

    /// <summary>電話</summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>電子郵件</summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>是否為 FB 帳號(Y/N)</summary>
    public string IsFb { get; set; } = string.Empty;

    /// <summary>主旨</summary>
    public string Subject { get; set; } = string.Empty;

    /// <summary>內容</summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>IP 位置</summary>
    public string Ip { get; set; } = string.Empty;

    /// <summary>有無發送(Y/N)</summary>
    public string Send { get; set; } = string.Empty;

    /// <summary>發送時間</summary>
    public DateTime SendTime { get; set; }
}

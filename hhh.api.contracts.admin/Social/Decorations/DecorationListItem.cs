namespace hhh.api.contracts.admin.Social.Decorations;

/// <summary>
/// 全室裝修收名單 列表項目
/// (對應舊版 hhh-api/.../third/v1/Decoration.php → lists_get → decoration_model::lists()
///  SELECT * FROM decoration ORDER BY id DESC)
/// </summary>
public class DecorationListItem
{
    public uint Id { get; set; }
    /// <summary>電子郵件</summary>
    public string Email { get; set; } = string.Empty;
    /// <summary>聯絡姓名</summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>聯絡電話</summary>
    public string Phone { get; set; } = string.Empty;
    /// <summary>所在地區</summary>
    public string Area { get; set; } = string.Empty;
    /// <summary>房屋類型(預售屋/新屋/中古屋)</summary>
    public string Type { get; set; } = string.Empty;
    /// <summary>房屋實際坪數</summary>
    public string Pin { get; set; } = string.Empty;
    /// <summary>建立時間</summary>
    public DateTime CreateTime { get; set; }
}

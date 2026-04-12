namespace hhh.api.contracts.admin.Social.Briefs;

/// <summary>
/// 屋主上傳名片領好康 列表項目
/// (對應舊版 hhh-api/.../third/v1/Events.php → brief_get → events_model::brief_lists()
///  SELECT * FROM brief ORDER BY brief_id DESC)
/// </summary>
public class BriefListItem
{
    public int BriefId { get; set; }
    /// <summary>姓名</summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>電子郵件</summary>
    public string Email { get; set; } = string.Empty;
    /// <summary>電話</summary>
    public string Phone { get; set; } = string.Empty;
    /// <summary>地區</summary>
    public string Area { get; set; } = string.Empty;
    /// <summary>名片圖檔 URL</summary>
    public string? Image { get; set; }
    /// <summary>實際坪數</summary>
    public string Pin { get; set; } = string.Empty;
    /// <summary>預算</summary>
    public string Fee { get; set; } = string.Empty;
    /// <summary>建立時間</summary>
    public DateTime CreateTime { get; set; }
}

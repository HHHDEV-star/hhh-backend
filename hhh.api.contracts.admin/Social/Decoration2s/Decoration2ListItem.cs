using System.Text.Json.Serialization;

namespace hhh.api.contracts.admin.Social.Decoration2s;

/// <summary>
/// 全室裝修收名單列表項目
/// （對應舊版 PHP:hhh-backstage/event/decoration2.php Kendo Grid 欄位）
/// 資料來源為外部 API（q.ptt.cx/v_lst），非本地資料表
/// </summary>
public class Decoration2ListItem
{
    /// <summary>ID</summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }

    /// <summary>電子郵件</summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }

    /// <summary>姓名</summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>電話</summary>
    [JsonPropertyName("phone")]
    public string? Phone { get; set; }

    /// <summary>地區</summary>
    [JsonPropertyName("loc")]
    public string? Loc { get; set; }

    /// <summary>房屋類型</summary>
    [JsonPropertyName("h_class")]
    public string? HClass { get; set; }

    /// <summary>實際坪數</summary>
    [JsonPropertyName("size")]
    public string? Size { get; set; }

    /// <summary>性別</summary>
    [JsonPropertyName("gender")]
    public string? Gender { get; set; }

    /// <summary>房廳衛</summary>
    [JsonPropertyName("room")]
    public string? Room { get; set; }

    /// <summary>風格</summary>
    [JsonPropertyName("style")]
    public string? Style { get; set; }

    /// <summary>預選裝修日期</summary>
    [JsonPropertyName("prefer_date")]
    public string? PreferDate { get; set; }

    /// <summary>版本</summary>
    [JsonPropertyName("version")]
    public string? Version { get; set; }

    /// <summary>建立時間</summary>
    [JsonPropertyName("create_time")]
    public string? CreateTime { get; set; }
}

namespace hhh.api.contracts.admin.Rss;

/// <summary>
/// RSS 排程關聯項目（專欄或個案的精簡資訊）
/// </summary>
public class RssScheduleRefItem
{
    /// <summary>ID（hcolumn_id 或 hcase_id）</summary>
    public uint Id { get; set; }

    /// <summary>標題（專欄 ctitle 或個案 caption）</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>封面圖（專欄 clogo 或個案 cover）</summary>
    public string Cover { get; set; } = string.Empty;

    /// <summary>上線狀態</summary>
    public bool Onoff { get; set; }
}

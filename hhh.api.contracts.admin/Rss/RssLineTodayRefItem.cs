namespace hhh.api.contracts.admin.Rss;

/// <summary>
/// RSS LineToday 排程關聯影音項目
/// </summary>
public class RssLineTodayRefItem
{
    /// <summary>影音 ID（hvideo_id）</summary>
    public uint Id { get; set; }

    /// <summary>影音標題</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>影音名稱</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>關聯設計師 ID</summary>
    public uint HdesignerId { get; set; }

    /// <summary>上線狀態</summary>
    public bool Onoff { get; set; }
}

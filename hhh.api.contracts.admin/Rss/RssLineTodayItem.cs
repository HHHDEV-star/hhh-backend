namespace hhh.api.contracts.admin.Rss;

/// <summary>
/// RSS LineToday 排程項目
/// (跟 Yahoo/MSN 不同:用 hvideo 取代 hcolumn + hcase)
/// </summary>
public class RssLineTodayItem
{
    public uint Id { get; set; }

    /// <summary>推送日期</summary>
    public DateOnly? Date { get; set; }

    /// <summary>影音編號(逗號分隔，原始值)</summary>
    public string? Hvideo { get; set; }

    /// <summary>影音明細列表（由 Hvideo 展開，帶標題/名稱/設計師/上線狀態）</summary>
    public List<RssLineTodayRefItem> Videos { get; set; } = [];

    /// <summary>建立時間</summary>
    public DateTime? CreateTime { get; set; }

    /// <summary>修改時間</summary>
    public DateTime? UpdateTime { get; set; }
}

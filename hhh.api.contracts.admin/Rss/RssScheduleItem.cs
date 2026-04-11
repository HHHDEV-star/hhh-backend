namespace hhh.api.contracts.admin.Rss;

/// <summary>
/// RSS 排程項目(共用於 Yahoo / MSN,兩者 schema 完全相同)
/// </summary>
public class RssScheduleItem
{
    public uint Id { get; set; }

    /// <summary>推送日期</summary>
    public DateOnly? Date { get; set; }

    /// <summary>專欄編號(逗號分隔)</summary>
    public string? Hcolumn { get; set; }

    /// <summary>個案編號(逗號分隔)</summary>
    public string? Hcase { get; set; }

    /// <summary>建立時間</summary>
    public DateTime? CreateTime { get; set; }

    /// <summary>修改時間</summary>
    public DateTime? UpdateTime { get; set; }
}

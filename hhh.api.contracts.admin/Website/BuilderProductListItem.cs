namespace hhh.api.contracts.admin.Website;

/// <summary>建案列表單筆資料</summary>
public class BuilderProductListItem
{
    public uint Id { get; set; }
    public uint BuilderId { get; set; }

    /// <summary>建商名稱（JOIN builder.title）</summary>
    public string BuilderTitle { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
    public string? Types { get; set; }
    public string City { get; set; } = string.Empty;
    public sbyte Onoff { get; set; }
    public DateTime UpdateTime { get; set; }
    public string Cover { get; set; } = string.Empty;

    /// <summary>關聯的專欄 ID 列表（對應 PHP GROUP_CONCAT）</summary>
    public List<uint> ColumnIds { get; set; } = new();

    /// <summary>關聯的影片 ID 列表（對應 PHP GROUP_CONCAT）</summary>
    public List<uint> VideoIds { get; set; } = new();
}

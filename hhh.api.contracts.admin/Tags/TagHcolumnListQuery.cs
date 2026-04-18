using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Tags;

/// <summary>專欄標籤列表查詢參數</summary>
public class TagHcolumnListQuery : PagedRequest
{
    /// <summary>專欄類型</summary>
    public string? Ctype { get; set; }

    /// <summary>專欄標題（模糊比對）</summary>
    public string? Ctitle { get; set; }

    /// <summary>建立日期起始</summary>
    public DateOnly? StartDate { get; set; }

    /// <summary>建立日期結束</summary>
    public DateOnly? EndDate { get; set; }

    /// <summary>標籤關鍵字（模糊比對 tag）</summary>
    public string? SearchTag { get; set; }
}

using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Tags;

/// <summary>影音標籤列表查詢參數</summary>
public class TagHvideoListQuery : PagedRequest
{
    /// <summary>設計師 ID</summary>
    public uint? HdesignerId { get; set; }

    /// <summary>影音標題（模糊比對）</summary>
    public string? Title { get; set; }

    /// <summary>建立日期起始</summary>
    public DateOnly? StartDate { get; set; }

    /// <summary>建立日期結束</summary>
    public DateOnly? EndDate { get; set; }

    /// <summary>標籤關鍵字（模糊比對 tag）</summary>
    public string? SearchTag { get; set; }
}

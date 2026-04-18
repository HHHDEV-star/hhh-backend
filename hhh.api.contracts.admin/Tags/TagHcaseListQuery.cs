using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Tags;

/// <summary>個案標籤列表查詢參數</summary>
public class TagHcaseListQuery : PagedRequest
{
    /// <summary>設計師 ID</summary>
    public uint? HdesignerId { get; set; }

    /// <summary>標籤關鍵字（模糊比對 tag）</summary>
    public string? SearchTag { get; set; }
}

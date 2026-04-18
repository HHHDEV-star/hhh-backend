using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Tags;

/// <summary>圖庫標籤列表查詢參數</summary>
public class TagImageListQuery : PagedRequest
{
    /// <summary>個案 ID</summary>
    public uint? HcaseId { get; set; }

    /// <summary>標籤關鍵字（模糊比對 tag1-tag5）</summary>
    public string? SearchTag { get; set; }
}

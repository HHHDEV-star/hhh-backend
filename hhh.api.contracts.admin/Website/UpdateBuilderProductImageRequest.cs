using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Website;

/// <summary>更新建案圖片請求</summary>
public class UpdateBuilderProductImageRequest
{
    /// <summary>圖片標題</summary>
    [StringLength(64)]
    public string? Title { get; set; }

    /// <summary>圖片描述</summary>
    [StringLength(128)]
    public string? Descr { get; set; }

    /// <summary>排序</summary>
    public byte OrderNo { get; set; }
}

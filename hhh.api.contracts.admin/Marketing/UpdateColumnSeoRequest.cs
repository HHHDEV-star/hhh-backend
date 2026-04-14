using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Marketing;

/// <summary>批次更新專欄 SEO 單筆項目</summary>
public class UpdateColumnSeoItem
{
    /// <summary>專欄 ID</summary>
    [Required]
    public uint HcolumnId { get; set; }

    /// <summary>SEO 標題</summary>
    [StringLength(128)]
    public string? SeoTitle { get; set; }

    /// <summary>SEO 描述</summary>
    public string? SeoDescription { get; set; }
}

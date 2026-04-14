using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Marketing;

/// <summary>批次更新個案 SEO 單筆項目</summary>
public class UpdateCaseSeoItem
{
    /// <summary>個案 ID</summary>
    [Required]
    public uint HcaseId { get; set; }

    /// <summary>SEO 標題</summary>
    [StringLength(128)]
    public string? SeoTitle { get; set; }

    /// <summary>SEO 描述</summary>
    public string? SeoDescription { get; set; }
}

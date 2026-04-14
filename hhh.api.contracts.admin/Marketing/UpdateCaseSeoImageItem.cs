using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Marketing;

/// <summary>批次更新個案 SEO 圖片單筆項目</summary>
public class UpdateCaseSeoImageItem
{
    /// <summary>個案 ID</summary>
    [Required]
    public uint HcaseId { get; set; }

    /// <summary>SEO 圖片 URL</summary>
    [Required]
    [StringLength(128)]
    public string SeoImage { get; set; } = string.Empty;
}

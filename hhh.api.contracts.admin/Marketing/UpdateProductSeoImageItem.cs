using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Marketing;

/// <summary>批次更新產品 SEO 圖片單筆項目</summary>
public class UpdateProductSeoImageItem
{
    /// <summary>產品 ID</summary>
    [Required]
    public uint Id { get; set; }

    /// <summary>SEO 圖片 URL</summary>
    [Required]
    [StringLength(128)]
    public string SeoImage { get; set; } = string.Empty;
}

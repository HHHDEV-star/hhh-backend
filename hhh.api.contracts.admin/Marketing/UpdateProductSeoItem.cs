using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Marketing;

/// <summary>批次更新產品 SEO 標題單筆項目</summary>
public class UpdateProductSeoItem
{
    /// <summary>產品 ID</summary>
    [Required]
    public uint Id { get; set; }

    /// <summary>SEO 標題</summary>
    [StringLength(128)]
    public string? SeoTitle { get; set; }
}

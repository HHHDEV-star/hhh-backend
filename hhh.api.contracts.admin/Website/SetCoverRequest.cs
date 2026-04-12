using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Website;

/// <summary>設定建案封面請求</summary>
public class SetCoverRequest
{
    /// <summary>要設為封面的圖片 ID</summary>
    [Required]
    public uint ImageId { get; set; }

    /// <summary>圖片的 name 欄位值（用於更新 builder_product.cover）</summary>
    [Required]
    [StringLength(200)]
    public string Cover { get; set; } = string.Empty;
}

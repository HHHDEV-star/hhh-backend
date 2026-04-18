using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Editorial.Cases;

/// <summary>
/// 設定個案封面圖（對應舊版 Cases/image PUT cover）
/// </summary>
public class SetCaseCoverRequest
{
    /// <summary>圖片 ID</summary>
    [Required]
    public uint HcaseImgId { get; set; }
}

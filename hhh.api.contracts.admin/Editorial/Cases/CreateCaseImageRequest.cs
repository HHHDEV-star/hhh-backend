using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Editorial.Cases;

/// <summary>
/// 新增個案圖片（對應舊版 Cases/image POST）
/// </summary>
public class CreateCaseImageRequest
{
    /// <summary>圖片 URL（上傳完成後的路徑）</summary>
    [Required]
    [StringLength(128)]
    public string Name { get; set; } = string.Empty;

    /// <summary>標題（浮水印標題）</summary>
    [StringLength(64)]
    public string? Title { get; set; }
}

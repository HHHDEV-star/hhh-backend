using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Website;

/// <summary>新增建案圖片請求</summary>
public class CreateBuilderProductImageRequest
{
    /// <summary>圖片檔名</summary>
    [Required(ErrorMessage = "圖片名稱必填")]
    [StringLength(128)]
    public string Name { get; set; } = string.Empty;

    /// <summary>排序</summary>
    public byte OrderNo { get; set; }
}

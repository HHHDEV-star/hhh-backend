using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.WebSite.DecoImages;

/// <summary>更新查證照圖片審核狀態請求(id 走 URL)</summary>
public class UpdateDecoImageOnoffRequest
{
    /// <summary>審核狀態(true=通過 / false=不通過)</summary>
    [Required]
    public bool Onoff { get; set; }
}

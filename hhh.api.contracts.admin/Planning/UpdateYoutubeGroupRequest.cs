using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Planning;

/// <summary>更新 YouTube 群組</summary>
public class UpdateYoutubeGroupRequest
{
    /// <summary>群組名稱(代號)</summary>
    [Required(ErrorMessage = "群組名稱為必填")]
    [StringLength(50, ErrorMessage = "群組名稱最多 50 字")]
    public string Name { get; set; } = string.Empty;

    /// <summary>是否啟用 (Y/N)</summary>
    [Required(ErrorMessage = "啟用狀態為必填")]
    [RegularExpression("^[YN]$", ErrorMessage = "啟用狀態僅接受 Y 或 N")]
    public string Onoff { get; set; } = string.Empty;
}

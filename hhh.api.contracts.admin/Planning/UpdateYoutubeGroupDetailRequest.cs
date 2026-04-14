using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Planning;

/// <summary>更新 YouTube 群組明細（排序 + 開關）</summary>
public class UpdateYoutubeGroupDetailRequest
{
    /// <summary>排序（最小值 1）</summary>
    [Required(ErrorMessage = "排序為必填")]
    [Range(1, 255, ErrorMessage = "排序值須介於 1 ~ 255")]
    public byte Sort { get; set; }

    /// <summary>是否開啟 (Y/N)</summary>
    [Required(ErrorMessage = "開關狀態為必填")]
    [RegularExpression("^[YN]$", ErrorMessage = "開關狀態僅接受 Y 或 N")]
    public string Onoff { get; set; } = string.Empty;
}

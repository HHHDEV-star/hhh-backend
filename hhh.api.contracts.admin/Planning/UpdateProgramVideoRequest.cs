using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Planning;

/// <summary>更新節目影片（排序、開關、頻道、播放日期、上架日期）</summary>
public class UpdateProgramVideoRequest
{
    /// <summary>頻道名稱</summary>
    [Required(ErrorMessage = "頻道名稱為必填")]
    [StringLength(20)]
    public string ChanName { get; set; } = string.Empty;

    /// <summary>播放日期</summary>
    [Required(ErrorMessage = "播放日期為必填")]
    public DateOnly DisplayDate { get; set; }

    /// <summary>上架日期時間</summary>
    [Required(ErrorMessage = "上架日期為必填")]
    public DateTime DisplayDatetime { get; set; }

    /// <summary>是否開啟 (Y/N)</summary>
    [Required(ErrorMessage = "開關狀態為必填")]
    [RegularExpression("^[YN]$", ErrorMessage = "開關狀態僅接受 Y 或 N")]
    public string Onoff { get; set; } = string.Empty;

    /// <summary>排序</summary>
    [Required(ErrorMessage = "排序為必填")]
    [Range(1, 65535, ErrorMessage = "排序值須大於 0")]
    public ushort Sort { get; set; }
}

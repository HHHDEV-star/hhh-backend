using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Website.HomepageInnerSets;

/// <summary>
/// 新增首頁區塊元素
/// (對應舊版 Homepage/innerset_post — 舊版是 batch,本 API 改成 single record)
/// </summary>
public class CreateHomepageInnerSetRequest
{
    /// <summary>主題編號</summary>
    [Required]
    [Range(1, uint.MaxValue)]
    public uint MappingId { get; set; }

    /// <summary>元素排序</summary>
    [Required]
    [Range(1, 255)]
    public byte InnerSort { get; set; }

    /// <summary>主題類型(case/video/column/product/ad/designer/brand/fans/week)</summary>
    [Required]
    [StringLength(10)]
    public string ThemeType { get; set; } = string.Empty;

    /// <summary>FK → outer_site_set.oss_id</summary>
    [Required]
    public uint OuterSet { get; set; }

    /// <summary>上線狀態(Y/N)</summary>
    [Required]
    [RegularExpression("^[YN]$", ErrorMessage = "onoff 僅接受 Y 或 N")]
    public string Onoff { get; set; } = "Y";

    /// <summary>開始時間</summary>
    [Required]
    public DateTime StartTime { get; set; }

    /// <summary>結束時間</summary>
    [Required]
    public DateTime EndTime { get; set; }
}

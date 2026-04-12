using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.CallIns;

/// <summary>0809 來電資料單筆項目（用於批次新增）</summary>
public class CallinDataItemRequest
{
    /// <summary>使用者編號</summary>
    [StringLength(10)]
    public string UsersSn { get; set; } = string.Empty;

    /// <summary>設計師名稱</summary>
    [Required]
    [StringLength(20)]
    public string DesignerTitle { get; set; } = string.Empty;

    /// <summary>活動日期</summary>
    [Required]
    public DateOnly ActivityTime { get; set; }

    /// <summary>來電時間</summary>
    [Required]
    [StringLength(20)]
    public string CallinTime { get; set; } = string.Empty;

    /// <summary>通話時長</summary>
    [StringLength(20)]
    public string CallinPeriod { get; set; } = string.Empty;

    /// <summary>來電類型</summary>
    [StringLength(10)]
    public string CallinType { get; set; } = string.Empty;

    /// <summary>電話號碼</summary>
    [Required]
    [StringLength(20)]
    public string Phone { get; set; } = string.Empty;
}

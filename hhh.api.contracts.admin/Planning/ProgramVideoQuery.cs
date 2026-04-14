using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Planning;

/// <summary>節目影片查詢條件</summary>
public class ProgramVideoQuery
{
    /// <summary>頻道名稱</summary>
    [Required(ErrorMessage = "頻道名稱為必填")]
    public string Channel { get; set; } = string.Empty;

    /// <summary>開始日期</summary>
    [Required(ErrorMessage = "開始日期為必填")]
    public DateOnly Sdate { get; set; }

    /// <summary>結束日期</summary>
    [Required(ErrorMessage = "結束日期為必填")]
    public DateOnly Edate { get; set; }
}

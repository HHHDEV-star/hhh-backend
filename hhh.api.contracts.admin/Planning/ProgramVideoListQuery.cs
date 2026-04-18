using System.ComponentModel.DataAnnotations;
using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Planning;

/// <summary>
/// 節目影片列表查詢參數（合併 ProgramVideoQuery + ListQuery）
/// （對應舊版 PHP: Program/unit_get）
/// </summary>
public class ProgramVideoListQuery : PagedRequest
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

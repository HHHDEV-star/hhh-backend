using System.ComponentModel.DataAnnotations;
using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Planning;

/// <summary>
/// 節目表列表查詢參數
/// （對應舊版 PHP: Program/playlist_get）
/// </summary>
public class ProgramListQuery : PagedRequest
{
    /// <summary>開始日期</summary>
    [Required(ErrorMessage = "開始日期為必填")]
    public DateOnly Sdate { get; set; }

    /// <summary>結束日期</summary>
    [Required(ErrorMessage = "結束日期為必填")]
    public DateOnly Edate { get; set; }
}

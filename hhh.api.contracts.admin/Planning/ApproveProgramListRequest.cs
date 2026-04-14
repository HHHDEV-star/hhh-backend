using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Planning;

/// <summary>審核節目表（批次設為上架）</summary>
public class ApproveProgramListRequest
{
    /// <summary>開始日期</summary>
    [Required(ErrorMessage = "開始日期為必填")]
    public DateOnly Sdate { get; set; }

    /// <summary>結束日期</summary>
    [Required(ErrorMessage = "結束日期為必填")]
    public DateOnly Edate { get; set; }
}

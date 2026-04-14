using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Planning;

/// <summary>依群組批次產生節目表</summary>
public class GenerateFromGroupRequest
{
    /// <summary>群組 ID</summary>
    [Required(ErrorMessage = "群組 ID 為必填")]
    public uint Gid { get; set; }

    /// <summary>頻道名稱</summary>
    [Required(ErrorMessage = "頻道名稱為必填")]
    public string Channel { get; set; } = string.Empty;

    /// <summary>開始日期</summary>
    [Required(ErrorMessage = "開始日期為必填")]
    public DateOnly Sdate { get; set; }

    /// <summary>結束日期</summary>
    [Required(ErrorMessage = "結束日期為必填")]
    public DateOnly Edate { get; set; }

    /// <summary>上架日期時間</summary>
    [Required(ErrorMessage = "上架日期為必填")]
    public DateTime Adate { get; set; }
}

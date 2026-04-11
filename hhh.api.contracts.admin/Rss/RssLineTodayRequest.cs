using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Rss;

/// <summary>
/// RSS LineToday 排程新增/更新請求
/// </summary>
public class RssLineTodayRequest
{
    /// <summary>推送日期(必填)</summary>
    [Required]
    public DateOnly Date { get; set; }

    /// <summary>影音編號(逗號分隔)</summary>
    [StringLength(60)]
    public string? Hvideo { get; set; }
}

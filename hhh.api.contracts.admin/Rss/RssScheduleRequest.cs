using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Rss;

/// <summary>
/// RSS 排程新增/更新請求(共用於 Yahoo / MSN)
/// </summary>
public class RssScheduleRequest
{
    /// <summary>推送日期(必填)</summary>
    [Required]
    public DateOnly Date { get; set; }

    /// <summary>專欄編號(逗號分隔)</summary>
    [StringLength(60)]
    public string? Hcolumn { get; set; }

    /// <summary>個案編號(逗號分隔)</summary>
    [StringLength(60)]
    public string? Hcase { get; set; }
}

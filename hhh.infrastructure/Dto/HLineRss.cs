using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Table("_hLINE_RSS")]
public partial class HLineRss
{
    /// <summary>
    /// 日期
    /// </summary>
    [Key]
    [Column("dateString")]
    [StringLength(8)]
    public string DateString { get; set; } = null!;

    /// <summary>
    /// 專欄ID
    /// </summary>
    [Column("strID", TypeName = "text")]
    public string StrId { get; set; } = null!;

    /// <summary>
    /// 個案ID
    /// </summary>
    [Column("strCaseID", TypeName = "text")]
    public string StrCaseId { get; set; } = null!;

    /// <summary>
    /// 更新時間
    /// </summary>
    [Column("updateTimeUnix")]
    public ulong UpdateTimeUnix { get; set; }
}

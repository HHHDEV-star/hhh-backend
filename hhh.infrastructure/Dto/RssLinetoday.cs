using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Table("rss_linetoday")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class RssLinetoday
{
    /// <summary>
    /// PK
    /// </summary>
    [Key]
    [Column("id")]
    public uint Id { get; set; }

    /// <summary>
    /// 推送日期 (2019-06-21)
    /// </summary>
    [Column("date")]
    public DateOnly? Date { get; set; }

    /// <summary>
    /// MSN_影音編號
    /// </summary>
    [Column("hvideo")]
    [StringLength(60)]
    public string? Hvideo { get; set; }

    /// <summary>
    /// 建立時間 (2019-06-21 11:00:00)
    /// </summary>
    [Column("create_time", TypeName = "datetime")]
    public DateTime? CreateTime { get; set; }

    /// <summary>
    /// 修改時間 (2019-06-21 11:00:00)
    /// </summary>
    [Column("update_time", TypeName = "datetime")]
    public DateTime? UpdateTime { get; set; }
}

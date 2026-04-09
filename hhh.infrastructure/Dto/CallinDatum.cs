using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Table("callin_data")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class CallinDatum
{
    [Key]
    [Column("seq")]
    public int Seq { get; set; }

    [Column("users_sn")]
    [StringLength(10)]
    public string UsersSn { get; set; } = null!;

    [Column("designer_title")]
    [StringLength(20)]
    public string DesignerTitle { get; set; } = null!;

    [Column("activity_time")]
    public DateOnly ActivityTime { get; set; }

    [Column("callin_time")]
    [StringLength(20)]
    public string CallinTime { get; set; } = null!;

    [Column("callin_period")]
    [StringLength(20)]
    public string CallinPeriod { get; set; } = null!;

    [Column("callin_type")]
    [StringLength(10)]
    public string CallinType { get; set; } = null!;

    [Column("phone")]
    [StringLength(20)]
    public string Phone { get; set; } = null!;

    [Column("create_time", TypeName = "datetime")]
    public DateTime CreateTime { get; set; }

    [Column("send_mail")]
    [StringLength(1)]
    public string SendMail { get; set; } = null!;

    [Column("send_time", TypeName = "datetime")]
    public DateTime? SendTime { get; set; }
}

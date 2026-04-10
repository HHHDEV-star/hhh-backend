using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

/// <summary>
/// 活動表單
/// </summary>
[Table("events")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class Event
{
    [Key]
    [Column("events_id")]
    public int EventsId { get; set; }

    [Column("events_type")]
    [StringLength(20)]
    [MySqlCollation("utf8mb3_bin")]
    public string EventsType { get; set; } = null!;

    [Column("name")]
    [StringLength(10)]
    public string Name { get; set; } = null!;

    [Column("email", TypeName = "text")]
    public string Email { get; set; } = null!;

    [Column("mobile")]
    [StringLength(15)]
    public string Mobile { get; set; } = null!;

    [Column("area")]
    [StringLength(10)]
    public string Area { get; set; } = null!;

    /// <summary>
    /// 會員id
    /// </summary>
    [Column("uid")]
    public int Uid { get; set; }

    [Column("create_time", TypeName = "datetime")]
    public DateTime CreateTime { get; set; }
}

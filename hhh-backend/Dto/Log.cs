using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("logs")]
[Index("Guid", Name = "guid")]
[Index("Status", Name = "status")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class Log
{
    /// <summary>
    /// PK
    /// </summary>
    [Key]
    [Column("id")]
    public uint Id { get; set; }

    /// <summary>
    /// GUID
    /// </summary>
    [Column("guid")]
    public Guid Guid { get; set; }

    /// <summary>
    /// 網址
    /// </summary>
    [Column("url")]
    [StringLength(150)]
    public string Url { get; set; } = null!;

    /// <summary>
    /// 類型
    /// </summary>
    [Column("type")]
    [StringLength(50)]
    public string? Type { get; set; }

    /// <summary>
    /// 編號
    /// </summary>
    [Column("num", TypeName = "mediumint unsigned")]
    public uint? Num { get; set; }

    /// <summary>
    /// IP
    /// </summary>
    [Column("ip")]
    [StringLength(50)]
    public string Ip { get; set; } = null!;

    /// <summary>
    /// 處理狀態(N:未處理 / Y:已處理)
    /// </summary>
    [Column("status")]
    [StringLength(1)]
    public string Status { get; set; } = null!;

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("ins_time", TypeName = "datetime")]
    public DateTime InsTime { get; set; }

    /// <summary>
    /// 處理時間
    /// </summary>
    [Column("upd_time", TypeName = "datetime")]
    public DateTime UpdTime { get; set; }
}

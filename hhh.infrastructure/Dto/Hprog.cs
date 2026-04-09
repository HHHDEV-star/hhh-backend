using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Table("_hprog")]
[Index("ChanId", Name = "chan_id")]
[Index("Debut", Name = "debut")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class Hprog
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("hprog_id")]
    public uint HprogId { get; set; }

    /// <summary>
    /// 頻道id
    /// </summary>
    [Column("chan_id")]
    public uint ChanId { get; set; }

    /// <summary>
    /// 節目種類
    /// </summary>
    [Column("prog_type")]
    [StringLength(32)]
    public string ProgType { get; set; } = null!;

    /// <summary>
    /// 首播時間
    /// </summary>
    [Column("debut")]
    [StringLength(32)]
    public string Debut { get; set; } = null!;

    [Column("displaytime", TypeName = "datetime")]
    public DateTime Displaytime { get; set; }

    /// <summary>
    /// 是否露出這個節目(0:否1:是) 
    /// </summary>
    [Column("onoff")]
    public byte Onoff { get; set; }

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("creat_time", TypeName = "timestamp")]
    public DateTime CreatTime { get; set; }
}

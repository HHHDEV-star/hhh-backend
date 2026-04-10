using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("_hprog_chan")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class HprogChan
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("chan_id")]
    public uint ChanId { get; set; }

    /// <summary>
    /// 頻道名稱
    /// </summary>
    [Column("cname")]
    [StringLength(64)]
    public string Cname { get; set; } = null!;

    /// <summary>
    /// 頻道logo
    /// </summary>
    [Column("clogo")]
    [StringLength(128)]
    public string Clogo { get; set; } = null!;

    /// <summary>
    /// 排序
    /// </summary>
    [Column("corder")]
    public ushort Corder { get; set; }

    /// <summary>
    /// 首播時間
    /// </summary>
    [Column("premiere")]
    [StringLength(128)]
    public string? Premiere { get; set; }

    /// <summary>
    /// 重播時間
    /// </summary>
    [Column("replay")]
    [StringLength(128)]
    public string? Replay { get; set; }

    /// <summary>
    /// 開/關
    /// </summary>
    [Column("onoff")]
    public short Onoff { get; set; }

    /// <summary>
    /// 撥出頻道
    /// </summary>
    [Column("broadcast")]
    [StringLength(128)]
    public string? Broadcast { get; set; }

    /// <summary>
    /// 頻道名稱縮寫
    /// </summary>
    [Column("cname_s")]
    [StringLength(45)]
    public string CnameS { get; set; } = null!;
}

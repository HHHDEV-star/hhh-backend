using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("rss_transfer")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class RssTransfer
{
    /// <summary>
    /// PK
    /// </summary>
    [Key]
    [Column("id")]
    public uint Id { get; set; }

    /// <summary>
    /// 來自
    /// </summary>
    [Column("source")]
    [StringLength(20)]
    public string Source { get; set; } = null!;

    /// <summary>
    /// 類型(column : 專欄 / case : 個案)
    /// </summary>
    [Column("type")]
    [StringLength(10)]
    public string Type { get; set; } = null!;

    /// <summary>
    /// 編號
    /// </summary>
    [Column("num")]
    public ushort Num { get; set; }

    /// <summary>
    /// IP
    /// </summary>
    [Column("ip")]
    [StringLength(15)]
    public string Ip { get; set; } = null!;

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("datetime", TypeName = "datetime")]
    public DateTime Datetime { get; set; }
}

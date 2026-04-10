using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("_hprize")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class Hprize
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("hprize_id")]
    public uint HprizeId { get; set; }

    /// <summary>
    /// 獎品logo
    /// </summary>
    [Column("logo")]
    [StringLength(128)]
    public string Logo { get; set; } = null!;

    /// <summary>
    /// 獎品名稱
    /// </summary>
    [Column("title")]
    [StringLength(128)]
    public string Title { get; set; } = null!;

    /// <summary>
    /// 獎品說明
    /// </summary>
    [Column("desc", TypeName = "text")]
    public string Desc { get; set; } = null!;

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("creat_time", TypeName = "timestamp")]
    public DateTime CreatTime { get; set; }
}

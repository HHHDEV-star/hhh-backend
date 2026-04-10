using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("prog_list")]
[Index("ProgDate", "Onoff", Name = "prog_date")]
public partial class ProgList
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("prog_list_id")]
    public uint ProgListId { get; set; }

    /// <summary>
    /// 播出日期
    /// </summary>
    [Column("prog_date")]
    public DateOnly ProgDate { get; set; }

    /// <summary>
    /// 播出時間
    /// </summary>
    [Column("prog_time", TypeName = "time")]
    public TimeOnly ProgTime { get; set; }

    /// <summary>
    /// 節目名稱
    /// </summary>
    [Column("prog_name")]
    [StringLength(30)]
    public string ProgName { get; set; } = null!;

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("create_time", TypeName = "datetime")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新時間
    /// </summary>
    [Column("update_time", TypeName = "datetime")]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// 是否露出(N:否Y:是)
    /// </summary>
    [Column("onoff")]
    [StringLength(1)]
    public string Onoff { get; set; } = null!;
}

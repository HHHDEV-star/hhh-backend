using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("announcement")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class Announcement
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("aid")]
    public uint Aid { get; set; }

    /// <summary>
    /// 內容
    /// </summary>
    [Column("content")]
    [StringLength(50)]
    public string Content { get; set; } = null!;

    /// <summary>
    /// 排序
    /// </summary>
    [Column("sort")]
    public byte Sort { get; set; }

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("create_time", TypeName = "datetime")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 修改時間
    /// </summary>
    [Column("update_time", TypeName = "datetime")]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// 上線狀態(N:關Y:開)
    /// </summary>
    [Column("onoff")]
    [StringLength(1)]
    public string Onoff { get; set; } = null!;

    /// <summary>
    /// 公告連結
    /// </summary>
    [Column("link")]
    [StringLength(200)]
    public string? Link { get; set; }

    /// <summary>
    /// 開始時間
    /// </summary>
    [Column("start_time", TypeName = "datetime")]
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 結束時間
    /// </summary>
    [Column("end_time", TypeName = "datetime")]
    public DateTime? EndTime { get; set; }
}

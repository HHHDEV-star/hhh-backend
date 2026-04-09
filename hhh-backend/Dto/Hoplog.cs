using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("_hoplog")]
[Index("CreatTime", Name = "creat_time")]
public partial class Hoplog
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("id")]
    public uint Id { get; set; }

    /// <summary>
    /// 帳號ID
    /// </summary>
    [Column("uid")]
    public uint Uid { get; set; }

    /// <summary>
    /// 帳號
    /// </summary>
    [Column("uname")]
    [StringLength(32)]
    public string Uname { get; set; } = null!;

    /// <summary>
    /// 操作描述
    /// </summary>
    [Column("opdesc", TypeName = "text")]
    public string Opdesc { get; set; } = null!;

    /// <summary>
    /// 執行SQL
    /// </summary>
    [Column("sqlcmd", TypeName = "text")]
    public string Sqlcmd { get; set; } = null!;

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("creat_time", TypeName = "timestamp")]
    public DateTime CreatTime { get; set; }

    /// <summary>
    /// IP
    /// </summary>
    [Column("ip")]
    [StringLength(128)]
    public string? Ip { get; set; }

    /// <summary>
    /// 頁面名稱
    /// </summary>
    [Column("page_name")]
    [StringLength(32)]
    public string? PageName { get; set; }

    /// <summary>
    /// 執行動作
    /// </summary>
    [Column("action")]
    [StringLength(4)]
    public string? Action { get; set; }
}

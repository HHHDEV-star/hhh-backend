using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("_hnewspaper")]
[Index("Email", Name = "email", IsUnique = true)]
[Index("Onoff", Name = "onoff")]
public partial class Hnewspaper
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("id")]
    public uint Id { get; set; }

    /// <summary>
    /// EMAIL
    /// </summary>
    [Column("email")]
    [StringLength(64)]
    [MySqlCollation("utf8mb3_general_ci")]
    public string Email { get; set; } = null!;

    /// <summary>
    /// 發送狀態( 成功:0 失敗:1)
    /// </summary>
    [Column("failcount")]
    public uint Failcount { get; set; }

    /// <summary>
    /// 是否開啟(否:0 是:1)
    /// </summary>
    [Column("onoff")]
    public byte Onoff { get; set; }

    /// <summary>
    /// 發送次數
    /// </summary>
    [Column("is_send")]
    public bool IsSend { get; set; }

    /// <summary>
    /// 更新時間
    /// </summary>
    [Column("update_time", TypeName = "timestamp")]
    public DateTime UpdateTime { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Keyless]
[Table("_hevent_attend")]
[Index("HeventId", Name = "hevent_id")]
[Index("HeventId", "Uid", Name = "unique_index", IsUnique = true)]
public partial class HeventAttend
{
    /// <summary>
    /// pk
    /// </summary>
    [Column("hevent_id")]
    public uint HeventId { get; set; }

    /// <summary>
    /// 使用者ID
    /// </summary>
    [Column("uid")]
    public uint Uid { get; set; }

    /// <summary>
    /// 答案
    /// </summary>
    [Column("answer")]
    [StringLength(256)]
    public string? Answer { get; set; }

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("create_time", TypeName = "timestamp")]
    public DateTime CreateTime { get; set; }
}

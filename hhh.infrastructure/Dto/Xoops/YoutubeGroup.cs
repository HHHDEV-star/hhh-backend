using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("youtube_group")]
[Index("Name", Name = "name", IsUnique = true)]
[MySqlCollation("utf8mb3_general_ci")]
public partial class YoutubeGroup
{
    /// <summary>
    /// PK
    /// </summary>
    [Key]
    [Column("gid")]
    public uint Gid { get; set; }

    /// <summary>
    /// 群組名稱(代號)
    /// </summary>
    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 是否關閉(N:否 / Y:是
    /// </summary>
    [Column("onoff")]
    [StringLength(1)]
    public string Onoff { get; set; } = null!;

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("create_time", TypeName = "datetime")]
    public DateTime CreateTime { get; set; }
}

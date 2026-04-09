using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("youtube_group_detail")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class YoutubeGroupDetail
{
    /// <summary>
    /// PK
    /// </summary>
    [Key]
    [Column("id")]
    public uint Id { get; set; }

    /// <summary>
    /// FK : youtube_group.gid
    /// </summary>
    [Column("gid")]
    public uint Gid { get; set; }

    /// <summary>
    /// FK :  youtube.yid
    /// </summary>
    [Column("yid")]
    public uint Yid { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [Column("sort")]
    public byte Sort { get; set; }

    /// <summary>
    /// 是否開啟(Y:是 / N:否
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

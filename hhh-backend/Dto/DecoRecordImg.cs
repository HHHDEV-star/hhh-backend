using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("deco_record_img")]
[Index("Bldsno", Name = "bldsno")]
[MySqlCharSet("utf8mb4")]
[MySqlCollation("utf8mb4_0900_ai_ci")]
public partial class DecoRecordImg
{
    /// <summary>
    /// PK
    /// </summary>
    [Key]
    [Column("id")]
    public uint Id { get; set; }

    /// <summary>
    /// FK
    /// </summary>
    [Column("bldsno")]
    public uint Bldsno { get; set; }

    /// <summary>
    /// 圖片位置(檔名
    /// </summary>
    [Column("img_path")]
    [StringLength(200)]
    public string ImgPath { get; set; } = null!;

    /// <summary>
    /// 順序
    /// </summary>
    [Column("sort", TypeName = "mediumint unsigned")]
    public uint Sort { get; set; }

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
    /// 是否審核通過(Y:是/N:否
    /// </summary>
    [Column("onoff")]
    [StringLength(1)]
    public string Onoff { get; set; } = null!;
}

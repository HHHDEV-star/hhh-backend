using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Table("short_url")]
[Index("Code", Name = "code", IsUnique = true)]
public partial class ShortUrl
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("id")]
    public uint Id { get; set; }

    /// <summary>
    /// 縮指碼
    /// </summary>
    [Column("code")]
    [StringLength(6)]
    [MySqlCollation("utf8mb3_bin")]
    public string Code { get; set; } = null!;

    /// <summary>
    /// 網址
    /// </summary>
    [Column("url")]
    [StringLength(200)]
    public string Url { get; set; } = null!;

    /// <summary>
    /// FB追蹤參數
    /// </summary>
    [Column("track_a")]
    [StringLength(200)]
    public string? TrackA { get; set; }

    /// <summary>
    /// 第二組追蹤參數
    /// </summary>
    [Column("track_b")]
    [StringLength(200)]
    public string? TrackB { get; set; }

    /// <summary>
    /// 第三組追蹤參數
    /// </summary>
    [Column("track_c")]
    [StringLength(200)]
    public string? TrackC { get; set; }

    /// <summary>
    /// 第四組追蹤參數
    /// </summary>
    [Column("track_d")]
    [StringLength(200)]
    public string? TrackD { get; set; }

    /// <summary>
    /// 備註
    /// </summary>
    [Column("memo")]
    [StringLength(50)]
    public string Memo { get; set; } = null!;

    /// <summary>
    /// 預覽文案
    /// </summary>
    [Column("preview")]
    [StringLength(500)]
    public string Preview { get; set; } = null!;

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("create_time", TypeName = "timestamp")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新時間
    /// </summary>
    [Column("update_time", TypeName = "timestamp")]
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 自定義標題
    /// </summary>
    [Column("photo")]
    [StringLength(200)]
    public string? Photo { get; set; }

    /// <summary>
    /// 自定義圖片
    /// </summary>
    [Column("replace_title")]
    [StringLength(200)]
    public string? ReplaceTitle { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Table("_hpublish")]
[Index("Pdate", Name = "pdate")]
[Index("Type", Name = "type")]
[Index("Viewed", Name = "viewed")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class Hpublish
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("hpublish_id")]
    public uint HpublishId { get; set; }

    /// <summary>
    /// 書籍類別
    /// </summary>
    [Column("type")]
    [StringLength(32)]
    public string Type { get; set; } = null!;

    /// <summary>
    /// logo
    /// </summary>
    [Column("logo")]
    [StringLength(64)]
    public string Logo { get; set; } = null!;

    /// <summary>
    /// 名稱
    /// </summary>
    [Column("title")]
    [StringLength(128)]
    public string Title { get; set; } = null!;

    /// <summary>
    /// 作者
    /// </summary>
    [Column("author")]
    [StringLength(64)]
    public string Author { get; set; } = null!;

    /// <summary>
    /// 出版日期
    /// </summary>
    [Column("pdate")]
    public DateOnly Pdate { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    [Column("desc", TypeName = "text")]
    public string Desc { get; set; } = null!;

    [Column("editor", TypeName = "text")]
    public string Editor { get; set; } = null!;

    /// <summary>
    /// 觀看數
    /// </summary>
    [Column("viewed")]
    public uint Viewed { get; set; }

    /// <summary>
    /// 推薦數
    /// </summary>
    [Column("recommend")]
    public uint Recommend { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Table("_hcontest")]
[Index("Applytime", Name = "applytime")]
[Index("C1", Name = "c1")]
[Index("ClassType", Name = "class_type")]
[Index("Uid", Name = "uid")]
[Index("Year", Name = "year")]
public partial class Hcontest
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("contest_id")]
    public uint ContestId { get; set; }

    /// <summary>
    /// ID
    /// </summary>
    [Column("uid")]
    public uint Uid { get; set; }

    /// <summary>
    /// 年份
    /// </summary>
    [Column("year")]
    public ushort Year { get; set; }

    /// <summary>
    /// 組別1
    /// </summary>
    [Column("class_type")]
    [StringLength(16)]
    public string ClassType { get; set; } = null!;

    /// <summary>
    /// 組別2
    /// </summary>
    [Column("c1")]
    [StringLength(64)]
    public string C1 { get; set; } = null!;

    /// <summary>
    /// 報名者
    /// </summary>
    [Column("c2")]
    [StringLength(64)]
    public string C2 { get; set; } = null!;

    /// <summary>
    /// 公司/學校
    /// </summary>
    [Column("c3")]
    [StringLength(64)]
    public string C3 { get; set; } = null!;

    [Column("c4")]
    [StringLength(64)]
    public string C4 { get; set; } = null!;

    /// <summary>
    /// 電話
    /// </summary>
    [Column("c5")]
    [StringLength(64)]
    public string C5 { get; set; } = null!;

    /// <summary>
    /// 手機
    /// </summary>
    [Column("c6")]
    [StringLength(64)]
    public string C6 { get; set; } = null!;

    /// <summary>
    /// 地址
    /// </summary>
    [Column("c7")]
    [StringLength(64)]
    public string C7 { get; set; } = null!;

    /// <summary>
    /// email
    /// </summary>
    [Column("c8")]
    [StringLength(64)]
    public string C8 { get; set; } = null!;

    /// <summary>
    /// 作品
    /// </summary>
    [Column("c9")]
    [StringLength(128)]
    public string C9 { get; set; } = null!;

    /// <summary>
    /// 作品描述
    /// </summary>
    [Column("c10", TypeName = "text")]
    public string C10 { get; set; } = null!;

    /// <summary>
    /// 作品描述2
    /// </summary>
    [Column("c11", TypeName = "text")]
    public string C11 { get; set; } = null!;

    /// <summary>
    /// 作品描述3
    /// </summary>
    [Column("c12", TypeName = "text")]
    public string C12 { get; set; } = null!;

    [Column("c13", TypeName = "text")]
    public string C13 { get; set; } = null!;

    [Column("applytime", TypeName = "timestamp")]
    public DateTime Applytime { get; set; }

    [Column("pay")]
    [StringLength(16)]
    public string Pay { get; set; } = null!;

    /// <summary>
    /// 末五碼
    /// </summary>
    [Column("an")]
    [StringLength(16)]
    public string An { get; set; } = null!;

    [Column("score", TypeName = "text")]
    public string Score { get; set; } = null!;

    [Column("wp")]
    public uint Wp { get; set; }

    [Column("wp_detail", TypeName = "text")]
    public string WpDetail { get; set; } = null!;

    /// <summary>
    /// 入圍
    /// </summary>
    [Column("finalist")]
    public byte Finalist { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Table("_hext1")]
[Index("Ext1Date", Name = "ext1_date")]
[Index("Ext1TypeId", Name = "ext1_type_id")]
public partial class Hext1
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("id")]
    public uint Id { get; set; }

    /// <summary>
    /// 外網上稿日期
    /// </summary>
    [Column("ext1_date")]
    [StringLength(16)]
    public string Ext1Date { get; set; } = null!;

    /// <summary>
    /// 分類
    /// </summary>
    [Column("ext1_type_id")]
    public ushort Ext1TypeId { get; set; }

    /// <summary>
    /// 設計師ID
    /// </summary>
    [Column("hdesigner_id")]
    public uint HdesignerId { get; set; }

    /// <summary>
    /// 廠商ID
    /// </summary>
    [Column("hbrand_id")]
    public uint HbrandId { get; set; }

    /// <summary>
    /// 文章標題
    /// </summary>
    [Column("ext1_title")]
    [StringLength(256)]
    public string Ext1Title { get; set; } = null!;

    /// <summary>
    /// 網址
    /// </summary>
    [Column("ext1_url")]
    [StringLength(256)]
    public string Ext1Url { get; set; } = null!;

    /// <summary>
    /// 點擊數
    /// </summary>
    [Column("mcount")]
    public uint Mcount { get; set; }
}

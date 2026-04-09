using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Table("_hdesigner_addtion")]
[Index("HdesignerId", Name = "hdesigner_id")]
public partial class HdesignerAddtion
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("sn")]
    public uint Sn { get; set; }

    /// <summary>
    /// 設計師id
    /// </summary>
    [Column("hdesigner_id")]
    public uint HdesignerId { get; set; }

    /// <summary>
    /// 專業證照
    /// </summary>
    [Column("atype")]
    [StringLength(16)]
    public string Atype { get; set; } = null!;

    /// <summary>
    /// 設計師姓名
    /// </summary>
    [Column("aname")]
    [StringLength(64)]
    public string Aname { get; set; } = null!;

    /// <summary>
    /// 證照名稱
    /// </summary>
    [Column("avalue1")]
    [StringLength(16)]
    public string Avalue1 { get; set; } = null!;

    /// <summary>
    /// 證照號
    /// </summary>
    [Column("avalue2")]
    [StringLength(16)]
    public string Avalue2 { get; set; } = null!;
}

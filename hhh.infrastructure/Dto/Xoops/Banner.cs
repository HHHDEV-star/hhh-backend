using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("_banner")]
[Index("Bid", "Cid", Name = "idxbannerbidcid")]
[Index("Cid", Name = "idxbannercid")]
public partial class Banner
{
    [Key]
    [Column("bid")]
    public ushort Bid { get; set; }

    [Column("cid")]
    public byte Cid { get; set; }

    [Column("imptotal")]
    public uint Imptotal { get; set; }

    [Column("impmade", TypeName = "mediumint unsigned")]
    public uint Impmade { get; set; }

    [Column("clicks", TypeName = "mediumint unsigned")]
    public uint Clicks { get; set; }

    [Column("imageurl")]
    [StringLength(255)]
    public string Imageurl { get; set; } = null!;

    [Column("clickurl")]
    [StringLength(255)]
    public string Clickurl { get; set; } = null!;

    [Column("date")]
    public int Date { get; set; }

    [Column("htmlbanner")]
    public bool Htmlbanner { get; set; }

    [Column("htmlcode", TypeName = "text")]
    public string? Htmlcode { get; set; }
}

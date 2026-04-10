using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("_bannerfinish")]
[Index("Cid", Name = "cid")]
public partial class Bannerfinish
{
    [Key]
    [Column("bid")]
    public ushort Bid { get; set; }

    [Column("cid")]
    public ushort Cid { get; set; }

    [Column("impressions", TypeName = "mediumint unsigned")]
    public uint Impressions { get; set; }

    [Column("clicks", TypeName = "mediumint unsigned")]
    public uint Clicks { get; set; }

    [Column("datestart")]
    public uint Datestart { get; set; }

    [Column("dateend")]
    public uint Dateend { get; set; }
}

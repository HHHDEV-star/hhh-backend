using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("_hmillion_prize")]
[Index("Pdate", Name = "pdate")]
[Index("Year", Name = "year")]
public partial class HmillionPrize
{
    [Key]
    [Column("prize_id")]
    public uint PrizeId { get; set; }

    [Column("year")]
    public uint Year { get; set; }

    [Column("ptitle")]
    [StringLength(128)]
    public string Ptitle { get; set; } = null!;

    [Column("pdesc", TypeName = "text")]
    public string Pdesc { get; set; } = null!;

    [Column("pimg")]
    [StringLength(64)]
    public string Pimg { get; set; } = null!;

    [Column("pquota")]
    public ushort Pquota { get; set; }

    [Column("pdate")]
    public DateOnly Pdate { get; set; }

    [Column("plink")]
    [StringLength(64)]
    public string Plink { get; set; } = null!;

    [Column("pvalue")]
    public uint Pvalue { get; set; }

    [Column("vendor_id")]
    public uint VendorId { get; set; }
}

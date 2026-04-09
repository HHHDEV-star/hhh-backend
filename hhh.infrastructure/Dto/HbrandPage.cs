using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Table("_hbrand_page")]
[Index("HbrandId", Name = "hbrand_id")]
public partial class HbrandPage
{
    [Key]
    [Column("sn")]
    public uint Sn { get; set; }

    [Column("hbrand_id")]
    public int HbrandId { get; set; }

    [Column("bptitle")]
    [StringLength(128)]
    public string Bptitle { get; set; } = null!;

    [Column("bptype")]
    [StringLength(4)]
    public string Bptype { get; set; } = null!;

    [Column("bplink")]
    [StringLength(128)]
    public string Bplink { get; set; } = null!;

    [Column("bphtml")]
    public string Bphtml { get; set; } = null!;

    [Column("bporder")]
    public ushort Bporder { get; set; }
}

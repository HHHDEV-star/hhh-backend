using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("_hcontest_forum")]
[Index("Applytime", Name = "applytime")]
[Index("Dinner", Name = "dinner")]
public partial class HcontestForum
{
    [Key]
    [Column("sn")]
    public uint Sn { get; set; }

    [Column("c1")]
    [StringLength(64)]
    public string C1 { get; set; } = null!;

    [Column("c2")]
    [StringLength(64)]
    public string C2 { get; set; } = null!;

    [Column("c3")]
    [StringLength(64)]
    public string C3 { get; set; } = null!;

    [Column("c4")]
    [StringLength(64)]
    public string C4 { get; set; } = null!;

    [Column("c5")]
    [StringLength(64)]
    public string C5 { get; set; } = null!;

    [Column("c6")]
    [StringLength(64)]
    public string C6 { get; set; } = null!;

    [Column("dinner")]
    [StringLength(8)]
    public string Dinner { get; set; } = null!;

    [Column("dno")]
    [StringLength(8)]
    public string Dno { get; set; } = null!;

    [Column("pay")]
    [StringLength(16)]
    public string Pay { get; set; } = null!;

    [Column("applytime", TypeName = "timestamp")]
    public DateTime Applytime { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("_hcomparison_tbl")]
[Index("Count", Name = "count")]
[Index("Name", Name = "name")]
[Index("Type", Name = "type")]
public partial class HcomparisonTbl
{
    [Key]
    [Column("sn")]
    public uint Sn { get; set; }

    [Column("type")]
    [StringLength(32)]
    public string Type { get; set; } = null!;

    [Column("name")]
    [StringLength(16)]
    public string Name { get; set; } = null!;

    [Column("count")]
    public uint Count { get; set; }
}

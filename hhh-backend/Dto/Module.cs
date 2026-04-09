using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("_modules")]
[Index("Dirname", Name = "dirname")]
[Index("Hasadmin", Name = "hasadmin")]
[Index("Hascomments", Name = "hascomments")]
[Index("Hasmain", Name = "hasmain")]
[Index("Hasnotification", Name = "hasnotification")]
[Index("Hassearch", Name = "hassearch")]
[Index("Isactive", Name = "isactive")]
[Index("Weight", Name = "weight")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class Module
{
    [Key]
    [Column("mid")]
    public ushort Mid { get; set; }

    [Column("name")]
    [StringLength(150)]
    public string Name { get; set; } = null!;

    [Column("version")]
    public ushort Version { get; set; }

    [Column("last_update")]
    public uint LastUpdate { get; set; }

    [Column("weight")]
    public ushort Weight { get; set; }

    [Column("isactive")]
    public byte Isactive { get; set; }

    [Column("dirname")]
    [StringLength(25)]
    public string Dirname { get; set; } = null!;

    [Column("hasmain")]
    public byte Hasmain { get; set; }

    [Column("hasadmin")]
    public byte Hasadmin { get; set; }

    [Column("hassearch")]
    public byte Hassearch { get; set; }

    [Column("hasconfig")]
    public byte Hasconfig { get; set; }

    [Column("hascomments")]
    public byte Hascomments { get; set; }

    [Column("hasnotification")]
    public byte Hasnotification { get; set; }
}

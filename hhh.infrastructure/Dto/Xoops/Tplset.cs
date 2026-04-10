using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("_tplset")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class Tplset
{
    [Key]
    [Column("tplset_id")]
    public uint TplsetId { get; set; }

    [Column("tplset_name")]
    [StringLength(50)]
    public string TplsetName { get; set; } = null!;

    [Column("tplset_desc")]
    [StringLength(255)]
    public string TplsetDesc { get; set; } = null!;

    [Column("tplset_credits", TypeName = "text")]
    public string? TplsetCredits { get; set; }

    [Column("tplset_created")]
    public uint TplsetCreated { get; set; }
}

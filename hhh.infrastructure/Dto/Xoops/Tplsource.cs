using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("_tplsource")]
[Index("TplId", Name = "tpl_id")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class Tplsource
{
    [Key]
    [Column("tpl_id", TypeName = "mediumint unsigned")]
    public uint TplId { get; set; }

    [Column("tpl_source", TypeName = "mediumtext")]
    public string? TplSource1 { get; set; }
}

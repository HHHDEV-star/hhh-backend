using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Keyless]
[Table("_hpush")]
[Index("Ip", Name = "ip")]
[Index("Pushtime", "Pushtype", "Pushid", Name = "pushtime")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class Hpush
{
    [Column("ip")]
    [StringLength(16)]
    public string Ip { get; set; } = null!;

    [Column("pushtime")]
    public uint Pushtime { get; set; }

    [Column("pushtype")]
    [StringLength(64)]
    public string Pushtype { get; set; } = null!;

    [Column("pushid")]
    public uint Pushid { get; set; }
}

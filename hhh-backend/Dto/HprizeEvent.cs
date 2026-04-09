using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("_hprize_event")]
[Index("En", Name = "en")]
[Index("Key1", Name = "key1")]
[Index("Uid", Name = "uid")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class HprizeEvent
{
    [Key]
    [Column("sn")]
    public ulong Sn { get; set; }

    [Column("en")]
    [StringLength(16)]
    public string En { get; set; } = null!;

    [Column("uid")]
    public uint Uid { get; set; }

    [Column("key1")]
    [StringLength(16)]
    public string Key1 { get; set; } = null!;

    [Column("txt1")]
    [StringLength(128)]
    public string Txt1 { get; set; } = null!;

    [Column("ip")]
    [StringLength(16)]
    public string Ip { get; set; } = null!;

    [Column("created_at", TypeName = "timestamp")]
    public DateTime CreatedAt { get; set; }
}

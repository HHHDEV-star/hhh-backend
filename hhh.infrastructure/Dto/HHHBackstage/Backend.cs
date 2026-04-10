using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.HHHBackstage;

[PrimaryKey("Id", "IpAddress")]
[Table("backend")]
[Index("Timestamp", Name = "ci_sessions_timestamp")]
public partial class Backend
{
    [Key]
    [Column("id")]
    [StringLength(128)]
    public string Id { get; set; } = null!;

    [Key]
    [Column("ip_address")]
    [StringLength(45)]
    public string IpAddress { get; set; } = null!;

    [Column("timestamp")]
    public uint Timestamp { get; set; }

    [Column("data", TypeName = "blob")]
    public byte[] Data { get; set; } = null!;
}

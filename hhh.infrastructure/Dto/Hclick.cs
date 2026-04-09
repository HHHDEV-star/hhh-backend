using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Table("_hclick")]
public partial class Hclick
{
    [Key]
    [Column("id")]
    public uint Id { get; set; }

    [Column("desc")]
    [StringLength(64)]
    public string Desc { get; set; } = null!;

    [Column("counter")]
    public uint Counter { get; set; }
}

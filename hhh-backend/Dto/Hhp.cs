using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("_hhp")]
[Index("Hptype", Name = "hptype")]
[Index("Ttorder", Name = "ttorder")]
public partial class Hhp
{
    [Key]
    [Column("hpid")]
    public uint Hpid { get; set; }

    [Column("hptype")]
    [StringLength(64)]
    public string Hptype { get; set; } = null!;

    [Column("hptype_id")]
    public uint HptypeId { get; set; }

    [Column("hpimg")]
    [StringLength(128)]
    public string Hpimg { get; set; } = null!;

    [Column("hptitle")]
    [StringLength(128)]
    public string Hptitle { get; set; } = null!;

    [Column("hptext", TypeName = "text")]
    public string Hptext { get; set; } = null!;

    [Column("ttorder")]
    public uint Ttorder { get; set; }
}

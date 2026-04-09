using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("_configoption")]
[Index("ConfId", Name = "conf_id")]
public partial class Configoption
{
    [Key]
    [Column("confop_id", TypeName = "mediumint unsigned")]
    public uint ConfopId { get; set; }

    [Column("confop_name")]
    [StringLength(255)]
    public string ConfopName { get; set; } = null!;

    [Column("confop_value")]
    [StringLength(255)]
    public string ConfopValue { get; set; } = null!;

    [Column("conf_id")]
    public ushort ConfId { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Table("_xoopsnotifications")]
[Index("NotCategory", Name = "not_class")]
[Index("NotEvent", Name = "not_event")]
[Index("NotItemid", Name = "not_itemid")]
[Index("NotModid", Name = "not_modid")]
[Index("NotUid", Name = "not_uid")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class Xoopsnotification
{
    [Key]
    [Column("not_id", TypeName = "mediumint unsigned")]
    public uint NotId { get; set; }

    [Column("not_modid")]
    public ushort NotModid { get; set; }

    [Column("not_itemid", TypeName = "mediumint unsigned")]
    public uint NotItemid { get; set; }

    [Column("not_category")]
    [StringLength(30)]
    public string NotCategory { get; set; } = null!;

    [Column("not_event")]
    [StringLength(30)]
    public string NotEvent { get; set; } = null!;

    [Column("not_uid", TypeName = "mediumint unsigned")]
    public uint NotUid { get; set; }

    [Column("not_mode")]
    public bool NotMode { get; set; }
}

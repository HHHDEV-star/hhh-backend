using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("_groups")]
[Index("GroupType", Name = "group_type")]
public partial class Group
{
    [Key]
    [Column("groupid")]
    public ushort Groupid { get; set; }

    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Column("description", TypeName = "text")]
    public string? Description { get; set; }

    [Column("group_type")]
    [StringLength(10)]
    public string GroupType { get; set; } = null!;
}

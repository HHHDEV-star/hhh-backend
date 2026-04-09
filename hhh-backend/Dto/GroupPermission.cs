using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("_group_permission")]
[Index("GpermGroupid", Name = "groupid")]
[Index("GpermItemid", Name = "itemid")]
public partial class GroupPermission
{
    [Key]
    [Column("gperm_id")]
    public uint GpermId { get; set; }

    [Column("gperm_groupid")]
    public ushort GpermGroupid { get; set; }

    [Column("gperm_itemid", TypeName = "mediumint unsigned")]
    public uint GpermItemid { get; set; }

    [Column("gperm_modid", TypeName = "mediumint unsigned")]
    public uint GpermModid { get; set; }

    [Column("gperm_name")]
    [StringLength(50)]
    public string GpermName { get; set; } = null!;
}

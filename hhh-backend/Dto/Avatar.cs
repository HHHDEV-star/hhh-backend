using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("_avatar")]
[Index("AvatarType", "AvatarDisplay", Name = "avatar_type")]
public partial class Avatar
{
    [Key]
    [Column("avatar_id", TypeName = "mediumint unsigned")]
    public uint AvatarId { get; set; }

    [Column("avatar_file")]
    [StringLength(30)]
    public string AvatarFile { get; set; } = null!;

    [Column("avatar_name")]
    [StringLength(100)]
    public string AvatarName { get; set; } = null!;

    [Column("avatar_mimetype")]
    [StringLength(30)]
    public string AvatarMimetype { get; set; } = null!;

    [Column("avatar_created")]
    public int AvatarCreated { get; set; }

    [Column("avatar_display")]
    public byte AvatarDisplay { get; set; }

    [Column("avatar_weight")]
    public ushort AvatarWeight { get; set; }

    [Column("avatar_type")]
    [StringLength(1)]
    public string AvatarType { get; set; } = null!;
}

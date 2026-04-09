using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Keyless]
[Table("_avatar_user_link")]
[Index("AvatarId", "UserId", Name = "avatar_user_id")]
public partial class AvatarUserLink
{
    [Column("avatar_id", TypeName = "mediumint unsigned")]
    public uint AvatarId { get; set; }

    [Column("user_id", TypeName = "mediumint unsigned")]
    public uint UserId { get; set; }
}

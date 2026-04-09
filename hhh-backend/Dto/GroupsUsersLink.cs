using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("_groups_users_link")]
[Index("Groupid", "Uid", Name = "groupid_uid", IsUnique = true)]
[Index("Uid", Name = "uid")]
public partial class GroupsUsersLink
{
    [Key]
    [Column("linkid", TypeName = "mediumint unsigned")]
    public uint Linkid { get; set; }

    [Column("groupid")]
    public ushort Groupid { get; set; }

    [Column("uid", TypeName = "mediumint unsigned")]
    public uint Uid { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Keyless]
[Table("_online")]
[Index("OnlineModule", Name = "online_module")]
[Index("OnlineUid", Name = "online_uid")]
[Index("OnlineUpdated", Name = "online_updated")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class Online
{
    [Column("online_uid", TypeName = "mediumint unsigned")]
    public uint OnlineUid { get; set; }

    [Column("online_uname")]
    [StringLength(25)]
    public string OnlineUname { get; set; } = null!;

    [Column("online_updated")]
    public uint OnlineUpdated { get; set; }

    [Column("online_module")]
    public ushort OnlineModule { get; set; }

    [Column("online_ip")]
    [StringLength(15)]
    public string OnlineIp { get; set; } = null!;
}

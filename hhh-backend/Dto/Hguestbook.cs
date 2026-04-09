using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("_hguestbook")]
[Index("OwnerReaded", Name = "owner_readed")]
[Index("OwnerUid", Name = "owner_uid")]
[Index("PostTime", Name = "post_time")]
[Index("ReplyTime", Name = "reply_time")]
public partial class Hguestbook
{
    [Key]
    [Column("sn")]
    public uint Sn { get; set; }

    [Column("owner_uid")]
    public uint OwnerUid { get; set; }

    [Column("owner_reply", TypeName = "text")]
    public string OwnerReply { get; set; } = null!;

    [Column("owner_readed")]
    public byte OwnerReaded { get; set; }

    [Column("reply_time")]
    public uint ReplyTime { get; set; }

    [Column("poster_uid")]
    public uint PosterUid { get; set; }

    [Column("poster_nickname")]
    [StringLength(64)]
    public string PosterNickname { get; set; } = null!;

    [Column("poster_msg", TypeName = "text")]
    public string PosterMsg { get; set; } = null!;

    [Column("post_time")]
    public uint PostTime { get; set; }

    [Column("hide_msg")]
    public byte HideMsg { get; set; }

    [Column("poster_ip")]
    [StringLength(16)]
    public string PosterIp { get; set; } = null!;
}

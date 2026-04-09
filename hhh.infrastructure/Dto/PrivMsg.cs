using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Table("_priv_msgs")]
[Index("FromUserid", "MsgId", Name = "msgidfromuserid")]
[Index("ToUserid", Name = "to_userid")]
[Index("ToUserid", "ReadMsg", Name = "touseridreadmsg")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class PrivMsg
{
    [Key]
    [Column("msg_id", TypeName = "mediumint unsigned")]
    public uint MsgId { get; set; }

    [Column("msg_image")]
    [StringLength(100)]
    public string? MsgImage { get; set; }

    [Column("subject")]
    [StringLength(255)]
    public string Subject { get; set; } = null!;

    [Column("from_userid", TypeName = "mediumint unsigned")]
    public uint FromUserid { get; set; }

    [Column("to_userid", TypeName = "mediumint unsigned")]
    public uint ToUserid { get; set; }

    [Column("msg_time")]
    public uint MsgTime { get; set; }

    [Column("msg_text", TypeName = "text")]
    public string? MsgText { get; set; }

    [Column("read_msg")]
    public byte ReadMsg { get; set; }
}

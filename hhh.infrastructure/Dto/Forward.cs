using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Table("forward")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class Forward
{
    /// <summary>
    /// PK
    /// </summary>
    [Key]
    [Column("id")]
    public uint Id { get; set; }

    /// <summary>
    /// FK:_users.uid
    /// </summary>
    [Column("uid")]
    public int Uid { get; set; }

    /// <summary>
    /// 寄件者
    /// </summary>
    [Column("sender")]
    [StringLength(50)]
    public string Sender { get; set; } = null!;

    /// <summary>
    /// 收件者
    /// </summary>
    [Column("recipient")]
    [StringLength(500)]
    public string Recipient { get; set; } = null!;

    /// <summary>
    /// 網址
    /// </summary>
    [Column("url")]
    [StringLength(200)]
    public string Url { get; set; } = null!;

    /// <summary>
    /// 表題
    /// </summary>
    [Column("subject")]
    [StringLength(500)]
    public string Subject { get; set; } = null!;

    /// <summary>
    /// 內文
    /// </summary>
    [Column("content", TypeName = "text")]
    public string Content { get; set; } = null!;

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("ins_time", TypeName = "datetime")]
    public DateTime InsTime { get; set; }

    /// <summary>
    /// 寄出時間
    /// </summary>
    [Column("send_time", TypeName = "datetime")]
    public DateTime SendTime { get; set; }

    /// <summary>
    /// 是否寄出(N:否 / Y:是)
    /// </summary>
    [Column("send_status")]
    [StringLength(1)]
    public string SendStatus { get; set; } = null!;

    /// <summary>
    /// MessageID
    /// </summary>
    [Column("message_id")]
    [StringLength(60)]
    public string? MessageId { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("edm_log")]
public partial class EdmLog
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("id")]
    public uint Id { get; set; }

    /// <summary>
    /// 收件群組
    /// </summary>
    [Column("user_group")]
    public byte UserGroup { get; set; }

    /// <summary>
    /// 測試收信人
    /// </summary>
    [Column("test_mail")]
    [StringLength(200)]
    public string? TestMail { get; set; }

    /// <summary>
    /// 信件主旨
    /// </summary>
    [Column("subject")]
    [StringLength(40)]
    public string? Subject { get; set; }

    /// <summary>
    /// 信件URL
    /// </summary>
    [Column("url")]
    [StringLength(128)]
    public string? Url { get; set; }

    /// <summary>
    /// 信件內容
    /// </summary>
    [Column("content")]
    public string? Content { get; set; }

    /// <summary>
    /// 是否已寄送
    /// </summary>
    [Column("is_send")]
    public byte IsSend { get; set; }

    /// <summary>
    /// 寄送時間
    /// </summary>
    [Column("user_count")]
    public int UserCount { get; set; }

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("create_time", TypeName = "timestamp")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新時間
    /// </summary>
    [Column("is_send_time", TypeName = "datetime")]
    public DateTime IsSendTime { get; set; }
}

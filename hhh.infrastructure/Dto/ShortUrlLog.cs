using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Table("short_url_log")]
public partial class ShortUrlLog
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("id")]
    public ulong Id { get; set; }

    /// <summary>
    /// short_url pk
    /// </summary>
    [Column("short_url_id")]
    public int ShortUrlId { get; set; }

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("create_time", TypeName = "timestamp")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// ip
    /// </summary>
    [Column("ip")]
    [StringLength(128)]
    public string? Ip { get; set; }

    /// <summary>
    /// 是否是否為手機(0:否 1:是)
    /// </summary>
    [Required]
    [Column("is_mobile")]
    public bool? IsMobile { get; set; }

    /// <summary>
    /// 會員ID
    /// </summary>
    [Column("user_id")]
    public int UserId { get; set; }

    /// <summary>
    /// 追蹤碼
    /// </summary>
    [Column("track")]
    [StringLength(200)]
    public string? Track { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("user_log")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class UserLog
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("log_id")]
    public int LogId { get; set; }

    /// <summary>
    /// 使用者ID
    /// </summary>
    [Column("user_id")]
    public int? UserId { get; set; }

    /// <summary>
    /// 設計師ID
    /// </summary>
    [Column("designer_id")]
    public int? DesignerId { get; set; }

    /// <summary>
    /// 廠商ID
    /// </summary>
    [Column("brand_id")]
    public int? BrandId { get; set; }

    /// <summary>
    /// 瀏覽網頁
    /// </summary>
    [Column("page_type")]
    [StringLength(20)]
    public string PageType { get; set; } = null!;

    /// <summary>
    /// 網頁對應ID
    /// </summary>
    [Column("table_id")]
    public int TableId { get; set; }

    /// <summary>
    /// IP
    /// </summary>
    [Column("ip")]
    [StringLength(50)]
    public string Ip { get; set; } = null!;

    /// <summary>
    /// 瀏覽時間
    /// </summary>
    [Column("ins_time", TypeName = "datetime")]
    public DateTime InsTime { get; set; }
}

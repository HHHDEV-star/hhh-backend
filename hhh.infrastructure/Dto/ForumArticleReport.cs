using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

/// <summary>
/// 檢舉文章
/// </summary>
[Table("forum_article_report")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class ForumArticleReport
{
    [Key]
    [Column("report_id")]
    public uint ReportId { get; set; }

    /// <summary>
    /// 文章類型 0:本文 1:回覆	
    /// </summary>
    [Column("report_type")]
    public byte ReportType { get; set; }

    /// <summary>
    /// 文章或回覆文章id	
    /// </summary>
    [Column("data_id")]
    public uint DataId { get; set; }

    /// <summary>
    /// 使用者編號	
    /// </summary>
    [Column("uid")]
    public uint Uid { get; set; }

    /// <summary>
    /// ip
    /// </summary>
    [Column("ip")]
    [StringLength(20)]
    public string Ip { get; set; } = null!;

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("date_added", TypeName = "datetime")]
    public DateTime DateAdded { get; set; }

    /// <summary>
    /// 是否寄出(0 : 未寄出 / 1 : 已寄出
    /// </summary>
    [Column("send_email")]
    [StringLength(1)]
    public string SendEmail { get; set; } = null!;

    /// <summary>
    /// 寄出時間
    /// </summary>
    [Column("send_datetime", TypeName = "datetime")]
    public DateTime SendDatetime { get; set; }
}

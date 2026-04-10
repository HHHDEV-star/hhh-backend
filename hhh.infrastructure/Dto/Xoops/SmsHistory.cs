using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

/// <summary>
/// 簡訊傳送紀錄
/// </summary>
[Table("sms_history")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class SmsHistory
{
    [Key]
    [Column("seq")]
    public int Seq { get; set; }

    [Column("phone")]
    [StringLength(15)]
    public string Phone { get; set; } = null!;

    [Column("message")]
    [StringLength(150)]
    public string Message { get; set; } = null!;

    [Column("send_time")]
    public DateOnly SendTime { get; set; }

    [Column("update_time", TypeName = "datetime")]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// 0:待查詢 1:完成 2:失敗 
    /// </summary>
    [Column("status")]
    public sbyte Status { get; set; }

    [Column("SourceProdID")]
    [StringLength(32)]
    public string SourceProdId { get; set; } = null!;

    [Column("SourceMsgID")]
    [StringLength(32)]
    public string SourceMsgId { get; set; } = null!;

    /// <summary>
    /// 回應碼
    /// </summary>
    [Column("response")]
    [StringLength(10)]
    public string? Response { get; set; }
}

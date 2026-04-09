using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

/// <summary>
/// go收納
/// </summary>
[Table("go_storage")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class GoStorage
{
    [Key]
    [Column("seq")]
    public int Seq { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    [Column("name")]
    [StringLength(50)]
    public string? Name { get; set; }

    /// <summary>
    /// 電話
    /// </summary>
    [Column("phone")]
    [StringLength(20)]
    public string? Phone { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    [Column("address", TypeName = "text")]
    public string? Address { get; set; }

    /// <summary>
    /// 電子郵件	
    /// </summary>
    [Column("email")]
    [StringLength(50)]
    public string? Email { get; set; }

    /// <summary>
    /// 裝修日期
    /// </summary>
    [Column("time")]
    public DateOnly? Time { get; set; }

    /// <summary>
    /// 預算
    /// </summary>
    [Column("budget")]
    [StringLength(20)]
    public string? Budget { get; set; }

    /// <summary>
    /// 坪數
    /// </summary>
    [Column("pin")]
    [StringLength(20)]
    public string? Pin { get; set; }

    /// <summary>
    /// 成人小孩老人
    /// </summary>
    [Column("pattern")]
    [StringLength(30)]
    public string? Pattern { get; set; }

    /// <summary>
    /// 需求格局
    /// </summary>
    [Column("layout")]
    [StringLength(30)]
    public string? Layout { get; set; }

    /// <summary>
    /// 同意接受案件竣工後將進行紀錄，並作為媒體宣傳的露出使用
    /// </summary>
    [Column("is_agree")]
    public sbyte IsAgree { get; set; }

    /// <summary>
    /// 是否寄出(Y:是 / N:否	
    /// </summary>
    [Column("send_status")]
    [StringLength(1)]
    public string SendStatus { get; set; } = null!;

    /// <summary>
    /// 寄出時間
    /// </summary>
    [Column("send_datetime", TypeName = "datetime")]
    public DateTime SendDatetime { get; set; }

    [Column("create_time", TypeName = "datetime")]
    public DateTime CreateTime { get; set; }
}

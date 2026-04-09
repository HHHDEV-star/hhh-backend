using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("contact")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class Contact
{
    /// <summary>
    /// PK
    /// </summary>
    [Key]
    [Column("id")]
    public uint Id { get; set; }

    /// <summary>
    /// 姓名 / 公司名
    /// </summary>
    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 公司名稱
    /// </summary>
    [Column("company")]
    [StringLength(25)]
    public string Company { get; set; } = null!;

    /// <summary>
    /// 聯繫電話
    /// </summary>
    [Column("phone")]
    [StringLength(50)]
    public string Phone { get; set; } = null!;

    /// <summary>
    /// 電子郵件
    /// </summary>
    [Column("email")]
    [StringLength(50)]
    public string Email { get; set; } = null!;

    /// <summary>
    /// 是否為FB帳號(Y:是 / N:否
    /// </summary>
    [Column("is_fb")]
    [StringLength(1)]
    public string IsFb { get; set; } = null!;

    /// <summary>
    /// 主旨
    /// </summary>
    [Column("subject")]
    [StringLength(100)]
    public string Subject { get; set; } = null!;

    /// <summary>
    /// 內容
    /// </summary>
    [Column("content", TypeName = "text")]
    public string Content { get; set; } = null!;

    /// <summary>
    /// IP
    /// </summary>
    [Column("ip")]
    [StringLength(30)]
    public string Ip { get; set; } = null!;

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("create_time", TypeName = "datetime")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 是否寄出(N:未寄出 / Y:已寄出)
    /// </summary>
    [Column("send")]
    [StringLength(1)]
    public string Send { get; set; } = null!;

    /// <summary>
    /// 寄出時間
    /// </summary>
    [Column("send_time", TypeName = "datetime")]
    public DateTime SendTime { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("decoration")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class Decoration
{
    /// <summary>
    /// PK
    /// </summary>
    [Key]
    [Column("id")]
    public uint Id { get; set; }

    /// <summary>
    /// 電子郵件
    /// </summary>
    [Column("email")]
    [StringLength(100)]
    public string Email { get; set; } = null!;

    /// <summary>
    /// 聯絡姓名
    /// </summary>
    [Column("name")]
    [StringLength(30)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 聯絡電話
    /// </summary>
    [Column("phone")]
    [StringLength(30)]
    public string Phone { get; set; } = null!;

    /// <summary>
    /// 所在地區
    /// </summary>
    [Column("area")]
    [StringLength(10)]
    public string Area { get; set; } = null!;

    /// <summary>
    /// 您目前房屋類型
    /// </summary>
    [Column("type")]
    [StringLength(10)]
    public string Type { get; set; } = null!;

    /// <summary>
    /// 房屋實際坪數
    /// </summary>
    [Column("pin")]
    [StringLength(20)]
    public string Pin { get; set; } = null!;

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("create_time", TypeName = "datetime")]
    public DateTime CreateTime { get; set; }
}

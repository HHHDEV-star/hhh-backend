using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("precise")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class Precise
{
    /// <summary>
    /// PK
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// 身份別
    /// </summary>
    [Column("identity")]
    [StringLength(20)]
    public string Identity { get; set; } = null!;

    /// <summary>
    /// 郵件
    /// </summary>
    [Column("email")]
    [StringLength(100)]
    public string Email { get; set; } = null!;

    /// <summary>
    /// 中文姓名
    /// </summary>
    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 公司名稱
    /// </summary>
    [Column("company")]
    [StringLength(100)]
    public string Company { get; set; } = null!;

    /// <summary>
    /// 手機號碼
    /// </summary>
    [Column("mobile")]
    [StringLength(15)]
    public string Mobile { get; set; } = null!;

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("create_time", TypeName = "datetime")]
    public DateTime CreateTime { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

/// <summary>
/// 幸福回娘家
/// </summary>
[Table("brief")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class Brief
{
    [Key]
    [Column("brief_id")]
    public int BriefId { get; set; }

    [Column("name")]
    [StringLength(20)]
    public string Name { get; set; } = null!;

    [Column("email", TypeName = "text")]
    public string Email { get; set; } = null!;

    [Column("phone")]
    [StringLength(20)]
    public string Phone { get; set; } = null!;

    [Column("area")]
    [StringLength(10)]
    public string Area { get; set; } = null!;

    /// <summary>
    /// 預算
    /// </summary>
    [Column("fee")]
    [StringLength(20)]
    public string Fee { get; set; } = null!;

    /// <summary>
    /// 名片圖檔
    /// </summary>
    [Column("image")]
    [StringLength(100)]
    public string? Image { get; set; }

    [Column("pin")]
    [StringLength(10)]
    public string Pin { get; set; } = null!;

    [Column("create_time", TypeName = "datetime")]
    public DateTime CreateTime { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("photos_edm")]
[Index("Email", Name = "email", IsUnique = true)]
[MySqlCollation("utf8mb3_general_ci")]
public partial class PhotosEdm
{
    [Key]
    [Column("id")]
    public uint Id { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    [Column("name")]
    [StringLength(200)]
    public string Name { get; set; } = null!;

    [Column("email")]
    [StringLength(200)]
    public string Email { get; set; } = null!;

    /// <summary>
    /// 是否訂閱(0:否 / 1:是
    /// </summary>
    [Column("onoff")]
    [StringLength(1)]
    public string Onoff { get; set; } = null!;

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("update_time", TypeName = "datetime")]
    public DateTime UpdateTime { get; set; }
}

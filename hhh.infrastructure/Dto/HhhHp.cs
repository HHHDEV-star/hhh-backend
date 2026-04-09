using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Table("_hhh_hp")]
public partial class HhhHp
{
    [Key]
    [Column("id")]
    public uint Id { get; set; }

    /// <summary>
    /// 名字
    /// </summary>
    [Column("name")]
    [StringLength(256)]
    public string? Name { get; set; }

    /// <summary>
    /// 手機
    /// </summary>
    [Column("phone")]
    [StringLength(20)]
    public string? Phone { get; set; }

    /// <summary>
    /// 郵件
    /// </summary>
    [Column("email")]
    [StringLength(50)]
    public string? Email { get; set; }

    /// <summary>
    /// 城市
    /// </summary>
    [Column("city")]
    [StringLength(45)]
    public string? City { get; set; }

    /// <summary>
    /// 地區
    /// </summary>
    [Column("region")]
    [StringLength(256)]
    public string? Region { get; set; }

    /// <summary>
    /// (1:現在有需求 0:之後考量)
    /// </summary>
    [Column("is_request")]
    public byte IsRequest { get; set; }

    /// <summary>
    /// (1:同意收到 0:沒勾選)
    /// </summary>
    [Column("is_agree")]
    public byte IsAgree { get; set; }

    /// <summary>
    /// 創建時間
    /// </summary>
    [Column("create_time", TypeName = "datetime")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 建案編號
    /// </summary>
    [Column("hp_builder_id")]
    [StringLength(45)]
    public string? HpBuilderId { get; set; }
}

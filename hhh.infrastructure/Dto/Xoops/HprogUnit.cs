using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("_hprog_unit")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class HprogUnit
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("sn")]
    public uint Sn { get; set; }

    /// <summary>
    /// 設計師ID
    /// </summary>
    [Column("hdesigner_id")]
    public uint HdesignerId { get; set; }

    /// <summary>
    /// 個案ID
    /// </summary>
    [Column("hcase_id")]
    public uint HcaseId { get; set; }

    /// <summary>
    /// 廠商ID
    /// </summary>
    [Column("hbrand_id")]
    public uint HbrandId { get; set; }

    /// <summary>
    /// 影片ID
    /// </summary>
    [Column("hvideo_id")]
    public uint HvideoId { get; set; }

    /// <summary>
    /// 專欄ID
    /// </summary>
    [Column("hcolumn_id")]
    public uint HcolumnId { get; set; }

    /// <summary>
    /// 單元名稱
    /// </summary>
    [Column("title")]
    [StringLength(128)]
    public string Title { get; set; } = null!;

    /// <summary>
    /// 影片說明
    /// </summary>
    [Column("desc", TypeName = "text")]
    public string Desc { get; set; } = null!;

    /// <summary>
    /// 影片資訊
    /// </summary>
    [Column("info", TypeName = "text")]
    public string Info { get; set; } = null!;

    /// <summary>
    /// 影片縮圖
    /// </summary>
    [Column("timg")]
    [StringLength(128)]
    public string Timg { get; set; } = null!;

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("creat_time", TypeName = "timestamp")]
    public DateTime CreatTime { get; set; }
}

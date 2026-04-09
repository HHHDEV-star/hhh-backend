using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Table("_htopic")]
[Index("CreateTime", Name = "create_time")]
[Index("Onoff", Name = "onoff")]
[Index("Viewed", Name = "viewed")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class Htopic
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("htopic_id")]
    public uint HtopicId { get; set; }

    /// <summary>
    /// 主題名稱
    /// </summary>
    [Column("title")]
    [StringLength(64)]
    public string Title { get; set; } = null!;

    /// <summary>
    /// 主題敘述
    /// </summary>
    [Column("desc", TypeName = "text")]
    public string Desc { get; set; } = null!;

    /// <summary>
    /// logo
    /// </summary>
    [Column("logo")]
    [StringLength(64)]
    public string Logo { get; set; } = null!;

    /// <summary>
    /// 個案IDs
    /// </summary>
    [Column("strarr_hcase_id")]
    [StringLength(128)]
    public string StrarrHcaseId { get; set; } = null!;

    /// <summary>
    /// 專欄IDs
    /// </summary>
    [Column("strarr_hcolumn_id")]
    [StringLength(128)]
    public string StrarrHcolumnId { get; set; } = null!;

    /// <summary>
    /// 觀看數
    /// </summary>
    [Column("viewed")]
    public uint Viewed { get; set; }

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("create_time", TypeName = "timestamp")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 上線狀態(0:否1:是)
    /// </summary>
    [Column("onoff")]
    public byte Onoff { get; set; }

    /// <summary>
    /// 寄送次數
    /// </summary>
    [Column("is_send")]
    public bool IsSend { get; set; }

    [Column("seo_image")]
    [StringLength(128)]
    public string? SeoImage { get; set; }

    [Column("seo_title")]
    [StringLength(128)]
    public string? SeoTitle { get; set; }

    [Column("seo_description", TypeName = "text")]
    public string? SeoDescription { get; set; }

    /// <summary>
    /// 更新時間
    /// </summary>
    [Column("update_time", TypeName = "timestamp")]
    public DateTime UpdateTime { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("_htopic2")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class Htopic2
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("id")]
    public uint Id { get; set; }

    /// <summary>
    /// 名稱
    /// </summary>
    [Column("title")]
    [StringLength(128)]
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
    /// 設計師ID
    /// </summary>
    [Column("strarr_hdesigner_id", TypeName = "text")]
    public string StrarrHdesignerId { get; set; } = null!;

    /// <summary>
    /// 個案ID
    /// </summary>
    [Column("strarr_hcase_id", TypeName = "text")]
    public string StrarrHcaseId { get; set; } = null!;

    /// <summary>
    /// 影音ID
    /// </summary>
    [Column("strarr_hvideo_id", TypeName = "text")]
    public string StrarrHvideoId { get; set; } = null!;

    /// <summary>
    /// 專欄ID
    /// </summary>
    [Column("strarr_hcolumn_id", TypeName = "text")]
    public string StrarrHcolumnId { get; set; } = null!;

    /// <summary>
    /// 上線狀態(0:否1:是)
    /// </summary>
    [Column("onoff")]
    public byte Onoff { get; set; }
}

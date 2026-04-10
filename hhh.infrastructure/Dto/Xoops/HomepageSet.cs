using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("homepage_set")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class HomepageSet
{
    /// <summary>
    /// PK
    /// </summary>
    [Key]
    [Column("ps_id")]
    public uint PsId { get; set; }

    /// <summary>
    /// 主題編號(廣告&amp;影片&amp;內容)
    /// </summary>
    [Column("mapping_id")]
    public uint MappingId { get; set; }

    /// <summary>
    /// 元素排序
    /// </summary>
    [Column("inner_sort")]
    public byte InnerSort { get; set; }

    /// <summary>
    /// 主題類型(case:個案 video:影音 column:居家 product:產品 ad:內容廣告 fans:粉絲推薦 week:本週推薦)
    /// </summary>
    [Column("theme_type")]
    [StringLength(10)]
    public string ThemeType { get; set; } = null!;

    /// <summary>
    /// FK : outer_site_set.oss_id
    /// </summary>
    [Column("outer_set")]
    public uint OuterSet { get; set; }

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("create_time", TypeName = "datetime")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新時間
    /// </summary>
    [Column("update_time", TypeName = "datetime")]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// 上線狀態(N:關Y:開)	
    /// </summary>
    [Column("onoff")]
    [StringLength(1)]
    public string Onoff { get; set; } = null!;

    /// <summary>
    /// 開始時間
    /// </summary>
    [Column("start_time", TypeName = "datetime")]
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 結束時間
    /// </summary>
    [Column("end_time", TypeName = "datetime")]
    public DateTime? EndTime { get; set; }
}

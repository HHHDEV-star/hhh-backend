using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("outer_site_set")]
public partial class OuterSiteSet
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("oss_id")]
    public uint OssId { get; set; }

    /// <summary>
    /// 區塊標題
    /// </summary>
    [Column("title")]
    [StringLength(20)]
    [MySqlCollation("utf8mb3_general_ci")]
    public string Title { get; set; } = null!;

    /// <summary>
    /// 區塊右上角更多(文字)
    /// </summary>
    [Column("more_copy")]
    [StringLength(10)]
    [MySqlCollation("utf8mb3_general_ci")]
    public string? MoreCopy { get; set; }

    /// <summary>
    /// 更多連結
    /// </summary>
    [Column("more_url")]
    [StringLength(60)]
    [MySqlCollation("utf8mb3_general_ci")]
    public string? MoreUrl { get; set; }

    /// <summary>
    /// 區塊位置
    /// </summary>
    [Column("sort")]
    public byte? Sort { get; set; }

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
    [MySqlCollation("utf8mb3_general_ci")]
    public string Onoff { get; set; } = null!;

    /// <summary>
    /// 主題類型(case:個案 video:影音 column:居家 product:產品 ad:內容廣告 fans:粉絲推薦 week:本週推薦)
    /// </summary>
    [Column("theme_type")]
    [StringLength(10)]
    [MySqlCollation("utf8mb3_general_ci")]
    public string ThemeType { get; set; } = null!;

    /// <summary>
    /// 最大行數
    /// </summary>
    [Column("max_row")]
    public sbyte MaxRow { get; set; }
}

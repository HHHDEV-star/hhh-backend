using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("psychological")]
[MySqlCharSet("utf8mb4")]
[MySqlCollation("utf8mb4_0900_ai_ci")]
public partial class Psychological
{
    /// <summary>
    /// PK
    /// </summary>
    [Key]
    [Column("id")]
    public uint Id { get; set; }

    /// <summary>
    /// 標題
    /// </summary>
    [Column("title")]
    [StringLength(200)]
    public string Title { get; set; } = null!;

    /// <summary>
    /// 圖示位置
    /// </summary>
    [Column("icon")]
    [StringLength(200)]
    public string Icon { get; set; } = null!;

    /// <summary>
    /// FB分享圖示
    /// </summary>
    [Column("icon_for_fb")]
    [StringLength(200)]
    public string IconForFb { get; set; } = null!;

    /// <summary>
    /// 內容
    /// </summary>
    [Column("content", TypeName = "text")]
    public string Content { get; set; } = null!;

    /// <summary>
    /// 人氣數
    /// </summary>
    [Column("viewed", TypeName = "mediumint unsigned")]
    public uint Viewed { get; set; }

    /// <summary>
    /// 選項A_標題
    /// </summary>
    [Column("option_a_title")]
    [StringLength(200)]
    public string OptionATitle { get; set; } = null!;

    /// <summary>
    /// 選項A_內容
    /// </summary>
    [Column("option_a", TypeName = "text")]
    public string OptionA { get; set; } = null!;

    /// <summary>
    /// 選項B_標題
    /// </summary>
    [Column("option_b_title")]
    [StringLength(200)]
    public string OptionBTitle { get; set; } = null!;

    /// <summary>
    /// 選項B_內容
    /// </summary>
    [Column("option_b", TypeName = "text")]
    public string OptionB { get; set; } = null!;

    /// <summary>
    /// 選項C_標題
    /// </summary>
    [Column("option_c_title")]
    [StringLength(200)]
    public string OptionCTitle { get; set; } = null!;

    /// <summary>
    /// 選項C_內容
    /// </summary>
    [Column("option_c", TypeName = "text")]
    public string OptionC { get; set; } = null!;

    /// <summary>
    /// 選項D_標題
    /// </summary>
    [Column("option_d_title")]
    [StringLength(200)]
    public string OptionDTitle { get; set; } = null!;

    /// <summary>
    /// 選項D_內容
    /// </summary>
    [Column("option_d", TypeName = "text")]
    public string OptionD { get; set; } = null!;

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("create_time", TypeName = "datetime")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 選項A連結
    /// </summary>
    [Column("option_a_link")]
    [StringLength(100)]
    public string? OptionALink { get; set; }

    /// <summary>
    /// 選項B連結
    /// </summary>
    [Column("option_b_link")]
    [StringLength(100)]
    public string? OptionBLink { get; set; }

    /// <summary>
    /// 選項C連結
    /// </summary>
    [Column("option_c_link")]
    [StringLength(100)]
    public string? OptionCLink { get; set; }

    /// <summary>
    /// 選項D連結
    /// </summary>
    [Column("option_d_link")]
    [StringLength(100)]
    public string? OptionDLink { get; set; }

    /// <summary>
    /// 是否開啟(1:開 / 0:關
    /// </summary>
    [Column("onoff")]
    [StringLength(1)]
    public string Onoff { get; set; } = null!;
}

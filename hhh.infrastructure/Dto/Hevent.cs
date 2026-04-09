using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Table("_hevent")]
public partial class Hevent
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("hevent_id")]
    public uint HeventId { get; set; }

    /// <summary>
    /// 獎品ID
    /// </summary>
    [Column("hprize_id")]
    public uint HprizeId { get; set; }

    /// <summary>
    /// logo路徑
    /// </summary>
    [Column("elogo")]
    [StringLength(128)]
    public string Elogo { get; set; } = null!;

    /// <summary>
    /// 活動開始時間
    /// </summary>
    [Column("estart", TypeName = "datetime")]
    public DateTime Estart { get; set; }

    /// <summary>
    /// 活動結束時間
    /// </summary>
    [Column("eend", TypeName = "datetime")]
    public DateTime Eend { get; set; }

    /// <summary>
    /// 活動類別
    /// </summary>
    [Column("type")]
    [StringLength(32)]
    public string Type { get; set; } = null!;

    /// <summary>
    /// 活動標題
    /// </summary>
    [Column("title")]
    [StringLength(128)]
    public string Title { get; set; } = null!;

    /// <summary>
    /// 活動內文描述
    /// </summary>
    [Column("desc", TypeName = "text")]
    public string Desc { get; set; } = null!;

    /// <summary>
    /// 活動連結
    /// </summary>
    [Column("elink")]
    [StringLength(128)]
    public string Elink { get; set; } = null!;

    /// <summary>
    /// 得獎名單標題
    /// </summary>
    [Column("winner_title")]
    [StringLength(128)]
    public string WinnerTitle { get; set; } = null!;

    /// <summary>
    /// 得獎名單內文
    /// </summary>
    [Column("winner_html", TypeName = "text")]
    public string WinnerHtml { get; set; } = null!;

    /// <summary>
    /// 問題
    /// </summary>
    [Column("question", TypeName = "text")]
    public string Question { get; set; } = null!;

    /// <summary>
    /// 答案
    /// </summary>
    [Column("answer")]
    [StringLength(128)]
    public string Answer { get; set; } = null!;

    /// <summary>
    /// 點擊數
    /// </summary>
    [Column("clicks")]
    public int Clicks { get; set; }
}

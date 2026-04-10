using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

/// <summary>
/// 關鍵字搜尋紀錄
/// </summary>
[Table("search_history")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class SearchHistory
{
    /// <summary>
    /// id
    /// </summary>
    [Key]
    [Column("search_id")]
    public uint SearchId { get; set; }

    /// <summary>
    /// 關鍵字
    /// </summary>
    [Column("keyword")]
    [StringLength(255)]
    public string Keyword { get; set; } = null!;

    /// <summary>
    /// 次數
    /// </summary>
    [Column("today_count")]
    public uint TodayCount { get; set; }

    /// <summary>
    /// 日期
    /// </summary>
    [Column("date_added")]
    public DateOnly DateAdded { get; set; }
}

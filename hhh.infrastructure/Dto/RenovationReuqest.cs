using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Table("renovation_reuqest")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class RenovationReuqest
{
    /// <summary>
    /// PK
    /// </summary>
    [Key]
    [Column("id")]
    public uint Id { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    [Column("name")]
    [StringLength(50)]
    public string? Name { get; set; }

    /// <summary>
    /// 性別(M:男性 / F:女性)
    /// </summary>
    [Column("sex")]
    [StringLength(1)]
    public string? Sex { get; set; }

    /// <summary>
    /// 電話
    /// </summary>
    [Column("phone")]
    [StringLength(20)]
    public string? Phone { get; set; }

    /// <summary>
    /// 電子郵件
    /// </summary>
    [Column("email")]
    [StringLength(50)]
    public string? Email { get; set; }

    /// <summary>
    /// 是否為FB帳號(Y:是 / N:否
    /// </summary>
    [Column("is_fb")]
    [StringLength(1)]
    public string IsFb { get; set; } = null!;

    /// <summary>
    /// 所在縣市
    /// </summary>
    [Column("area")]
    [StringLength(20)]
    public string? Area { get; set; }

    /// <summary>
    /// 希望裝修時間
    /// </summary>
    [Column("time")]
    public DateOnly? Time { get; set; }

    /// <summary>
    /// 房屋類型
    /// </summary>
    [Column("type")]
    [StringLength(20)]
    public string? Type { get; set; }

    /// <summary>
    /// 房屋型態
    /// </summary>
    [Column("mode")]
    [StringLength(20)]
    public string? Mode { get; set; }

    /// <summary>
    /// 裝修預算
    /// </summary>
    [Column("budget")]
    [StringLength(20)]
    public string? Budget { get; set; }

    /// <summary>
    /// 裝修坪數
    /// </summary>
    [Column("pin")]
    [StringLength(20)]
    public string? Pin { get; set; }

    /// <summary>
    /// 裝修格局
    /// </summary>
    [Column("pattern")]
    [StringLength(20)]
    public string? Pattern { get; set; }

    /// <summary>
    /// 風格需求
    /// </summary>
    [Column("style")]
    [StringLength(20)]
    public string? Style { get; set; }

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("ctime", TypeName = "datetime")]
    public DateTime? Ctime { get; set; }

    /// <summary>
    /// 是否寄出(Y:是 / N:否
    /// </summary>
    [Column("send_status")]
    [StringLength(1)]
    public string SendStatus { get; set; } = null!;

    /// <summary>
    /// 寄出時間
    /// </summary>
    [Column("send_datetime", TypeName = "datetime")]
    public DateTime SendDatetime { get; set; }

    [Column("site_lists", TypeName = "text")]
    public string? SiteLists { get; set; }

    /// <summary>
    /// 全國設計師id
    /// </summary>
    [Column("bldsno")]
    public int Bldsno { get; set; }

    /// <summary>
    /// UTM_SOURCE
    /// </summary>
    [Column("utm_source")]
    [StringLength(20)]
    public string? UtmSource { get; set; }

    [Column("loan", TypeName = "bit(1)")]
    public ulong? Loan { get; set; }
}

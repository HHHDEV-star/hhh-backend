using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("execute_detail")]
[Index("IsComplete", Name = "is_complete")]
[Index("IsDelete", Name = "is_delete")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class ExecuteDetail
{
    /// <summary>
    /// PK
    /// </summary>
    [Key]
    [Column("exd_id")]
    public uint ExdId { get; set; }

    /// <summary>
    /// FK：execute_form
    /// </summary>
    [Column("exf_id")]
    public uint ExfId { get; set; }

    /// <summary>
    /// 大項目
    /// </summary>
    [Column("lv1")]
    [StringLength(50)]
    public string Lv1 { get; set; } = null!;

    [Column("category")]
    [StringLength(50)]
    public string Category { get; set; } = null!;

    /// <summary>
    /// 小項目
    /// </summary>
    [Column("lv2")]
    [StringLength(50)]
    public string Lv2 { get; set; } = null!;

    /// <summary>
    /// 建立幾個項目
    /// </summary>
    [Column("create_num")]
    public byte CreateNum { get; set; }

    /// <summary>
    /// 預計排程日期
    /// </summary>
    [Column("set_date")]
    public DateOnly SetDate { get; set; }

    /// <summary>
    /// 幾天前發送通知
    /// </summary>
    [Column("alert_days")]
    [StringLength(10)]
    public string AlertDays { get; set; } = null!;

    /// <summary>
    /// 第一次提醒日期
    /// </summary>
    [Column("alert_date_1")]
    public DateOnly? AlertDate1 { get; set; }

    /// <summary>
    /// 第二次提醒日期
    /// </summary>
    [Column("alert_date_2")]
    public DateOnly? AlertDate2 { get; set; }

    /// <summary>
    /// 備註
    /// </summary>
    [Column("note", TypeName = "text")]
    public string Note { get; set; } = null!;

    /// <summary>
    /// 執行部門(郵件)
    /// </summary>
    [Column("execute_man")]
    [StringLength(50)]
    public string? ExecuteMan { get; set; }

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
    /// 是否寄出通知(N：否 / Y：是)
    /// </summary>
    [Column("is_send")]
    [StringLength(1)]
    public string IsSend { get; set; } = null!;

    /// <summary>
    /// 是否同意執行
    /// </summary>
    [Column("is_allow")]
    [StringLength(1)]
    public string IsAllow { get; set; } = null!;

    /// <summary>
    /// 寄出時間
    /// </summary>
    [Column("send_time", TypeName = "datetime")]
    public DateTime SendTime { get; set; }

    /// <summary>
    /// 是否刪除(Y:是 / N:否)
    /// </summary>
    [Column("is_delete")]
    [StringLength(1)]
    public string IsDelete { get; set; } = null!;

    /// <summary>
    /// 是否完成 (Y:是 / N:否/ D:轉約
    /// </summary>
    [Column("is_complete")]
    [StringLength(1)]
    public string IsComplete { get; set; } = null!;

    /// <summary>
    /// 完成人(郵件)
    /// </summary>
    [Column("complete_man")]
    [StringLength(50)]
    public string CompleteMan { get; set; } = null!;

    /// <summary>
    /// 完成時間
    /// </summary>
    [Column("complete_time", TypeName = "datetime")]
    public DateTime CompleteTime { get; set; }

    /// <summary>
    /// 合約金額(未稅)
    /// </summary>
    [Column("price")]
    public uint Price { get; set; }
}

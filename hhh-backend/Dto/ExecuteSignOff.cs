using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

/// <summary>
/// 簽核資料
/// </summary>
[Table("execute_sign_off")]
[Index("FromPk", Name = "from_pk")]
[Index("FromTable", Name = "from_table")]
[Index("Sort", Name = "sort")]
[MySqlCharSet("utf8mb4")]
[MySqlCollation("utf8mb4_0900_ai_ci")]
public partial class ExecuteSignOff
{
    /// <summary>
    /// PK
    /// </summary>
    [Key]
    [Column("eso_id")]
    public uint EsoId { get; set; }

    /// <summary>
    /// 審核排序
    /// </summary>
    [Column("sort")]
    public byte Sort { get; set; }

    /// <summary>
    /// 覆核人
    /// </summary>
    [Column("review")]
    [StringLength(50)]
    public string? Review { get; set; }

    /// <summary>
    /// 同意
    /// </summary>
    [Column("allow")]
    [StringLength(1)]
    public string Allow { get; set; } = null!;

    /// <summary>
    /// 同意時間
    /// </summary>
    [Column("allow_datetime", TypeName = "datetime")]
    public DateTime AllowDatetime { get; set; }

    /// <summary>
    /// 拒絕
    /// </summary>
    [Column("deny")]
    [StringLength(1)]
    public string Deny { get; set; } = null!;

    /// <summary>
    /// 拒絕時間
    /// </summary>
    [Column("deny_datetime", TypeName = "datetime")]
    public DateTime DenyDatetime { get; set; }

    /// <summary>
    /// 備註
    /// </summary>
    [Column("note", TypeName = "text")]
    public string Note { get; set; } = null!;

    /// <summary>
    /// 覆核對象
    /// </summary>
    [Column("identity")]
    [StringLength(20)]
    public string Identity { get; set; } = null!;

    /// <summary>
    /// 資料表
    /// </summary>
    [Column("from_table")]
    [StringLength(20)]
    public string FromTable { get; set; } = null!;

    /// <summary>
    /// PK
    /// </summary>
    [Column("from_pk")]
    public uint FromPk { get; set; }

    /// <summary>
    /// 時間
    /// </summary>
    [Column("datetime", TypeName = "datetime")]
    public DateTime Datetime { get; set; }
}

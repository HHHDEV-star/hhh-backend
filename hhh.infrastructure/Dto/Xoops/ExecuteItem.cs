using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

/// <summary>
/// 執行項目表
/// </summary>
[Table("execute_items")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class ExecuteItem
{
    [Key]
    [Column("execute_items_id")]
    public int ExecuteItemsId { get; set; }

    /// <summary>
    /// 合約名稱
    /// </summary>
    [Column("contract_name")]
    [StringLength(50)]
    public string? ContractName { get; set; }

    /// <summary>
    /// 分類
    /// </summary>
    [Column("contract_type")]
    [StringLength(20)]
    public string? ContractType { get; set; }

    /// <summary>
    /// 大項目
    /// </summary>
    [Column("name")]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 執行項
    /// </summary>
    [Column("email")]
    [StringLength(50)]
    public string Email { get; set; } = null!;

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("create_time", TypeName = "datetime")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 是否刪除(0:否 / 1:是
    /// </summary>
    [Column("is_del")]
    public sbyte IsDel { get; set; }
}

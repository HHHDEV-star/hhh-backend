using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Table("quotation_brand")]
public partial class QuotationBrand
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// 金額(未稅/萬)
    /// </summary>
    [Column("price")]
    [StringLength(45)]
    public string Price { get; set; } = null!;

    /// <summary>
    /// 備註
    /// </summary>
    [Column("note", TypeName = "text")]
    public string? Note { get; set; }

    /// <summary>
    /// 品牌方案內容
    /// </summary>
    [Column("content")]
    [StringLength(45)]
    public string? Content { get; set; }

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
    /// 客戶名稱
    /// </summary>
    [Column("name")]
    [StringLength(45)]
    public string? Name { get; set; }

    /// <summary>
    /// 負責業務
    /// </summary>
    [Column("sales")]
    [StringLength(45)]
    public string? Sales { get; set; }

    /// <summary>
    /// 報價時間
    /// </summary>
    [Column("quota_time")]
    public DateOnly QuotaTime { get; set; }

    /// <summary>
    /// 建立者
    /// </summary>
    [Column("creator")]
    [StringLength(45)]
    public string? Creator { get; set; }

    /// <summary>
    /// 最後更新者
    /// </summary>
    [Column("last_update")]
    [StringLength(45)]
    public string? LastUpdate { get; set; }

    /// <summary>
    /// 是否刪除:1(刪除) 0:保留
    /// </summary>
    [Column("is_del")]
    [StringLength(45)]
    public string IsDel { get; set; } = null!;
}

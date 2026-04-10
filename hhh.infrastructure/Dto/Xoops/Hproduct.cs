using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("_hproduct")]
[Index("Cate1", Name = "cate1")]
[Index("Cate3", Name = "cate3")]
[Index("HbrandId", Name = "hbrand_id")]
[Index("Onoff", Name = "onoff")]
[Index("UpdatedAt", Name = "updated_at")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class Hproduct
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("id")]
    public uint Id { get; set; }

    /// <summary>
    /// 廠商ID
    /// </summary>
    [Column("hbrand_id")]
    public uint HbrandId { get; set; }

    /// <summary>
    /// 產品名稱
    /// </summary>
    [Column("name")]
    [StringLength(64)]
    public string Name { get; set; } = null!;

    [Column("sort")]
    public int Sort { get; set; }

    /// <summary>
    /// 型號
    /// </summary>
    [Column("model")]
    [StringLength(32)]
    public string Model { get; set; } = null!;

    /// <summary>
    /// 分類
    /// </summary>
    [Column("cate1")]
    [StringLength(16)]
    public string Cate1 { get; set; } = null!;

    /// <summary>
    /// 分類2
    /// </summary>
    [Column("cate2")]
    [StringLength(16)]
    public string Cate2 { get; set; } = null!;

    /// <summary>
    /// 分類3
    /// </summary>
    [Column("cate3")]
    [StringLength(16)]
    public string Cate3 { get; set; } = null!;

    /// <summary>
    /// 使用空間
    /// </summary>
    [Column("space")]
    [StringLength(32)]
    public string Space { get; set; } = null!;

    /// <summary>
    /// 敘述
    /// </summary>
    [Column("descr", TypeName = "text")]
    public string Descr { get; set; } = null!;

    /// <summary>
    /// 簡介
    /// </summary>
    [Column("brief", TypeName = "text")]
    public string Brief { get; set; } = null!;

    /// <summary>
    /// 哪裡買
    /// </summary>
    [Column("wherebuy", TypeName = "text")]
    public string Wherebuy { get; set; } = null!;

    /// <summary>
    /// 封面圖
    /// </summary>
    [Column("cover")]
    [StringLength(200)]
    public string Cover { get; set; } = null!;

    /// <summary>
    /// 上線狀態(0:關1:開)
    /// </summary>
    [Column("onoff")]
    public sbyte Onoff { get; set; }

    /// <summary>
    /// 更新時間
    /// </summary>
    [Column("updated_at", TypeName = "timestamp")]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// 點擊數
    /// </summary>
    [Column("clicks")]
    public int Clicks { get; set; }

    /// <summary>
    /// 寄送次數
    /// </summary>
    [Column("is_send")]
    public sbyte IsSend { get; set; }

    [Column("seo_title")]
    [StringLength(128)]
    public string? SeoTitle { get; set; }

    [Column("seo_image")]
    [StringLength(128)]
    public string? SeoImage { get; set; }

    [Column("product_top")]
    public int ProductTop { get; set; }
}

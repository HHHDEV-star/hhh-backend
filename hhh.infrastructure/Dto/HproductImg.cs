using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Table("_hproduct_img")]
[Index("HproductId", Name = "hproduct_id")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class HproductImg
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("id")]
    public uint Id { get; set; }

    /// <summary>
    /// 產品ID
    /// </summary>
    [Column("hproduct_id")]
    public uint HproductId { get; set; }

    /// <summary>
    /// 建案id
    /// </summary>
    [Column("builder_product_id")]
    public uint BuilderProductId { get; set; }

    /// <summary>
    /// 產品圖片名稱
    /// </summary>
    [Column("name")]
    [StringLength(128)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 產品圖片標題
    /// </summary>
    [Column("title")]
    [StringLength(64)]
    public string Title { get; set; } = null!;

    /// <summary>
    /// 敘述
    /// </summary>
    [Column("descr")]
    [StringLength(128)]
    public string Descr { get; set; } = null!;

    /// <summary>
    /// 排序
    /// </summary>
    [Column("order_no")]
    public byte OrderNo { get; set; }

    /// <summary>
    /// 是否設定為封面(0:否1:是)
    /// </summary>
    [Column("is_cover")]
    public bool IsCover { get; set; }
}

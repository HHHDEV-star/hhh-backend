using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("shop_img")]
[Index("ShopId", Name = "hproduct_id")]
public partial class ShopImg
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
    [Column("shop_id")]
    public uint ShopId { get; set; }

    /// <summary>
    /// 圖片名稱
    /// </summary>
    [Column("name")]
    [StringLength(128)]
    [MySqlCollation("utf8mb3_general_ci")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 標題
    /// </summary>
    [Column("title")]
    [StringLength(64)]
    [MySqlCollation("utf8mb3_general_ci")]
    public string Title { get; set; } = null!;

    /// <summary>
    /// 描述
    /// </summary>
    [Column("descr")]
    [StringLength(128)]
    [MySqlCollation("utf8mb3_general_ci")]
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

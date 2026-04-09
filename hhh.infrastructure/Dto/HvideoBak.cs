using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Table("_hvideo_bak")]
[Index("CreatTime", Name = "creat_time")]
[Index("HbrandId", Name = "hbrand_id")]
[Index("HcaseId", Name = "hcase_id")]
[Index("HdesignerId", Name = "hdesigner_id")]
[Index("Recommend", Name = "recommend")]
[Index("TagVpattern", Name = "tag_vpattern")]
[Index("TagVtype", Name = "tag_vtype")]
[Index("VfileType", Name = "vfile_type")]
[Index("Viewed", Name = "viewed")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class HvideoBak
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("hvideo_id")]
    public uint HvideoId { get; set; }

    /// <summary>
    /// 設計師ID
    /// </summary>
    [Column("hdesigner_id")]
    public uint HdesignerId { get; set; }

    /// <summary>
    /// 個案ID
    /// </summary>
    [Column("hcase_id")]
    public uint HcaseId { get; set; }

    /// <summary>
    /// 廠商ID
    /// </summary>
    [Column("hbrand_id")]
    public uint HbrandId { get; set; }

    /// <summary>
    /// 專欄ID
    /// </summary>
    [Column("hcolumn_id")]
    public uint HcolumnId { get; set; }

    /// <summary>
    /// 建案id
    /// </summary>
    [Column("builder_product_id")]
    public uint BuilderProductId { get; set; }

    /// <summary>
    /// 影音類型
    /// </summary>
    [Column("vfile_type")]
    [StringLength(32)]
    public string VfileType { get; set; } = null!;

    /// <summary>
    /// 名稱
    /// </summary>
    [Column("name")]
    [StringLength(128)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 標籤-單元類型
    /// </summary>
    [Column("tag_vtype")]
    public string TagVtype { get; set; } = null!;

    /// <summary>
    /// 標籤-空間格局
    /// </summary>
    [Column("tag_vpattern")]
    [StringLength(128)]
    public string TagVpattern { get; set; } = null!;

    /// <summary>
    /// Tag
    /// </summary>
    [Column("tag")]
    [StringLength(150)]
    public string? Tag { get; set; }

    /// <summary>
    /// 計算機使用
    /// </summary>
    [Column("style_tag")]
    [StringLength(20)]
    public string? StyleTag { get; set; }

    /// <summary>
    /// 計算機使用
    /// </summary>
    [Column("fee_tag")]
    [StringLength(20)]
    public string? FeeTag { get; set; }

    /// <summary>
    /// Tag更新時間
    /// </summary>
    [Column("tag_datetime", TypeName = "datetime")]
    public DateTime TagDatetime { get; set; }

    /// <summary>
    /// 影音標題
    /// </summary>
    [Column("title")]
    [StringLength(128)]
    public string Title { get; set; } = null!;

    /// <summary>
    /// 影音說明
    /// </summary>
    [Column("desc", TypeName = "text")]
    public string Desc { get; set; } = null!;

    /// <summary>
    /// 強制設定縮圖
    /// </summary>
    [Column("thumbnail_time")]
    [StringLength(16)]
    public string ThumbnailTime { get; set; } = null!;

    /// <summary>
    /// 外部影片
    /// </summary>
    [Column("iframe", TypeName = "text")]
    public string Iframe { get; set; } = null!;

    /// <summary>
    /// 大陸地區觀看
    /// </summary>
    [Column("for_china", TypeName = "text")]
    public string ForChina { get; set; } = null!;

    /// <summary>
    /// 推薦
    /// </summary>
    [Column("recommend")]
    public uint Recommend { get; set; }

    /// <summary>
    /// 觀看數
    /// </summary>
    [Column("viewed")]
    public uint Viewed { get; set; }

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("creat_time", TypeName = "timestamp")]
    public DateTime CreatTime { get; set; }

    /// <summary>
    /// 寬x高
    /// </summary>
    [Column("wxh")]
    [StringLength(128)]
    public string Wxh { get; set; } = null!;

    /// <summary>
    /// 點擊數
    /// </summary>
    [Column("clicks")]
    public int Clicks { get; set; }

    /// <summary>
    /// 贊助
    /// </summary>
    [Column("sponsor")]
    [StringLength(20)]
    public string? Sponsor { get; set; }

    /// <summary>
    /// 發送次數
    /// </summary>
    [Column("is_send")]
    public bool IsSend { get; set; }
}

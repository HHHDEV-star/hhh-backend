using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("_hcolumn")]
[Index("Bid", Name = "bid")]
[Index("CreatTime", Name = "creat_time")]
[Index("Ctitle", Name = "ctitle")]
[Index("Ctype", Name = "ctype")]
[Index("Onoff", Name = "onoff")]
[Index("Recommend", Name = "recommend")]
[Index("Viewed", Name = "viewed")]
public partial class Hcolumn
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("hcolumn_id")]
    public uint HcolumnId { get; set; }

    /// <summary>
    /// 建案id
    /// </summary>
    [Column("builder_product_id")]
    public uint BuilderProductId { get; set; }

    /// <summary>
    /// 標籤
    /// </summary>
    [Column("ctag")]
    [StringLength(64)]
    public string Ctag { get; set; } = null!;

    /// <summary>
    /// Tag
    /// </summary>
    [Column("tag")]
    [StringLength(150)]
    public string? Tag { get; set; }

    /// <summary>
    /// Tag更新時間
    /// </summary>
    [Column("tag_datetime", TypeName = "datetime")]
    public DateTime TagDatetime { get; set; }

    /// <summary>
    /// 專欄類別
    /// </summary>
    [Column("ctype")]
    [StringLength(32)]
    public string Ctype { get; set; } = null!;

    /// <summary>
    /// 專欄類別_子項
    /// </summary>
    [Column("ctype_sub")]
    [StringLength(32)]
    public string? CtypeSub { get; set; }

    /// <summary>
    /// 專欄名稱
    /// </summary>
    [Column("ctitle")]
    [StringLength(128)]
    public string Ctitle { get; set; } = null!;

    /// <summary>
    /// 短專欄名稱
    /// </summary>
    [Column("cshort_title")]
    [StringLength(32)]
    public string CshortTitle { get; set; } = null!;

    /// <summary>
    /// 專欄敘述
    /// </summary>
    [Column("cdesc", TypeName = "text")]
    public string Cdesc { get; set; } = null!;

    /// <summary>
    /// 專欄logo
    /// </summary>
    [Column("clogo")]
    [StringLength(128)]
    public string Clogo { get; set; } = null!;

    /// <summary>
    /// 圖文提供
    /// </summary>
    [Column("extend_str", TypeName = "text")]
    public string ExtendStr { get; set; } = null!;

    /// <summary>
    /// 觀看數
    /// </summary>
    [Column("viewed")]
    public uint Viewed { get; set; }

    /// <summary>
    /// 推薦
    /// </summary>
    [Column("recommend")]
    public uint Recommend { get; set; }

    /// <summary>
    /// 上線狀態(0:關1:開)
    /// </summary>
    [Column("onoff")]
    public byte Onoff { get; set; }

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("creat_time", TypeName = "timestamp")]
    public DateTime CreatTime { get; set; }

    /// <summary>
    /// json:設計師ID
    /// </summary>
    [Column("json_did", TypeName = "text")]
    public string JsonDid { get; set; } = null!;

    /// <summary>
    /// json:廠商ID
    /// </summary>
    [Column("json_bid", TypeName = "text")]
    public string JsonBid { get; set; } = null!;

    /// <summary>
    /// 廠商ID
    /// </summary>
    [Column("bid")]
    public uint? Bid { get; set; }

    /// <summary>
    /// 設計師ID
    /// </summary>
    [Column("hdesigner_ids", TypeName = "text")]
    public string? HdesignerIds { get; set; }

    /// <summary>
    /// 自由內容
    /// </summary>
    [Column("page_content")]
    public string? PageContent { get; set; }

    /// <summary>
    /// 寄送次數
    /// </summary>
    [Column("is_send")]
    public byte IsSend { get; set; }

    /// <summary>
    /// 上架日期(2020-02-12))
    /// </summary>
    [Column("sdate")]
    public DateOnly Sdate { get; set; }

    [Column("seo_image")]
    [StringLength(128)]
    public string? SeoImage { get; set; }

    [Column("seo_title")]
    [StringLength(128)]
    public string? SeoTitle { get; set; }

    [Column("seo_description", TypeName = "text")]
    public string? SeoDescription { get; set; }

    [Column("json_ld", TypeName = "text")]
    public string? JsonLd { get; set; }

    /// <summary>
    /// 更新時間
    /// </summary>
    [Column("update_time", TypeName = "timestamp")]
    public DateTime UpdateTime { get; set; }
}

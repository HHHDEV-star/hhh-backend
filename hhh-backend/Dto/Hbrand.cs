using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("_hbrand")]
[Index("Border", Name = "border")]
[Index("Bspace", Name = "bspace")]
[Index("Bstype", Name = "bstype")]
[Index("Onoff", Name = "onoff")]
[Index("Recommend", Name = "recommend")]
[Index("Vr360Id", Name = "vr360_id")]
public partial class Hbrand
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("hbrand_id")]
    public uint HbrandId { get; set; }

    /// <summary>
    /// logo
    /// </summary>
    [Column("logo")]
    [StringLength(512)]
    public string Logo { get; set; } = null!;

    /// <summary>
    /// logo2
    /// </summary>
    [Column("logo2")]
    [StringLength(512)]
    public string Logo2 { get; set; } = null!;

    /// <summary>
    /// 廠商名稱
    /// </summary>
    [Column("title")]
    [StringLength(512)]
    public string Title { get; set; } = null!;

    /// <summary>
    /// 產品封面圖id
    /// </summary>
    [Column("product_id")]
    public int ProductId { get; set; }

    /// <summary>
    /// 分公司
    /// </summary>
    [Column("sub_company", TypeName = "text")]
    public string? SubCompany { get; set; }

    /// <summary>
    /// 免付費電話
    /// </summary>
    [Column("service_phone")]
    [StringLength(25)]
    public string ServicePhone { get; set; } = null!;

    /// <summary>
    /// 電話
    /// </summary>
    [Column("phone")]
    [StringLength(128)]
    public string Phone { get; set; } = null!;

    /// <summary>
    /// 地址
    /// </summary>
    [Column("address")]
    [StringLength(128)]
    public string Address { get; set; } = null!;

    /// <summary>
    /// 網站
    /// </summary>
    [Column("website")]
    [StringLength(512)]
    public string Website { get; set; } = null!;

    /// <summary>
    /// Facebook page URL
    /// </summary>
    [Column("fbpageurl", TypeName = "text")]
    public string Fbpageurl { get; set; } = null!;

    /// <summary>
    /// 廠商EMAIL
    /// </summary>
    [Column("email")]
    [StringLength(128)]
    public string Email { get; set; } = null!;

    /// <summary>
    /// 負責業務EMAIL
    /// </summary>
    [Column("sales_email")]
    [StringLength(64)]
    public string SalesEmail { get; set; } = null!;

    [Column("brand_email", TypeName = "text")]
    public string BrandEmail { get; set; } = null!;

    /// <summary>
    /// 與廠商聯繫連結
    /// </summary>
    [Column("contact_link")]
    [StringLength(512)]
    public string? ContactLink { get; set; }

    /// <summary>
    /// 品牌介紹
    /// </summary>
    [Column("intro", TypeName = "text")]
    public string Intro { get; set; } = null!;

    /// <summary>
    /// 獲獎紀錄
    /// </summary>
    [Column("history", TypeName = "text")]
    public string History { get; set; } = null!;

    /// <summary>
    /// 品牌描述
    /// </summary>
    [Column("desc", TypeName = "text")]
    public string Desc { get; set; } = null!;

    /// <summary>
    /// 幸福空間推薦
    /// </summary>
    [Column("gchoice", TypeName = "text")]
    public string Gchoice { get; set; } = null!;

    /// <summary>
    /// 產品類別
    /// </summary>
    [Column("btype")]
    [StringLength(64)]
    public string Btype { get; set; } = null!;

    /// <summary>
    /// 已從頁面移除
    /// </summary>
    [Column("bstype")]
    [StringLength(128)]
    public string Bstype { get; set; } = null!;

    /// <summary>
    /// 已從頁面移除
    /// </summary>
    [Column("bspace")]
    [StringLength(128)]
    public string Bspace { get; set; } = null!;

    /// <summary>
    /// 影片ID
    /// </summary>
    [Column("hvideo_id")]
    public uint HvideoId { get; set; }

    /// <summary>
    /// 推薦
    /// </summary>
    [Column("recommend")]
    public uint Recommend { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [Column("border")]
    public uint Border { get; set; }

    /// <summary>
    /// 進口品牌
    /// </summary>
    [Column("imported")]
    public sbyte Imported { get; set; }

    /// <summary>
    /// 上線狀態(0:關閉 1:開啟)
    /// </summary>
    [Column("onoff")]
    public byte Onoff { get; set; }

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("creat_time", TypeName = "timestamp")]
    public DateTime CreatTime { get; set; }

    /// <summary>
    /// vr306_ID
    /// </summary>
    [Column("vr360_id")]
    [StringLength(16)]
    public string Vr360Id { get; set; } = null!;

    /// <summary>
    /// 點擊數
    /// </summary>
    [Column("clicks")]
    public int Clicks { get; set; }

    /// <summary>
    /// 手機背景圖
    /// </summary>
    [Column("background_mobile")]
    [StringLength(200)]
    public string? BackgroundMobile { get; set; }

    /// <summary>
    /// 已寄送次數
    /// </summary>
    [Column("is_send")]
    public sbyte IsSend { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Table("_hcase")]
[Index("Area", Name = "area")]
[Index("Condition", Name = "condition")]
[Index("Corder", Name = "corder")]
[Index("CreatTime", Name = "creat_time")]
[Index("HdesignerId", Name = "hdesigner_id")]
[Index("Onoff", Name = "onoff")]
[Index("Recommend", Name = "recommend")]
[Index("Sdate", Name = "sdate")]
[Index("Sdate", "Onoff", Name = "sdate_onoff")]
[Index("Style", Name = "style")]
[Index("Type", Name = "type")]
[Index("Viewed", Name = "viewed")]
[Index("Vr360Id", Name = "vr360_id")]
public partial class Hcase
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("hcase_id")]
    public uint HcaseId { get; set; }

    /// <summary>
    /// 設計師id
    /// </summary>
    [Column("hdesigner_id")]
    public uint HdesignerId { get; set; }

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
    /// 個案名稱
    /// </summary>
    [Column("caption")]
    [StringLength(256)]
    public string Caption { get; set; } = null!;

    /// <summary>
    /// 短說明
    /// </summary>
    [Column("short_desc", TypeName = "text")]
    public string ShortDesc { get; set; } = null!;

    /// <summary>
    /// 長說明
    /// </summary>
    [Column("long_desc", TypeName = "text")]
    public string LongDesc { get; set; } = null!;

    /// <summary>
    /// 居住成員
    /// </summary>
    [Column("member")]
    [StringLength(64)]
    public string Member { get; set; } = null!;

    /// <summary>
    /// 裝潢費用
    /// </summary>
    [Column("fee")]
    public uint Fee { get; set; }

    /// <summary>
    /// 裝潢費用補充說明
    /// </summary>
    [Column("feedesc")]
    [StringLength(128)]
    public string Feedesc { get; set; } = null!;

    /// <summary>
    /// 房屋坪數
    /// </summary>
    [Column("area")]
    public uint Area { get; set; }

    /// <summary>
    /// 房屋坪數補充說明
    /// </summary>
    [Column("areaDesc")]
    [StringLength(32)]
    public string AreaDesc { get; set; } = null!;

    /// <summary>
    /// 房屋位置
    /// </summary>
    [Column("location")]
    [StringLength(32)]
    public string Location { get; set; } = null!;

    /// <summary>
    /// 預算計算的加成比例
    /// </summary>
    [Column("level")]
    [StringLength(10)]
    [MySqlCollation("utf8mb3_general_ci")]
    public string Level { get; set; } = null!;

    /// <summary>
    /// 設計風格
    /// </summary>
    [Column("style")]
    [StringLength(64)]
    public string Style { get; set; } = null!;

    /// <summary>
    /// 自訂的設計風格
    /// </summary>
    [Column("style2")]
    [StringLength(32)]
    public string Style2 { get; set; } = null!;

    /// <summary>
    /// 房屋類型
    /// </summary>
    [Column("type")]
    [StringLength(64)]
    public string Type { get; set; } = null!;

    /// <summary>
    /// 房屋狀況
    /// </summary>
    [Column("condition")]
    [StringLength(32)]
    public string Condition { get; set; } = null!;

    /// <summary>
    /// 圖片提供
    /// </summary>
    [Column("provider")]
    [StringLength(64)]
    public string Provider { get; set; } = null!;

    /// <summary>
    /// 空間格局
    /// </summary>
    [Column("layout")]
    [StringLength(128)]
    public string Layout { get; set; } = null!;

    /// <summary>
    /// 主要建材
    /// </summary>
    [Column("materials")]
    [StringLength(256)]
    public string Materials { get; set; } = null!;

    /// <summary>
    /// 封面圖
    /// </summary>
    [Column("cover")]
    [StringLength(128)]
    public string Cover { get; set; } = null!;

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
    /// 上線狀態(0:關1:開)
    /// </summary>
    [Column("onoff")]
    public byte Onoff { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [Column("corder")]
    public int Corder { get; set; }

    /// <summary>
    /// 圖片尺寸
    /// </summary>
    [Column("size_byte")]
    public uint SizeByte { get; set; }

    /// <summary>
    /// VR360 ID
    /// </summary>
    [Column("vr360_id")]
    [StringLength(16)]
    public string Vr360Id { get; set; } = null!;

    /// <summary>
    /// iStage
    /// </summary>
    [Column("istaging")]
    [StringLength(40)]
    public string? Istaging { get; set; }

    /// <summary>
    /// 已寄送次數
    /// </summary>
    [Column("is_send")]
    public byte IsSend { get; set; }

    /// <summary>
    /// 上架日期(2020-02-12)
    /// </summary>
    [Column("sdate")]
    public DateOnly Sdate { get; set; }

    /// <summary>
    /// 自動計算裝潢費用(0: 否 / 1:是)
    /// </summary>
    [Column("auto_count_fee")]
    public bool AutoCountFee { get; set; }

    [Column("seo_image")]
    [StringLength(128)]
    public string? SeoImage { get; set; }

    [Column("seo_title")]
    [StringLength(128)]
    public string? SeoTitle { get; set; }

    [Column("seo_description", TypeName = "text")]
    public string? SeoDescription { get; set; }

    [Column("case_top")]
    [StringLength(45)]
    public string CaseTop { get; set; } = null!;

    [Column("sdate_order")]
    [StringLength(45)]
    public string SdateOrder { get; set; } = null!;

    /// <summary>
    /// 更新時間
    /// </summary>
    [Column("update_time", TypeName = "timestamp")]
    public DateTime UpdateTime { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("builder")]
[Index("Border", Name = "border")]
[Index("Onoff", Name = "onoff")]
[Index("Recommend", Name = "recommend")]
[Index("Vr360Id", Name = "vr360_id")]
public partial class Builder
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("builder_id")]
    public uint BuilderId { get; set; }

    /// <summary>
    /// logo
    /// </summary>
    [Column("logo")]
    [StringLength(128)]
    public string Logo { get; set; } = null!;

    /// <summary>
    /// logo2
    /// </summary>
    [Column("logo2")]
    [StringLength(128)]
    public string Logo2 { get; set; } = null!;

    /// <summary>
    /// 廠商名稱
    /// </summary>
    [Column("title")]
    [StringLength(128)]
    public string Title { get; set; } = null!;

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
    [StringLength(64)]
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

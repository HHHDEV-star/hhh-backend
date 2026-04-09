using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Table("_had")]
[Index("Adtype", Name = "adtype")]
[Index("Keyword", Name = "keyword")]
[Index("Onoff", Name = "onoff")]
public partial class Had
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("adid")]
    public uint Adid { get; set; }

    /// <summary>
    /// 廣告類型
    /// </summary>
    [Column("adtype")]
    [StringLength(64)]
    public string Adtype { get; set; } = null!;

    /// <summary>
    /// LOGO
    /// </summary>
    [Column("adlogo")]
    [StringLength(512)]
    public string Adlogo { get; set; } = null!;

    /// <summary>
    /// 手機版圖片
    /// </summary>
    [Column("adlogo_mobile")]
    [StringLength(512)]
    public string? AdlogoMobile { get; set; }

    /// <summary>
    /// 桌機版webp
    /// </summary>
    [Column("adlogo_webp")]
    [StringLength(512)]
    public string AdlogoWebp { get; set; } = null!;

    /// <summary>
    /// 手機版webp
    /// </summary>
    [Column("adlogo_mobile_webp")]
    [StringLength(512)]
    public string AdlogoMobileWebp { get; set; } = null!;

    /// <summary>
    /// Logo圖示
    /// </summary>
    [Column("logo_icon")]
    [StringLength(512)]
    public string? LogoIcon { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    [Column("addesc")]
    [StringLength(64)]
    public string Addesc { get; set; } = null!;

    /// <summary>
    /// 長描述
    /// </summary>
    [Column("adlongdesc", TypeName = "text")]
    public string Adlongdesc { get; set; } = null!;

    /// <summary>
    /// 廣告連結
    /// </summary>
    [Column("adhref")]
    [StringLength(512)]
    public string Adhref { get; set; } = null!;

    /// <summary>
    /// 廣告連結(右邊
    /// </summary>
    [Column("adhref_r")]
    [StringLength(512)]
    public string? AdhrefR { get; set; }

    /// <summary>
    /// 關鍵字
    /// </summary>
    [Column("keyword")]
    [StringLength(64)]
    public string? Keyword { get; set; }

    /// <summary>
    /// tab名稱(已經從頁面拿掉了)
    /// </summary>
    [Column("tabname")]
    [StringLength(32)]
    public string Tabname { get; set; } = null!;

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("creat_time", TypeName = "timestamp")]
    public DateTime CreatTime { get; set; }

    /// <summary>
    /// 點擊數
    /// </summary>
    [Column("click_counter")]
    public uint ClickCounter { get; set; }

    /// <summary>
    /// 上線狀態(0:關1:開)
    /// </summary>
    [Column("onoff")]
    public byte Onoff { get; set; }

    /// <summary>
    /// 開始時間
    /// </summary>
    [Column("start_time", TypeName = "datetime")]
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 結束時間
    /// </summary>
    [Column("end_time", TypeName = "datetime")]
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// 大banner浮水logo 
    /// </summary>
    [Column("water_mark")]
    [StringLength(512)]
    public string? WaterMark { get; set; }

    /// <summary>
    /// 設計師ID
    /// </summary>
    [Column("hdesigner_id")]
    public int HdesignerId { get; set; }

    /// <summary>
    /// 廠商ID	
    /// </summary>
    [Column("hbrand_id")]
    public int HbrandId { get; set; }

    /// <summary>
    /// 建案ID	
    /// </summary>
    [Column("builder_product_id")]
    public int BuilderProductId { get; set; }

    /// <summary>
    /// 是否寄出通知(N:否 / Y:是
    /// </summary>
    [Column("is_send")]
    [StringLength(1)]
    public string IsSend { get; set; } = null!;

    /// <summary>
    /// 寄送時間
    /// </summary>
    [Column("send_datetime", TypeName = "datetime")]
    public DateTime SendDatetime { get; set; }

    /// <summary>
    /// alt使用預設為幸福空間
    /// </summary>
    [Column("alt_use")]
    [StringLength(45)]
    public string AltUse { get; set; } = null!;

    /// <summary>
    /// 首頁大廣告下方文字第一段
    /// </summary>
    [Column("index_char_1")]
    [StringLength(45)]
    public string? IndexChar1 { get; set; }

    /// <summary>
    /// 首頁大廣告下方文字第二段第一節
    /// </summary>
    [Column("index_char_2_1")]
    [StringLength(20)]
    [MySqlCollation("utf8mb3_general_ci")]
    public string? IndexChar21 { get; set; }

    /// <summary>
    /// 首頁大廣告下方文字第二段第二節
    /// </summary>
    [Column("index_char_2_2")]
    [StringLength(20)]
    public string? IndexChar22 { get; set; }

    /// <summary>
    /// 首頁大廣告下方文字第二段第三節
    /// </summary>
    [Column("index_char_2_3")]
    [StringLength(20)]
    public string? IndexChar23 { get; set; }
}

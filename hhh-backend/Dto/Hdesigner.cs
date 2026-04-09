using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("_hdesigner")]
[Index("CreatTime", Name = "creat_time")]
[Index("Dorder", Name = "dorder")]
[Index("Guarantee", Name = "guarantee")]
[Index("MobileOrder", Name = "latest_id")]
[Index("Location", Name = "location")]
[Index("Onoff", Name = "onoff")]
[Index("Region", Name = "region")]
public partial class Hdesigner
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("hdesigner_id")]
    public uint HdesignerId { get; set; }

    /// <summary>
    /// 頭像
    /// </summary>
    [Column("img_path")]
    [StringLength(128)]
    public string ImgPath { get; set; } = null!;

    /// <summary>
    /// 公司抬頭
    /// </summary>
    [Column("title")]
    [StringLength(128)]
    public string Title { get; set; } = null!;

    /// <summary>
    /// 設計師名稱
    /// </summary>
    [Column("name")]
    [StringLength(128)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 接案區域
    /// </summary>
    [Column("region")]
    [StringLength(128)]
    public string Region { get; set; } = null!;

    /// <summary>
    /// 設計師所在區域
    /// </summary>
    [Column("location")]
    [StringLength(32)]
    public string Location { get; set; } = null!;

    /// <summary>
    /// 接案類型
    /// </summary>
    [Column("type")]
    [StringLength(256)]
    public string Type { get; set; } = null!;

    /// <summary>
    /// 接案風格
    /// </summary>
    [Column("style")]
    [StringLength(256)]
    public string Style { get; set; } = null!;

    /// <summary>
    /// 接案預算
    /// </summary>
    [Column("budget")]
    [StringLength(64)]
    public string Budget { get; set; } = null!;

    /// <summary>
    /// 接案坪數
    /// </summary>
    [Column("area")]
    [StringLength(64)]
    public string Area { get; set; } = null!;

    /// <summary>
    /// 特殊接案
    /// </summary>
    [Column("special")]
    [StringLength(64)]
    public string Special { get; set; } = null!;

    /// <summary>
    /// 收費方式
    /// </summary>
    [Column("charge")]
    [StringLength(512)]
    public string Charge { get; set; } = null!;

    /// <summary>
    /// 付費方式
    /// </summary>
    [Column("payment")]
    [StringLength(128)]
    public string Payment { get; set; } = null!;

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
    /// 傳真
    /// </summary>
    [Column("fax")]
    [StringLength(128)]
    public string Fax { get; set; } = null!;

    /// <summary>
    /// 地址
    /// </summary>
    [Column("address")]
    [StringLength(512)]
    public string Address { get; set; } = null!;

    /// <summary>
    /// E-mail
    /// </summary>
    [Column("mail")]
    [StringLength(256)]
    public string Mail { get; set; } = null!;

    /// <summary>
    /// 網站
    /// </summary>
    [Column("website")]
    [StringLength(128)]
    public string Website { get; set; } = null!;

    /// <summary>
    /// 其他網址連結
    /// </summary>
    [Column("blog")]
    [StringLength(512)]
    public string Blog { get; set; } = null!;

    /// <summary>
    /// 品牌定位
    /// </summary>
    [Column("position", TypeName = "text")]
    public string Position { get; set; } = null!;

    /// <summary>
    /// 設計理念
    /// </summary>
    [Column("idea", TypeName = "text")]
    public string Idea { get; set; } = null!;

    /// <summary>
    /// header.description
    /// </summary>
    [Column("description", TypeName = "text")]
    public string? Description { get; set; }

    /// <summary>
    /// 相關經歷
    /// </summary>
    [Column("career", TypeName = "text")]
    public string Career { get; set; } = null!;

    /// <summary>
    /// 獲獎紀錄
    /// </summary>
    [Column("awards", TypeName = "text")]
    public string Awards { get; set; } = null!;

    /// <summary>
    /// 相關證照
    /// </summary>
    [Column("license", TypeName = "text")]
    public string License { get; set; } = null!;

    /// <summary>
    /// 排序
    /// </summary>
    [Column("dorder")]
    public uint Dorder { get; set; }

    /// <summary>
    /// 上線狀態(0:關1:開)
    /// </summary>
    [Column("onoff")]
    public byte Onoff { get; set; }

    /// <summary>
    /// 指定[設計師群組]的會員
    /// </summary>
    [Column("xoops_uid")]
    public uint XoopsUid { get; set; }

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("creat_time", TypeName = "timestamp")]
    public DateTime CreatTime { get; set; }

    /// <summary>
    /// 業務的email
    /// </summary>
    [Column("sales_mail")]
    [StringLength(64)]
    public string SalesMail { get; set; } = null!;

    /// <summary>
    /// 通知設計師的email
    /// </summary>
    [Column("designer_mail")]
    [StringLength(128)]
    public string DesignerMail { get; set; } = null!;

    /// <summary>
    /// 幸福經紀人
    /// </summary>
    [Column("guarantee")]
    public ushort Guarantee { get; set; }

    /// <summary>
    /// FB page URL
    /// </summary>
    [Column("fbpageurl", TypeName = "text")]
    public string Fbpageurl { get; set; } = null!;

    /// <summary>
    /// 座標X
    /// </summary>
    [Column("coordinate_x")]
    [StringLength(32)]
    public string CoordinateX { get; set; } = null!;

    /// <summary>
    /// 座標Y
    /// </summary>
    [Column("coordinate_y")]
    [StringLength(32)]
    public string CoordinateY { get; set; } = null!;

    /// <summary>
    /// 獎項名稱
    /// </summary>
    [Column("awards_name")]
    [StringLength(128)]
    public string AwardsName { get; set; } = null!;

    /// <summary>
    /// 獎項logo
    /// </summary>
    [Column("awards_logo")]
    [StringLength(32)]
    public string AwardsLogo { get; set; } = null!;

    /// <summary>
    /// 公司統編
    /// </summary>
    [Column("taxid")]
    [StringLength(24)]
    public string Taxid { get; set; } = null!;

    /// <summary>
    /// 點擊數
    /// </summary>
    [Column("clicks")]
    public int Clicks { get; set; }

    /// <summary>
    /// 設計師背景圖
    /// </summary>
    [Column("background")]
    [StringLength(200)]
    public string? Background { get; set; }

    /// <summary>
    /// 手機板背景圖
    /// </summary>
    [Column("background_mobile")]
    [StringLength(200)]
    public string? BackgroundMobile { get; set; }

    /// <summary>
    /// LINE URL
    /// </summary>
    [Column("line_link")]
    [StringLength(100)]
    public string? LineLink { get; set; }

    /// <summary>
    /// 手機板排序
    /// </summary>
    [Column("mobile_order")]
    public int MobileOrder { get; set; }

    /// <summary>
    /// 寄送數目
    /// </summary>
    [Column("is_send")]
    public sbyte IsSend { get; set; }

    /// <summary>
    /// 預算最小值
    /// </summary>
    [Column("min_budget")]
    public uint MinBudget { get; set; }

    /// <summary>
    /// 預算最大值
    /// </summary>
    [Column("max_budget")]
    public uint MaxBudget { get; set; }

    /// <summary>
    /// 置頂(N: 否 / Y:是
    /// </summary>
    [Column("top")]
    [StringLength(1)]
    public string Top { get; set; } = null!;

    /// <summary>
    /// SEO內容
    /// </summary>
    [Column("seo", TypeName = "text")]
    public string? Seo { get; set; }

    /// <summary>
    /// Meta內容
    /// </summary>
    [Column("meta_description", TypeName = "text")]
    public string? MetaDescription { get; set; }

    /// <summary>
    /// JSON-LD內容
    /// </summary>
    [Column("json_ld", TypeName = "text")]
    public string? JsonLd { get; set; }

    [Column("internal_id")]
    public int? InternalId { get; set; }

    /// <summary>
    /// 電腦版  day:用最新預設  favor:用人氣預設
    /// </summary>
    [Column("order_computer")]
    [StringLength(45)]
    public string OrderComputer { get; set; } = null!;

    /// <summary>
    /// 手機版   day:用最新預設   favor:用人氣預設
    /// </summary>
    [Column("order_mb")]
    [StringLength(45)]
    public string OrderMb { get; set; } = null!;

    /// <summary>
    /// 為了首六功能所創建新top
    /// </summary>
    [Column("top_six")]
    [StringLength(45)]
    public string TopSix { get; set; } = null!;

    /// <summary>
    /// 1:新銳設計師
    /// </summary>
    [Column("emering")]
    [StringLength(50)]
    public string Emering { get; set; } = null!;

    /// <summary>
    /// 1:優質設計師
    /// </summary>
    [Column("premium")]
    [StringLength(50)]
    public string Premium { get; set; } = null!;

    [Column("line_id")]
    [StringLength(200)]
    public string? LineId { get; set; }

    /// <summary>
    /// 站內搜尋關鍵字
    /// </summary>
    [Column("search_keywords", TypeName = "text")]
    public string? SearchKeywords { get; set; }

    /// <summary>
    /// 站內搜尋關鍵字(電腦自動)
    /// </summary>
    [Column("search_keywords_auto", TypeName = "text")]
    public string? SearchKeywordsAuto { get; set; }

    /// <summary>
    /// 更新時間
    /// </summary>
    [Column("update_time", TypeName = "timestamp")]
    public DateTime UpdateTime { get; set; }
}

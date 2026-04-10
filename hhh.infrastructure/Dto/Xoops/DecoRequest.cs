using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

/// <summary>
/// 軟裝需求單
/// </summary>
[Table("deco_request")]
[Index("PhoneRepeat", Name = "phone_repeat")]
[Index("Type", Name = "type")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class DecoRequest
{
    [Key]
    [Column("seq")]
    public int Seq { get; set; }

    /// <summary>
    /// GUID
    /// </summary>
    [Column("guid")]
    public Guid? Guid { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    [Column("name")]
    [StringLength(50)]
    public string? Name { get; set; }

    /// <summary>
    /// 性別
    /// </summary>
    [Column("sex")]
    [StringLength(4)]
    public string? Sex { get; set; }

    /// <summary>
    /// 電話
    /// </summary>
    [Column("phone")]
    [StringLength(20)]
    public string? Phone { get; set; }

    /// <summary>
    /// 重複資料(裝修需求單 
    /// </summary>
    [Column("phone_repeat")]
    [StringLength(1)]
    public string PhoneRepeat { get; set; } = null!;

    /// <summary>
    /// LINE_ID
    /// </summary>
    [Column("line_id")]
    [StringLength(50)]
    public string? LineId { get; set; }

    /// <summary>
    /// 您需要進行
    /// </summary>
    [Column("need_request")]
    [StringLength(200)]
    public string? NeedRequest { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    [Column("address", TypeName = "text")]
    public string? Address { get; set; }

    /// <summary>
    /// 房屋類型
    /// </summary>
    [Column("house_type")]
    [StringLength(50)]
    public string? HouseType { get; set; }

    /// <summary>
    /// 屋齡
    /// </summary>
    [Column("house_years")]
    public sbyte HouseYears { get; set; }

    /// <summary>
    /// 房屋型態
    /// </summary>
    [Column("house_mode")]
    [StringLength(50)]
    public string? HouseMode { get; set; }

    /// <summary>
    /// 大樓高幾樓
    /// </summary>
    [Column("house_all_lv")]
    public sbyte HouseAllLv { get; set; }

    /// <summary>
    /// 要裝修第幾樓
    /// </summary>
    [Column("house_in_lv")]
    public int HouseInLv { get; set; }

    /// <summary>
    /// 電子郵件	
    /// </summary>
    [Column("email")]
    [StringLength(50)]
    public string? Email { get; set; }

    /// <summary>
    /// 裝修日期
    /// </summary>
    [Column("time")]
    public DateOnly? Time { get; set; }

    /// <summary>
    /// 預算
    /// </summary>
    [Column("budget")]
    [StringLength(20)]
    public string? Budget { get; set; }

    /// <summary>
    /// 坪數
    /// </summary>
    [Column("pin")]
    [StringLength(20)]
    public string? Pin { get; set; }

    /// <summary>
    /// 挑高高度
    /// </summary>
    [Column("house_hight")]
    public sbyte HouseHight { get; set; }

    /// <summary>
    /// 風格需求
    /// </summary>
    [Column("style")]
    [StringLength(100)]
    public string? Style { get; set; }

    /// <summary>
    /// 成人小孩老人
    /// </summary>
    [Column("pattern")]
    [StringLength(30)]
    public string? Pattern { get; set; }

    /// <summary>
    /// 需求功能
    /// </summary>
    [Column("functions")]
    [StringLength(50)]
    public string? Functions { get; set; }

    /// <summary>
    /// 目前格局(_房,_廳,_衛)
    /// </summary>
    [Column("now_layout")]
    [StringLength(10)]
    public string? NowLayout { get; set; }

    /// <summary>
    /// 需求格局
    /// </summary>
    [Column("layout")]
    [StringLength(30)]
    public string? Layout { get; set; }

    /// <summary>
    /// 何處得知軟裝
    /// </summary>
    [Column("how_to_know")]
    [StringLength(100)]
    public string? HowToKnow { get; set; }

    /// <summary>
    /// 備註欄位需求
    /// </summary>
    [Column("note_text", TypeName = "text")]
    public string? NoteText { get; set; }

    /// <summary>
    /// 約定辦公室面訪時間
    /// </summary>
    [Column("meeting_date", TypeName = "datetime")]
    public DateTime MeetingDate { get; set; }

    /// <summary>
    /// 同意接受案件竣工後將進行紀錄，並作為媒體宣傳的露出使用
    /// </summary>
    [Column("is_agree")]
    public sbyte IsAgree { get; set; }

    /// <summary>
    /// 是否寄出(Y:是 / N:否	
    /// </summary>
    [Column("send_status")]
    [StringLength(1)]
    public string SendStatus { get; set; } = null!;

    /// <summary>
    /// 寄出時間
    /// </summary>
    [Column("send_datetime", TypeName = "datetime")]
    public DateTime SendDatetime { get; set; }

    [Column("create_time", TypeName = "datetime")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新時間
    /// </summary>
    [Column("update_time", TypeName = "datetime")]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// 丈量費
    /// </summary>
    [Column("deco_set_price", TypeName = "mediumint unsigned")]
    public uint DecoSetPrice { get; set; }

    /// <summary>
    /// 提案費
    /// </summary>
    [Column("proposal_price", TypeName = "mediumint")]
    public int ProposalPrice { get; set; }

    /// <summary>
    /// 已收費用
    /// </summary>
    [Column("deco_price", TypeName = "mediumint unsigned")]
    public uint DecoPrice { get; set; }

    /// <summary>
    /// 付款狀態
    /// </summary>
    [Column("payment_status")]
    [StringLength(1)]
    public string PaymentStatus { get; set; } = null!;

    /// <summary>
    /// 付款時間
    /// </summary>
    [Column("payment_time", TypeName = "datetime")]
    public DateTime PaymentTime { get; set; }

    /// <summary>
    /// 是否刪除(Y:是/N:否
    /// </summary>
    [Column("is_delete")]
    [StringLength(1)]
    public string IsDelete { get; set; } = null!;

    /// <summary>
    /// 分類(待辦,結案,進行中)
    /// </summary>
    [Column("type")]
    [StringLength(10)]
    public string Type { get; set; } = null!;

    /// <summary>
    /// 第一次來電日期
    /// </summary>
    [Column("first_contact")]
    public DateOnly FirstContact { get; set; }

    /// <summary>
    /// 資料來源(經紀人部門,自動來電,前台表單,講座)
    /// </summary>
    [Column("source")]
    [StringLength(20)]
    public string Source { get; set; } = null!;

    /// <summary>
    /// 提醒日期
    /// </summary>
    [Column("set_date")]
    public DateOnly SetDate { get; set; }

    /// <summary>
    /// utm_source
    /// </summary>
    [Column("utm_source")]
    [StringLength(20)]
    public string? UtmSource { get; set; }

    [Column("loan", TypeName = "bit(1)")]
    public ulong? Loan { get; set; }

    [Column("deco_requestcol")]
    [StringLength(45)]
    public string? DecoRequestcol { get; set; }

    /// <summary>
    /// 裝修事項
    /// </summary>
    [Column("want_deco_matters")]
    [StringLength(45)]
    public string WantDecoMatters { get; set; } = null!;

    /// <summary>
    /// 裝修預算
    /// </summary>
    [Column("want_deco_budget")]
    [StringLength(45)]
    public string WantDecoBudget { get; set; } = null!;

    /// <summary>
    /// 預計裝修時間
    /// </summary>
    [Column("want_deco_time")]
    [StringLength(45)]
    public string WantDecoTime { get; set; } = null!;

    /// <summary>
    /// utm媒介
    /// </summary>
    [Column("utm_medium")]
    [StringLength(45)]
    public string? UtmMedium { get; set; }

    /// <summary>
    /// utm活動
    /// </summary>
    [Column("utm_campaign")]
    [StringLength(45)]
    public string? UtmCampaign { get; set; }
}

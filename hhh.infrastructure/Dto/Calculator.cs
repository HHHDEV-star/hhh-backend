using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Table("calculator")]
[Index("Guid", Name = "guid")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class Calculator
{
    /// <summary>
    /// PK
    /// </summary>
    [Key]
    [Column("id")]
    public uint Id { get; set; }

    [Column("guid")]
    public Guid Guid { get; set; }

    /// <summary>
    /// 房屋類型
    /// </summary>
    [Column("house_type")]
    [StringLength(20)]
    public string HouseType { get; set; } = null!;

    /// <summary>
    /// 裝修地區
    /// </summary>
    [Column("location")]
    [StringLength(20)]
    public string Location { get; set; } = null!;

    /// <summary>
    /// 屋齡
    /// </summary>
    [Column("house_year")]
    [StringLength(20)]
    public string HouseYear { get; set; } = null!;

    /// <summary>
    /// 地板材質
    /// </summary>
    [Column("floor")]
    [StringLength(20)]
    public string Floor { get; set; } = null!;

    /// <summary>
    /// 隔間材質
    /// </summary>
    [Column("compartment")]
    [StringLength(20)]
    public string Compartment { get; set; } = null!;

    /// <summary>
    /// 天花板類型
    /// </summary>
    [Column("ceiling")]
    [StringLength(20)]
    public string Ceiling { get; set; } = null!;

    /// <summary>
    /// 變動裝修
    /// </summary>
    [Column("change_area")]
    [StringLength(20)]
    public string ChangeArea { get; set; } = null!;

    /// <summary>
    /// 格局-房
    /// </summary>
    [Column("room")]
    [StringLength(10)]
    public string Room { get; set; } = null!;

    /// <summary>
    /// 格局-廳
    /// </summary>
    [Column("liveroom")]
    [StringLength(10)]
    public string Liveroom { get; set; } = null!;

    /// <summary>
    /// 格局-衛
    /// </summary>
    [Column("bathroom")]
    [StringLength(10)]
    public string Bathroom { get; set; } = null!;

    /// <summary>
    /// 坪數
    /// </summary>
    [Column("pin")]
    public ushort Pin { get; set; }

    /// <summary>
    /// 聯絡姓名
    /// </summary>
    [Column("name")]
    [StringLength(20)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 聯絡電話
    /// </summary>
    [Column("phone")]
    [StringLength(20)]
    public string Phone { get; set; } = null!;

    /// <summary>
    /// 電子郵件
    /// </summary>
    [Column("email")]
    [StringLength(50)]
    public string Email { get; set; } = null!;

    /// <summary>
    /// 是否為FB帳號(Y:是 / N:否
    /// </summary>
    [Column("is_fb")]
    [StringLength(1)]
    public string IsFb { get; set; } = null!;

    /// <summary>
    /// 意見回饋
    /// </summary>
    [Column("message", TypeName = "text")]
    public string Message { get; set; } = null!;

    /// <summary>
    /// 裝修預估總金額
    /// </summary>
    [Column("total")]
    [StringLength(100)]
    public string Total { get; set; } = null!;

    /// <summary>
    /// 是否可聯繫客戶
    /// </summary>
    [Column("contact_status")]
    [StringLength(20)]
    public string ContactStatus { get; set; } = null!;

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("create_time", TypeName = "datetime")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// IP
    /// </summary>
    [Column("ip")]
    [StringLength(50)]
    public string Ip { get; set; } = null!;

    /// <summary>
    /// 是否寄出郵件(N : 否 / Y : 是)
    /// </summary>
    [Column("send_mail")]
    [StringLength(1)]
    public string SendMail { get; set; } = null!;

    /// <summary>
    /// 寄出時間
    /// </summary>
    [Column("send_time", TypeName = "datetime")]
    public DateTime SendTime { get; set; }

    /// <summary>
    /// 貸款意願
    /// </summary>
    [Column("loan_status")]
    [StringLength(1)]
    public string LoanStatus { get; set; } = null!;

    /// <summary>
    /// 找收納達人意願N:否 Y:是) 
    /// </summary>
    [Column("storage_status")]
    [StringLength(1)]
    public string StorageStatus { get; set; } = null!;

    /// <summary>
    /// 是否尋找倉儲(N:否 Y:是)
    /// </summary>
    [Column("warehousing_status")]
    [StringLength(1)]
    public string WarehousingStatus { get; set; } = null!;

    /// <summary>
    /// 是否租屋(N:否 Y:是)
    /// </summary>
    [Column("rent_house_status")]
    [StringLength(1)]
    public string RentHouseStatus { get; set; } = null!;

    [Column("loan", TypeName = "bit(1)")]
    public ulong? Loan { get; set; }
}

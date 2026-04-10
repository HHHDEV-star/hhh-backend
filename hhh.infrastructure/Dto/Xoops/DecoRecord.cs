using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

/// <summary>
/// 室內裝修業登記資料
/// </summary>
[Table("deco_record")]
[Index("City", Name = "city")]
[Index("CompanyName", Name = "company_name")]
[Index("DecoGuid", Name = "deco_guid")]
[Index("District", Name = "district")]
[Index("Onoff", Name = "onoff")]
[Index("RegisterNumber", Name = "register_number")]
[Index("Street", Name = "street")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class DecoRecord
{
    /// <summary>
    /// ID
    /// </summary>
    [Key]
    [Column("bldsno")]
    public int Bldsno { get; set; }

    [Column("url")]
    [StringLength(10)]
    public string? Url { get; set; }

    [Column("register_number")]
    [StringLength(15)]
    public string? RegisterNumber { get; set; }

    /// <summary>
    /// 公司名稱
    /// </summary>
    [Column("company_name")]
    [StringLength(50)]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string CompanyName { get; set; } = null!;

    /// <summary>
    /// Logo
    /// </summary>
    [Column("avatar")]
    [StringLength(200)]
    public string? Avatar { get; set; }

    /// <summary>
    /// Logo尺寸位置
    /// </summary>
    [Column("avatar_xywd")]
    [StringLength(50)]
    public string AvatarXywd { get; set; } = null!;

    /// <summary>
    /// 負責人
    /// </summary>
    [Column("company_ceo")]
    [StringLength(10)]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string? CompanyCeo { get; set; }

    /// <summary>
    /// 登記範圍
    /// </summary>
    [Column("company_scope")]
    [StringLength(20)]
    public string? CompanyScope { get; set; }

    /// <summary>
    /// 專業人員人數
    /// </summary>
    [Column("company_people")]
    [StringLength(10)]
    public string? CompanyPeople { get; set; }

    [Column("city")]
    [StringLength(10)]
    public string? City { get; set; }

    [Column("district")]
    [StringLength(10)]
    public string? District { get; set; }

    [Column("street")]
    [StringLength(50)]
    public string? Street { get; set; }

    /// <summary>
    /// 公司地址	
    /// </summary>
    [Column("address")]
    [StringLength(100)]
    public string? Address { get; set; }

    /// <summary>
    /// 設立日期
    /// </summary>
    [Column("create_date")]
    public DateOnly? CreateDate { get; set; }

    /// <summary>
    /// 換發日期
    /// </summary>
    [Column("reissue_date")]
    public DateOnly? ReissueDate { get; set; }

    /// <summary>
    /// 換證期限	
    /// </summary>
    [Column("renew_period")]
    public DateOnly? RenewPeriod { get; set; }

    /// <summary>
    /// 最近異動日期
    /// </summary>
    [Column("update_date")]
    public DateOnly? UpdateDate { get; set; }

    /// <summary>
    /// 有效期限
    /// </summary>
    [Column("expirt_date")]
    public DateOnly? ExpirtDate { get; set; }

    [Column("deco_guid")]
    [StringLength(100)]
    public string? DecoGuid { get; set; }

    [Column("email")]
    [StringLength(250)]
    public string? Email { get; set; }

    [Column("phone")]
    [StringLength(20)]
    public string? Phone { get; set; }

    [Column("cellphone")]
    [StringLength(10)]
    public string? Cellphone { get; set; }

    [Column("lineid")]
    [StringLength(30)]
    public string? Lineid { get; set; }

    [Column("default_phone")]
    [StringLength(10)]
    public string? DefaultPhone { get; set; }

    [Column("website")]
    [StringLength(100)]
    public string? Website { get; set; }

    [Column("service_phone")]
    [StringLength(30)]
    public string? ServicePhone { get; set; }

    /// <summary>
    /// 註記
    /// </summary>
    [Column("note")]
    [StringLength(100)]
    public string? Note { get; set; }

    [Column("status_note")]
    [StringLength(50)]
    public string? StatusNote { get; set; }

    [Column("data_update_date", TypeName = "datetime")]
    public DateTime DataUpdateDate { get; set; }

    /// <summary>
    /// FK:_hdesigner.hdesigner_id 
    /// </summary>
    [Column("hdesigner_id")]
    public uint HdesignerId { get; set; }

    [Column("onoff")]
    public sbyte Onoff { get; set; }
}

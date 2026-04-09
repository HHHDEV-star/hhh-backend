using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("calculator_request")]
public partial class CalculatorRequest
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// 名字
    /// </summary>
    [Column("name")]
    [StringLength(50)]
    public string? Name { get; set; }

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("create_time", TypeName = "datetime")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 手機
    /// </summary>
    [Column("phone")]
    [StringLength(20)]
    public string? Phone { get; set; }

    /// <summary>
    /// 電子郵件
    /// </summary>
    [Column("email")]
    [StringLength(50)]
    public string? Email { get; set; }

    /// <summary>
    /// 所在縣市
    /// </summary>
    [Column("city")]
    [StringLength(20)]
    public string? City { get; set; }

    /// <summary>
    /// 坪數
    /// </summary>
    [Column("area")]
    [StringLength(20)]
    public string? Area { get; set; }

    /// <summary>
    /// 裝修類型(輕裝修、全室裝修、局部裝修)
    /// </summary>
    [Column("ca_type")]
    [StringLength(20)]
    public string? CaType { get; set; }

    /// <summary>
    /// (需求平台:全室A、全室B、官網)
    /// </summary>
    [Column("source_web")]
    [StringLength(45)]
    public string? SourceWeb { get; set; }

    /// <summary>
    /// 行銷同意(1:同意,0:不同意,2:無)
    /// </summary>
    [Column("marketing_consent")]
    [StringLength(10)]
    public string? MarketingConsent { get; set; }

    /// <summary>
    /// 房屋類型(主要是全室裝修A/B版有，新版沒有)
    /// </summary>
    [Column("h_class")]
    [StringLength(45)]
    public string? HClass { get; set; }

    /// <summary>
    /// utm來源
    /// </summary>
    [Column("utm_source")]
    [StringLength(45)]
    public string? UtmSource { get; set; }

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

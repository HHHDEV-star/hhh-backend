using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Table("builder_product")]
[Index("BuilderId", Name = "hbrand_id")]
[Index("Onoff", Name = "onoff")]
[Index("UpdatedAt", Name = "updated_at")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class BuilderProduct
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("builder_product_id")]
    public uint BuilderProductId { get; set; }

    /// <summary>
    /// 廠商ID
    /// </summary>
    [Column("builder_id")]
    public uint BuilderId { get; set; }

    /// <summary>
    /// 產品名稱
    /// </summary>
    [Column("name")]
    [StringLength(64)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 類型
    /// </summary>
    [Column("types")]
    [StringLength(10)]
    public string? Types { get; set; }

    /// <summary>
    /// 標籤
    /// </summary>
    [Column("btag")]
    [StringLength(250)]
    public string? Btag { get; set; }

    /// <summary>
    /// 建案類別
    /// </summary>
    [Column("builder_type")]
    [StringLength(10)]
    public string? BuilderType { get; set; }

    /// <summary>
    /// 格局規劃	
    /// </summary>
    [Column("layout")]
    [StringLength(250)]
    public string? Layout { get; set; }

    /// <summary>
    /// 總價	
    /// </summary>
    [Column("total_price")]
    [StringLength(100)]
    public string? TotalPrice { get; set; }

    /// <summary>
    /// 敘述
    /// </summary>
    [Column("descr", TypeName = "text")]
    public string Descr { get; set; } = null!;

    /// <summary>
    /// 單價
    /// </summary>
    [Column("unit_price")]
    [StringLength(100)]
    public string? UnitPrice { get; set; }

    /// <summary>
    /// 簡介
    /// </summary>
    [Column("brief", TypeName = "text")]
    public string Brief { get; set; } = null!;

    [Column("address")]
    [StringLength(255)]
    public string Address { get; set; } = null!;

    /// <summary>
    /// 縣市
    /// </summary>
    [Column("city")]
    [StringLength(10)]
    public string City { get; set; } = null!;

    [Column("website", TypeName = "text")]
    public string? Website { get; set; }

    /// <summary>
    /// 免付費電話
    /// </summary>
    [Column("service_phone")]
    [StringLength(255)]
    public string? ServicePhone { get; set; }

    [Column("phone")]
    [StringLength(255)]
    public string Phone { get; set; } = null!;

    /// <summary>
    /// 通知email
    /// </summary>
    [Column("email", TypeName = "text")]
    public string? Email { get; set; }

    /// <summary>
    /// 封面圖
    /// </summary>
    [Column("cover")]
    [StringLength(200)]
    public string Cover { get; set; } = null!;

    /// <summary>
    /// Youtube影片封面
    /// </summary>
    [Column("yt_cover")]
    [StringLength(200)]
    public string? YtCover { get; set; }

    /// <summary>
    /// 上線狀態(0:關1:開)
    /// </summary>
    [Column("onoff")]
    public sbyte Onoff { get; set; }

    /// <summary>
    /// 更新時間
    /// </summary>
    [Column("updated_at", TypeName = "timestamp")]
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// 點擊數
    /// </summary>
    [Column("clicks")]
    public int Clicks { get; set; }

    /// <summary>
    /// 寄送次數
    /// </summary>
    [Column("is_send")]
    public sbyte IsSend { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [Column("sort")]
    public int Sort { get; set; }

    /// <summary>
    /// iStaging
    /// </summary>
    [Column("istaging")]
    [StringLength(40)]
    public string? Istaging { get; set; }

    /// <summary>
    /// 負責業務郵件
    /// </summary>
    [Column("sales_email")]
    [StringLength(20)]
    public string? SalesEmail { get; set; }

    /// <summary>
    /// 負責業務助理郵件
    /// </summary>
    [Column("sales_assistant_email")]
    [StringLength(20)]
    public string? SalesAssistantEmail { get; set; }

    /// <summary>
    /// 樂居實價登錄網址
    /// </summary>
    [Column("leju_url")]
    [StringLength(200)]
    public string? LejuUrl { get; set; }

    /// <summary>
    /// 是否有影片(0:否 / 1:是
    /// </summary>
    [Column("is_video")]
    [StringLength(1)]
    public string IsVideo { get; set; } = null!;

    /// <summary>
    /// 交通評價
    /// </summary>
    [Column("review_a")]
    public sbyte ReviewA { get; set; }

    /// <summary>
    /// 生活機能評價
    /// </summary>
    [Column("review_b")]
    public sbyte ReviewB { get; set; }

    /// <summary>
    /// 建材公設評價
    /// </summary>
    [Column("review_c")]
    public sbyte ReviewC { get; set; }

    /// <summary>
    /// 總價評價
    /// </summary>
    [Column("review_d")]
    public sbyte ReviewD { get; set; }

    /// <summary>
    /// 空間坪數評價
    /// </summary>
    [Column("review_e")]
    public sbyte ReviewE { get; set; }

    [Column("w")]
    [StringLength(10)]
    public string? W { get; set; }

    [Column("h")]
    [StringLength(10)]
    public string? H { get; set; }

    /// <summary>
    /// 更新時間
    /// </summary>
    [Column("update_time", TypeName = "timestamp")]
    public DateTime UpdateTime { get; set; }
}

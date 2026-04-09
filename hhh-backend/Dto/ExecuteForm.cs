using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("execute_form")]
[Index("IsDelete", Name = "is_delete")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class ExecuteForm
{
    /// <summary>
    /// PK
    /// </summary>
    [Key]
    [Column("exf_id")]
    public uint ExfId { get; set; }

    /// <summary>
    /// 合約編號
    /// </summary>
    [Column("num")]
    [StringLength(50)]
    public string Num { get; set; } = null!;

    /// <summary>
    /// 設計公司
    /// </summary>
    [Column("company")]
    [StringLength(50)]
    public string Company { get; set; } = null!;

    /// <summary>
    /// 手機
    /// </summary>
    [Column("mobile")]
    [StringLength(15)]
    public string Mobile { get; set; } = null!;

    /// <summary>
    /// 電話
    /// </summary>
    [Column("telete")]
    [StringLength(50)]
    public string Telete { get; set; } = null!;

    /// <summary>
    /// 設計師
    /// </summary>
    [Column("designer")]
    [StringLength(50)]
    public string Designer { get; set; } = null!;

    /// <summary>
    /// 上架日期
    /// </summary>
    [Column("sdate")]
    public DateOnly Sdate { get; set; }

    /// <summary>
    /// 下架日期
    /// </summary>
    [Column("edate")]
    public DateOnly Edate { get; set; }

    /// <summary>
    /// 到期日
    /// </summary>
    [Column("contract_time")]
    public DateOnly ContractTime { get; set; }

    /// <summary>
    /// 聯絡人
    /// </summary>
    [Column("contract_person")]
    [StringLength(50)]
    public string? ContractPerson { get; set; }

    /// <summary>
    /// 接案業務
    /// </summary>
    [Column("sales_man")]
    [StringLength(50)]
    public string SalesMan { get; set; } = null!;

    /// <summary>
    /// 是否結案(N:否 / Y:是 / T:未上線
    /// </summary>
    [Column("is_close")]
    [StringLength(1)]
    public string IsClose { get; set; } = null!;

    /// <summary>
    /// 是否刪除(Y:是 / N:否)
    /// </summary>
    [Column("is_delete")]
    [StringLength(1)]
    public string IsDelete { get; set; } = null!;

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("create_time", TypeName = "datetime")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 最後更新者
    /// </summary>
    [Column("last_update")]
    [StringLength(50)]
    public string? LastUpdate { get; set; }

    /// <summary>
    /// 更新時間
    /// </summary>
    [Column("update_time", TypeName = "datetime")]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// 部門別
    /// </summary>
    [Column("sales_dept")]
    [StringLength(20)]
    public string? SalesDept { get; set; }

    /// <summary>
    /// 業務同意
    /// </summary>
    [Column("allow_sales")]
    [StringLength(50)]
    public string AllowSales { get; set; } = null!;

    /// <summary>
    /// 業務同意時間
    /// </summary>
    [Column("allow_sales_time", TypeName = "datetime")]
    public DateTime AllowSalesTime { get; set; }

    /// <summary>
    /// 財務同意
    /// </summary>
    [Column("allow_finance")]
    [StringLength(50)]
    public string AllowFinance { get; set; } = null!;

    /// <summary>
    /// 財務同意時間
    /// </summary>
    [Column("allow_finance_time", TypeName = "datetime")]
    public DateTime AllowFinanceTime { get; set; }

    /// <summary>
    /// 主管同意
    /// </summary>
    [Column("allow_man")]
    [StringLength(50)]
    public string AllowMan { get; set; } = null!;

    /// <summary>
    /// 同意執行時間
    /// </summary>
    [Column("allow_time", TypeName = "datetime")]
    public DateTime AllowTime { get; set; }

    /// <summary>
    /// 合約樣板
    /// </summary>
    [Column("detail_status")]
    [StringLength(20)]
    public string DetailStatus { get; set; } = null!;

    /// <summary>
    /// 合約金額(含稅
    /// </summary>
    [Column("tax_included_price")]
    public uint TaxIncludedPrice { get; set; }

    /// <summary>
    /// 合約金額(未稅)
    /// </summary>
    [Column("price")]
    public uint Price { get; set; }

    /// <summary>
    /// 建立者
    /// </summary>
    [Column("creator")]
    [StringLength(50)]
    public string? Creator { get; set; }

    /// <summary>
    /// 額度(已使用數/總額度數)
    /// </summary>
    [Column("quota")]
    [StringLength(10)]
    public string? Quota { get; set; }

    /// <summary>
    /// 備註說明
    /// </summary>
    [Column("note", TypeName = "text")]
    public string? Note { get; set; }

    /// <summary>
    /// FB投放費用
    /// </summary>
    [Column("FB_price")]
    [StringLength(45)]
    public string FbPrice { get; set; } = null!;

    /// <summary>
    /// YT投放費用
    /// </summary>
    [Column("YT_price")]
    [StringLength(45)]
    public string YtPrice { get; set; } = null!;

    /// <summary>
    /// 攝影外拍費用
    /// </summary>
    [Column("photo_outside_price")]
    [StringLength(45)]
    public string PhotoOutsidePrice { get; set; } = null!;

    /// <summary>
    /// 攝影交通費
    /// </summary>
    [Column("photo_trans_price")]
    [StringLength(45)]
    public string PhotoTransPrice { get; set; } = null!;

    /// <summary>
    /// 主持費用
    /// </summary>
    [Column("host_price")]
    [StringLength(45)]
    public string HostPrice { get; set; } = null!;

    /// <summary>
    /// 轉約前號碼(用,隔開)
    /// </summary>
    [Column("transfer_num")]
    [StringLength(256)]
    public string? TransferNum { get; set; }
}

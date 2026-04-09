using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

/// <summary>
/// 經紀人表單
/// </summary>
[Table("agent_form")]
[Index("IsDel", Name = "is_del")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class AgentForm
{
    /// <summary>
    /// PK
    /// </summary>
    [Key]
    [Column("agent_id")]
    public uint AgentId { get; set; }

    /// <summary>
    /// 聯繫人員
    /// </summary>
    [Column("content_for")]
    [StringLength(10)]
    public string ContentFor { get; set; } = null!;

    /// <summary>
    /// 屋主姓名
    /// </summary>
    [Column("fullname")]
    [StringLength(10)]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string Fullname { get; set; } = null!;

    /// <summary>
    /// 市話
    /// </summary>
    [Column("phone")]
    [StringLength(20)]
    public string? Phone { get; set; }

    /// <summary>
    /// 手機
    /// </summary>
    [Column("cellphone")]
    [StringLength(20)]
    public string? Cellphone { get; set; }

    /// <summary>
    /// EMAIL
    /// </summary>
    [Column("email")]
    [StringLength(200)]
    public string? Email { get; set; }

    /// <summary>
    /// 需要進行
    /// </summary>
    [Column("need_item")]
    [StringLength(200)]
    public string NeedItem { get; set; } = null!;

    /// <summary>
    /// 預計裝修時間
    /// </summary>
    [Column("decoration_time")]
    public DateOnly DecorationTime { get; set; }

    /// <summary>
    /// 縣市
    /// </summary>
    [Column("county")]
    [StringLength(10)]
    public string? County { get; set; }

    /// <summary>
    /// 區域
    /// </summary>
    [Column("district")]
    [StringLength(10)]
    public string? District { get; set; }

    /// <summary>
    /// 裝修地址
    /// </summary>
    [Column("address")]
    [StringLength(255)]
    public string? Address { get; set; }

    /// <summary>
    /// 房屋類型
    /// </summary>
    [Column("house_type")]
    [StringLength(20)]
    public string? HouseType { get; set; }

    /// <summary>
    /// 中古幾年
    /// </summary>
    [Column("house_type_year")]
    [StringLength(50)]
    public string? HouseTypeYear { get; set; }

    /// <summary>
    /// 房屋型態
    /// </summary>
    [Column("house_status")]
    [StringLength(20)]
    public string? HouseStatus { get; set; }

    /// <summary>
    /// 大樓高
    /// </summary>
    [Column("house_status_high")]
    [StringLength(10)]
    public string? HouseStatusHigh { get; set; }

    /// <summary>
    /// 裝修幾樓	
    /// </summary>
    [Column("house_status_floor")]
    [StringLength(10)]
    public string? HouseStatusFloor { get; set; }

    /// <summary>
    /// 空間坪數 權狀
    /// </summary>
    [Column("location_ping_paper")]
    [StringLength(30)]
    public string? LocationPingPaper { get; set; }

    /// <summary>
    /// 空間坪數 實際
    /// </summary>
    [Column("location_ping_real")]
    [StringLength(30)]
    public string? LocationPingReal { get; set; }

    /// <summary>
    /// 目前格局 房
    /// </summary>
    [Column("plecement_h")]
    public byte? PlecementH { get; set; }

    /// <summary>
    /// 目前格局 廳
    /// </summary>
    [Column("plecement_c")]
    public byte? PlecementC { get; set; }

    /// <summary>
    /// 目前格局 衛
    /// </summary>
    [Column("plecement_t")]
    public byte? PlecementT { get; set; }

    /// <summary>
    /// 即將入住成員
    /// </summary>
    [Column("family")]
    [StringLength(255)]
    public string? Family { get; set; }

    /// <summary>
    /// 風格需求
    /// </summary>
    [Column("need_style")]
    [StringLength(255)]
    public string? NeedStyle { get; set; }

    /// <summary>
    /// 其他風格
    /// </summary>
    [Column("need_style_other")]
    [StringLength(255)]
    public string? NeedStyleOther { get; set; }

    /// <summary>
    /// 何處得知幸福經紀人 其他
    /// </summary>
    [Column("agent_where_other")]
    [StringLength(50)]
    public string? AgentWhereOther { get; set; }

    /// <summary>
    /// 是否更新 array
    /// </summary>
    [Column("need_update_array")]
    [StringLength(200)]
    public string? NeedUpdateArray { get; set; }

    /// <summary>
    /// 設計師/廠商可否聯繫
    /// </summary>
    [Column("designer_content")]
    public byte DesignerContent { get; set; }

    /// <summary>
    /// 初步聯繫方式
    /// </summary>
    [Column("content_way")]
    [StringLength(200)]
    public string? ContentWay { get; set; }

    /// <summary>
    /// 聯繫時間
    /// </summary>
    [Column("content_time")]
    [StringLength(50)]
    public string? ContentTime { get; set; }

    /// <summary>
    /// 何處得知幸福經紀人
    /// </summary>
    [Column("agent_where")]
    [StringLength(200)]
    public string? AgentWhere { get; set; }

    /// <summary>
    /// 管道來源
    /// </summary>
    [Column("agent_source")]
    [StringLength(50)]
    public string? AgentSource { get; set; }

    /// <summary>
    /// 是否同意行情價3-5 
    /// </summary>
    [Column("market_rule")]
    public sbyte? MarketRule { get; set; }

    /// <summary>
    /// 是否同意行情價6-12 
    /// </summary>
    [Column("market_rule1")]
    public sbyte? MarketRule1 { get; set; }

    /// <summary>
    /// 裝修預算
    /// </summary>
    [Column("budget")]
    [StringLength(50)]
    public string? Budget { get; set; }

    /// <summary>
    /// 提案/丈量費預算
    /// </summary>
    [Column("mbudget")]
    [StringLength(50)]
    public string? Mbudget { get; set; }

    /// <summary>
    /// 需要申請優惠貸款
    /// </summary>
    [Column("agent_loan")]
    public byte? AgentLoan { get; set; }

    /// <summary>
    /// 推薦設計公司
    /// </summary>
    [Column("design_company")]
    [StringLength(100)]
    public string? DesignCompany { get; set; }

    /// <summary>
    /// 下次追蹤時間
    /// </summary>
    [Column("follow_time")]
    public DateOnly? FollowTime { get; set; }

    /// <summary>
    /// 備註
    /// </summary>
    [Column("agent_note", TypeName = "mediumtext")]
    public string? AgentNote { get; set; }

    [Column("is_del")]
    public sbyte IsDel { get; set; }

    /// <summary>
    /// 新增時間
    /// </summary>
    [Column("date_added", TypeName = "datetime")]
    public DateTime DateAdded { get; set; }

    /// <summary>
    /// 修改時間
    /// </summary>
    [Column("date_modified", TypeName = "datetime")]
    public DateTime DateModified { get; set; }

    /// <summary>
    /// 挑高
    /// </summary>
    [Column("higher")]
    [StringLength(10)]
    public string? Higher { get; set; }

    /// <summary>
    /// 初步面台時間
    /// </summary>
    [Column("interview_time")]
    [StringLength(100)]
    public string? InterviewTime { get; set; }

    /// <summary>
    /// 備註
    /// </summary>
    [Column("customer_note", TypeName = "text")]
    public string? CustomerNote { get; set; }
}

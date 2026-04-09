using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Table("designer_branch")]
[Index("DesignerId", Name = "designer_id")]
public partial class DesignerBranch
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// 設計師id
    /// </summary>
    [Column("designer_id")]
    public int DesignerId { get; set; }

    /// <summary>
    /// 公司抬頭
    /// </summary>
    [Column("title")]
    [StringLength(50)]
    public string? Title { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    [Column("address")]
    [StringLength(100)]
    public string? Address { get; set; }

    /// <summary>
    /// 電話
    /// </summary>
    [Column("tel")]
    [StringLength(20)]
    public string? Tel { get; set; }

    /// <summary>
    /// 傳真
    /// </summary>
    [Column("fax")]
    [StringLength(20)]
    public string? Fax { get; set; }

    /// <summary>
    /// 信箱
    /// </summary>
    [Column("email")]
    [StringLength(128)]
    public string? Email { get; set; }

    /// <summary>
    /// line帳號
    /// </summary>
    [Column("line")]
    [StringLength(128)]
    public string? Line { get; set; }

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("create_time", TypeName = "timestamp")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新時間
    /// </summary>
    [Column("update_time", TypeName = "timestamp")]
    public DateTime? UpdateTime { get; set; }

    [Column("branch_office")]
    [StringLength(45)]
    public string BranchOffice { get; set; } = null!;

    [Column("branch_office_title")]
    [StringLength(45)]
    public string BranchOfficeTitle { get; set; } = null!;
}

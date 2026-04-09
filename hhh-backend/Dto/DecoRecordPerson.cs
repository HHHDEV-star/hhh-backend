using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

/// <summary>
/// 聘用專業技術人員清冊
/// </summary>
[Table("deco_record_person")]
[Index("Bldsno", Name = "bldsno")]
[Index("Name", Name = "name")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class DecoRecordPerson
{
    [Key]
    [Column("seq")]
    public int Seq { get; set; }

    [Column("bldsno")]
    public int Bldsno { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    [Column("name")]
    [StringLength(10)]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 登記證書字號
    /// </summary>
    [Column("record_number")]
    [StringLength(15)]
    public string RecordNumber { get; set; } = null!;

    /// <summary>
    /// 到職日期
    /// </summary>
    [Column("employment_date")]
    public DateOnly? EmploymentDate { get; set; }

    [Column("register_date")]
    public DateOnly? RegisterDate { get; set; }

    [Column("expiry_date")]
    [StringLength(20)]
    public string? ExpiryDate { get; set; }

    /// <summary>
    /// 登記資格
    /// </summary>
    [Column("registration")]
    [StringLength(20)]
    public string Registration { get; set; } = null!;

    [Column("update_date", TypeName = "datetime")]
    public DateTime UpdateDate { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.HHHBackstage;

/// <summary>
/// 專案資料
/// </summary>
[Table("acl_projects")]
[Index("App", Name = "app_UNIQUE", IsUnique = true)]
public partial class AclProject
{
    /// <summary>
    /// 主鍵
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// 專案名稱
    /// </summary>
    [Column("name")]
    [StringLength(45)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 專案app資料夾名稱
    /// </summary>
    [Column("app")]
    [StringLength(45)]
    public string App { get; set; } = null!;

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("create_date", TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    /// <summary>
    /// 編輯時間
    /// </summary>
    [Column("update_date", TypeName = "datetime")]
    public DateTime? UpdateDate { get; set; }
}

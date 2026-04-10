using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.HHHBackstage;

/// <summary>
/// 連結路徑資料
/// </summary>
[Table("acl_menu_path")]
[Index("MenuGroupId", Name = "menu_group_id")]
[Index("ProjectId", Name = "project_id")]
public partial class AclMenuPath
{
    /// <summary>
    /// 主鍵
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("menu_group_id")]
    public int MenuGroupId { get; set; }

    /// <summary>
    /// 功能所屬專案編號
    /// </summary>
    [Column("project_id")]
    public int ProjectId { get; set; }

    /// <summary>
    /// 功能名稱
    /// </summary>
    [Column("name")]
    [StringLength(45)]
    public string? Name { get; set; }

    /// <summary>
    /// 功能路徑
    /// </summary>
    [Column("path")]
    [StringLength(100)]
    public string? Path { get; set; }

    /// <summary>
    /// 功能排序
    /// </summary>
    [Column("sort_num")]
    public int? SortNum { get; set; }

    /// <summary>
    /// 是否顯示在目錄(0:否,1:是)
    /// </summary>
    [Column("is_show", TypeName = "enum('0','1')")]
    public string IsShow { get; set; } = null!;

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

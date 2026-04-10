using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.HHHBackstage;

[Table("acl_menu_group")]
public partial class AclMenuGroup
{
    /// <summary>
    /// 主鍵
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// 顯示圖標
    /// </summary>
    [Column("icon")]
    [StringLength(50)]
    public string? Icon { get; set; }

    /// <summary>
    /// 目錄群組名稱
    /// </summary>
    [Column("name")]
    [StringLength(45)]
    public string? Name { get; set; }

    /// <summary>
    /// 群組排序
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

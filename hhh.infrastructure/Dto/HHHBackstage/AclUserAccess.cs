using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.HHHBackstage;

/// <summary>
/// 使用者訪問權限
/// </summary>
[Table("acl_user_access")]
[Index("MenuPathId", Name = "menu_path_id")]
[Index("UserId", Name = "user_id")]
[Index("UserId", "MenuPathId", Name = "user_id_menu_path_id", IsUnique = true)]
public partial class AclUserAccess
{
    /// <summary>
    /// 主鍵
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// 使用者編號
    /// </summary>
    [Column("user_id")]
    public int UserId { get; set; }

    /// <summary>
    /// 目錄路徑編號
    /// </summary>
    [Column("menu_path_id")]
    public int MenuPathId { get; set; }

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("create_date", TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }
}

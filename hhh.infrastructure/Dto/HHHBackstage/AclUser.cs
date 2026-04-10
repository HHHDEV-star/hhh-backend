using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.HHHBackstage;

/// <summary>
/// 使用者帳號資訊
/// </summary>
[Table("acl_users")]
[Index("Account", Name = "account_UNIQUE", IsUnique = true)]
public partial class AclUser
{
    /// <summary>
    /// 主鍵
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// 使用者名稱
    /// </summary>
    [Column("name")]
    [StringLength(45)]
    public string? Name { get; set; }

    /// <summary>
    /// 帳號
    /// </summary>
    [Column("account")]
    [StringLength(45)]
    public string Account { get; set; } = null!;

    /// <summary>
    /// 電子郵件
    /// </summary>
    [Column("email")]
    [StringLength(50)]
    public string Email { get; set; } = null!;

    /// <summary>
    /// 密碼
    /// </summary>
    [Column("pwd")]
    [StringLength(50)]
    public string Pwd { get; set; } = null!;

    /// <summary>
    /// 是否刪除(0:否 1:是)
    /// </summary>
    [Column("is_del", TypeName = "enum('0','1')")]
    public string IsDel { get; set; } = null!;

    /// <summary>
    /// 是否可以遠端(0:否 / 1:可
    /// </summary>
    [Column("is_remote", TypeName = "enum('0','1')")]
    public string IsRemote { get; set; } = null!;

    /// <summary>
    /// 業務身份(0:否 / 1:是)
    /// </summary>
    [Column("is_execute_sales", TypeName = "enum('0','1')")]
    public string IsExecuteSales { get; set; } = null!;

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

    /// <summary>
    /// 登入時間
    /// </summary>
    [Column("login_date", TypeName = "datetime")]
    public DateTime LoginDate { get; set; }

    /// <summary>
    /// 職位
    /// </summary>
    [Column("position")]
    [StringLength(50)]
    public string Position { get; set; } = null!;
}

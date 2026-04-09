using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("admin")]
[Index("Account", Name = "account", IsUnique = true)]
public partial class Admin
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("id")]
    public uint Id { get; set; }

    /// <summary>
    /// 帳號
    /// </summary>
    [Column("account")]
    [StringLength(20)]
    public string Account { get; set; } = null!;

    /// <summary>
    /// 密碼
    /// </summary>
    [Column("pwd")]
    [StringLength(40)]
    public string Pwd { get; set; } = null!;

    /// <summary>
    /// 名稱
    /// </summary>
    [Column("name")]
    [StringLength(40)]
    public string? Name { get; set; }

    /// <summary>
    /// 信箱
    /// </summary>
    [Column("email")]
    [StringLength(200)]
    public string? Email { get; set; }

    /// <summary>
    /// 電話
    /// </summary>
    [Column("tel")]
    [StringLength(20)]
    public string? Tel { get; set; }

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("create_time", TypeName = "timestamp")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 允許使用頁面
    /// </summary>
    [Column("allow_page", TypeName = "text")]
    public string? AllowPage { get; set; }

    /// <summary>
    /// 啟用狀態
    /// </summary>
    [Column("is_active")]
    public sbyte IsActive { get; set; }
}

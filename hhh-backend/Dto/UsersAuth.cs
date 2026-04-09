using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("_users_auth")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class UsersAuth
{
    /// <summary>
    /// PK
    /// </summary>
    [Key]
    [Column("id")]
    public uint Id { get; set; }

    /// <summary>
    /// FK : _users.uid
    /// </summary>
    [Column("uid")]
    public int Uid { get; set; }

    /// <summary>
    /// 認證碼
    /// </summary>
    [Column("guid")]
    public Guid Guid { get; set; }

    /// <summary>
    /// 是否啟用(0:未啟用 / 1:已啟用
    /// </summary>
    [Column("status")]
    [StringLength(1)]
    public string Status { get; set; } = null!;

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("create_time", TypeName = "datetime")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 認證時間
    /// </summary>
    [Column("auth_datetime", TypeName = "datetime")]
    public DateTime? AuthDatetime { get; set; }
}

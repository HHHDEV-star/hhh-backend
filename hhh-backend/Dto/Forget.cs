using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("_forget")]
public partial class Forget
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("forget_id")]
    public int ForgetId { get; set; }

    /// <summary>
    /// email
    /// </summary>
    [Column("email")]
    [StringLength(128)]
    public string Email { get; set; } = null!;

    /// <summary>
    /// 會員全球唯一碼
    /// </summary>
    [Column("guid")]
    public Guid Guid { get; set; }

    /// <summary>
    /// eamil識別碼
    /// </summary>
    [Column("email_guid")]
    public Guid EmailGuid { get; set; }

    /// <summary>
    /// 狀態(0:未使用 1:已使用)
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
    /// 更新時間
    /// </summary>
    [Column("update_time", TypeName = "datetime")]
    public DateTime UpdateTime { get; set; }
}

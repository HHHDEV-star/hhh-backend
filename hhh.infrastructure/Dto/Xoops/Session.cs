using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("_session")]
[Index("SessUpdated", Name = "updated")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class Session
{
    /// <summary>
    /// seeionID
    /// </summary>
    [Key]
    [Column("sess_id")]
    [StringLength(32)]
    public string SessId { get; set; } = null!;

    /// <summary>
    /// 更新時間
    /// </summary>
    [Column("sess_updated")]
    public uint SessUpdated { get; set; }

    /// <summary>
    /// IP
    /// </summary>
    [Column("sess_ip")]
    [StringLength(15)]
    public string SessIp { get; set; } = null!;

    /// <summary>
    /// session資料
    /// </summary>
    [Column("sess_data", TypeName = "text")]
    public string? SessData { get; set; }
}

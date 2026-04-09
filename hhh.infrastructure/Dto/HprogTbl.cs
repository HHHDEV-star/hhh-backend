using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Keyless]
[Table("_hprog_tbl")]
[Index("HprogId", Name = "hprog_id")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class HprogTbl
{
    /// <summary>
    /// pk
    /// </summary>
    [Column("hprog_id")]
    public uint HprogId { get; set; }

    [Column("sn")]
    public uint Sn { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [Column("order")]
    public ushort Order { get; set; }
}

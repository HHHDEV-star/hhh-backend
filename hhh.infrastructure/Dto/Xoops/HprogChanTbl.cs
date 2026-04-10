using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Keyless]
[Table("_hprog_chan_tbl")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class HprogChanTbl
{
    [Column("program")]
    [StringLength(32)]
    public string Program { get; set; } = null!;

    [Column("chan_id")]
    public uint ChanId { get; set; }

    [Column("order")]
    public uint Order { get; set; }
}

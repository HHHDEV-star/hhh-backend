using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Keyless]
[Table("tmp_tbl")]
public partial class TmpTbl
{
    [Column("colId")]
    public int? ColId { get; set; }
}

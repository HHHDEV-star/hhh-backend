using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("_hcolumn_page")]
[Index("HcolumnId", Name = "hcolumn_id")]
public partial class HcolumnPage
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("sn")]
    public uint Sn { get; set; }

    /// <summary>
    /// 專欄ID
    /// </summary>
    [Column("hcolumn_id")]
    public uint HcolumnId { get; set; }

    /// <summary>
    /// 自由內容html
    /// </summary>
    [Column("cphtml", TypeName = "text")]
    public string Cphtml { get; set; } = null!;

    /// <summary>
    /// 排序
    /// </summary>
    [Column("cporder")]
    public byte Cporder { get; set; }
}

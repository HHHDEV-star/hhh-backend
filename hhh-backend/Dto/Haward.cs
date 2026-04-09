using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("_hawards")]
[Index("HcaseId", Name = "hcase_id")]
[Index("HdesignerId", Name = "hdesigner_id")]
public partial class Haward
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("hawards_id")]
    public uint HawardsId { get; set; }

    /// <summary>
    /// 獎項名稱
    /// </summary>
    [Column("awards_name")]
    [StringLength(128)]
    public string AwardsName { get; set; } = null!;

    /// <summary>
    /// 設計師ID
    /// </summary>
    [Column("hdesigner_id")]
    public uint HdesignerId { get; set; }

    /// <summary>
    /// 個案ID
    /// </summary>
    [Column("hcase_id")]
    public uint HcaseId { get; set; }

    /// <summary>
    /// 獎項LOGO
    /// </summary>
    [Column("logo")]
    [StringLength(32)]
    public string Logo { get; set; } = null!;

    /// <summary>
    /// 上線狀態(0:關1:開)
    /// </summary>
    [Column("onoff")]
    public byte Onoff { get; set; }
}

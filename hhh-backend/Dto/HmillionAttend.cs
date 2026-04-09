using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[PrimaryKey("Uid", "Year")]
[Table("_hmillion_attend")]
[Index("AttendStatus", Name = "attend_status")]
[Index("HouseCity", Name = "house_city")]
public partial class HmillionAttend
{
    [Key]
    [Column("uid")]
    public uint Uid { get; set; }

    [Key]
    [Column("year")]
    public ushort Year { get; set; }

    [Column("house_city")]
    [StringLength(32)]
    public string HouseCity { get; set; } = null!;

    [Column("house_area")]
    [StringLength(32)]
    public string HouseArea { get; set; } = null!;

    [Column("house_type")]
    [StringLength(48)]
    public string HouseType { get; set; } = null!;

    [Column("house_ping")]
    public uint HousePing { get; set; }

    [Column("house_style")]
    [StringLength(48)]
    public string HouseStyle { get; set; } = null!;

    [Column("house_budget")]
    [StringLength(32)]
    public string HouseBudget { get; set; } = null!;

    [Column("txt1", TypeName = "text")]
    public string Txt1 { get; set; } = null!;

    [Column("txt2", TypeName = "text")]
    public string Txt2 { get; set; } = null!;

    [Column("txt3", TypeName = "text")]
    public string Txt3 { get; set; } = null!;

    [Column("txt4", TypeName = "text")]
    public string Txt4 { get; set; } = null!;

    [Column("applytime", TypeName = "timestamp")]
    public DateTime Applytime { get; set; }

    [Column("ip")]
    [StringLength(16)]
    public string Ip { get; set; } = null!;

    [Column("extsn")]
    [StringLength(32)]
    public string Extsn { get; set; } = null!;

    [Column("attend_status")]
    [StringLength(8)]
    public string AttendStatus { get; set; } = null!;

    [Column("luckydraw")]
    [StringLength(32)]
    public string Luckydraw { get; set; } = null!;

    [Column("vote_title")]
    [StringLength(64)]
    public string VoteTitle { get; set; } = null!;

    [Column("vote")]
    public uint Vote { get; set; }

    [Column("vote_plus")]
    public uint VotePlus { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("_hcontest_vote")]
[Index("Uid", "ContestId", Name = "uid", IsUnique = true)]
[Index("VoteType", Name = "vote_type")]
[Index("Year", Name = "year")]
public partial class HcontestVote
{
    [Key]
    [Column("sn")]
    public uint Sn { get; set; }

    [Column("year")]
    public ushort Year { get; set; }

    [Column("vote_type")]
    [StringLength(8)]
    public string VoteType { get; set; } = null!;

    [Column("c1")]
    [StringLength(32)]
    public string C1 { get; set; } = null!;

    [Column("uid")]
    public uint Uid { get; set; }

    [Column("contest_id")]
    public uint ContestId { get; set; }

    [Column("score")]
    public ushort Score { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[PrimaryKey("VoteDate", "VoterUid")]
[Table("_hmillion_vote")]
[Index("CandidateUid", Name = "candidate_uid")]
[Index("Year", Name = "year")]
public partial class HmillionVote
{
    [Column("year")]
    public ushort Year { get; set; }

    [Key]
    [Column("vote_date")]
    public DateOnly VoteDate { get; set; }

    [Key]
    [Column("voter_uid")]
    public uint VoterUid { get; set; }

    [Column("voter_ip")]
    [StringLength(16)]
    public string VoterIp { get; set; } = null!;

    [Column("vote_time")]
    public uint VoteTime { get; set; }

    [Column("candidate_uid")]
    public uint CandidateUid { get; set; }
}

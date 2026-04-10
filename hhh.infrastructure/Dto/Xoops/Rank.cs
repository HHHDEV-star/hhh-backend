using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("_ranks")]
[Index("RankMax", Name = "rank_max")]
[Index("RankMin", Name = "rank_min")]
[Index("RankMin", "RankMax", "RankSpecial", Name = "rankminrankmaxranspecial")]
[Index("RankSpecial", Name = "rankspecial")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class Rank
{
    [Key]
    [Column("rank_id")]
    public ushort RankId { get; set; }

    [Column("rank_title")]
    [StringLength(50)]
    public string RankTitle { get; set; } = null!;

    [Column("rank_min", TypeName = "mediumint unsigned")]
    public uint RankMin { get; set; }

    [Column("rank_max", TypeName = "mediumint unsigned")]
    public uint RankMax { get; set; }

    [Column("rank_special")]
    public byte RankSpecial { get; set; }

    [Column("rank_image")]
    [StringLength(255)]
    public string? RankImage { get; set; }
}

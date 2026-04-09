using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Table("_smiles")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class Smile
{
    [Key]
    [Column("id")]
    public ushort Id { get; set; }

    [Column("code")]
    [StringLength(50)]
    public string Code { get; set; } = null!;

    [Column("smile_url")]
    [StringLength(100)]
    public string SmileUrl { get; set; } = null!;

    [Column("emotion")]
    [StringLength(75)]
    public string Emotion { get; set; } = null!;

    [Column("display")]
    public bool Display { get; set; }
}

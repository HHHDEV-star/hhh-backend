using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Table("deco_request_files")]
[Index("Seq", Name = "exf_id")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class DecoRequestFile
{
    [Key]
    [Column("deco_file_id")]
    public uint DecoFileId { get; set; }

    [Column("seq")]
    public int Seq { get; set; }

    [Column("orig_name")]
    [StringLength(255)]
    public string OrigName { get; set; } = null!;

    [Column("file_name")]
    [StringLength(255)]
    public string FileName { get; set; } = null!;

    [Column("create_time", TypeName = "datetime")]
    public DateTime CreateTime { get; set; }
}

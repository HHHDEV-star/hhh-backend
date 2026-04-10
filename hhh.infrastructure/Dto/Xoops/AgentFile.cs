using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("agent_files")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class AgentFile
{
    [Key]
    [Column("file_id")]
    public uint FileId { get; set; }

    [Column("agent_id")]
    public uint AgentId { get; set; }

    [Column("file_name")]
    [StringLength(50)]
    public string FileName { get; set; } = null!;

    [Column("file_url")]
    [StringLength(255)]
    public string FileUrl { get; set; } = null!;

    [Column("date_added", TypeName = "datetime")]
    public DateTime DateAdded { get; set; }

    [Column("is_del")]
    public sbyte IsDel { get; set; }
}

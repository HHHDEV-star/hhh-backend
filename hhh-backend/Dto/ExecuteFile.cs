using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("execute_files")]
[Index("ExfId", Name = "exf_id")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class ExecuteFile
{
    [Key]
    [Column("exf_file_id")]
    public uint ExfFileId { get; set; }

    [Column("exf_id")]
    public int ExfId { get; set; }

    [Column("orig_name")]
    [StringLength(255)]
    public string OrigName { get; set; } = null!;

    [Column("file_name")]
    [StringLength(255)]
    public string FileName { get; set; } = null!;

    [Column("create_time", TypeName = "datetime")]
    public DateTime CreateTime { get; set; }
}

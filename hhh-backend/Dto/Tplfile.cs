using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("_tplfile")]
[Index("TplRefid", "TplType", Name = "tpl_refid")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class Tplfile
{
    [Key]
    [Column("tpl_id", TypeName = "mediumint unsigned")]
    public uint TplId { get; set; }

    [Column("tpl_refid")]
    public ushort TplRefid { get; set; }

    [Column("tpl_module")]
    [StringLength(25)]
    public string TplModule { get; set; } = null!;

    [Column("tpl_tplset")]
    [StringLength(50)]
    public string TplTplset { get; set; } = null!;

    [Column("tpl_file")]
    [StringLength(50)]
    public string TplFile1 { get; set; } = null!;

    [Column("tpl_desc")]
    [StringLength(255)]
    public string TplDesc { get; set; } = null!;

    [Column("tpl_lastmodified")]
    public uint TplLastmodified { get; set; }

    [Column("tpl_lastimported")]
    public uint TplLastimported { get; set; }

    [Column("tpl_type")]
    [StringLength(20)]
    public string TplType { get; set; } = null!;
}

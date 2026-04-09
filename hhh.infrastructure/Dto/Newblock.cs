using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Table("_newblocks")]
[Index("Isactive", "Visible", "Mid", Name = "isactive_visible_mid")]
[Index("Mid", Name = "mid")]
[Index("Mid", "FuncNum", Name = "mid_funcnum")]
[Index("Visible", Name = "visible")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class Newblock
{
    [Key]
    [Column("bid", TypeName = "mediumint unsigned")]
    public uint Bid { get; set; }

    [Column("mid")]
    public ushort Mid { get; set; }

    [Column("func_num")]
    public byte FuncNum { get; set; }

    [Column("options")]
    [StringLength(255)]
    public string Options { get; set; } = null!;

    [Column("name")]
    [StringLength(150)]
    public string Name { get; set; } = null!;

    [Column("title")]
    [StringLength(255)]
    public string Title { get; set; } = null!;

    [Column("content", TypeName = "text")]
    public string? Content { get; set; }

    [Column("side")]
    public byte Side { get; set; }

    [Column("weight")]
    public ushort Weight { get; set; }

    [Column("visible")]
    public byte Visible { get; set; }

    [Column("block_type")]
    [StringLength(1)]
    public string BlockType { get; set; } = null!;

    [Column("c_type")]
    [StringLength(1)]
    public string CType { get; set; } = null!;

    [Column("isactive")]
    public byte Isactive { get; set; }

    [Column("dirname")]
    [StringLength(50)]
    public string Dirname { get; set; } = null!;

    [Column("func_file")]
    [StringLength(50)]
    public string FuncFile { get; set; } = null!;

    [Column("show_func")]
    [StringLength(50)]
    public string ShowFunc { get; set; } = null!;

    [Column("edit_func")]
    [StringLength(50)]
    public string EditFunc { get; set; } = null!;

    [Column("template")]
    [StringLength(50)]
    public string Template { get; set; } = null!;

    [Column("bcachetime")]
    public uint Bcachetime { get; set; }

    [Column("last_modified")]
    public uint LastModified { get; set; }
}

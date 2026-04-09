using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("_config")]
[Index("ConfModid", "ConfCatid", Name = "conf_mod_cat_id")]
[Index("ConfOrder", Name = "conf_order")]
public partial class Config
{
    [Key]
    [Column("conf_id")]
    public ushort ConfId { get; set; }

    [Column("conf_modid")]
    public ushort ConfModid { get; set; }

    [Column("conf_catid")]
    public ushort ConfCatid { get; set; }

    [Column("conf_name")]
    [StringLength(25)]
    public string ConfName { get; set; } = null!;

    [Column("conf_title")]
    [StringLength(255)]
    public string ConfTitle { get; set; } = null!;

    [Column("conf_value", TypeName = "text")]
    public string? ConfValue { get; set; }

    [Column("conf_desc")]
    [StringLength(255)]
    public string ConfDesc { get; set; } = null!;

    [Column("conf_formtype")]
    [StringLength(15)]
    public string ConfFormtype { get; set; } = null!;

    [Column("conf_valuetype")]
    [StringLength(10)]
    public string ConfValuetype { get; set; } = null!;

    [Column("conf_order")]
    public ushort ConfOrder { get; set; }
}

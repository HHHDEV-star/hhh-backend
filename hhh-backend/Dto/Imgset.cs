using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("_imgset")]
[Index("ImgsetRefid", Name = "imgset_refid")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class Imgset
{
    [Key]
    [Column("imgset_id")]
    public ushort ImgsetId { get; set; }

    [Column("imgset_name")]
    [StringLength(50)]
    public string ImgsetName { get; set; } = null!;

    [Column("imgset_refid", TypeName = "mediumint unsigned")]
    public uint ImgsetRefid { get; set; }
}

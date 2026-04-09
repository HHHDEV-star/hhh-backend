using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Table("_imagecategory")]
[Index("ImgcatDisplay", Name = "imgcat_display")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class Imagecategory
{
    [Key]
    [Column("imgcat_id")]
    public ushort ImgcatId { get; set; }

    [Column("imgcat_name")]
    [StringLength(100)]
    public string ImgcatName { get; set; } = null!;

    [Column("imgcat_maxsize")]
    public uint ImgcatMaxsize { get; set; }

    [Column("imgcat_maxwidth")]
    public ushort ImgcatMaxwidth { get; set; }

    [Column("imgcat_maxheight")]
    public ushort ImgcatMaxheight { get; set; }

    [Column("imgcat_display")]
    public byte ImgcatDisplay { get; set; }

    [Column("imgcat_weight")]
    public ushort ImgcatWeight { get; set; }

    [Column("imgcat_type")]
    [StringLength(1)]
    public string ImgcatType { get; set; } = null!;

    [Column("imgcat_storetype")]
    [StringLength(5)]
    public string ImgcatStoretype { get; set; } = null!;
}

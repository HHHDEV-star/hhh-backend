using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Table("_imgsetimg")]
[Index("ImgsetimgImgset", Name = "imgsetimg_imgset")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class Imgsetimg
{
    [Key]
    [Column("imgsetimg_id", TypeName = "mediumint unsigned")]
    public uint ImgsetimgId { get; set; }

    [Column("imgsetimg_file")]
    [StringLength(50)]
    public string ImgsetimgFile { get; set; } = null!;

    [Column("imgsetimg_body", TypeName = "blob")]
    public byte[]? ImgsetimgBody { get; set; }

    [Column("imgsetimg_imgset")]
    public ushort ImgsetimgImgset { get; set; }
}

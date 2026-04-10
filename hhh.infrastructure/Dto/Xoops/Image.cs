using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("_image")]
[Index("ImageDisplay", Name = "image_display")]
[Index("ImgcatId", Name = "imgcat_id")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class Image
{
    [Key]
    [Column("image_id", TypeName = "mediumint unsigned")]
    public uint ImageId { get; set; }

    [Column("image_name")]
    [StringLength(30)]
    public string ImageName { get; set; } = null!;

    [Column("image_nicename")]
    [StringLength(255)]
    public string ImageNicename { get; set; } = null!;

    [Column("image_mimetype")]
    [StringLength(30)]
    public string ImageMimetype { get; set; } = null!;

    [Column("image_created")]
    public uint ImageCreated { get; set; }

    [Column("image_display")]
    public byte ImageDisplay { get; set; }

    [Column("image_weight")]
    public ushort ImageWeight { get; set; }

    [Column("imgcat_id")]
    public ushort ImgcatId { get; set; }
}

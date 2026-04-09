using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Keyless]
[Table("_imagebody")]
[Index("ImageId", Name = "image_id")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class Imagebody
{
    [Column("image_id", TypeName = "mediumint unsigned")]
    public uint ImageId { get; set; }

    [Column("image_body", TypeName = "mediumblob")]
    public byte[]? ImageBody1 { get; set; }
}

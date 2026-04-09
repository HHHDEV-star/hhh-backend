using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("_hcolumn_img")]
[Index("HcolumnId", Name = "hcolumn_id")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class HcolumnImg
{
    [Key]
    [Column("hcolumn_img_id")]
    public int HcolumnImgId { get; set; }

    [Column("hcolumn_id")]
    public int HcolumnId { get; set; }

    [Column("name", TypeName = "text")]
    public string Name { get; set; } = null!;

    [Column("create_time", TypeName = "datetime")]
    public DateTime CreateTime { get; set; }

    [Column("is_del")]
    public sbyte IsDel { get; set; }
}

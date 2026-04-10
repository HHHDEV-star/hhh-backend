using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Keyless]
[Table("_imgset_tplset_link")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class ImgsetTplsetLink
{
    [Column("imgset_id")]
    public ushort ImgsetId { get; set; }

    [Column("tplset_name")]
    [StringLength(50)]
    public string TplsetName { get; set; } = null!;
}

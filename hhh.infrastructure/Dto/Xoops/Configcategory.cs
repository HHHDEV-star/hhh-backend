using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("_configcategory")]
public partial class Configcategory
{
    [Key]
    [Column("confcat_id")]
    public ushort ConfcatId { get; set; }

    [Column("confcat_name")]
    [StringLength(255)]
    public string ConfcatName { get; set; } = null!;

    [Column("confcat_order")]
    public ushort ConfcatOrder { get; set; }
}

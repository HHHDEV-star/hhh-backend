using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Keyless]
[Table("designer_keyword_search")]
[Index("Name", Name = "name_UNIQUE", IsUnique = true)]
public partial class DesignerKeywordSearch
{
    /// <summary>
    /// 名稱
    /// </summary>
    [Column("name")]
    [StringLength(45)]
    public string? Name { get; set; }

    /// <summary>
    /// 次數
    /// </summary>
    [Column("count")]
    public int? Count { get; set; }
}

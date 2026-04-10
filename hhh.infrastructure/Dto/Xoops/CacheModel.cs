using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("_cache_model")]
[Index("CacheExpires", Name = "cache_expires")]
public partial class CacheModel
{
    [Key]
    [Column("cache_key")]
    [StringLength(64)]
    public string CacheKey { get; set; } = null!;

    [Column("cache_expires")]
    public uint CacheExpires { get; set; }

    [Column("cache_data", TypeName = "text")]
    public string? CacheData { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("_hmillion_vendor")]
[Index("Year", Name = "year")]
public partial class HmillionVendor
{
    [Key]
    [Column("vendor_id")]
    public uint VendorId { get; set; }

    [Column("year")]
    public uint Year { get; set; }

    [Column("vname")]
    [StringLength(128)]
    public string Vname { get; set; } = null!;

    [Column("vimg")]
    [StringLength(64)]
    public string Vimg { get; set; } = null!;

    [Column("vlink")]
    [StringLength(128)]
    public string Vlink { get; set; } = null!;
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("_hmyfav_mobile")]
[Index("Uid", Name = "uid")]
public partial class HmyfavMobile
{
    [Key]
    [Column("id")]
    public ulong Id { get; set; }

    [Column("uid")]
    public uint Uid { get; set; }

    [Column("page_title")]
    [StringLength(32)]
    public string PageTitle { get; set; } = null!;

    [Column("url")]
    [StringLength(256)]
    public string Url { get; set; } = null!;
}

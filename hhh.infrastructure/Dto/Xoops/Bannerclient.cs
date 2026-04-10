using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("_bannerclient")]
[Index("Login", Name = "login")]
public partial class Bannerclient
{
    [Key]
    [Column("cid")]
    public ushort Cid { get; set; }

    [Column("name")]
    [StringLength(60)]
    public string Name { get; set; } = null!;

    [Column("contact")]
    [StringLength(60)]
    public string Contact { get; set; } = null!;

    [Column("email")]
    [StringLength(60)]
    public string Email { get; set; } = null!;

    [Column("login")]
    [StringLength(10)]
    public string Login { get; set; } = null!;

    [Column("passwd")]
    [StringLength(10)]
    public string Passwd { get; set; } = null!;

    [Column("extrainfo", TypeName = "text")]
    public string? Extrainfo { get; set; }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("mobile_inner_setup")]
public partial class MobileInnerSetup
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("id")]
    public byte Id { get; set; }

    /// <summary>
    /// 內頁腰帶廣告圖(
    /// </summary>
    [Column("belt_img")]
    [StringLength(200)]
    public string? BeltImg { get; set; }

    /// <summary>
    /// 內頁腰帶廣告網址
    /// </summary>
    [Column("belt_url")]
    [StringLength(200)]
    public string? BeltUrl { get; set; }
}

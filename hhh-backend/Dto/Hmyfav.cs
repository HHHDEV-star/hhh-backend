using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("_hmyfav")]
[Index("Uid", Name = "uid")]
public partial class Hmyfav
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("id")]
    public ulong Id { get; set; }

    /// <summary>
    /// 使用者ID
    /// </summary>
    [Column("uid")]
    public uint Uid { get; set; }

    /// <summary>
    /// 頁面名稱
    /// </summary>
    [Column("page_title")]
    [StringLength(32)]
    public string PageTitle { get; set; } = null!;

    /// <summary>
    /// 描述
    /// </summary>
    [Column("desc")]
    [StringLength(256)]
    public string Desc { get; set; } = null!;

    /// <summary>
    /// 連結
    /// </summary>
    [Column("url")]
    [StringLength(256)]
    public string Url { get; set; } = null!;
}

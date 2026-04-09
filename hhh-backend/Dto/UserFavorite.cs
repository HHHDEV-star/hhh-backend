using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("user_favorite")]
[Index("UserId", "Type", Name = "index_index")]
[Index("UserId", "Type", "TableId", Name = "unique_index", IsUnique = true)]
public partial class UserFavorite
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
    [Column("user_id")]
    public uint UserId { get; set; }

    /// <summary>
    /// 我的最愛類型
    /// </summary>
    [Column("type")]
    [StringLength(10)]
    public string Type { get; set; } = null!;

    /// <summary>
    /// 我的最愛ID
    /// </summary>
    [Column("table_id")]
    public uint TableId { get; set; }

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("create_time", TypeName = "timestamp")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 收藏狀態(0:收藏1:移除收藏)
    /// </summary>
    [Column("status")]
    [StringLength(1)]
    public string Status { get; set; } = null!;
}

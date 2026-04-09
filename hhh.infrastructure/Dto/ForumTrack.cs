using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto;

[Table("forum_track")]
[Index("ArticleId", Name = "article_id")]
[Index("Uid", Name = "uid")]
[MySqlCharSet("utf8mb4")]
[MySqlCollation("utf8mb4_0900_ai_ci")]
public partial class ForumTrack
{
    /// <summary>
    /// PK
    /// </summary>
    [Key]
    [Column("id")]
    public uint Id { get; set; }

    /// <summary>
    /// FK:_users.uid
    /// </summary>
    [Column("uid")]
    public int Uid { get; set; }

    /// <summary>
    /// FK:forum_article.artcle_id
    /// </summary>
    [Column("article_id")]
    public int ArticleId { get; set; }

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("create_time", TypeName = "datetime")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 讀取檢驗時間
    /// </summary>
    [Column("check_datetime", TypeName = "datetime")]
    public DateTime CheckDatetime { get; set; }
}

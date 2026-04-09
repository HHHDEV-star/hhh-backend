using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

/// <summary>
/// 討論區文章
/// </summary>
[Table("forum_article")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class ForumArticle
{
    /// <summary>
    /// 文章編號
    /// </summary>
    [Key]
    [Column("article_id")]
    public int ArticleId { get; set; }

    /// <summary>
    /// GUID
    /// </summary>
    [Column("guid")]
    [StringLength(36)]
    public string Guid { get; set; } = null!;

    /// <summary>
    /// 發文使用者
    /// </summary>
    [Column("uid")]
    public int Uid { get; set; }

    /// <summary>
    /// 文章分類
    /// </summary>
    [Column("category")]
    public int Category { get; set; }

    /// <summary>
    /// 標題
    /// </summary>
    [Column("title")]
    [StringLength(100)]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string Title { get; set; } = null!;

    /// <summary>
    /// 簡介
    /// </summary>
    [Column("description")]
    [StringLength(250)]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string Description { get; set; } = null!;

    /// <summary>
    /// 內容
    /// </summary>
    [Column("content", TypeName = "mediumtext")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string Content { get; set; } = null!;

    /// <summary>
    /// 回覆數量
    /// </summary>
    [Column("reply_count")]
    public int ReplyCount { get; set; }

    /// <summary>
    /// 置頂
    /// </summary>
    [Column("is_top")]
    public sbyte IsTop { get; set; }

    /// <summary>
    /// 好的數量
    /// </summary>
    [Column("good_count")]
    public int GoodCount { get; set; }

    /// <summary>
    /// 不好的數量
    /// </summary>
    [Column("bad_count")]
    public int BadCount { get; set; }

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("date_added", TypeName = "datetime")]
    public DateTime DateAdded { get; set; }

    /// <summary>
    /// 修改時間
    /// </summary>
    [Column("date_modified", TypeName = "datetime")]
    public DateTime DateModified { get; set; }

    /// <summary>
    /// 刪除 0:否 1:是
    /// </summary>
    [Column("is_del")]
    public sbyte IsDel { get; set; }

    /// <summary>
    /// 讀取次數
    /// </summary>
    [Column("read_count")]
    public int ReadCount { get; set; }

    /// <summary>
    /// 是否自己隱藏(0:否 / 1:是
    /// </summary>
    [Column("is_hidden")]
    public sbyte IsHidden { get; set; }

    /// <summary>
    /// 0:未讀 1:已讀
    /// </summary>
    [Column("reply_read")]
    public sbyte ReplyRead { get; set; }

    [Column("seo_title")]
    [StringLength(128)]
    public string? SeoTitle { get; set; }

    [Column("seo_image")]
    [StringLength(128)]
    public string? SeoImage { get; set; }

    [Column("jsonld", TypeName = "mediumtext")]
    public string? Jsonld { get; set; }
}

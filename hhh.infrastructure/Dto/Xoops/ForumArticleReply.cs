using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("forum_article_reply")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class ForumArticleReply
{
    [Key]
    [Column("article_reply_id")]
    public int ArticleReplyId { get; set; }

    [Column("article_id")]
    public int ArticleId { get; set; }

    /// <summary>
    /// 回覆會員
    /// </summary>
    [Column("uid")]
    public int Uid { get; set; }

    /// <summary>
    /// 回覆內容
    /// </summary>
    [Column("reply_content")]
    [MySqlCharSet("utf8mb4")]
    [MySqlCollation("utf8mb4_0900_ai_ci")]
    public string ReplyContent { get; set; } = null!;

    /// <summary>
    /// 回覆好的數量
    /// </summary>
    [Column("reply_good_count")]
    public int ReplyGoodCount { get; set; }

    /// <summary>
    /// 回覆不好的數量
    /// </summary>
    [Column("reply_bad_count")]
    public int ReplyBadCount { get; set; }

    /// <summary>
    /// 是否刪除(0:否 / 1:是)
    /// </summary>
    [Column("is_del")]
    public sbyte IsDel { get; set; }

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
    /// 是否自己隱藏(0:否 / 1:是
    /// </summary>
    [Column("is_hidden")]
    public sbyte IsHidden { get; set; }
}

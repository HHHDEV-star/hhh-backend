using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

/// <summary>
/// 討論區文章評論
/// </summary>
[Table("forum_article_review")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class ForumArticleReview
{
    /// <summary>
    /// 評論ID
    /// </summary>
    [Key]
    [Column("review_id")]
    public uint ReviewId { get; set; }

    /// <summary>
    /// 評論文章類型 0:本文 1:回覆
    /// </summary>
    [Column("review_type")]
    public sbyte ReviewType { get; set; }

    /// <summary>
    /// 評論狀態 0:無評論 1:讚 2::不讚
    /// </summary>
    [Column("review_status")]
    public sbyte ReviewStatus { get; set; }

    /// <summary>
    /// 使用者編號
    /// </summary>
    [Column("uid")]
    public uint Uid { get; set; }

    /// <summary>
    /// 文章或回覆文章id
    /// </summary>
    [Column("data_id")]
    public uint DataId { get; set; }

    /// <summary>
    /// 修改時間
    /// </summary>
    [Column("date_modified", TypeName = "datetime")]
    public DateTime DateModified { get; set; }
}

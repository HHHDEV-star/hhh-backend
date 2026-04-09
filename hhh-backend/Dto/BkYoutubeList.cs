using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Keyless]
[Table("bk_youtube_list")]
public partial class BkYoutubeList
{
    /// <summary>
    /// pk
    /// </summary>
    [Column("yid")]
    public uint Yid { get; set; }

    /// <summary>
    /// 設計師pk
    /// </summary>
    [Column("hdesigner_id")]
    public uint HdesignerId { get; set; }

    /// <summary>
    /// 廠商id
    /// </summary>
    [Column("hbrand_id")]
    public uint HbrandId { get; set; }

    /// <summary>
    /// 建商ID
    /// </summary>
    [Column("builder_id")]
    public int BuilderId { get; set; }

    /// <summary>
    /// 頻道編號(Youtube
    /// </summary>
    [Column("channel_id")]
    [StringLength(50)]
    [MySqlCollation("utf8mb3_general_ci")]
    public string ChannelId { get; set; } = null!;

    /// <summary>
    /// 影片標題
    /// </summary>
    [Column("title")]
    [StringLength(60)]
    [MySqlCollation("utf8mb3_general_ci")]
    public string Title { get; set; } = null!;

    /// <summary>
    /// 影片敘述
    /// </summary>
    [Column("description", TypeName = "text")]
    [MySqlCollation("utf8mb3_general_ci")]
    public string Description { get; set; } = null!;

    /// <summary>
    /// 圖片位置
    /// </summary>
    [Column("youtube_img")]
    [StringLength(60)]
    [MySqlCollation("utf8mb3_general_ci")]
    public string YoutubeImg { get; set; } = null!;

    /// <summary>
    /// youtube影片id
    /// </summary>
    [Column("youtube_video_id")]
    [StringLength(11)]
    [MySqlCollation("utf8mb3_general_ci")]
    public string YoutubeVideoId { get; set; } = null!;

    /// <summary>
    /// 影片發布時間
    /// </summary>
    [Column("published_time", TypeName = "datetime")]
    public DateTime PublishedTime { get; set; }

    /// <summary>
    /// 換頁token
    /// </summary>
    [Column("page_token")]
    [StringLength(50)]
    [MySqlCollation("utf8mb3_general_ci")]
    public string PageToken { get; set; } = null!;

    /// <summary>
    /// 建立時間
    /// </summary>
    [Column("create_time", TypeName = "datetime")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新時間
    /// </summary>
    [Column("update_time", TypeName = "datetime")]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// 是否開啟(Y:開 / N:關
    /// </summary>
    [Column("onoff")]
    [StringLength(1)]
    [MySqlCollation("utf8mb3_general_ci")]
    public string Onoff { get; set; } = null!;

    /// <summary>
    /// 是否刪除(0 : 否 / 1 : 是)
    /// </summary>
    [Column("is_del")]
    public bool IsDel { get; set; }

    [Column("original_yt_id")]
    [StringLength(60)]
    [MySqlCollation("utf8mb3_general_ci")]
    public string? OriginalYtId { get; set; }
}

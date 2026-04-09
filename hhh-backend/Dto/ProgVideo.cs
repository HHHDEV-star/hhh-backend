using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("prog_video")]
[Index("ChanId", "DisplayDate", "Onoff", Name = "front_use")]
public partial class ProgVideo
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("prog_id")]
    public uint ProgId { get; set; }

    /// <summary>
    /// 頻道名稱
    /// </summary>
    [Column("chan_id")]
    [StringLength(20)]
    [MySqlCollation("utf8mb3_general_ci")]
    public string ChanId { get; set; } = null!;

    /// <summary>
    /// 頻道名稱
    /// </summary>
    [Column("chan_name")]
    [StringLength(20)]
    [MySqlCollation("utf8mb3_general_ci")]
    public string ChanName { get; set; } = null!;

    /// <summary>
    /// 播出時間
    /// </summary>
    [Column("display_date")]
    public DateOnly DisplayDate { get; set; }

    /// <summary>
    /// 播出時間(精確到小時)
    /// </summary>
    [Column("display_datetime", TypeName = "datetime")]
    public DateTime DisplayDatetime { get; set; }

    /// <summary>
    /// FK:youtube_group.gid
    /// </summary>
    [Column("gid")]
    public int Gid { get; set; }

    /// <summary>
    /// 群組名稱
    /// </summary>
    [Column("group_name")]
    [StringLength(50)]
    public string GroupName { get; set; } = null!;

    /// <summary>
    /// FK : youtube_list.yid
    /// </summary>
    [Column("yid")]
    public int Yid { get; set; }

    /// <summary>
    /// youtube影片id
    /// </summary>
    [Column("youtube_video_id")]
    [StringLength(11)]
    public string? YoutubeVideoId { get; set; }

    /// <summary>
    /// 是否露出(N:否Y:是) 
    /// </summary>
    [Column("onoff")]
    [StringLength(1)]
    [MySqlCollation("utf8mb3_general_ci")]
    public string Onoff { get; set; } = null!;

    /// <summary>
    /// 郵件內容
    /// </summary>
    [Column("mail_content", TypeName = "text")]
    [MySqlCollation("utf8mb3_general_ci")]
    public string MailContent { get; set; } = null!;

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
    /// 排序
    /// </summary>
    [Column("sort")]
    public ushort Sort { get; set; }

    /// <summary>
    /// 是否寄出(N:未寄出 / Y:已寄出)
    /// </summary>
    [Column("is_send")]
    [StringLength(1)]
    public string IsSend { get; set; } = null!;

    /// <summary>
    /// 寄出時間
    /// </summary>
    [Column("send_datetime", TypeName = "datetime")]
    public DateTime SendDatetime { get; set; }
}

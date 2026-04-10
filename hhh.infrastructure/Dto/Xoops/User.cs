using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("_users")]
[Index("Email", Name = "email", IsUnique = true)]
[Index("LastLogin", Name = "last_login")]
[Index("Level", Name = "level")]
[Index("Uid", "Uname", Name = "uiduname")]
[Index("Uname", Name = "uname", IsUnique = true)]
[Index("Uname", "Pass", Name = "unamepass")]
[Index("UserRegdate", Name = "user_regdate")]
public partial class User
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("uid")]
    public uint Uid { get; set; }

    /// <summary>
    /// 全球唯一碼
    /// </summary>
    [Column("guid")]
    public Guid Guid { get; set; }

    /// <summary>
    /// 是否啟用(0:未啟用 / 1:已啟用
    /// </summary>
    [Column("active")]
    [StringLength(1)]
    public string Active { get; set; } = null!;

    /// <summary>
    /// 性別(N:不限 / M:男 / F:女)
    /// </summary>
    [Column("sex")]
    [StringLength(1)]
    public string Sex { get; set; } = null!;

    /// <summary>
    /// 生日
    /// </summary>
    [Column("birthday")]
    public DateOnly Birthday { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    [Column("name")]
    [StringLength(60)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 帳號
    /// </summary>
    [Column("uname")]
    [StringLength(128)]
    public string Uname { get; set; } = null!;

    /// <summary>
    /// EMAIL
    /// </summary>
    [Column("email")]
    [StringLength(128)]
    public string Email { get; set; } = null!;

    [Column("url")]
    [StringLength(100)]
    public string Url { get; set; } = null!;

    /// <summary>
    /// 頭像
    /// </summary>
    [Column("user_avatar")]
    [StringLength(256)]
    [MySqlCollation("utf8mb3_general_ci")]
    public string UserAvatar { get; set; } = null!;

    /// <summary>
    /// 註冊時間
    /// </summary>
    [Column("user_regdate")]
    public uint UserRegdate { get; set; }

    /// <summary>
    /// 註冊時間(yyyy-mm-dd)
    /// </summary>
    [Column("user_regdate_datetime", TypeName = "datetime")]
    public DateTime UserRegdateDatetime { get; set; }

    [Column("user_icq")]
    [StringLength(15)]
    public string UserIcq { get; set; } = null!;

    /// <summary>
    /// 地址
    /// </summary>
    [Column("user_from")]
    [StringLength(100)]
    public string UserFrom { get; set; } = null!;

    [Column("user_sig", TypeName = "tinytext")]
    public string? UserSig { get; set; }

    [Column("user_viewemail")]
    public byte UserViewemail { get; set; }

    [Column("actkey")]
    [StringLength(8)]
    public string Actkey { get; set; } = null!;

    [Column("user_aim")]
    [StringLength(18)]
    public string UserAim { get; set; } = null!;

    [Column("user_yim")]
    [StringLength(25)]
    public string UserYim { get; set; } = null!;

    [Column("user_msnm")]
    [StringLength(100)]
    public string UserMsnm { get; set; } = null!;

    /// <summary>
    /// 密碼
    /// </summary>
    [Column("pass")]
    [StringLength(32)]
    public string Pass { get; set; } = null!;

    [Column("posts", TypeName = "mediumint unsigned")]
    public uint Posts { get; set; }

    [Column("attachsig")]
    public byte Attachsig { get; set; }

    [Column("rank")]
    public ushort Rank { get; set; }

    [Column("level")]
    public byte Level { get; set; }

    [Column("theme")]
    [StringLength(100)]
    public string Theme { get; set; } = null!;

    [Column("timezone_offset", TypeName = "float(3,1)")]
    public float TimezoneOffset { get; set; }

    /// <summary>
    /// 最後登入時間
    /// </summary>
    [Column("last_login")]
    public uint LastLogin { get; set; }

    /// <summary>
    /// 最後登入時間 (yyyy-mm-dd)
    /// </summary>
    [Column("last_login_datetime", TypeName = "datetime")]
    public DateTime LastLoginDatetime { get; set; }

    [Column("umode")]
    [StringLength(10)]
    public string Umode { get; set; } = null!;

    [Column("uorder")]
    public byte Uorder { get; set; }

    [Required]
    [Column("notify_method")]
    public bool? NotifyMethod { get; set; }

    [Column("notify_mode")]
    public bool NotifyMode { get; set; }

    [Column("user_occ")]
    [StringLength(100)]
    public string UserOcc { get; set; } = null!;

    [Column("bio", TypeName = "tinytext")]
    public string? Bio { get; set; }

    /// <summary>
    /// 聯絡電話
    /// </summary>
    [Column("user_intrest")]
    [StringLength(150)]
    public string UserIntrest { get; set; } = null!;

    [Column("user_mailok")]
    public byte UserMailok { get; set; }

    /// <summary>
    /// fb_token
    /// </summary>
    [Column("facebook_token")]
    [StringLength(128)]
    public string? FacebookToken { get; set; }

    [Column("fb_id")]
    [StringLength(32)]
    public string? FbId { get; set; }

    /// <summary>
    /// google_token
    /// </summary>
    [Column("google_token")]
    [StringLength(128)]
    public string? GoogleToken { get; set; }

    /// <summary>
    /// 更新時間
    /// </summary>
    [Column("update_time", TypeName = "timestamp")]
    public DateTime UpdateTime { get; set; }

    /// <summary>
    /// 會員信箱狀態(X:信箱無效 / B:不寄發)
    /// </summary>
    [Column("email_status")]
    [StringLength(1)]
    public string? EmailStatus { get; set; }

    /// <summary>
    /// 討論區黑名單(N: 否 / Y:是)
    /// </summary>
    [Column("forum_block")]
    [StringLength(1)]
    public string ForumBlock { get; set; } = null!;
}

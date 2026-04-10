using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

[Table("_hcase_img")]
[Index("HcaseId", Name = "hcase_id")]
[MySqlCollation("utf8mb3_general_ci")]
public partial class HcaseImg
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("hcase_img_id")]
    public uint HcaseImgId { get; set; }

    /// <summary>
    /// 個案ID
    /// </summary>
    [Column("hcase_id")]
    public uint HcaseId { get; set; }

    /// <summary>
    /// Tag
    /// </summary>
    [Column("tag1")]
    [StringLength(150)]
    public string? Tag1 { get; set; }

    /// <summary>
    /// Tag : 家具、燈飾、軟裝
    /// </summary>
    [Column("tag2")]
    [StringLength(150)]
    public string? Tag2 { get; set; }

    /// <summary>
    /// Tag : 顏色
    /// </summary>
    [Column("tag3")]
    [StringLength(150)]
    public string? Tag3 { get; set; }

    /// <summary>
    /// Tag : 建材、家電與設備 
    /// </summary>
    [Column("tag4")]
    [StringLength(150)]
    public string? Tag4 { get; set; }

    /// <summary>
    /// Tag : 其他主題
    /// </summary>
    [Column("tag5")]
    [StringLength(150)]
    public string? Tag5 { get; set; }

    /// <summary>
    /// Tag編寫者
    /// </summary>
    [Column("tag_man")]
    [StringLength(20)]
    public string? TagMan { get; set; }

    /// <summary>
    /// Tag更新時間
    /// </summary>
    [Column("tag_datetime", TypeName = "datetime")]
    public DateTime TagDatetime { get; set; }

    /// <summary>
    /// 標題
    /// </summary>
    [Column("title")]
    [StringLength(64)]
    [MySqlCollation("utf8mb3_unicode_ci")]
    public string? Title { get; set; }

    /// <summary>
    /// 排序號
    /// </summary>
    [Column("order")]
    public ushort Order { get; set; }

    /// <summary>
    /// 檔案名稱
    /// </summary>
    [Column("name")]
    [StringLength(128)]
    [MySqlCollation("utf8mb3_unicode_ci")]
    public string Name { get; set; } = null!;

    /// <summary>
    /// 描述
    /// </summary>
    [Column("desc", TypeName = "text")]
    [MySqlCollation("utf8mb3_unicode_ci")]
    public string? Desc { get; set; }

    /// <summary>
    /// 觀看次數
    /// </summary>
    [Column("viewed")]
    public uint Viewed { get; set; }

    /// <summary>
    /// 是否為平面圖
    /// </summary>
    [Column("is_flat")]
    public byte? IsFlat { get; set; }

    /// <summary>
    /// 是否為3D示意圖
    /// </summary>
    [Column("is_hint")]
    public byte? IsHint { get; set; }

    /// <summary>
    /// 檔案大小
    /// </summary>
    [Column("size_byte")]
    public uint? SizeByte { get; set; }

    /// <summary>
    /// 寬
    /// </summary>
    [Column("w")]
    public short? W { get; set; }

    /// <summary>
    /// 高
    /// </summary>
    [Column("h")]
    public short? H { get; set; }

    /// <summary>
    /// 設為封面(0:否 1:是)
    /// </summary>
    [Column("is_cover")]
    public bool IsCover { get; set; }

    [Column("is_update")]
    public sbyte IsUpdate { get; set; }

    [Column("new_name")]
    [StringLength(128)]
    public string? NewName { get; set; }

    /// <summary>
    /// 換圖處理
    /// </summary>
    [Column("change_name")]
    [StringLength(1)]
    public string ChangeName { get; set; } = null!;
}

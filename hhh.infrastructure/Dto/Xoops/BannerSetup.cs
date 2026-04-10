using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh.infrastructure.Dto.Xoops;

/// <summary>
/// 首頁為主
/// </summary>
[Table("banner_setup")]
public partial class BannerSetup
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("id")]
    public byte Id { get; set; }

    /// <summary>
    /// 推薦個案IDs
    /// </summary>
    [Column("strarr_hcase_id")]
    [StringLength(200)]
    public string? StrarrHcaseId { get; set; }

    /// <summary>
    /// 本週推薦設計師IDs
    /// </summary>
    [Column("strarr_hdesigner_id")]
    [StringLength(200)]
    public string? StrarrHdesignerId { get; set; }

    /// <summary>
    /// 本週推薦廠商IDs
    /// </summary>
    [Column("strarr_hbrand_id")]
    [StringLength(200)]
    public string? StrarrHbrandId { get; set; }

    /// <summary>
    /// 特別推薦設計師
    /// </summary>
    [Column("recommend_designer", TypeName = "text")]
    public string? RecommendDesigner { get; set; }

    /// <summary>
    /// 特別推薦廠商
    /// </summary>
    [Column("recommend_brand", TypeName = "text")]
    public string? RecommendBrand { get; set; }

    /// <summary>
    /// 編輯精選
    /// </summary>
    [Column("editors_picks", TypeName = "text")]
    public string? EditorsPicks { get; set; }
}

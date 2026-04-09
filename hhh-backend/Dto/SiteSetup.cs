using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace hhh_backend.Dto;

[Table("site_setup")]
public partial class SiteSetup
{
    /// <summary>
    /// pk
    /// </summary>
    [Key]
    [Column("id")]
    public byte Id { get; set; }

    /// <summary>
    /// 全站搜尋關鍵字
    /// </summary>
    [Column("all_search_tag")]
    [StringLength(200)]
    public string? AllSearchTag { get; set; }

    /// <summary>
    /// 影音搜尋關鍵字
    /// </summary>
    [Column("video_search_tag")]
    [StringLength(200)]
    public string? VideoSearchTag { get; set; }

    /// <summary>
    /// 設計師搜尋關鍵字
    /// </summary>
    [Column("designer_search_tag")]
    [StringLength(200)]
    public string? DesignerSearchTag { get; set; }

    /// <summary>
    /// 案例搜尋關鍵字
    /// </summary>
    [Column("case_search_tag")]
    [StringLength(200)]
    public string? CaseSearchTag { get; set; }

    /// <summary>
    /// 專欄搜尋關鍵字
    /// </summary>
    [Column("column_search_tag")]
    [StringLength(200)]
    public string? ColumnSearchTag { get; set; }

    /// <summary>
    /// 廠商搜尋關鍵字
    /// </summary>
    [Column("brand_search_tag")]
    [StringLength(200)]
    public string? BrandSearchTag { get; set; }

    /// <summary>
    /// vimeo或youtube網址
    /// </summary>
    [Column("vimeo")]
    [StringLength(255)]
    public string? Vimeo { get; set; }

    /// <summary>
    /// 蓋掉vimeo的縮圖
    /// </summary>
    [Column("vimeo_cover")]
    [StringLength(255)]
    public string? VimeoCover { get; set; }

    /// <summary>
    /// 預覽影音
    /// </summary>
    [Column("preview_video")]
    [StringLength(200)]
    public string? PreviewVideo { get; set; }

    /// <summary>
    /// TAB1名稱
    /// </summary>
    [Column("mobile_index_tab1")]
    [StringLength(20)]
    public string? MobileIndexTab1 { get; set; }

    /// <summary>
    /// TAB2名稱
    /// </summary>
    [Column("mobile_index_tab2")]
    [StringLength(20)]
    public string? MobileIndexTab2 { get; set; }

    /// <summary>
    /// TAB3名稱
    /// </summary>
    [Column("mobile_index_tab3")]
    [StringLength(20)]
    public string? MobileIndexTab3 { get; set; }

    /// <summary>
    /// TAB4名稱
    /// </summary>
    [Column("mobile_index_tab4")]
    [StringLength(20)]
    public string? MobileIndexTab4 { get; set; }

    /// <summary>
    /// TAB5名稱
    /// </summary>
    [Column("mobile_index_tab5")]
    [StringLength(20)]
    public string? MobileIndexTab5 { get; set; }

    /// <summary>
    /// TAB1推薦設計師IDs
    /// </summary>
    [Column("mobile_index_tab1_designer")]
    [StringLength(100)]
    public string? MobileIndexTab1Designer { get; set; }

    /// <summary>
    /// TAB2推薦設計師IDs
    /// </summary>
    [Column("mobile_index_tab2_designer")]
    [StringLength(100)]
    public string? MobileIndexTab2Designer { get; set; }

    /// <summary>
    /// TAB3推薦設計師IDs
    /// </summary>
    [Column("mobile_index_tab3_designer")]
    [StringLength(100)]
    public string? MobileIndexTab3Designer { get; set; }

    /// <summary>
    /// TAB4推薦設計師IDs
    /// </summary>
    [Column("mobile_index_tab4_designer")]
    [StringLength(100)]
    public string? MobileIndexTab4Designer { get; set; }

    /// <summary>
    /// TAB5推薦設計師IDs
    /// </summary>
    [Column("mobile_index_tab5_designer")]
    [StringLength(100)]
    public string? MobileIndexTab5Designer { get; set; }

    /// <summary>
    /// 前導影片上傳
    /// </summary>
    [Column("desktop_pre_video")]
    [StringLength(200)]
    public string? DesktopPreVideo { get; set; }

    /// <summary>
    /// 手機版人氣好宅IDs
    /// </summary>
    [Column("mobile_hot_case")]
    [StringLength(128)]
    public string? MobileHotCase { get; set; }

    /// <summary>
    /// 手機版夯影音IDs
    /// </summary>
    [Column("mobile_hot_video")]
    [StringLength(128)]
    public string? MobileHotVideo { get; set; }

    /// <summary>
    /// 首頁youtube影片ID
    /// </summary>
    [Column("youtube_id")]
    [StringLength(11)]
    public string? YoutubeId { get; set; }

    /// <summary>
    /// 首頁影片標題
    /// </summary>
    [Column("youtube_title")]
    [StringLength(50)]
    public string? YoutubeTitle { get; set; }

    /// <summary>
    /// 討論區過濾字
    /// </summary>
    [Column("forum_filter")]
    [StringLength(200)]
    public string ForumFilter { get; set; } = null!;

    /// <summary>
    /// 查證照(查詢總數)
    /// </summary>
    [Column("decoquery")]
    public uint Decoquery { get; set; }
}

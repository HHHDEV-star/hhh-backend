using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Editorial.Columns;

/// <summary>
/// 編輯部 - 新增專欄請求
/// (對應舊版 hhh-api/.../base/v1/Column.php → index_post → column_model::insert_column_data())
/// 舊版必填欄位:onoff, ctitle, ctype, cdesc, extend_str (見 PHP $chk_empty)
/// </summary>
public class CreateEditorialColumnRequest
{
    /// <summary>上線狀態</summary>
    [Required]
    public bool Onoff { get; set; }

    /// <summary>專欄名稱</summary>
    [Required]
    [StringLength(128)]
    public string Ctitle { get; set; } = string.Empty;

    /// <summary>專欄類別</summary>
    [Required]
    [StringLength(32)]
    public string Ctype { get; set; } = string.Empty;

    /// <summary>專欄敘述</summary>
    [Required]
    public string Cdesc { get; set; } = string.Empty;

    /// <summary>圖文提供</summary>
    [Required]
    public string ExtendStr { get; set; } = string.Empty;

    /// <summary>上架日期(空白時 service 預設為今天,對應舊 PHP 行為)</summary>
    public DateOnly? Sdate { get; set; }

    /// <summary>專欄類別子項</summary>
    [StringLength(32)]
    public string? CtypeSub { get; set; }

    /// <summary>標籤(自訂)</summary>
    [StringLength(64)]
    public string? Ctag { get; set; }

    /// <summary>短專欄名稱</summary>
    [StringLength(32)]
    public string? CshortTitle { get; set; }

    /// <summary>SEO 標題</summary>
    [StringLength(128)]
    public string? SeoTitle { get; set; }

    /// <summary>SEO 描述</summary>
    public string? SeoDescription { get; set; }

    /// <summary>廠商 ID</summary>
    public uint? Bid { get; set; }

    /// <summary>設計師 ID 清單(text 欄位,逗號或 JSON 字串)</summary>
    public string? HdesignerIds { get; set; }

    /// <summary>建案 ID</summary>
    public uint BuilderProductId { get; set; }

    /// <summary>富文本 page content</summary>
    public string? PageContent { get; set; }
}

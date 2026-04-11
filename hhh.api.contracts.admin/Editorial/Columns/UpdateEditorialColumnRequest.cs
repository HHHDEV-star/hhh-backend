using System.ComponentModel.DataAnnotations;

namespace hhh.api.contracts.admin.Editorial.Columns;

/// <summary>
/// 編輯部 - 更新專欄請求(hcolumn_id 走 URL,不放 body)
/// (對應舊版 hhh-api/.../base/v1/Column.php → index_put → column_model::update_column_data())
/// 舊版 PUT 必填欄位:onoff, hcolumn_id (本 API hcolumn_id 走 URL,所以 body 內僅 onoff 必填)
/// 注意:舊版 PUT 比 POST 寬鬆很多 — 大部分欄位都可以選填
/// </summary>
public class UpdateEditorialColumnRequest
{
    /// <summary>上線狀態</summary>
    [Required]
    public bool Onoff { get; set; }

    /// <summary>上架日期</summary>
    public DateOnly Sdate { get; set; }

    /// <summary>專欄名稱</summary>
    [StringLength(128)]
    public string? Ctitle { get; set; }

    /// <summary>專欄類別</summary>
    [StringLength(32)]
    public string? Ctype { get; set; }

    /// <summary>專欄類別子項</summary>
    [StringLength(32)]
    public string? CtypeSub { get; set; }

    /// <summary>標籤(自訂)</summary>
    [StringLength(64)]
    public string? Ctag { get; set; }

    /// <summary>短專欄名稱</summary>
    [StringLength(32)]
    public string? CshortTitle { get; set; }

    /// <summary>專欄敘述</summary>
    public string? Cdesc { get; set; }

    /// <summary>圖文提供</summary>
    public string? ExtendStr { get; set; }

    /// <summary>SEO 標題</summary>
    [StringLength(128)]
    public string? SeoTitle { get; set; }

    /// <summary>SEO 描述</summary>
    public string? SeoDescription { get; set; }

    /// <summary>廠商 ID</summary>
    public uint? Bid { get; set; }

    /// <summary>設計師 ID 清單</summary>
    public string? HdesignerIds { get; set; }

    /// <summary>建案 ID</summary>
    public uint BuilderProductId { get; set; }

    /// <summary>富文本 page content</summary>
    public string? PageContent { get; set; }
}

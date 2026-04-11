namespace hhh.api.contracts.admin.Editorial.Columns;

/// <summary>
/// 編輯部 - 專欄詳細資料(編輯 popup 用)
/// (對應舊版 hhh-api/.../base/v1/Column.php → index_get(with id) → column_model::get_column_page_content($id)
///  舊版只回 page_content 一個欄位,這裡為 REST 一致性回完整 column 資料)
/// </summary>
public class EditorialColumnDetailResponse
{
    // ---- 列表也有的欄位 ----
    /// <summary>專欄 ID</summary>
    public uint HcolumnId { get; set; }

    /// <summary>建案 ID</summary>
    public uint BuilderProductId { get; set; }

    /// <summary>標籤(自訂)</summary>
    public string Ctag { get; set; } = string.Empty;

    /// <summary>專欄類別</summary>
    public string Ctype { get; set; } = string.Empty;

    /// <summary>專欄類別子項</summary>
    public string? CtypeSub { get; set; }

    /// <summary>專欄名稱</summary>
    public string Ctitle { get; set; } = string.Empty;

    /// <summary>短專欄名稱</summary>
    public string CshortTitle { get; set; } = string.Empty;

    /// <summary>專欄敘述</summary>
    public string Cdesc { get; set; } = string.Empty;

    /// <summary>專欄 logo 路徑</summary>
    public string Clogo { get; set; } = string.Empty;

    /// <summary>圖文提供</summary>
    public string ExtendStr { get; set; } = string.Empty;

    /// <summary>觀看數</summary>
    public uint Viewed { get; set; }

    /// <summary>推薦</summary>
    public uint Recommend { get; set; }

    /// <summary>上線狀態</summary>
    public bool Onoff { get; set; }

    /// <summary>建立時間</summary>
    public DateTime CreatTime { get; set; }

    /// <summary>廠商 ID</summary>
    public uint? Bid { get; set; }

    /// <summary>設計師 ID 清單(text 欄位)</summary>
    public string? HdesignerIds { get; set; }

    /// <summary>上架日期</summary>
    public DateOnly Sdate { get; set; }

    /// <summary>SEO 標題</summary>
    public string? SeoTitle { get; set; }

    /// <summary>SEO 圖片</summary>
    public string? SeoImage { get; set; }

    /// <summary>SEO 描述</summary>
    public string? SeoDescription { get; set; }

    /// <summary>更新時間</summary>
    public DateTime UpdateTime { get; set; }

    // ---- 編輯 popup 額外用的欄位 ----
    /// <summary>富文本 page content(對應舊版 get_column_page_content 唯一回傳的欄位)</summary>
    public string? PageContent { get; set; }

    /// <summary>系統 Tag</summary>
    public string? Tag { get; set; }
}

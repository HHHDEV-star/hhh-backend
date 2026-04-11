namespace hhh.api.contracts.admin.Editorial.Columns;

/// <summary>
/// 編輯部 - 專欄列表項目
/// (對應舊版 hhh-api/.../base/v1/Column.php → index_get(no id) → column_model::get_column_lists()
///  欄位順序對齊 hhh-backstage 的 column_lists 後台 view)
/// </summary>
public class EditorialColumnListItem
{
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

    /// <summary>上線狀態(true=開,false=關)</summary>
    public bool Onoff { get; set; }

    /// <summary>建立時間</summary>
    public DateTime CreatTime { get; set; }

    /// <summary>廠商 ID</summary>
    public uint? Bid { get; set; }

    /// <summary>設計師 ID 清單(text 欄位,逗號或 JSON 字串)</summary>
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
}

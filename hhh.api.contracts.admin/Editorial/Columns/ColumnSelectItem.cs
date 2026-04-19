namespace hhh.api.contracts.admin.Editorial.Columns;

/// <summary>
/// 專欄下拉選單項目（精簡欄位，供關聯選擇 / 下拉選單使用）
/// </summary>
public class ColumnSelectItem
{
    /// <summary>專欄 ID</summary>
    public uint HcolumnId { get; set; }

    /// <summary>專欄標題</summary>
    public string Ctitle { get; set; } = string.Empty;

    /// <summary>專欄類別</summary>
    public string Ctype { get; set; } = string.Empty;

    /// <summary>專欄 Logo</summary>
    public string Clogo { get; set; } = string.Empty;

    /// <summary>上線狀態</summary>
    public bool Onoff { get; set; }

    /// <summary>上架日期</summary>
    public DateOnly Sdate { get; set; }
}

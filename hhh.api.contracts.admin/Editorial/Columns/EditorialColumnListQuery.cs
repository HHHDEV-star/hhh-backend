using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Editorial.Columns;

/// <summary>
/// 編輯部專欄列表查詢參數
/// （對應舊版 PHP: Column.php → column_model::get_column_lists()）
/// </summary>
public class EditorialColumnListQuery : PagedRequest
{
    /// <summary>關鍵字搜尋（Ctitle / CshortTitle）</summary>
    public string? Keyword { get; set; }

    /// <summary>專欄類別</summary>
    public string? Ctype { get; set; }

    /// <summary>上下架狀態（0=下架, 1=上架）</summary>
    public byte? Onoff { get; set; }

    /// <summary>日期範圍起始</summary>
    public DateOnly? DateFrom { get; set; }

    /// <summary>日期範圍結束</summary>
    public DateOnly? DateTo { get; set; }
}

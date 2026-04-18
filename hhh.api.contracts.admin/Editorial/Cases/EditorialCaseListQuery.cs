using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Editorial.Cases;

/// <summary>
/// 編輯部個案列表查詢參數
/// （對應舊版 PHP: Cases.php → case_model::get_case_lists()）
/// </summary>
public class EditorialCaseListQuery : PagedRequest
{
    /// <summary>關鍵字搜尋（Caption / 設計師 Title）</summary>
    public string? Keyword { get; set; }

    /// <summary>設計師 ID</summary>
    public uint? HdesignerId { get; set; }

    /// <summary>上下架狀態（0=下架, 1=上架）</summary>
    public byte? Onoff { get; set; }

    /// <summary>風格</summary>
    public string? Style { get; set; }

    /// <summary>類型</summary>
    public string? Type { get; set; }

    /// <summary>日期範圍起始</summary>
    public DateOnly? DateFrom { get; set; }

    /// <summary>日期範圍結束</summary>
    public DateOnly? DateTo { get; set; }
}

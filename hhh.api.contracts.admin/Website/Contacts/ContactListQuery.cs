using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Website.Contacts;

/// <summary>
/// 聯絡我們列表查詢參數
/// </summary>
public class ContactListQuery : PagedRequest
{
    /// <summary>
    /// 關鍵字搜尋：模糊比對姓名 / 公司 / 電話 / Email / 主旨 / 內容
    /// </summary>
    public string? Keyword { get; set; }

    /// <summary>
    /// 發送狀態篩選（Y=已寄出 / N=未寄出，不帶=全部）
    /// </summary>
    public string? Send { get; set; }

    /// <summary>
    /// 建立時間起始
    /// </summary>
    public DateOnly? DateFrom { get; set; }

    /// <summary>
    /// 建立時間結束
    /// </summary>
    public DateOnly? DateTo { get; set; }
}

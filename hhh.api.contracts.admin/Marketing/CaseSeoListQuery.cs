using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Marketing;

/// <summary>
/// 個案 SEO 列表查詢參數
/// </summary>
public class CaseSeoListQuery : PagedRequest
{
    /// <summary>
    /// 關鍵字搜尋：模糊比對個案名稱 / SEO 標題
    /// </summary>
    public string? Keyword { get; set; }

    /// <summary>
    /// 設計師 ID 篩選。不帶則不過濾。
    /// </summary>
    public uint? HdesignerId { get; set; }

    /// <summary>
    /// 設計風格篩選（比對 style CSV 欄位是否包含此值）。不帶則不過濾。
    /// </summary>
    public string? Style { get; set; }

    /// <summary>
    /// 上線狀態篩選（0=關閉, 1=開啟）。不帶則不過濾（全部）。
    /// </summary>
    public byte? Onoff { get; set; }

    /// <summary>
    /// SEO 完成度篩選：
    /// complete = SEO 三欄位皆有值、
    /// incomplete = 至少一欄為空、
    /// 不帶則不過濾。
    /// </summary>
    public string? SeoStatus { get; set; }

    /// <summary>
    /// 上架日期起始（含），格式 yyyy-MM-dd
    /// </summary>
    public DateOnly? DateFrom { get; set; }

    /// <summary>
    /// 上架日期結束（含），格式 yyyy-MM-dd
    /// </summary>
    public DateOnly? DateTo { get; set; }
}

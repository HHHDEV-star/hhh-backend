using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Marketing;

/// <summary>
/// 產品 SEO 列表查詢參數
/// </summary>
public class ProductSeoListQuery : PagedRequest
{
    /// <summary>關鍵字搜尋：模糊比對產品名稱 / SEO 標題</summary>
    public string? Keyword { get; set; }

    /// <summary>上線狀態篩選（0=關閉, 1=開啟）。不帶則不過濾。</summary>
    public byte? Onoff { get; set; }

    /// <summary>
    /// SEO 完成度篩選：
    /// complete = SeoTitle + SeoImage 皆有值、
    /// incomplete = 至少一欄為空、
    /// 不帶則不過濾。
    /// </summary>
    public string? SeoStatus { get; set; }
}

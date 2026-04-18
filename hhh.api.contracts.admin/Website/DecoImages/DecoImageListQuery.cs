using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.WebSite.DecoImages;

/// <summary>
/// 查證照圖片審核列表查詢參數
/// </summary>
public class DecoImageListQuery : PagedRequest
{
    /// <summary>
    /// 關鍵字搜尋：模糊比對公司名稱 / 登記證號 / 負責人
    /// </summary>
    public string? Keyword { get; set; }

    /// <summary>
    /// 審核狀態篩選（Y=通過 / N=未通過，不帶=全部）
    /// </summary>
    public string? Onoff { get; set; }
}

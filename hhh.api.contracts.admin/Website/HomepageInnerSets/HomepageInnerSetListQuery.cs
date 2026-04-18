using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Website.HomepageInnerSets;

/// <summary>
/// 首頁區塊元素列表查詢參數
/// </summary>
public class HomepageInnerSetListQuery : PagedRequest
{
    /// <summary>
    /// 主題類型篩選（case/video/column/product/ad/designer/brand/fans/week）
    /// </summary>
    public string? ThemeType { get; set; }

    /// <summary>
    /// 上線狀態篩選（Y/N，不帶=全部）
    /// </summary>
    public string? Onoff { get; set; }

    /// <summary>
    /// 區塊篩選（outer_site_set.oss_id）
    /// </summary>
    public uint? OuterSet { get; set; }
}

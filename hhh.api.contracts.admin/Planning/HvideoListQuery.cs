using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Planning;

/// <summary>
/// 影音列表查詢參數（對應舊版 PHP: _hvideo.php）
/// </summary>
public class HvideoListQuery : PagedRequest
{
    /// <summary>
    /// 關鍵字搜尋：ID / 影音類型 / 標題 / 影音說明 / 標籤-單元類型 / 空間格局 /
    /// 關聯設計師（公司名稱、姓名）/ 關聯廠商（標題）/ 關聯專欄（標題）
    /// </summary>
    public string? Keyword { get; set; }
}

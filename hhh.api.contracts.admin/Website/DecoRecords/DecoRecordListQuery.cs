using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.WebSite.DecoRecords;

/// <summary>
/// 查證照列表查詢參數
/// </summary>
public class DecoRecordListQuery : PagedRequest
{
    /// <summary>
    /// 上線狀態篩選（0=關, 1=開）。不帶則不過濾（全部）。
    /// </summary>
    public byte? Onoff { get; set; }

    /// <summary>
    /// 關鍵字搜尋：跨欄位 LIKE（登記證號 / 公司名稱 / 地址 / 區域 / 路段 / 電話 / 手機 / 客服電話 / Email / Line ID / 網站）
    /// </summary>
    public string? Keyword { get; set; }
}

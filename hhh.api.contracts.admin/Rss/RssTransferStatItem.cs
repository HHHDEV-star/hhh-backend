namespace hhh.api.contracts.admin.Rss;

/// <summary>
/// RSS 轉接統計項目
/// (對應舊版 Transfer/statistics_get → transfer_model::statistics()
///  GROUP BY type, num + COUNT(*) as total,type 轉中文,url 計算)
/// </summary>
public class RssTransferStatItem
{
    /// <summary>類型(已轉中文:廠商/個案/專欄/設計師)</summary>
    public string Type { get; set; } = string.Empty;
    /// <summary>編號</summary>
    public ushort Num { get; set; }
    /// <summary>點擊總次數</summary>
    public int Total { get; set; }
    /// <summary>計算產生的前台 URL</summary>
    public string Url { get; set; } = string.Empty;
}

namespace hhh.api.contracts.admin.Rss;

/// <summary>
/// RSS 轉接紀錄項目
/// (對應舊版 Transfer/logs_get → transfer_model::read()
///  type 會從英文轉成中文,url 由 type + num 計算)
/// </summary>
public class RssTransferLogItem
{
    public uint Id { get; set; }
    /// <summary>來自</summary>
    public string Source { get; set; } = string.Empty;
    /// <summary>類型(已轉中文:廠商/個案/專欄/設計師)</summary>
    public string Type { get; set; } = string.Empty;
    /// <summary>編號</summary>
    public ushort Num { get; set; }
    /// <summary>計算產生的 URL(對應前台頁面)</summary>
    public string Url { get; set; } = string.Empty;
    /// <summary>IP</summary>
    public string Ip { get; set; } = string.Empty;
    /// <summary>時間</summary>
    public DateTime Datetime { get; set; }
}

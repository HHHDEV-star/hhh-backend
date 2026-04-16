namespace hhh.api.contracts.admin.Brokers.Decos;

/// <summary>
/// 軟裝需求單附件
/// （對應舊版 deco_request_files 表）
/// </summary>
public class DecoRequestFileItem
{
    /// <summary>附件 ID</summary>
    public uint DecoFileId { get; set; }

    /// <summary>所屬軟裝需求單 seq</summary>
    public int Seq { get; set; }

    /// <summary>原始檔名</summary>
    public string OrigName { get; set; } = string.Empty;

    /// <summary>儲存檔名(相對路徑或雲端 key)</summary>
    public string FileName { get; set; } = string.Empty;

    /// <summary>建立時間</summary>
    public DateTime CreateTime { get; set; }
}

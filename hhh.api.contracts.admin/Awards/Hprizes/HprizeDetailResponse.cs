namespace hhh.api.contracts.admin.Awards.Hprizes;

/// <summary>
/// 獎品完整詳細（對應舊版 _hprize_edit.php GET 模式）
/// </summary>
public class HprizeDetailResponse
{
    /// <summary>獎品 ID（hprize_id）</summary>
    public uint Id { get; set; }

    /// <summary>獎品名稱</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>獎品 LOGO URL（同列表 item 的 Logo 欄位邏輯）</summary>
    public string Logo { get; set; } = string.Empty;

    /// <summary>獎品說明</summary>
    public string Desc { get; set; } = string.Empty;

    /// <summary>建立時間</summary>
    public DateTime CreatTime { get; set; }
}

namespace hhh.api.contracts.admin.Hprizes;

/// <summary>
/// 獎品列表單筆項目（對應舊版 _hprize.php 表格欄位）
/// </summary>
public class HprizeListItem
{
    /// <summary>獎品 ID（hprize_id）</summary>
    public uint Id { get; set; }

    /// <summary>獎品名稱</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 獎品 LOGO URL。
    /// 1. 新上傳：由 IImageUploadService 產生的 /uploads/... 相對 URL。
    /// 2. 舊資料：可能存的是 https://m.hhh.com.tw/upload/... 的完整 URL，直接原樣回傳。
    /// </summary>
    public string Logo { get; set; } = string.Empty;

    /// <summary>獎品說明</summary>
    public string Desc { get; set; } = string.Empty;

    /// <summary>建立時間</summary>
    public DateTime CreatTime { get; set; }
}

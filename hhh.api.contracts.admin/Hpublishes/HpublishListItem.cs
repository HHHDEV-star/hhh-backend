namespace hhh.api.contracts.admin.Hpublishes;

/// <summary>
/// 出版列表單筆項目（對應舊版 _hpublish.php 表格欄位）。
/// </summary>
public class HpublishListItem
{
    /// <summary>出版 ID（hpublish_id）</summary>
    public uint Id { get; set; }

    /// <summary>書籍類別</summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// logo 檔名（存於舊 PHP 的 gs/publish/ 目錄，本 API 只保存字串，不做檔案管理）
    /// </summary>
    public string Logo { get; set; } = string.Empty;

    /// <summary>名稱（書名）</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>作者</summary>
    public string Author { get; set; } = string.Empty;

    /// <summary>出版日期</summary>
    public DateOnly Pdate { get; set; }

    /// <summary>觀看數（系統自動累計，不開放 API 寫入）</summary>
    public uint Viewed { get; set; }

    /// <summary>推薦數</summary>
    public uint Recommend { get; set; }
}

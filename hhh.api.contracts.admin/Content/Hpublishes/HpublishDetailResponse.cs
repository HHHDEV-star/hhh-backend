namespace hhh.api.contracts.admin.Content.Hpublishes;

/// <summary>
/// 出版單筆完整資料（對應舊版 _hpublish_edit.php GET 模式）。
/// </summary>
public class HpublishDetailResponse
{
    /// <summary>出版 ID（hpublish_id）</summary>
    public uint Id { get; set; }

    /// <summary>書籍類別</summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>logo 檔名</summary>
    public string Logo { get; set; } = string.Empty;

    /// <summary>名稱（書名）</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>作者</summary>
    public string Author { get; set; } = string.Empty;

    /// <summary>出版日期</summary>
    public DateOnly Pdate { get; set; }

    /// <summary>描述</summary>
    public string Desc { get; set; } = string.Empty;

    /// <summary>編輯者備註 / HTML</summary>
    public string Editor { get; set; } = string.Empty;

    /// <summary>觀看數（系統自動累計，唯讀）</summary>
    public uint Viewed { get; set; }

    /// <summary>推薦數</summary>
    public uint Recommend { get; set; }
}

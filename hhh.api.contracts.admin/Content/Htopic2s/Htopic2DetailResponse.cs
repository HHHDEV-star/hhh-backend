namespace hhh.api.contracts.admin.Content.Htopic2s;

/// <summary>
/// 議題 2 單筆完整資料(對應舊版 _htopic2_edit.php GET 模式)。
/// </summary>
public class Htopic2DetailResponse
{
    /// <summary>議題 2 ID</summary>
    public uint Id { get; set; }

    /// <summary>名稱</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>主題敘述</summary>
    public string Desc { get; set; } = string.Empty;

    /// <summary>logo 檔名</summary>
    public string Logo { get; set; } = string.Empty;

    /// <summary>設計師 ID CSV</summary>
    public string StrarrHdesignerId { get; set; } = string.Empty;

    /// <summary>個案 ID CSV</summary>
    public string StrarrHcaseId { get; set; } = string.Empty;

    /// <summary>影音 ID CSV</summary>
    public string StrarrHvideoId { get; set; } = string.Empty;

    /// <summary>專欄 ID CSV</summary>
    public string StrarrHcolumnId { get; set; } = string.Empty;

    /// <summary>上線狀態</summary>
    public bool Onoff { get; set; }
}

namespace hhh.api.contracts.admin.Htopic2s;

/// <summary>
/// 議題 2 列表單筆項目(對應舊版 _htopic2.php 表格欄位)。
/// </summary>
public class Htopic2ListItem
{
    /// <summary>議題 2 ID</summary>
    public uint Id { get; set; }

    /// <summary>名稱</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>主題敘述</summary>
    public string Desc { get; set; } = string.Empty;

    /// <summary>
    /// logo 檔名(存於舊站 gs/topic2/ 目錄,本 API 只保存字串,不做檔案管理)
    /// </summary>
    public string Logo { get; set; } = string.Empty;

    /// <summary>設計師 ID CSV(例:"12,34,56")</summary>
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

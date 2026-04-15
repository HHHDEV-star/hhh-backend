namespace hhh.api.contracts.admin.Editorial.Topics;

/// <summary>
/// 主題列表項目
/// （對應舊版 PHP:_htopic.php 列表顯示欄位）
/// </summary>
public class HtopicListItem
{
    /// <summary>主題 ID</summary>
    public uint HtopicId { get; set; }

    /// <summary>主題名稱</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>主題敘述</summary>
    public string Desc { get; set; } = string.Empty;

    /// <summary>上線狀態</summary>
    public bool Onoff { get; set; }

    /// <summary>logo</summary>
    public string Logo { get; set; } = string.Empty;

    /// <summary>個案 IDs（逗號分隔）</summary>
    public string StrarrHcaseId { get; set; } = string.Empty;

    /// <summary>專欄 IDs（逗號分隔）</summary>
    public string StrarrHcolumnId { get; set; } = string.Empty;

    /// <summary>建立時間</summary>
    public DateTime CreateTime { get; set; }

    /// <summary>觀看數</summary>
    public uint Viewed { get; set; }
}

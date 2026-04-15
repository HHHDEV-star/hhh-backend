namespace hhh.api.contracts.admin.Editorial.Topics;

/// <summary>
/// 主題完整資料
/// （對應舊版 PHP:_htopic_edit.php 編輯頁 SELECT *）
/// </summary>
public class HtopicDetailResponse
{
    /// <summary>主題 ID</summary>
    public uint HtopicId { get; set; }

    /// <summary>主題名稱</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>主題敘述</summary>
    public string Desc { get; set; } = string.Empty;

    /// <summary>logo</summary>
    public string Logo { get; set; } = string.Empty;

    /// <summary>個案 IDs（逗號分隔）</summary>
    public string StrarrHcaseId { get; set; } = string.Empty;

    /// <summary>專欄 IDs（逗號分隔）</summary>
    public string StrarrHcolumnId { get; set; } = string.Empty;

    /// <summary>觀看數</summary>
    public uint Viewed { get; set; }

    /// <summary>建立時間</summary>
    public DateTime CreateTime { get; set; }

    /// <summary>更新時間</summary>
    public DateTime UpdateTime { get; set; }

    /// <summary>上線狀態</summary>
    public bool Onoff { get; set; }

    /// <summary>寄送次數</summary>
    public bool IsSend { get; set; }

    /// <summary>FB 分享圖片</summary>
    public string? SeoImage { get; set; }

    /// <summary>FB 分享標題</summary>
    public string? SeoTitle { get; set; }

    /// <summary>FB 分享敘述</summary>
    public string? SeoDescription { get; set; }
}

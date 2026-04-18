using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Social.Forums;

/// <summary>
/// 討論區文章列表查詢參數
/// </summary>
public class ForumArticleListQuery : PagedRequest
{
    /// <summary>
    /// 關鍵字搜尋：模糊比對標題 / 發文者帳號 / 發文者 Email
    /// </summary>
    public string? Keyword { get; set; }

    /// <summary>
    /// 文章分類篩選。不帶則不過濾（全部分類）。
    /// </summary>
    public int? Category { get; set; }

    /// <summary>
    /// 置頂狀態篩選（true=置頂 / false=未置頂）。不帶則不過濾。
    /// </summary>
    public bool? IsTop { get; set; }

    /// <summary>
    /// 刪除狀態篩選（true=已刪除 / false=正常）。不帶則不過濾（全部）。
    /// </summary>
    public bool? IsDel { get; set; }

    /// <summary>
    /// 建立時間起始（含），格式 yyyy-MM-dd
    /// </summary>
    public DateOnly? DateFrom { get; set; }

    /// <summary>
    /// 建立時間結束（含），格式 yyyy-MM-dd
    /// </summary>
    public DateOnly? DateTo { get; set; }
}

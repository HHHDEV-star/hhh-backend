using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Social.Forums;

/// <summary>討論區回覆列表查詢條件</summary>
public class ForumReplyListQuery : PagedRequest
{
    /// <summary>模糊比對帳號 / 回覆內容</summary>
    public string? Keyword { get; set; }

    /// <summary>刪除狀態篩選</summary>
    public bool? IsDel { get; set; }
}

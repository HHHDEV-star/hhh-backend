using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Social.Forums;

/// <summary>
/// 討論區黑名單列表查詢參數
/// </summary>
public class ForumBlockListQuery : PagedRequest
{
    /// <summary>
    /// 帳號模糊搜尋
    /// </summary>
    public string? Uname { get; set; }

    /// <summary>
    /// Email 模糊搜尋
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// 黑名單狀態篩選：all=全部會員, Y=黑名單, N=非黑名單。不帶則預設只撈黑名單(Y)。
    /// </summary>
    public string? Status { get; set; }
}

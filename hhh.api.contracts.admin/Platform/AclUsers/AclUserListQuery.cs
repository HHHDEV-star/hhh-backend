using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Platform.AclUsers;

/// <summary>
/// ACL 帳號列表查詢參數
/// </summary>
public class AclUserListQuery : PagedRequest
{
    /// <summary>關鍵字搜尋：模糊比對 Name / Account / Email / Position</summary>
    public string? Keyword { get; set; }

    /// <summary>是否刪除篩選（"0"=否, "1"=是）。不帶則不過濾。</summary>
    public string? IsDel { get; set; }
}

using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Platform.AclMenuGroups;

/// <summary>
/// ACL 目錄群組列表查詢參數
/// </summary>
public class AclMenuGroupListQuery : PagedRequest
{
    /// <summary>關鍵字搜尋：模糊比對群組名稱</summary>
    public string? Keyword { get; set; }

    /// <summary>是否顯示篩選（"0"=否, "1"=是）。不帶則不過濾。</summary>
    public string? IsShow { get; set; }
}

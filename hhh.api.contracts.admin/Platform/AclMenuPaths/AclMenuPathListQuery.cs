using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Platform.AclMenuPaths;

/// <summary>
/// ACL 目錄功能列表查詢參數
/// </summary>
public class AclMenuPathListQuery : PagedRequest
{
    /// <summary>關鍵字搜尋：模糊比對功能名稱 / 路徑</summary>
    public string? Keyword { get; set; }

    /// <summary>所屬群組編號篩選。不帶則不過濾。</summary>
    public int? MenuGroupId { get; set; }

    /// <summary>是否顯示篩選（"0"=否, "1"=是）。不帶則不過濾。</summary>
    public string? IsShow { get; set; }
}

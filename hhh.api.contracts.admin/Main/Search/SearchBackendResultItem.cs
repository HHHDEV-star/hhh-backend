namespace hhh.api.contracts.admin.Main.Search;

/// <summary>
/// 後台搜尋結果項目(通用格式,6 種 type 共用)
/// (對應舊版 searchall_model::get_xxx_lists)
/// </summary>
public class SearchBackendResultItem
{
    /// <summary>資料 ID(hcase_id / hcolumn_id / hdesigner_id / hvideo_id / hbrand_id / product id)</summary>
    public uint Id { get; set; }

    /// <summary>標題/名稱</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>副標題(設計師有 name,其他可能為空)</summary>
    public string? SubTitle { get; set; }
}

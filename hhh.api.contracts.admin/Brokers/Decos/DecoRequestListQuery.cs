using hhh.api.contracts.Common;

namespace hhh.api.contracts.admin.Brokers.Decos;

/// <summary>
/// 軟裝需求單列表查詢參數（對應舊版 PHP:Renovation.php → deco_get）
/// </summary>
/// <remarks>
/// 業務規則:
/// <list type="bullet">
///   <item>type 為空或 "全部" 時,不過濾分類欄位;否則精確比對 deco_request.type。</item>
///   <item>永遠只回傳 payment_status='N' AND is_delete='N' 的未結案 / 未刪除資料。</item>
///   <item>排序固定 seq DESC(舊版前端 Kendo Grid 本身不帶排序參數)。</item>
/// </list>
/// </remarks>
public class DecoRequestListQuery : PagedRequest
{
    /// <summary>分類(待辦 / 進行中 / 結案);空值或 "全部" 表示不過濾</summary>
    public string? Type { get; set; }
}

using hhh.api.contracts.admin.Brokers.Renovations;
using hhh.api.contracts.Common;

namespace hhh.application.admin.Brokers.Renovations;

/// <summary>
/// 經紀人 - 裝修需求單 服務
/// </summary>
/// <remarks>
/// 對應舊版 PHP:hhh-api/application/controllers/third/v1/Renovation.php
/// 後台 view:hhh-backstage/backend/views/backend/event/renovation.php
/// </remarks>
public interface IRenovationService
{
    /// <summary>
    /// 取得裝修需求單列表
    /// (對應舊版 Renovation/index_get → Renovation_model::get())
    /// 業務規則:
    ///  - 從 renovation_reuqest 全量讀取,依 id DESC 排序
    ///  - 子查詢帶出 deco_record.company_name (僅當 bldsno != 0 才有值)
    ///  - site_lists JSON 字串會解析後回傳結構化內容
    ///  - 沒有任何查詢條件、沒有 server-side paging
    /// </summary>
    Task<PagedResponse<RenovationListItem>> GetListAsync(ListQuery query, CancellationToken cancellationToken = default);
}

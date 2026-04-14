using hhh.api.contracts.admin.Website.HomepageInnerSets;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;

namespace hhh.application.admin.Website.HomepageInnerSets;

public interface IHomepageInnerSetService
{
    /// <summary>取得首頁區塊元素列表(含 JOIN outer_site_set + enrichment)</summary>
    Task<PagedResponse<HomepageInnerSetListItem>> GetListAsync(ListQuery query, CancellationToken cancellationToken = default);

    /// <summary>新增單筆區塊元素</summary>
    Task<OperationResult<uint>> CreateAsync(CreateHomepageInnerSetRequest request, CancellationToken cancellationToken = default);

    /// <summary>修改單筆區塊元素</summary>
    Task<OperationResult<uint>> UpdateAsync(uint psId, UpdateHomepageInnerSetRequest request, CancellationToken cancellationToken = default);

    /// <summary>刪除單筆區塊元素(hard delete)</summary>
    Task<OperationResult> DeleteAsync(uint psId, CancellationToken cancellationToken = default);
}

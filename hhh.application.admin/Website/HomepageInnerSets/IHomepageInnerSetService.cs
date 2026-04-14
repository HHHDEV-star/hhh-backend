using hhh.api.contracts.admin.Website.HomepageInnerSets;
<<<<<<< HEAD
using hhh.api.contracts.Common;
=======
>>>>>>> origin/main
using hhh.application.admin.Common;

namespace hhh.application.admin.Website.HomepageInnerSets;

public interface IHomepageInnerSetService
{
    /// <summary>取得首頁區塊元素列表(含 JOIN outer_site_set + enrichment)</summary>
<<<<<<< HEAD
    Task<PagedResponse<HomepageInnerSetListItem>> GetListAsync(ListQuery query, CancellationToken cancellationToken = default);
=======
    Task<List<HomepageInnerSetListItem>> GetListAsync(CancellationToken cancellationToken = default);
>>>>>>> origin/main

    /// <summary>新增單筆區塊元素</summary>
    Task<OperationResult<uint>> CreateAsync(CreateHomepageInnerSetRequest request, CancellationToken cancellationToken = default);

    /// <summary>修改單筆區塊元素</summary>
    Task<OperationResult<uint>> UpdateAsync(uint psId, UpdateHomepageInnerSetRequest request, CancellationToken cancellationToken = default);

    /// <summary>刪除單筆區塊元素(hard delete)</summary>
    Task<OperationResult> DeleteAsync(uint psId, CancellationToken cancellationToken = default);
}

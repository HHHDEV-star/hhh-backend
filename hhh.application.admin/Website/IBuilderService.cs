using hhh.api.contracts.admin.Website;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;

namespace hhh.application.admin.Website;

/// <summary>
/// 建商管理服務
/// （對應舊版 PHP: Builder.php 13.01–13.04 + 13.13）
/// </summary>
public interface IBuilderService
{
    /// <summary>建商分頁列表（支援 onoff 篩選 + keyword 跨欄位搜尋）</summary>
    Task<PagedResponse<BuilderListItem>> GetListAsync(
        BuilderListQuery query,
        CancellationToken cancellationToken = default);

    /// <summary>取得單筆建商完整資料</summary>
    Task<BuilderDetailResponse?> GetByIdAsync(
        uint id, CancellationToken cancellationToken = default);

    /// <summary>新增建商</summary>
    Task<OperationResult<uint>> CreateAsync(
        CreateBuilderRequest request, CancellationToken cancellationToken = default);

    /// <summary>修改建商</summary>
    Task<OperationResult<uint>> UpdateAsync(
        uint id, UpdateBuilderRequest request, CancellationToken cancellationToken = default);

    /// <summary>刪除建商</summary>
    Task<OperationResult<uint>> DeleteAsync(
        uint id, CancellationToken cancellationToken = default);

    /// <summary>建商下拉選單</summary>
    Task<List<BuilderDropdownItem>> GetDropdownAsync(
        CancellationToken cancellationToken = default);
}

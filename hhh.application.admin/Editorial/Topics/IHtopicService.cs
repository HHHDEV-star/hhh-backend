using hhh.api.contracts.admin.Editorial.Topics;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;

namespace hhh.application.admin.Editorial.Topics;

/// <summary>
/// 編輯部 - 主題管理服務
/// </summary>
/// <remarks>
/// 對應舊版 PHP:hhh-admin/_htopic.php（列表/刪除）+ _htopic_edit.php（新增/編輯）
/// 資料表:_htopic
/// </remarks>
public interface IHtopicService
{
    /// <summary>取得主題列表（分頁,支援關鍵字搜尋）</summary>
    Task<PagedResponse<HtopicListItem>> GetListAsync(HtopicListQuery query, CancellationToken ct = default);

    /// <summary>取得單一主題完整資料</summary>
    Task<HtopicDetailResponse?> GetByIdAsync(uint id, CancellationToken ct = default);

    /// <summary>新增主題</summary>
    Task<OperationResult<uint>> CreateAsync(CreateHtopicRequest request, CancellationToken ct = default);

    /// <summary>更新主題</summary>
    Task<OperationResult<uint>> UpdateAsync(uint id, UpdateHtopicRequest request, CancellationToken ct = default);

    /// <summary>刪除主題（hard delete,對應舊版 PHP DELETE）</summary>
    Task<OperationResult> DeleteAsync(uint id, CancellationToken ct = default);
}

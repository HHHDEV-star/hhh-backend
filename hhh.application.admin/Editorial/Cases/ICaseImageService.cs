using hhh.api.contracts.admin.Editorial.Cases;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;

namespace hhh.application.admin.Editorial.Cases;

/// <summary>
/// 個案圖片管理服務
/// （對應舊版 PHP: Cases.php image 系列端點）
/// </summary>
public interface ICaseImageService
{
    /// <summary>取得個案圖片列表</summary>
    Task<PagedResponse<CaseImageListItem>> GetListAsync(uint hcaseId, ListQuery query, CancellationToken ct = default);

    /// <summary>新增個案圖片</summary>
    Task<OperationResult<uint>> CreateAsync(uint hcaseId, CreateCaseImageRequest request, CancellationToken ct = default);

    /// <summary>更新個案圖片資訊</summary>
    Task<OperationResult> UpdateAsync(uint hcaseImgId, UpdateCaseImageRequest request, CancellationToken ct = default);

    /// <summary>刪除個案圖片</summary>
    Task<OperationResult> DeleteAsync(uint hcaseImgId, CancellationToken ct = default);

    /// <summary>設定封面圖</summary>
    Task<OperationResult> SetCoverAsync(uint hcaseId, SetCaseCoverRequest request, CancellationToken ct = default);

    /// <summary>重新排序（依現有 order 欄位重整為連續序號）</summary>
    Task<OperationResult> ReorderAsync(uint hcaseId, CancellationToken ct = default);
}

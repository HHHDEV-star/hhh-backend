using hhh.api.contracts.admin.Tags;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;

namespace hhh.application.admin.Tags;

/// <summary>
/// 標籤管理服務(跨 hcase / hcolumn / hvideo / image 四種資源)
/// </summary>
/// <remarks>
/// 對應舊版 PHP:hhh-api/.../third/v1/Tag.php(tag_model)
/// </remarks>
public interface ITagService
{
    Task<PagedResponse<TagHcaseItem>> GetHcaseTagsAsync(uint? hdesignerId, string? searchTag, ListQuery query, CancellationToken ct = default);
    Task<OperationResult> UpdateHcaseTagAsync(uint hcaseId, UpdateTagRequest request, CancellationToken ct = default);

    Task<PagedResponse<TagHcolumnItem>> GetHcolumnTagsAsync(string? ctype, string? ctitle, DateOnly? startDate, DateOnly? endDate, string? searchTag, ListQuery query, CancellationToken ct = default);
    Task<OperationResult> UpdateHcolumnTagAsync(uint hcolumnId, UpdateTagRequest request, CancellationToken ct = default);

    Task<PagedResponse<TagHvideoItem>> GetHvideoTagsAsync(uint? hdesignerId, string? title, DateOnly? startDate, DateOnly? endDate, string? searchTag, ListQuery query, CancellationToken ct = default);
    Task<OperationResult> UpdateHvideoTagAsync(uint hvideoId, UpdateTagRequest request, CancellationToken ct = default);

    Task<PagedResponse<TagImageItem>> GetImageTagsAsync(uint? hcaseId, string? searchTag, ListQuery query, CancellationToken ct = default);
    Task<OperationResult> UpdateImageTagAsync(uint hcaseImgId, UpdateImageTagRequest request, string? operatorEmail, CancellationToken ct = default);
}

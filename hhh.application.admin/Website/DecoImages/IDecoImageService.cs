using hhh.api.contracts.admin.WebSite.DecoImages;
using hhh.application.admin.Common;

namespace hhh.application.admin.WebSite.DecoImages;

/// <summary>查證照圖片審核服務</summary>
public interface IDecoImageService
{
    Task<List<DecoImageListItem>> GetListAsync(CancellationToken ct = default);
    Task<OperationResult> UpdateOnoffAsync(uint id, UpdateDecoImageOnoffRequest request, CancellationToken ct = default);
}

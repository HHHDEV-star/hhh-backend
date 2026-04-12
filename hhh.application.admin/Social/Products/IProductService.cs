using hhh.api.contracts.admin.Social.Products;
using hhh.application.admin.Common;

namespace hhh.application.admin.Social.Products;

/// <summary>產品後台管理服務</summary>
public interface IProductService
{
    Task<List<ProductListItem>> GetListAsync(CancellationToken ct = default);
    Task<OperationResult> UpdateAsync(uint id, UpdateProductRequest request, CancellationToken ct = default);
    Task<List<ProductSeoItem>> GetSeoListAsync(CancellationToken ct = default);
    Task<OperationResult> UpdateSeoImageAsync(uint id, UpdateProductSeoImageRequest request, CancellationToken ct = default);
}

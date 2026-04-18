using hhh.api.contracts.admin.Social.Products;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;

namespace hhh.application.admin.Social.Products;

/// <summary>產品後台管理服務</summary>
public interface IProductService
{
    Task<PagedResponse<ProductListItem>> GetListAsync(ProductListQuery query, CancellationToken ct = default);
    Task<OperationResult> UpdateAsync(uint id, UpdateProductRequest request, CancellationToken ct = default);
    Task<PagedResponse<ProductSeoItem>> GetSeoListAsync(ProductSeoListQuery query, CancellationToken ct = default);
    Task<OperationResult> UpdateSeoImageAsync(uint id, UpdateProductSeoImageRequest request, CancellationToken ct = default);
}

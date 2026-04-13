using hhh.api.contracts.admin.Website;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;

namespace hhh.application.admin.Website.BuilderProduct;

/// <summary>
/// 建案管理 service（含建案圖片 + 設定封面）。
/// 對應舊版 PHP: Builder.php 的 product / product_img / img 相關端點。
/// </summary>
public interface IBuilderProductService
{
    // =========================================================================
    // BuilderProduct 建案
    // =========================================================================

    /// <summary>建案分頁列表（JOIN builder.title + subquery column_ids/video_ids）</summary>
    Task<PagedResponse<BuilderProductListItem>> GetListAsync(
        BuilderProductListRequest request, CancellationToken cancellationToken = default);

    /// <summary>取得單筆建案完整資料</summary>
    Task<BuilderProductDetailResponse?> GetByIdAsync(
        uint id, CancellationToken cancellationToken = default);

    /// <summary>新增建案（含連動 _hcolumn / _hvideo）</summary>
    Task<OperationResult<uint>> CreateAsync(
        CreateBuilderProductRequest request, CancellationToken cancellationToken = default);

    /// <summary>修改建案（含重連動 _hcolumn / _hvideo）</summary>
    Task<OperationResult<uint>> UpdateAsync(
        uint id, UpdateBuilderProductRequest request, CancellationToken cancellationToken = default);

    /// <summary>刪除建案</summary>
    Task<OperationResult<uint>> DeleteAsync(
        uint id, CancellationToken cancellationToken = default);

    /// <summary>建案下拉選單（僅 onoff=1）</summary>
    Task<List<BuilderProductDropdownItem>> GetDropdownAsync(
        CancellationToken cancellationToken = default);

    // =========================================================================
    // BuilderProductImage 建案圖片
    // =========================================================================

    /// <summary>取得指定建案的圖片列表</summary>
    Task<List<BuilderProductImageListItem>> GetImagesAsync(
        uint productId, CancellationToken cancellationToken = default);

    /// <summary>新增建案圖片（同步更新 parent update_time）</summary>
    Task<OperationResult<uint>> CreateImageAsync(
        uint productId, CreateBuilderProductImageRequest request, CancellationToken cancellationToken = default);

    /// <summary>修改建案圖片（同步更新 parent update_time）</summary>
    Task<OperationResult<uint>> UpdateImageAsync(
        uint productId, uint imageId, UpdateBuilderProductImageRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>刪除建案圖片（同步更新 parent update_time）</summary>
    Task<OperationResult<uint>> DeleteImageAsync(
        uint productId, uint imageId, CancellationToken cancellationToken = default);

    /// <summary>設定建案封面（reset is_cover + 更新 builder_product.cover）</summary>
    Task<OperationResult<uint>> SetCoverAsync(
        uint productId, SetCoverRequest request, CancellationToken cancellationToken = default);
}

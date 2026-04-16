using hhh.api.contracts.admin.Brokers.Decos;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;

namespace hhh.application.admin.Brokers.Decos;

/// <summary>
/// 軟裝需求單服務（deco_request + deco_request_files）
/// （對應舊版 PHP: Renovation.php + Renovation_model.php 中的 deco_* 系列）
/// </summary>
public interface IDecoRequestService
{
    /// <summary>取得軟裝需求單分頁列表(只含 payment_status='N' AND is_delete='N',依 seq DESC)</summary>
    Task<PagedResponse<DecoRequestListItem>> GetListAsync(
        DecoRequestListQuery query,
        CancellationToken cancellationToken = default);

    /// <summary>取得軟裝需求單明細(單筆)</summary>
    Task<OperationResult<DecoRequestDetailResponse>> GetByIdAsync(
        int seq,
        CancellationToken cancellationToken = default);

    /// <summary>新增軟裝需求單(後端自動產生 guid)</summary>
    Task<OperationResult<int>> CreateAsync(
        CreateDecoRequestRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>更新軟裝需求單</summary>
    Task<OperationResult> UpdateAsync(
        int seq,
        UpdateDecoRequestRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>批次軟刪除(is_delete='Y')</summary>
    Task<OperationResult> BatchSoftDeleteAsync(
        BatchDeleteDecoRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>取得軟裝需求單的附件列表(依 deco_file_id DESC)</summary>
    Task<List<DecoRequestFileItem>> GetFilesAsync(
        int seq,
        CancellationToken cancellationToken = default);

    /// <summary>批次新增附件</summary>
    Task<OperationResult> CreateFilesAsync(
        int seq,
        CreateDecoRequestFilesRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>批次刪除附件(硬刪除,對應舊 PHP 的 del_deco_request_files)</summary>
    Task<OperationResult> DeleteFilesAsync(
        BatchDeleteDecoRequestFiles request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 付款通知:更新價格欄位與付款狀態,並寫 _hoplog。
    /// (舊 PHP 版還會寄 email,.NET 未整合 SMTP 前以 log 取代)
    /// </summary>
    Task<OperationResult> SetPriceAsync(
        int seq,
        SetDecoPriceRequest request,
        CancellationToken cancellationToken = default);
}

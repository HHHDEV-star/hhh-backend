using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Hprizes;
using hhh.infrastructure.Storage;

namespace hhh.application.admin.Hprizes;

/// <summary>
/// 後台獎品服務（對應舊版 _hprize*.php 一整組）
/// </summary>
public interface IHprizeService
{
    /// <summary>
    /// 取得獎品分頁列表（對應舊版 _hprize.php）。
    /// </summary>
    Task<PagedResponse<HprizeListItem>> GetListAsync(
        HprizeListRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 取得單一獎品完整資料（對應舊版 _hprize_edit.php GET 分支）
    /// </summary>
    /// <returns>找不到時回傳 null。</returns>
    Task<HprizeDetailResponse?> GetByIdAsync(
        uint id,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 新增獎品。
    /// logo 由 controller 先上傳後以 ImageUploadResult 傳入；必填。
    /// </summary>
    Task<HprizeMutationResult> CreateAsync(
        CreateHprizeRequest request,
        ImageUploadResult logo,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 更新獎品。
    /// newLogo 為選填：傳入 null 代表保留原有 logo；
    /// 傳入新 ImageUploadResult 時，service 會把舊檔相對路徑塞到
    /// HprizeMutationResult.OldLogoRelativePath，讓 controller 負責刪檔。
    /// </summary>
    Task<HprizeMutationResult> UpdateAsync(
        uint id,
        UpdateHprizeRequest request,
        ImageUploadResult? newLogo,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 刪除獎品。會把舊 logo 的相對路徑塞到結果裡供 controller 清檔。
    /// </summary>
    Task<HprizeMutationResult> DeleteAsync(
        uint id,
        CancellationToken cancellationToken = default);
}

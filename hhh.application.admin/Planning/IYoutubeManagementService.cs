using hhh.api.contracts.admin.Planning;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;

namespace hhh.application.admin.Planning;

/// <summary>
/// YouTube 進階管理服務（同步、搜尋匯入、群組管理）
/// （對應舊版 PHP: Youtube.php sync_post / id_get + Program.php group CRUD）
/// </summary>
public interface IYoutubeManagementService
{
    // ── 群組管理 ──

    /// <summary>取得 YouTube 群組完整列表（分頁，gid DESC）</summary>
    Task<PagedResponse<YoutubeGroupListItem>> GetGroupListAsync(
        ListQuery query,
        CancellationToken cancellationToken = default);

    /// <summary>取得 YouTube 群組下拉選單（onoff='Y'，gid DESC）</summary>
    Task<List<YoutubeGroupDropdownItem>> GetGroupDropdownAsync(
        CancellationToken cancellationToken = default);

    /// <summary>新增 YouTube 群組（名稱不可重複）</summary>
    Task<OperationResult<uint>> CreateGroupAsync(
        CreateYoutubeGroupRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>更新 YouTube 群組（名稱 + 啟用狀態）</summary>
    Task<OperationResult> UpdateGroupAsync(
        uint gid, UpdateYoutubeGroupRequest request,
        CancellationToken cancellationToken = default);

    // ── 群組明細管理 ──

    /// <summary>取得群組明細列表（分頁，JOIN youtube_list + youtube_group）</summary>
    Task<PagedResponse<YoutubeGroupDetailListItem>> GetGroupDetailListAsync(
        ListQuery query,
        CancellationToken cancellationToken = default);

    /// <summary>更新群組明細（排序 + 開關）</summary>
    Task<OperationResult> UpdateGroupDetailAsync(
        uint id, UpdateYoutubeGroupDetailRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>刪除群組明細</summary>
    Task<OperationResult> DeleteGroupDetailAsync(
        uint id,
        CancellationToken cancellationToken = default);

    // ── YouTube 同步 / 匯入 ──

    /// <summary>同步 YouTube 頻道影片（呼叫 YouTube Data API）</summary>
    Task<OperationResult> SyncChannelAsync(
        CancellationToken cancellationToken = default);

    /// <summary>以 YouTube 影片 ID 搜尋並匯入（呼叫 YouTube Data API）</summary>
    Task<OperationResult> ImportByVideoIdAsync(
        string youtubeVideoId,
        CancellationToken cancellationToken = default);
}

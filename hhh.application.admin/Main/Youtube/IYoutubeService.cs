using hhh.api.contracts.admin.Main.Youtube;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;

namespace hhh.application.admin.Main.Youtube;

/// <summary>
/// Youtube 影片管理服務
/// </summary>
/// <remarks>
/// 對應舊版 PHP:hhh-api/.../third/v1/Youtube.php(youtube_model)
/// 後台 view:modify_youtube.php / youtube.php
/// </remarks>
public interface IYoutubeService
{
    /// <summary>
    /// 取得 Youtube 影片列表
    /// (對應舊版 youtube_model::get_youtube_list:篩 is_del=0,ORDER BY yid DESC)
    /// </summary>
    Task<PagedResponse<YoutubeListItem>> GetListAsync(ListQuery query, CancellationToken cancellationToken = default);

    /// <summary>新增 Youtube 影片</summary>
    Task<OperationResult<uint>> CreateAsync(
        CreateYoutubeRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>更新 Youtube 影片</summary>
    Task<OperationResult<uint>> UpdateAsync(
        uint yid,
        UpdateYoutubeRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>刪除 Youtube 影片(hard delete)</summary>
    Task<OperationResult> DeleteAsync(
        uint yid,
        CancellationToken cancellationToken = default);
}

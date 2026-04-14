using hhh.api.contracts.admin.Planning;
using hhh.application.admin.Common;

namespace hhh.application.admin.Planning;

/// <summary>
/// 節目管理服務（prog_video + prog_list + _hprog_chan）
/// （對應舊版 PHP: Program.php + _hprog_chan.php）
/// </summary>
public interface IProgramVideoService
{
    /// <summary>取得節目影片列表（依頻道 + 日期範圍，JOIN youtube_list）</summary>
    Task<List<ProgramVideoListItem>> GetListAsync(
        ProgramVideoQuery query,
        CancellationToken cancellationToken = default);

    /// <summary>更新節目影片（頻道、日期、開關、排序）</summary>
    Task<OperationResult> UpdateAsync(
        uint progId, UpdateProgramVideoRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>刪除節目影片</summary>
    Task<OperationResult> DeleteAsync(
        uint progId,
        CancellationToken cancellationToken = default);

    /// <summary>依群組批次產生節目表（展開日期範圍 × 群組明細）</summary>
    Task<OperationResult> GenerateFromGroupAsync(
        GenerateFromGroupRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>更新節目影片的郵件內容</summary>
    Task<OperationResult> UpdateMailContentAsync(
        uint progId, string content,
        CancellationToken cancellationToken = default);

    // ── 節目表 (prog_list) ──

    /// <summary>取得節目表列表（依日期範圍，含未上架）</summary>
    Task<List<ProgramListItem>> GetProgramListAsync(
        DateOnly sdate, DateOnly edate,
        CancellationToken cancellationToken = default);

    /// <summary>審核節目表（批次設為上架 onoff='Y'）</summary>
    Task<OperationResult> ApproveProgramListAsync(
        DateOnly sdate, DateOnly edate,
        CancellationToken cancellationToken = default);

    // ── 頻道管理 (_hprog_chan) ──

    /// <summary>取得頻道列表</summary>
    Task<List<ChannelListItem>> GetChannelListAsync(
        CancellationToken cancellationToken = default);

    /// <summary>取得單一頻道</summary>
    Task<ChannelListItem?> GetChannelAsync(
        uint chanId,
        CancellationToken cancellationToken = default);

    /// <summary>新增頻道</summary>
    Task<OperationResult<uint>> CreateChannelAsync(
        CreateChannelRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>更新頻道</summary>
    Task<OperationResult> UpdateChannelAsync(
        uint chanId, UpdateChannelRequest request,
        CancellationToken cancellationToken = default);
}

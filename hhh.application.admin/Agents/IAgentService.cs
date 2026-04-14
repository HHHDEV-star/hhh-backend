using hhh.api.contracts.admin.Agents;
using hhh.application.admin.Common;

namespace hhh.application.admin.Agents;

/// <summary>
/// 經紀人管理服務（agent_form + agent_files）
/// （對應舊版 PHP: Agent.php + Agent_model.php）
/// </summary>
public interface IAgentService
{
    /// <summary>取得經紀人列表（依日期範圍 + 關鍵字篩選，含總數）</summary>
    Task<AgentListResponse> GetListAsync(
        AgentQuery query,
        CancellationToken cancellationToken = default);

    /// <summary>軟刪除經紀人（is_del = 1）</summary>
    Task<OperationResult> DeleteAsync(
        uint agentId,
        CancellationToken cancellationToken = default);

    /// <summary>取得經紀人附件列表</summary>
    Task<List<AgentFileListItem>> GetFilesAsync(
        uint agentId,
        CancellationToken cancellationToken = default);

    /// <summary>軟刪除經紀人附件（is_del = 1）</summary>
    Task<OperationResult> DeleteFileAsync(
        uint fileId,
        CancellationToken cancellationToken = default);
}

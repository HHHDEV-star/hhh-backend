using hhh.api.contracts.admin.Agents;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;

namespace hhh.application.admin.Agents;

/// <summary>
/// 經紀人管理服務（agent_form + agent_files）
/// （對應舊版 PHP: Agent.php + Agent_model.php）
/// </summary>
public interface IAgentService
{
    /// <summary>取得經紀人列表（依日期範圍 + 關鍵字篩選，分頁）</summary>
    Task<PagedResponse<AgentListItem>> GetListAsync(
        AgentListQuery query,
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

    /// <summary>取得經紀人表單詳情（單筆）</summary>
    Task<OperationResult<AgentDetailResponse>> GetByIdAsync(
        uint agentId,
        CancellationToken cancellationToken = default);

    /// <summary>新增經紀人表單</summary>
    Task<OperationResult<uint>> CreateAsync(
        CreateAgentRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>更新經紀人表單</summary>
    Task<OperationResult> UpdateAsync(
        uint agentId,
        UpdateAgentRequest request,
        CancellationToken cancellationToken = default);
}

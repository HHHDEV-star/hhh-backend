using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Agents;
using hhh.application.admin.Agents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hhh.webapi.admin.Controllers;

/// <summary>
/// 幸福經紀人管理 API（agent_form + agent_files）
/// （對應舊版 PHP: Agent.php + Agent_model.php）
/// </summary>
[Route("api/agents")]
[Authorize]
[Tags("Agents")]
public class AgentsController : ApiControllerBase
{
    private readonly IAgentService _agentService;

    public AgentsController(IAgentService agentService)
    {
        _agentService = agentService;
    }

    /// <summary>取得經紀人列表</summary>
    /// <remarks>
    /// 對應舊版 PHP: Agent/lists_get
    /// 支援日期範圍 + 關鍵字篩選。關鍵字以 09 開頭會搜手機號碼，其餘搜多欄位（姓名、市話、縣市…）。
    /// </remarks>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<AgentListResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetList(
        [FromQuery] AgentQuery query,
        CancellationToken cancellationToken)
    {
        var data = await _agentService.GetListAsync(query, cancellationToken);
        return Ok(ApiResponse<AgentListResponse>.Success(data));
    }

    /// <summary>刪除經紀人（軟刪除）</summary>
    /// <remarks>
    /// 對應舊版 PHP: Agent/lists_delete
    /// 設定 is_del=1，不會真正刪除資料。
    /// </remarks>
    [HttpDelete("{agentId:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        uint agentId,
        CancellationToken cancellationToken)
    {
        var result = await _agentService.DeleteAsync(agentId, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        return Ok(ApiResponse.Ok(result.Message));
    }

    /// <summary>取得經紀人附件列表</summary>
    /// <remarks>
    /// 對應舊版 PHP: Agent/files_get
    /// </remarks>
    [HttpGet("{agentId:int}/files")]
    [ProducesResponseType(typeof(ApiResponse<List<AgentFileListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFiles(
        uint agentId,
        CancellationToken cancellationToken)
    {
        var data = await _agentService.GetFilesAsync(agentId, cancellationToken);
        return Ok(ApiResponse<List<AgentFileListItem>>.Success(data));
    }

    /// <summary>刪除經紀人附件（軟刪除）</summary>
    /// <remarks>
    /// 對應舊版 PHP: Agent/files_delete
    /// 設定 is_del=1，不會真正刪除檔案。
    /// </remarks>
    [HttpDelete("{agentId:int}/files/{fileId:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteFile(
        uint agentId,
        uint fileId,
        CancellationToken cancellationToken)
    {
        var result = await _agentService.DeleteFileAsync(fileId, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));
        return Ok(ApiResponse.Ok(result.Message));
    }
}

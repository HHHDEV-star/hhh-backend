using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Main.Youtube;
using hhh.api.contracts.admin.Planning;
using hhh.application.admin.Main.Youtube;
using hhh.application.admin.Planning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hhh.webapi.admin.Controllers;

/// <summary>
/// 節目企劃 API — YouTube 管理 + 節目影片
/// （對應舊版 PHP: third/v1/Youtube.php + Program.php）
/// </summary>
[Route("api/planning")]
[Authorize]
[Tags("Planning")]
public class PlanningController : ApiControllerBase
{
    private readonly IYoutubeService _youtubeService;
    private readonly IYoutubeManagementService _youtubeManagementService;
    private readonly IProgramVideoService _programVideoService;

    public PlanningController(
        IYoutubeService youtubeService,
        IYoutubeManagementService youtubeManagementService,
        IProgramVideoService programVideoService)
    {
        _youtubeService = youtubeService;
        _youtubeManagementService = youtubeManagementService;
        _programVideoService = programVideoService;
    }

    // =========================================================================
    // YouTube CRUD (youtube)
    // =========================================================================

    /// <summary>取得 YouTube 影片列表</summary>
    /// <remarks>
    /// 對應舊版 PHP: Youtube/index_get（10.1）
    /// 篩選 is_del=0 且 channel_id 在白名單內，ORDER BY yid DESC，全量回傳。
    /// </remarks>
    [HttpGet("youtube/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<YoutubeListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetYoutubeList([FromQuery] ListQuery query, CancellationToken cancellationToken)
    {
        var data = await _youtubeService.GetListAsync(query, cancellationToken);
        return Ok(ApiResponse<PagedResponse<YoutubeListItem>>.Success(data));
    }

    /// <summary>新增 YouTube 影片</summary>
    /// <remarks>
    /// 對應舊版 PHP: Youtube/index_post（10.6）
    /// 舊版必填: hdesigner_id、hbrand_id、builder_id、channel_id、title、description。
    /// 寫入時系統自動設定 onoff='Y'、is_del=false、create_time=now、update_time=now。
    /// </remarks>
    [HttpPost("youtube")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateYoutube(
        [FromBody] CreateYoutubeRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _youtubeService.CreateAsync(request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return StatusCode(StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { yid = result.Data }, result.Message));
    }

    /// <summary>更新 YouTube 影片</summary>
    /// <remarks>
    /// 對應舊版 PHP: Youtube/index_put（10.3）
    /// 舊版必填: yid（URL）、onoff。
    /// </remarks>
    [HttpPut("youtube/{yid:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateYoutube(
        uint yid,
        [FromBody] UpdateYoutubeRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _youtubeService.UpdateAsync(yid, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { yid = result.Data }, result.Message));
    }

    /// <summary>刪除 YouTube 影片</summary>
    /// <remarks>
    /// 對應舊版 PHP: Youtube/index_delete（10.5）
    /// 注意: 舊版是 hard DELETE（非 soft delete），本 API 保留此行為。
    /// </remarks>
    [HttpDelete("youtube/{yid:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteYoutube(uint yid, CancellationToken cancellationToken)
    {
        var result = await _youtubeService.DeleteAsync(yid, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }

    // =========================================================================
    // YouTube 同步 / 匯入 (youtube)
    // =========================================================================

    /// <summary>同步 YouTube 頻道影片</summary>
    /// <remarks>
    /// 對應舊版 PHP: Youtube/sync_post（10.4）
    /// 呼叫 YouTube Data API 抓取幸福空間頻道最新影片，
    /// 並 upsert 到 youtube_list，再自動解析標題配對設計師/廠商 ID。
    /// 需要在 appsettings.json 設定 Youtube:ApiKey。
    /// </remarks>
    [HttpPost("youtube/sync")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    public async Task<IActionResult> SyncYoutube(CancellationToken cancellationToken)
    {
        var result = await _youtubeManagementService.SyncChannelAsync(cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }

    /// <summary>以 YouTube 影片 ID 搜尋並匯入</summary>
    /// <remarks>
    /// 對應舊版 PHP: Youtube/id_get（10.2）
    /// 輸入 YouTube 影片 ID，呼叫 YouTube Data API 取得影片資訊後 upsert 到 youtube_list。
    /// 需要在 appsettings.json 設定 Youtube:ApiKey。
    /// </remarks>
    [HttpPost("youtube/import-by-id")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ImportYoutubeByVideoId(
        [FromQuery] string id,
        CancellationToken cancellationToken)
    {
        var result = await _youtubeManagementService.ImportByVideoIdAsync(id, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }

    // =========================================================================
    // YouTube 群組 (youtube-groups)
    // =========================================================================

    /// <summary>取得 YouTube 群組列表（全部）</summary>
    /// <remarks>
    /// 對應舊版 PHP: Program/group_get → read_group()
    /// 回傳所有群組（含停用），排序: gid DESC。
    /// </remarks>
    [HttpGet("youtube-groups/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<YoutubeGroupListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetYoutubeGroupList(
        [FromQuery] ListQuery query,
        CancellationToken cancellationToken)
    {
        var data = await _youtubeManagementService.GetGroupListAsync(query, cancellationToken);
        return Ok(ApiResponse<PagedResponse<YoutubeGroupListItem>>.Success(data));
    }

    /// <summary>取得 YouTube 群組下拉選單（僅啟用）</summary>
    /// <remarks>
    /// 對應舊版 PHP: Program/group_dropdownlist_get
    /// 回傳 onoff='Y' 的群組，排序: gid DESC。
    /// </remarks>
    [HttpGet("youtube-groups/dropdown")]
    [ProducesResponseType(typeof(ApiResponse<List<YoutubeGroupDropdownItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetYoutubeGroupDropdown(CancellationToken cancellationToken)
    {
        var data = await _youtubeManagementService.GetGroupDropdownAsync(cancellationToken);
        return Ok(ApiResponse<List<YoutubeGroupDropdownItem>>.Success(data));
    }

    /// <summary>新增 YouTube 群組</summary>
    /// <remarks>
    /// 對應舊版 PHP: Program/group_post → insert_group()
    /// 新增時自動設定 onoff='Y'、create_time=now。名稱不可重複。
    /// </remarks>
    [HttpPost("youtube-groups")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateYoutubeGroup(
        [FromBody] CreateYoutubeGroupRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _youtubeManagementService.CreateGroupAsync(request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return StatusCode(StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { gid = result.Data }, result.Message));
    }

    /// <summary>更新 YouTube 群組</summary>
    /// <remarks>
    /// 對應舊版 PHP: Program/group_put → update_group()
    /// 可更新名稱及啟用狀態。名稱不可與其他群組重複。
    /// </remarks>
    [HttpPut("youtube-groups/{gid:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateYoutubeGroup(
        uint gid,
        [FromBody] UpdateYoutubeGroupRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _youtubeManagementService.UpdateGroupAsync(gid, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { gid }, result.Message));
    }

    // =========================================================================
    // YouTube 群組明細 (youtube-group-details)
    // =========================================================================

    /// <summary>取得 YouTube 群組明細列表</summary>
    /// <remarks>
    /// 對應舊版 PHP: Program/group_detail_get → get_group_detail()
    /// JOIN youtube_group_detail + youtube_list + youtube_group，
    /// 排序: gid DESC, sort ASC。
    /// </remarks>
    [HttpGet("youtube-group-details/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<YoutubeGroupDetailListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetYoutubeGroupDetailList(
        [FromQuery] ListQuery query,
        CancellationToken cancellationToken)
    {
        var data = await _youtubeManagementService.GetGroupDetailListAsync(query, cancellationToken);
        return Ok(ApiResponse<PagedResponse<YoutubeGroupDetailListItem>>.Success(data));
    }

    /// <summary>更新 YouTube 群組明細</summary>
    /// <remarks>
    /// 對應舊版 PHP: Program/group_detail_put → update_group_detail()
    /// 可更新排序及開關狀態。
    /// </remarks>
    [HttpPut("youtube-group-details/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateYoutubeGroupDetail(
        uint id,
        [FromBody] UpdateYoutubeGroupDetailRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _youtubeManagementService.UpdateGroupDetailAsync(id, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { id }, result.Message));
    }

    /// <summary>刪除 YouTube 群組明細</summary>
    /// <remarks>
    /// 對應舊版 PHP: Program/group_detail_delete → delete_group_detail()
    /// 硬刪除。
    /// </remarks>
    [HttpDelete("youtube-group-details/{id:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteYoutubeGroupDetail(
        uint id, CancellationToken cancellationToken)
    {
        var result = await _youtubeManagementService.DeleteGroupDetailAsync(id, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }

    // =========================================================================
    // 節目影片 (program-videos)
    // =========================================================================

    /// <summary>取得節目影片列表</summary>
    /// <remarks>
    /// 對應舊版 PHP: Program/unit_get → get_unit_data()
    /// 依頻道名稱 + 日期範圍查詢，JOIN youtube_list 取影片資訊。
    /// </remarks>
    [HttpGet("program-videos/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<ProgramVideoListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProgramVideoList(
        [FromQuery] ProgramVideoQuery query,
        [FromQuery] ListQuery listQuery,
        CancellationToken cancellationToken)
    {
        var data = await _programVideoService.GetListAsync(query, listQuery, cancellationToken);
        return Ok(ApiResponse<PagedResponse<ProgramVideoListItem>>.Success(data));
    }

    /// <summary>更新節目影片</summary>
    /// <remarks>
    /// 對應舊版 PHP: Program/unit_put → update_unit_list()
    /// 可更新頻道、播放日期、上架日期、排序、開關。
    /// </remarks>
    [HttpPut("program-videos/{progId:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProgramVideo(
        uint progId,
        [FromBody] UpdateProgramVideoRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _programVideoService.UpdateAsync(progId, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { progId }, result.Message));
    }

    /// <summary>刪除節目影片</summary>
    /// <remarks>
    /// 對應舊版 PHP: Program/unit_delete → delete()
    /// 硬刪除。
    /// </remarks>
    [HttpDelete("program-videos/{progId:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProgramVideo(
        uint progId, CancellationToken cancellationToken)
    {
        var result = await _programVideoService.DeleteAsync(progId, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }

    /// <summary>依群組批次產生節目表</summary>
    /// <remarks>
    /// 對應舊版 PHP: Program/index_by_group_post → create_daily_by_group()
    /// 將群組內的影片展開到指定日期範圍，自動跳過重複。
    /// </remarks>
    [HttpPost("program-videos/generate-from-group")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    public async Task<IActionResult> GenerateProgramVideosFromGroup(
        [FromBody] GenerateFromGroupRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _programVideoService.GenerateFromGroupAsync(request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return StatusCode(StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { }, result.Message));
    }

    /// <summary>更新節目影片郵件內容</summary>
    /// <remarks>
    /// 對應舊版 PHP: Program/mail_content_post → set_mail_content()
    /// 儲存自訂的郵件 HTML 內容。
    /// </remarks>
    [HttpPatch("program-videos/{progId:int}/mail-content")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProgramVideoMailContent(
        uint progId,
        [FromBody] UpdateMailContentRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _programVideoService.UpdateMailContentAsync(progId, request.Content, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { progId }, result.Message));
    }

    // =========================================================================
    // 節目表 (program-list)
    // =========================================================================

    /// <summary>取得節目表列表</summary>
    /// <remarks>
    /// 對應舊版 PHP: Program/playlist_get (backstage)
    /// 依日期範圍查詢，含未上架資料。排序: prog_date ASC, prog_time ASC。
    /// </remarks>
    [HttpGet("program-list/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<ProgramListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProgramList(
        [FromQuery] DateOnly sdate,
        [FromQuery] DateOnly edate,
        [FromQuery] ListQuery listQuery,
        CancellationToken cancellationToken)
    {
        var data = await _programVideoService.GetProgramListAsync(sdate, edate, listQuery, cancellationToken);
        return Ok(ApiResponse<PagedResponse<ProgramListItem>>.Success(data));
    }

    /// <summary>審核節目表（批次上架）</summary>
    /// <remarks>
    /// 對應舊版 PHP: Program/list_put → update_prog_list_onoff()
    /// 將指定日期範圍內的節目全部設為 onoff='Y'。
    /// </remarks>
    [HttpPost("program-list/approve")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ApproveProgramList(
        [FromBody] ApproveProgramListRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _programVideoService.ApproveProgramListAsync(
            request.Sdate, request.Edate, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { }, result.Message));
    }

    // =========================================================================
    // 頻道管理 (channels)
    // =========================================================================

    /// <summary>取得頻道列表</summary>
    /// <remarks>
    /// 對應舊版 PHP: _hprog_chan.php 列表
    /// 全量回傳，排序: chan_id DESC。
    /// </remarks>
    [HttpGet("channels/list")]
    [ProducesResponseType(typeof(ApiResponse<PagedResponse<ChannelListItem>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetChannelList(
        [FromQuery] ListQuery query,
        CancellationToken cancellationToken)
    {
        var data = await _programVideoService.GetChannelListAsync(query, cancellationToken);
        return Ok(ApiResponse<PagedResponse<ChannelListItem>>.Success(data));
    }

    /// <summary>取得單一頻道</summary>
    [HttpGet("channels/{chanId:int}")]
    [ProducesResponseType(typeof(ApiResponse<ChannelListItem>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetChannel(uint chanId, CancellationToken cancellationToken)
    {
        var data = await _programVideoService.GetChannelAsync(chanId, cancellationToken);
        if (data is null)
            return StatusCode(404, ApiResponse.Error(404, $"找不到頻道 chan_id={chanId}"));

        return Ok(ApiResponse<ChannelListItem>.Success(data));
    }

    /// <summary>新增頻道</summary>
    /// <remarks>
    /// 對應舊版 PHP: _hprog_chan_edit.php 新增
    /// Logo 欄位傳入 URL 字串；檔案上傳請透過通用上傳 API 取得 URL 後再填入。
    /// </remarks>
    [HttpPost("channels")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateChannel(
        [FromBody] CreateChannelRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _programVideoService.CreateChannelAsync(request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return StatusCode(StatusCodes.Status201Created,
            ApiResponse<object>.Created(new { chanId = result.Data }, result.Message));
    }

    /// <summary>更新頻道</summary>
    /// <remarks>
    /// 對應舊版 PHP: _hprog_chan_edit.php 編輯
    /// Logo 欄位若不傳（null）則保留原值。
    /// </remarks>
    [HttpPut("channels/{chanId:int}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateChannel(
        uint chanId,
        [FromBody] UpdateChannelRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _programVideoService.UpdateChannelAsync(chanId, request, cancellationToken);
        if (!result.IsSuccess)
            return StatusCode(result.Code, ApiResponse.Error(result.Code, result.Message));

        return Ok(ApiResponse<object>.Success(new { chanId }, result.Message));
    }
}

using hhh.api.contracts.admin.Planning;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Planning;

public class ProgramVideoService : IProgramVideoService
{
    private readonly XoopsContext _db;

    /// <summary>頻道名稱 → 頻道 ID 對照（對應 PHP channel.php config）</summary>
    private static readonly Dictionary<string, string> ChannelMap = new(StringComparer.Ordinal)
    {
        ["幸福空間居家台"] = "gstv",
        ["TVBS(歡樂台)"] = "tvbs",
        ["台灣綜合"] = "tntv",
        ["東風衛視"] = "aziotv",
        ["Li Tv 立視線上影視"] = "supertv",
        ["東森財經"] = "ebcfnc",
        ["衛視中文台"] = "scc",
        ["緯來戲劇台"] = "vldrama",
    };

    public ProgramVideoService(XoopsContext db)
    {
        _db = db;
    }

    /// <inheritdoc />
    public async Task<PagedResponse<ProgramVideoListItem>> GetListAsync(
        ProgramVideoQuery query, ListQuery listQuery,
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: Program_model::get_unit_data()
        // JOIN prog_video + youtube_list, WHERE chan_name + display_date BETWEEN
        return await (
            from p in _db.ProgVideos.AsNoTracking()
            join y in _db.YoutubeLists.AsNoTracking() on p.Yid equals (int)y.Yid
            where p.ChanName == query.Channel
               && p.DisplayDate >= query.Sdate
               && p.DisplayDate <= query.Edate
            orderby p.Sort ascending
            select new ProgramVideoListItem
            {
                ProgId = p.ProgId,
                ChanId = p.ChanId,
                ChanName = p.ChanName,
                DisplayDate = p.DisplayDate,
                DisplayDatetime = p.DisplayDatetime,
                Gid = p.Gid,
                GroupName = p.GroupName,
                Yid = p.Yid,
                YoutubeVideoId = y.YoutubeVideoId,
                Onoff = p.Onoff,
                Sort = p.Sort,
                HdesignerId = y.HdesignerId,
                HbrandId = y.HbrandId,
                BuilderId = y.BuilderId,
                Title = y.Title,
            })
            .ToPagedResponseAsync(listQuery.Page, listQuery.PageSize, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<OperationResult> UpdateAsync(
        uint progId, UpdateProgramVideoRequest request,
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: Program_model::update_unit_list()
        if (!ChannelMap.TryGetValue(request.ChanName, out var chanId))
            return OperationResult.BadRequest($"不支援的頻道名稱「{request.ChanName}」");

        var affected = await _db.ProgVideos
            .Where(p => p.ProgId == progId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.ChanId, chanId)
                .SetProperty(p => p.ChanName, request.ChanName)
                .SetProperty(p => p.DisplayDate, request.DisplayDate)
                .SetProperty(p => p.DisplayDatetime, request.DisplayDatetime)
                .SetProperty(p => p.Onoff, request.Onoff)
                .SetProperty(p => p.Sort, request.Sort)
                .SetProperty(p => p.UpdateTime, DateTime.UtcNow),
                cancellationToken);

        return affected == 0
            ? OperationResult.NotFound($"找不到節目 prog_id={progId}")
            : OperationResult.Ok("節目更新成功");
    }

    /// <inheritdoc />
    public async Task<OperationResult> DeleteAsync(
        uint progId,
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: Program_model::delete()
        var affected = await _db.ProgVideos
            .Where(p => p.ProgId == progId)
            .ExecuteDeleteAsync(cancellationToken);

        return affected == 0
            ? OperationResult.NotFound($"找不到節目 prog_id={progId}")
            : OperationResult.Ok("節目刪除成功");
    }

    /// <inheritdoc />
    public async Task<OperationResult> GenerateFromGroupAsync(
        GenerateFromGroupRequest request,
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: Program_model::create_daily_by_group()

        if (!ChannelMap.TryGetValue(request.Channel, out var chanId))
            return OperationResult.BadRequest($"不支援的頻道名稱「{request.Channel}」");

        if (request.Sdate > request.Edate)
            return OperationResult.BadRequest("開始日期不得大於結束日期");

        // 取得群組
        var group = await _db.YoutubeGroups
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.Gid == request.Gid, cancellationToken);

        if (group is null)
            return OperationResult.NotFound($"找不到群組 gid={request.Gid}");

        // 取得群組明細
        var groupDetails = await _db.YoutubeGroupDetails
            .AsNoTracking()
            .Where(d => d.Gid == request.Gid)
            .OrderBy(d => d.Sort)
            .ToListAsync(cancellationToken);

        if (groupDetails.Count == 0)
            return OperationResult.BadRequest("此群組尚無明細資料");

        var now = DateTime.UtcNow;
        var days = request.Edate.DayNumber - request.Sdate.DayNumber;
        var sortCnt = 1;

        for (var i = 0; i <= days; i++)
        {
            var targetDate = request.Sdate.AddDays(i);

            foreach (var detail in groupDetails)
            {
                // 檢查是否有重複資料（同頻道 + 同日期 + 同影片）
                var exists = await _db.ProgVideos
                    .AnyAsync(p => p.ChanId == chanId
                        && p.DisplayDate == targetDate
                        && p.Yid == (int)detail.Yid,
                        cancellationToken);

                if (exists) continue;

                // 取得目前同頻道同日期已有幾筆（作為排序基底）
                var currentCount = await _db.ProgVideos
                    .CountAsync(p => p.ChanId == chanId
                        && p.DisplayDate == targetDate,
                        cancellationToken);

                _db.ProgVideos.Add(new infrastructure.Dto.Xoops.ProgVideo
                {
                    ChanId = chanId,
                    ChanName = request.Channel,
                    DisplayDate = targetDate,
                    DisplayDatetime = request.Adate,
                    Gid = (int)detail.Gid,
                    GroupName = group.Name,
                    Yid = (int)detail.Yid,
                    Onoff = "Y",
                    Sort = (ushort)(currentCount + sortCnt),
                    MailContent = string.Empty,
                    IsSend = "N",
                    SendDatetime = default,
                    CreateTime = now,
                    UpdateTime = now,
                });
                sortCnt++;
            }
        }

        await _db.SaveChangesAsync(cancellationToken);
        return OperationResult.Created("節目表產生成功");
    }

    /// <inheritdoc />
    public async Task<OperationResult> UpdateMailContentAsync(
        uint progId, string content,
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: Program_model::set_mail_content()
        var affected = await _db.ProgVideos
            .Where(p => p.ProgId == progId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.MailContent, content),
                cancellationToken);

        return affected == 0
            ? OperationResult.NotFound($"找不到節目 prog_id={progId}")
            : OperationResult.Ok("郵件內容儲存成功");
    }

    // ── 節目表 (prog_list) ──

    /// <inheritdoc />
    public async Task<PagedResponse<ProgramListItem>> GetProgramListAsync(
        DateOnly sdate, DateOnly edate, ListQuery listQuery,
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: Program_model::get_pgrogram_list() (backstage)
        // SELECT prog_date, TIME_FORMAT(prog_time,'%H:%i'), prog_name, onoff
        // WHERE prog_date BETWEEN sdate AND edate
        // ORDER BY prog_date ASC, prog_time ASC
        return await _db.ProgLists
            .AsNoTracking()
            .Where(p => p.ProgDate >= sdate && p.ProgDate <= edate)
            .OrderBy(p => p.ProgDate)
            .ThenBy(p => p.ProgTime)
            .Select(p => new ProgramListItem
            {
                ProgDate = p.ProgDate,
                ProgTime = p.ProgTime.ToString("HH:mm"),
                ProgName = p.ProgName,
                Onoff = p.Onoff,
            })
            .ToPagedResponseAsync(listQuery.Page, listQuery.PageSize, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<OperationResult> ApproveProgramListAsync(
        DateOnly sdate, DateOnly edate,
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: Program_model::update_prog_list_onoff()
        // UPDATE prog_list SET onoff='Y', update_time=now WHERE prog_date BETWEEN sdate AND edate
        if (sdate > edate)
            return OperationResult.BadRequest("開始日期不得大於結束日期");

        var affected = await _db.ProgLists
            .Where(p => p.ProgDate >= sdate && p.ProgDate <= edate)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.Onoff, "Y")
                .SetProperty(p => p.UpdateTime, DateTime.UtcNow),
                cancellationToken);

        return OperationResult.Ok($"已審核 {affected} 筆節目上架");
    }

    // ── 頻道管理 (_hprog_chan) ──

    /// <inheritdoc />
    public async Task<PagedResponse<ChannelListItem>> GetChannelListAsync(
        ListQuery query,
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: SELECT * FROM _hprog_chan ORDER BY chan_id DESC
        return await _db.HprogChans
            .AsNoTracking()
            .OrderByDescending(c => c.ChanId)
            .Select(c => new ChannelListItem
            {
                ChanId = c.ChanId,
                Cname = c.Cname,
                CnameS = c.CnameS,
                Clogo = c.Clogo,
                Broadcast = c.Broadcast,
                Premiere = c.Premiere,
                Replay = c.Replay,
                Onoff = c.Onoff,
                Corder = c.Corder,
            })
            .ToPagedResponseAsync(query.Page, query.PageSize, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<ChannelListItem?> GetChannelAsync(
        uint chanId,
        CancellationToken cancellationToken = default)
    {
        return await _db.HprogChans
            .AsNoTracking()
            .Where(c => c.ChanId == chanId)
            .Select(c => new ChannelListItem
            {
                ChanId = c.ChanId,
                Cname = c.Cname,
                CnameS = c.CnameS,
                Clogo = c.Clogo,
                Broadcast = c.Broadcast,
                Premiere = c.Premiere,
                Replay = c.Replay,
                Onoff = c.Onoff,
                Corder = c.Corder,
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<OperationResult<uint>> CreateChannelAsync(
        CreateChannelRequest request,
        CancellationToken cancellationToken = default)
    {
        var entity = new infrastructure.Dto.Xoops.HprogChan
        {
            Cname = request.Cname,
            CnameS = request.CnameS,
            Clogo = request.Clogo ?? string.Empty,
            Broadcast = request.Broadcast,
            Premiere = request.Premiere,
            Replay = request.Replay,
            Onoff = request.Onoff,
            Corder = request.Corder,
        };

        _db.HprogChans.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        return OperationResult<uint>.Created(entity.ChanId, "頻道新增成功");
    }

    /// <inheritdoc />
    public async Task<OperationResult> UpdateChannelAsync(
        uint chanId, UpdateChannelRequest request,
        CancellationToken cancellationToken = default)
    {
        var entity = await _db.HprogChans
            .FirstOrDefaultAsync(c => c.ChanId == chanId, cancellationToken);

        if (entity is null)
            return OperationResult.NotFound($"找不到頻道 chan_id={chanId}");

        entity.Cname = request.Cname;
        entity.CnameS = request.CnameS;
        if (request.Clogo is not null)
            entity.Clogo = request.Clogo;
        entity.Broadcast = request.Broadcast;
        entity.Premiere = request.Premiere;
        entity.Replay = request.Replay;
        entity.Onoff = request.Onoff;
        entity.Corder = request.Corder;

        await _db.SaveChangesAsync(cancellationToken);
        return OperationResult.Ok("頻道更新成功");
    }
}

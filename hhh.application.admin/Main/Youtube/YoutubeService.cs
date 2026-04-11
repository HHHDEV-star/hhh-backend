using hhh.api.contracts.admin.Main.Youtube;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using hhh.infrastructure.Logging;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Main.Youtube;

public class YoutubeService : IYoutubeService
{
    private const string PageName = "Youtube";

    // 舊 PHP 寫死的兩個頻道 ID(get_youtube_list 的 WHERE 條件)
    private static readonly string[] AllowedChannelIds =
    {
        "UC7FerAswXewzZ1S7_Ev7ePA", // 幸福空間
        "UCPMVWQcRW2_6XsOXrBJgcTw"  // (舊頻道)
    };

    private readonly XoopsContext _db;
    private readonly IOperationLogWriter _logWriter;

    public YoutubeService(XoopsContext db, IOperationLogWriter logWriter)
    {
        _db = db;
        _logWriter = logWriter;
    }

    public async Task<List<YoutubeListItem>> GetListAsync(CancellationToken cancellationToken = default)
    {
        // 對應舊 PHP youtube_model::get_youtube_list():
        //   WHERE channel_id IN (...) AND is_del = 0
        //   ORDER BY yid DESC
        return await _db.YoutubeLists
            .AsNoTracking()
            .Where(y => AllowedChannelIds.Contains(y.ChannelId) && !y.IsDel)
            .OrderByDescending(y => y.Yid)
            .Select(y => new YoutubeListItem
            {
                Yid = y.Yid,
                HdesignerId = y.HdesignerId,
                HbrandId = y.HbrandId,
                BuilderId = y.BuilderId,
                ChannelId = y.ChannelId,
                Title = y.Title,
                Description = y.Description,
                YoutubeImg = y.YoutubeImg,
                YoutubeVideoId = y.YoutubeVideoId,
                PublishedTime = y.PublishedTime,
                CreateTime = y.CreateTime,
                UpdateTime = y.UpdateTime,
                Onoff = y.Onoff == "Y",
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<OperationResult<uint>> CreateAsync(
        CreateYoutubeRequest request,
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        var entity = new YoutubeList
        {
            HdesignerId = request.HdesignerId,
            HbrandId = request.HbrandId,
            BuilderId = request.BuilderId,
            ChannelId = request.ChannelId,
            Title = request.Title,
            Description = request.Description,
            YoutubeImg = request.YoutubeImg ?? string.Empty,
            YoutubeVideoId = request.YoutubeVideoId ?? string.Empty,
            PublishedTime = request.PublishedTime ?? now,
            PageToken = string.Empty,
            CreateTime = now,
            UpdateTime = now,
            Onoff = "Y",
            IsDel = false,
        };

        _db.YoutubeLists.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Create,
            $"新增 Youtube yid={entity.Yid} 標題={request.Title}",
            cancellationToken: cancellationToken);

        return OperationResult<uint>.Created(entity.Yid);
    }

    public async Task<OperationResult<uint>> UpdateAsync(
        uint yid,
        UpdateYoutubeRequest request,
        CancellationToken cancellationToken = default)
    {
        var entity = await _db.YoutubeLists.FirstOrDefaultAsync(y => y.Yid == yid, cancellationToken);
        if (entity is null)
            return OperationResult<uint>.NotFound("找不到 Youtube 影片");

        entity.Onoff = request.Onoff ? "Y" : "N";
        entity.HdesignerId = request.HdesignerId;
        entity.HbrandId = request.HbrandId;
        entity.BuilderId = request.BuilderId;

        if (request.ChannelId is not null) entity.ChannelId = request.ChannelId;
        if (request.Title is not null) entity.Title = request.Title;
        if (request.Description is not null) entity.Description = request.Description;

        entity.UpdateTime = DateTime.UtcNow;

        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Update,
            $"修改 Youtube yid={yid} onoff={entity.Onoff}",
            cancellationToken: cancellationToken);

        return OperationResult<uint>.Ok(yid, "修改成功");
    }

    public async Task<OperationResult> DeleteAsync(
        uint yid,
        CancellationToken cancellationToken = default)
    {
        // 對應舊 PHP youtube_model::delete():hard DELETE,非 soft delete
        var entity = await _db.YoutubeLists.FirstOrDefaultAsync(y => y.Yid == yid, cancellationToken);
        if (entity is null)
            return OperationResult.NotFound("找不到 Youtube 影片");

        var oldTitle = entity.Title;

        _db.YoutubeLists.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Delete,
            $"刪除 Youtube yid={yid} 標題={oldTitle}",
            cancellationToken: cancellationToken);

        return OperationResult.Ok("刪除成功");
    }
}

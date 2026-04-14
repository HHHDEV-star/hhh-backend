using System.Text.Json;
using hhh.api.contracts.admin.Planning;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace hhh.application.admin.Planning;

public class YoutubeManagementService : IYoutubeManagementService
{
    // 幸福空間 YouTube 頻道 ID
    private const string DefaultChannelId = "UC7FerAswXewzZ1S7_Ev7ePA";

    private readonly XoopsContext _db;
    private readonly HttpClient _httpClient;
    private readonly string? _apiKey;
    private readonly ILogger<YoutubeManagementService> _logger;

    public YoutubeManagementService(
        XoopsContext db,
        HttpClient httpClient,
        IConfiguration configuration,
        ILogger<YoutubeManagementService> logger)
    {
        _db = db;
        _httpClient = httpClient;
        _apiKey = configuration["Youtube:ApiKey"];
        _logger = logger;
    }

    // ── 群組管理 ──

    /// <inheritdoc />
    public async Task<List<YoutubeGroupListItem>> GetGroupListAsync(
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: Program_model::read_group()
        // SELECT * FROM youtube_group ORDER BY gid DESC
        return await _db.YoutubeGroups
            .AsNoTracking()
            .OrderByDescending(g => g.Gid)
            .Select(g => new YoutubeGroupListItem
            {
                Gid = g.Gid,
                Name = g.Name,
                Onoff = g.Onoff,
                CreateTime = g.CreateTime,
            })
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<List<YoutubeGroupDropdownItem>> GetGroupDropdownAsync(
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: Program_model::read_group_dropdownlist()
        // SELECT * FROM youtube_group WHERE onoff='Y' ORDER BY gid DESC
        return await _db.YoutubeGroups
            .AsNoTracking()
            .Where(g => g.Onoff == "Y")
            .OrderByDescending(g => g.Gid)
            .Select(g => new YoutubeGroupDropdownItem
            {
                Gid = g.Gid,
                Name = g.Name,
            })
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<OperationResult<uint>> CreateGroupAsync(
        CreateYoutubeGroupRequest request,
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: Program_model::insert_group() + get_group_by_name() 檢查重複
        var exists = await _db.YoutubeGroups
            .AnyAsync(g => g.Name == request.Name, cancellationToken);

        if (exists)
            return OperationResult<uint>.Conflict($"群組名稱「{request.Name}」已存在");

        var entity = new infrastructure.Dto.Xoops.YoutubeGroup
        {
            Name = request.Name,
            Onoff = "Y",
            CreateTime = DateTime.UtcNow,
        };

        _db.YoutubeGroups.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        return OperationResult<uint>.Created(entity.Gid, "群組新增成功");
    }

    /// <inheritdoc />
    public async Task<OperationResult> UpdateGroupAsync(
        uint gid, UpdateYoutubeGroupRequest request,
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: Program_model::update_group()
        // UPDATE youtube_group SET name=?, onoff=? WHERE gid=?
        var entity = await _db.YoutubeGroups
            .FirstOrDefaultAsync(g => g.Gid == gid, cancellationToken);

        if (entity is null)
            return OperationResult.NotFound($"找不到群組 gid={gid}");

        // 檢查名稱是否與其他群組重複
        if (entity.Name != request.Name)
        {
            var nameExists = await _db.YoutubeGroups
                .AnyAsync(g => g.Name == request.Name && g.Gid != gid, cancellationToken);

            if (nameExists)
                return OperationResult.Conflict($"群組名稱「{request.Name}」已存在");
        }

        entity.Name = request.Name;
        entity.Onoff = request.Onoff;
        await _db.SaveChangesAsync(cancellationToken);

        return OperationResult.Ok("群組更新成功");
    }

    // ── 群組明細管理 ──

    /// <inheritdoc />
    public async Task<List<YoutubeGroupDetailListItem>> GetGroupDetailListAsync(
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: Program_model::get_group_detail()
        // JOIN youtube_group_detail + youtube_list + youtube_group
        // ORDER BY youtube_group.gid DESC, youtube_group_detail.sort ASC
        return await (
            from d in _db.YoutubeGroupDetails.AsNoTracking()
            join y in _db.YoutubeLists.AsNoTracking() on d.Yid equals y.Yid
            join g in _db.YoutubeGroups.AsNoTracking() on d.Gid equals g.Gid
            orderby g.Gid descending, d.Sort ascending
            select new YoutubeGroupDetailListItem
            {
                Id = d.Id,
                Gid = d.Gid,
                Name = g.Name,
                Yid = d.Yid,
                Title = y.Title,
                Sort = d.Sort,
                Onoff = d.Onoff,
                CreateTime = d.CreateTime,
            })
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<OperationResult> UpdateGroupDetailAsync(
        uint id, UpdateYoutubeGroupDetailRequest request,
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: Program_model::update_group_detail()
        // UPDATE youtube_group_detail SET sort=?, onoff=? WHERE id=?
        var affected = await _db.YoutubeGroupDetails
            .Where(d => d.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(d => d.Sort, request.Sort)
                .SetProperty(d => d.Onoff, request.Onoff),
                cancellationToken);

        return affected == 0
            ? OperationResult.NotFound($"找不到群組明細 id={id}")
            : OperationResult.Ok("群組明細更新成功");
    }

    /// <inheritdoc />
    public async Task<OperationResult> DeleteGroupDetailAsync(
        uint id,
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: Program_model::delete_group_detail()
        // DELETE FROM youtube_group_detail WHERE id=?
        var affected = await _db.YoutubeGroupDetails
            .Where(d => d.Id == id)
            .ExecuteDeleteAsync(cancellationToken);

        return affected == 0
            ? OperationResult.NotFound($"找不到群組明細 id={id}")
            : OperationResult.Ok("群組明細刪除成功");
    }

    // ── YouTube 同步 / 匯入 ──

    /// <inheritdoc />
    public async Task<OperationResult> SyncChannelAsync(
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(_apiKey))
            return OperationResult.BadRequest("未設定 Youtube:ApiKey，無法同步");

        // 對應 PHP: youtube_model::sync() — 抓取頻道影片並 upsert
        var url = $"https://www.googleapis.com/youtube/v3/search"
                + $"?key={_apiKey}"
                + $"&channelId={DefaultChannelId}"
                + $"&part=snippet,id"
                + $"&order=date"
                + $"&maxResults=50"
                + $"&type=video";

        var json = await _httpClient.GetStringAsync(url, cancellationToken);
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        if (!root.TryGetProperty("items", out var items))
            return OperationResult.BadRequest("YouTube API 回傳格式異常");

        var now = DateTime.UtcNow;
        foreach (var item in items.EnumerateArray())
        {
            var videoId = item.GetProperty("id").GetProperty("videoId").GetString();
            if (string.IsNullOrEmpty(videoId)) continue;

            var snippet = item.GetProperty("snippet");
            var channelId = snippet.GetProperty("channelId").GetString() ?? DefaultChannelId;
            var title = snippet.GetProperty("title").GetString() ?? string.Empty;
            var description = snippet.GetProperty("description").GetString() ?? string.Empty;
            var thumbnailUrl = snippet.GetProperty("thumbnails").GetProperty("high").GetProperty("url").GetString() ?? string.Empty;
            var publishedAt = snippet.GetProperty("publishedAt").GetString();
            var publishedTime = DateTime.TryParse(publishedAt, out var pt) ? pt : now;

            await UpsertVideoAsync(channelId, title, description, thumbnailUrl, videoId, publishedTime, now, cancellationToken);
        }

        // 對應 PHP: youtube_model::parse_customer_id() — 從標題解析設計師/廠商 ID
        await ParseCustomerIdAsync(cancellationToken);

        return OperationResult.Ok("YouTube 同步完成");
    }

    /// <inheritdoc />
    public async Task<OperationResult> ImportByVideoIdAsync(
        string youtubeVideoId,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(_apiKey))
            return OperationResult.BadRequest("未設定 Youtube:ApiKey，無法搜尋");

        if (string.IsNullOrWhiteSpace(youtubeVideoId))
            return OperationResult.BadRequest("請提供 YouTube 影片 ID");

        // 對應 PHP: youtube_model::get_video_list_by_id()
        var url = $"https://www.googleapis.com/youtube/v3/videos"
                + $"?key={_apiKey}"
                + $"&id={youtubeVideoId}"
                + $"&part=snippet,contentDetails,statistics";

        var json = await _httpClient.GetStringAsync(url, cancellationToken);
        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement;

        if (!root.TryGetProperty("items", out var items) || items.GetArrayLength() == 0)
            return OperationResult.BadRequest("找不到此 YouTube 影片");

        var firstItem = items[0];
        var snippet = firstItem.GetProperty("snippet");
        var channelId = snippet.GetProperty("channelId").GetString() ?? DefaultChannelId;
        var title = snippet.GetProperty("title").GetString() ?? string.Empty;
        var description = snippet.GetProperty("description").GetString() ?? string.Empty;

        // thumbnails 可能有 standard，也可能只有 high
        var thumbnailUrl = string.Empty;
        if (snippet.TryGetProperty("thumbnails", out var thumbnails))
        {
            if (thumbnails.TryGetProperty("standard", out var standard))
                thumbnailUrl = standard.GetProperty("url").GetString() ?? string.Empty;
            else if (thumbnails.TryGetProperty("high", out var high))
                thumbnailUrl = high.GetProperty("url").GetString() ?? string.Empty;
        }

        var videoId = firstItem.GetProperty("id").GetString() ?? youtubeVideoId;
        var publishedAt = snippet.GetProperty("publishedAt").GetString();
        var publishedTime = DateTime.TryParse(publishedAt, out var pt) ? pt : DateTime.UtcNow;

        var now = DateTime.UtcNow;
        await UpsertVideoAsync(channelId, title, description, thumbnailUrl, videoId, publishedTime, now, cancellationToken);

        return OperationResult.Ok("YouTube 影片匯入成功");
    }

    /// <summary>
    /// Upsert 影片到 youtube_list（對應 PHP youtube_model::_insert）
    /// 先嘗試 UPDATE，affected_rows == 0 則 INSERT
    /// </summary>
    private async Task UpsertVideoAsync(
        string channelId, string title, string description,
        string youtubeImg, string youtubeVideoId,
        DateTime publishedTime, DateTime now,
        CancellationToken cancellationToken)
    {
        // 嘗試更新已存在的紀錄
        var affected = await _db.YoutubeLists
            .Where(y => y.YoutubeVideoId == youtubeVideoId)
            .ExecuteUpdateAsync(s => s
                .SetProperty(y => y.ChannelId, channelId)
                .SetProperty(y => y.Title, title)
                .SetProperty(y => y.Description, description)
                .SetProperty(y => y.YoutubeImg, youtubeImg)
                .SetProperty(y => y.PublishedTime, publishedTime)
                .SetProperty(y => y.PageToken, string.Empty)
                .SetProperty(y => y.UpdateTime, now),
                cancellationToken);

        if (affected == 0)
        {
            // 不存在則新增
            _db.YoutubeLists.Add(new infrastructure.Dto.Xoops.YoutubeList
            {
                ChannelId = channelId,
                Title = title,
                Description = description,
                YoutubeImg = youtubeImg,
                YoutubeVideoId = youtubeVideoId,
                PublishedTime = publishedTime,
                PageToken = string.Empty,
                CreateTime = now,
                UpdateTime = now,
                Onoff = "Y",
                IsDel = false,
            });
            await _db.SaveChangesAsync(cancellationToken);
        }
    }

    /// <summary>
    /// 解析標題中的設計師/廠商名稱並更新 ID
    /// （對應 PHP youtube_model::parse_customer_id）
    /// 格式: ...【公司名-設計師名】... → 設計師
    /// 格式: ...【品牌名】... → 廠商
    /// </summary>
    private async Task ParseCustomerIdAsync(CancellationToken cancellationToken)
    {
        // 找出尚未配對的影片（hdesigner_id=0 且 hbrand_id=0）
        var unmatched = await _db.YoutubeLists
            .Where(y => y.HdesignerId == 0 && y.HbrandId == 0)
            .Select(y => new { y.Yid, y.Title })
            .ToListAsync(cancellationToken);

        foreach (var video in unmatched)
        {
            var idx = video.Title.LastIndexOf('【');
            if (idx < 0) continue;

            var segment = video.Title[idx..];
            var content = segment.Replace("【", "");
            var endIdx = content.IndexOf('】');
            if (endIdx >= 0) content = content[..endIdx];

            if (content.Contains('-'))
            {
                // 【公司名-設計師名】→ 對應 _hdesigner
                var parts = content.Split('-');
                if (parts.Length < 2) continue;
                var designerName = parts[1].Trim();

                var designerId = await _db.Hdesigners
                    .Where(d => d.Name == designerName)
                    .Select(d => (uint?)d.HdesignerId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (designerId.HasValue)
                {
                    await _db.YoutubeLists
                        .Where(y => y.Yid == video.Yid)
                        .ExecuteUpdateAsync(s => s
                            .SetProperty(y => y.HdesignerId, designerId.Value)
                            .SetProperty(y => y.UpdateTime, DateTime.UtcNow),
                            cancellationToken);
                }
            }
            else
            {
                // 【品牌名】→ 對應 _hbrand
                var brandName = content.Trim();

                var brandId = await _db.Hbrands
                    .Where(b => b.Title == brandName)
                    .Select(b => (uint?)b.HbrandId)
                    .FirstOrDefaultAsync(cancellationToken);

                if (brandId.HasValue)
                {
                    await _db.YoutubeLists
                        .Where(y => y.Yid == video.Yid)
                        .ExecuteUpdateAsync(s => s
                            .SetProperty(y => y.HbrandId, brandId.Value)
                            .SetProperty(y => y.UpdateTime, DateTime.UtcNow),
                            cancellationToken);
                }
            }
        }
    }
}

using hhh.api.contracts.admin.Social.Decorations;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using hhh.infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Social.Decorations;

public class DecorationService : IDecorationService
{
    private static readonly HashSet<string> AllowedTypes = new() { "預售屋", "新屋", "中古屋" };
    private static readonly HashSet<string> AllowedPins = new()
    {
        "10 坪以下", "11~20 坪", "21~30 坪", "31~40 坪", "41~50 坪", "51 坪以上"
    };

    private readonly XoopsContext _db;

    public DecorationService(XoopsContext db) => _db = db;

    public async Task<PagedResponse<DecorationListItem>> GetListAsync(DecorationListQuery query, CancellationToken cancellationToken = default)
    {
        var q = _db.Decorations.AsNoTracking().AsQueryable();

        // 關鍵字搜尋：姓名 / Email / 電話 / 地區
        if (!string.IsNullOrWhiteSpace(query.Keyword))
        {
            var like = $"%{query.Keyword.Trim()}%";
            q = q.Where(d =>
                EF.Functions.Like(d.Name, like) ||
                EF.Functions.Like(d.Email, like) ||
                EF.Functions.Like(d.Phone, like) ||
                EF.Functions.Like(d.Area, like));
        }

        // 房屋類型篩選
        if (!string.IsNullOrWhiteSpace(query.Type))
        {
            q = q.Where(d => d.Type == query.Type);
        }

        // 日期區間篩選（建立時間）
        if (query.DateFrom is { } dateFrom)
        {
            var from = dateFrom.ToDateTime(TimeOnly.MinValue);
            q = q.Where(d => d.CreateTime >= from);
        }
        if (query.DateTo is { } dateTo)
        {
            var to = dateTo.ToDateTime(TimeOnly.MaxValue);
            q = q.Where(d => d.CreateTime <= to);
        }

        return await q
            .OrderByDescending(d => d.Id)
            .Select(d => new DecorationListItem
            {
                Id = d.Id,
                Email = d.Email,
                Name = d.Name,
                Phone = d.Phone,
                Area = d.Area,
                Type = d.Type,
                Pin = d.Pin,
                CreateTime = d.CreateTime,
            })
            .ToPagedResponseAsync(query.Page, query.PageSize, cancellationToken);
    }

    public async Task<OperationResult<uint>> CreateAsync(
        CreateDecorationRequest request, CancellationToken cancellationToken = default)
    {
        // 對應舊 PHP 的 in_array 驗證
        if (!AllowedTypes.Contains(request.Type))
            return OperationResult<uint>.BadRequest("type 資料不符(僅接受 預售屋、新屋、中古屋)");

        if (!AllowedPins.Contains(request.Pin))
            return OperationResult<uint>.BadRequest("pin 資料不符(僅接受 10 坪以下、11~20 坪、21~30 坪、31~40 坪、41~50 坪、51 坪以上)");

        var entity = new Decoration
        {
            Email = request.Email,
            Name = request.Name,
            Phone = request.Phone,
            Area = request.Area,
            Type = request.Type,
            Pin = request.Pin,
            CreateTime = DateTime.UtcNow,
        };

        _db.Decorations.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        return OperationResult<uint>.Created(entity.Id);
    }
}

using hhh.api.contracts.Common;
using hhh.api.contracts.admin.Hprizes;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using hhh.infrastructure.Extensions;
using hhh.infrastructure.Logging;
using hhh.infrastructure.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace hhh.application.admin.Hprizes;

public class HprizeService : IHprizeService
{
    private const string PageName = "獎品";

    private readonly XoopsContext _db;
    private readonly IOperationLogWriter _logWriter;
    private readonly string _publicUrlPrefix;

    public HprizeService(
        XoopsContext db,
        IOperationLogWriter logWriter,
        IOptions<StorageOptions> storageOptions)
    {
        _db = db;
        _logWriter = logWriter;
        _publicUrlPrefix = (storageOptions.Value.PublicUrlPrefix ?? "/uploads").TrimEnd('/');
    }

    public async Task<PagedResponse<HprizeListItem>> GetListAsync(
        HprizeListRequest request,
        CancellationToken cancellationToken = default)
    {
        // 對應舊 PHP:SELECT * FROM _hprize WHERE 1=1 ORDER BY ...
        var query = _db.Hprizes.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.Q))
        {
            var q = request.Q.Trim();
            var like = $"%{q}%";
            query = query.Where(h =>
                EF.Functions.Like(h.HprizeId.ToString(), like) ||
                EF.Functions.Like(h.Title, like) ||
                EF.Functions.Like(h.Desc, like));
        }

        var ordered = ApplyOrdering(query, request.Sort, request.By);

        return await ordered
            .Select(h => new HprizeListItem
            {
                Id = h.HprizeId,
                Title = h.Title,
                Logo = h.Logo,
                Desc = h.Desc,
                CreatTime = h.CreatTime,
            })
            .ToPagedResponseAsync(request.Page, request.PageSize, cancellationToken);
    }

    public async Task<HprizeDetailResponse?> GetByIdAsync(
        uint id,
        CancellationToken cancellationToken = default)
    {
        var h = await _db.Hprizes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.HprizeId == id, cancellationToken);

        if (h is null)
            return null;

        return new HprizeDetailResponse
        {
            Id = h.HprizeId,
            Title = h.Title,
            Logo = h.Logo,
            Desc = h.Desc,
            CreatTime = h.CreatTime,
        };
    }

    public async Task<HprizeMutationResult> CreateAsync(
        CreateHprizeRequest request,
        ImageUploadResult logo,
        CancellationToken cancellationToken = default)
    {
        var entity = new Hprize
        {
            Title = request.Title,
            Desc = request.Desc,
            // 儲存 PublicUrl（新資料為 /uploads/hprize/xxx.png，舊資料可能是 https://... 全 URL）
            Logo = logo.PublicUrl,
            CreatTime = DateTime.UtcNow,
        };

        _db.Hprizes.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Create,
            $"新增獎品 id={entity.HprizeId} 標題={request.Title}",
            cancellationToken: cancellationToken);

        return HprizeMutationResult.Created(entity.HprizeId);
    }

    public async Task<HprizeMutationResult> UpdateAsync(
        uint id,
        UpdateHprizeRequest request,
        ImageUploadResult? newLogo,
        CancellationToken cancellationToken = default)
    {
        var entity = await _db.Hprizes
            .FirstOrDefaultAsync(x => x.HprizeId == id, cancellationToken);

        if (entity is null)
            return HprizeMutationResult.Fail(404, "找不到獎品");

        entity.Title = request.Title;
        entity.Desc = request.Desc;

        string? oldRelativePath = null;
        if (newLogo is not null)
        {
            // 舊 logo 如果是我們管的本機檔案，回傳相對路徑讓 controller 刪檔
            oldRelativePath = TryGetManagedRelativePath(entity.Logo);
            entity.Logo = newLogo.PublicUrl;
        }

        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Update,
            $"修改獎品 id={id} 標題={request.Title}" + (newLogo is not null ? " (含新 logo)" : ""),
            cancellationToken: cancellationToken);

        return HprizeMutationResult.Ok(id, oldLogoRelativePath: oldRelativePath);
    }

    public async Task<HprizeMutationResult> DeleteAsync(
        uint id,
        CancellationToken cancellationToken = default)
    {
        var entity = await _db.Hprizes
            .FirstOrDefaultAsync(x => x.HprizeId == id, cancellationToken);

        if (entity is null)
            return HprizeMutationResult.Fail(404, "找不到獎品");

        var oldRelativePath = TryGetManagedRelativePath(entity.Logo);
        var oldTitle = entity.Title;

        _db.Hprizes.Remove(entity);
        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Delete,
            $"刪除獎品 id={id} 標題={oldTitle}",
            cancellationToken: cancellationToken);

        return HprizeMutationResult.Deleted(oldLogoRelativePath: oldRelativePath);
    }

    // ---------------------------------------------------------------------
    // Helpers
    // ---------------------------------------------------------------------

    /// <summary>
    /// 若 logo 欄位是本機 IImageUploadService 產生的 URL（以 PublicUrlPrefix 開頭），
    /// 回傳對應的相對路徑給 Delete 用；否則回 null（例如舊資料的 https://... 全 URL
    /// 或空字串，我們不負責清理）。
    /// </summary>
    private string? TryGetManagedRelativePath(string? logoUrl)
    {
        if (string.IsNullOrWhiteSpace(logoUrl))
            return null;

        var prefix = _publicUrlPrefix + "/";
        if (!logoUrl.StartsWith(prefix, StringComparison.Ordinal))
            return null;

        return logoUrl.Substring(prefix.Length);
    }

    /// <summary>
    /// 排序白名單：未列出的欄位會 fallback 到 HprizeId。
    /// </summary>
    private static IOrderedQueryable<Hprize> ApplyOrdering(
        IQueryable<Hprize> query,
        string? sort,
        string? by)
    {
        var isAsc = string.Equals(by, "ASC", StringComparison.OrdinalIgnoreCase);
        var key = sort?.ToLowerInvariant();

        return key switch
        {
            "title" => isAsc ? query.OrderBy(h => h.Title) : query.OrderByDescending(h => h.Title),
            "creattime" => isAsc ? query.OrderBy(h => h.CreatTime) : query.OrderByDescending(h => h.CreatTime),
            _ => isAsc ? query.OrderBy(h => h.HprizeId) : query.OrderByDescending(h => h.HprizeId),
        };
    }
}

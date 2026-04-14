using hhh.api.contracts.admin.Website.SiteSetup;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Website.SiteSetup;

public class SiteSetupService : ISiteSetupService
{
    private readonly XoopsContext _db;

    public SiteSetupService(XoopsContext db)
    {
        _db = db;
    }

    /// <inheritdoc />
    public async Task<SiteSetupResponse?> GetAsync(CancellationToken cancellationToken = default)
    {
        // 對應 PHP: homepage_model::get_site_setup()
        // SELECT * FROM site_setup WHERE id = 1
        var entity = await _db.SiteSetups
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        if (entity is null)
            return null;

        return new SiteSetupResponse
        {
            YoutubeId = entity.YoutubeId,
            YoutubeTitle = entity.YoutubeTitle,
            AllSearchTag = entity.AllSearchTag,
            ForumFilter = entity.ForumFilter,
        };
    }

    /// <inheritdoc />
    public async Task<OperationResult> UpdateAsync(
        UpdateSiteSetupRequest request,
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: homepage_model::set_site_setup($input)
        // UPDATE site_setup SET ... WHERE id = 1
        var entity = await _db.SiteSetups
            .FirstOrDefaultAsync(cancellationToken);

        if (entity is null)
            return OperationResult.NotFound("找不到全域設定");

        entity.YoutubeId = request.YoutubeId;
        entity.YoutubeTitle = request.YoutubeTitle;
        entity.AllSearchTag = request.AllSearchTag;
        entity.ForumFilter = request.ForumFilter ?? string.Empty;

        await _db.SaveChangesAsync(cancellationToken);

        return OperationResult.Ok("全域設定修改成功");
    }
}

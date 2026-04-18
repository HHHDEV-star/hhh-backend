using hhh.api.contracts.admin.Social.Briefs;
using hhh.api.contracts.Common;

namespace hhh.application.admin.Social.Briefs;

/// <summary>屋主上傳名片領好康 服務</summary>
public interface IBriefService
{
    Task<PagedResponse<BriefListItem>> GetListAsync(BriefListQuery query, CancellationToken cancellationToken = default);
}

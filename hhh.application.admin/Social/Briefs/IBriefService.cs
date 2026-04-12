using hhh.api.contracts.admin.Social.Briefs;

namespace hhh.application.admin.Social.Briefs;

/// <summary>屋主上傳名片領好康 服務</summary>
public interface IBriefService
{
    Task<List<BriefListItem>> GetListAsync(CancellationToken cancellationToken = default);
}

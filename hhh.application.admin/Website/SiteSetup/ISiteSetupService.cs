using hhh.api.contracts.admin.Website.SiteSetup;
using hhh.application.admin.Common;

namespace hhh.application.admin.Website.SiteSetup;

public interface ISiteSetupService
{
    /// <summary>取得全域設定(id=1 的單筆紀錄)</summary>
    Task<SiteSetupResponse?> GetAsync(CancellationToken cancellationToken = default);

    /// <summary>更新全域設定</summary>
    Task<OperationResult> UpdateAsync(UpdateSiteSetupRequest request, CancellationToken cancellationToken = default);
}

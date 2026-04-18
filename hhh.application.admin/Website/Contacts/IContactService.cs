using hhh.api.contracts.admin.Website.Contacts;
using hhh.api.contracts.Common;

namespace hhh.application.admin.Website.Contacts;

public interface IContactService
{
    /// <summary>取得聯絡我們列表</summary>
    Task<PagedResponse<ContactListItem>> GetListAsync(ContactListQuery query, CancellationToken cancellationToken = default);
}

using hhh.api.contracts.admin.Website.Contacts;
<<<<<<< HEAD
using hhh.api.contracts.Common;
=======
>>>>>>> origin/main

namespace hhh.application.admin.Website.Contacts;

public interface IContactService
{
    /// <summary>取得聯絡我們列表</summary>
<<<<<<< HEAD
    Task<PagedResponse<ContactListItem>> GetListAsync(ListQuery query, CancellationToken cancellationToken = default);
=======
    Task<List<ContactListItem>> GetListAsync(CancellationToken cancellationToken = default);
>>>>>>> origin/main
}

using hhh.api.contracts.admin.Website.Contacts;

namespace hhh.application.admin.Website.Contacts;

public interface IContactService
{
    /// <summary>取得聯絡我們列表</summary>
    Task<List<ContactListItem>> GetListAsync(CancellationToken cancellationToken = default);
}

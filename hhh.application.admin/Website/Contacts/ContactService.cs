using hhh.api.contracts.admin.Website.Contacts;
using hhh.api.contracts.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Website.Contacts;

public class ContactService : IContactService
{
    private readonly XoopsContext _db;

    public ContactService(XoopsContext db)
    {
        _db = db;
    }

    /// <inheritdoc />
    public async Task<PagedResponse<ContactListItem>> GetListAsync(
        ListQuery query, CancellationToken cancellationToken = default)
    {
        // 對應 PHP: contact_model::get()
        // SELECT * FROM contact WHERE phone LIKE '0%' ORDER BY id DESC
        return await _db.Contacts
            .AsNoTracking()
            .Where(c => EF.Functions.Like(c.Phone, "0%"))
            .OrderByDescending(c => c.Id)
            .Select(c => new ContactListItem
            {
                Id = c.Id,
                CreateTime = c.CreateTime,
                Name = c.Name,
                Company = c.Company,
                Phone = c.Phone,
                Email = c.Email,
                IsFb = c.IsFb,
                Subject = c.Subject,
                Content = c.Content,
                Ip = c.Ip,
                Send = c.Send,
                SendTime = c.SendTime,
            })
            .ToPagedResponseAsync(query.Page, query.PageSize, cancellationToken);
    }
}

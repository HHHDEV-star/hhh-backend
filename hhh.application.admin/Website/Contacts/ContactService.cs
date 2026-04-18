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
        ContactListQuery query, CancellationToken cancellationToken = default)
    {
        // 對應 PHP: contact_model::get()
        // SELECT * FROM contact WHERE phone LIKE '0%' ORDER BY id DESC
        var baseQuery = _db.Contacts
            .AsNoTracking()
            .Where(c => EF.Functions.Like(c.Phone, "0%"));

        // 關鍵字篩選：模糊比對姓名 / 公司 / 電話 / Email / 主旨 / 內容
        if (!string.IsNullOrWhiteSpace(query.Keyword))
        {
            var kw = $"%{query.Keyword.Trim()}%";
            baseQuery = baseQuery.Where(c =>
                EF.Functions.Like(c.Name, kw) ||
                EF.Functions.Like(c.Company, kw) ||
                EF.Functions.Like(c.Phone, kw) ||
                EF.Functions.Like(c.Email, kw) ||
                EF.Functions.Like(c.Subject, kw) ||
                EF.Functions.Like(c.Content, kw));
        }

        // 發送狀態篩選（Y=已寄出 / N=未寄出）
        if (!string.IsNullOrWhiteSpace(query.Send))
        {
            var send = query.Send.Trim().ToUpper();
            baseQuery = baseQuery.Where(c => c.Send == send);
        }

        // 建立時間區間篩選
        if (query.DateFrom.HasValue)
        {
            var from = query.DateFrom.Value.ToDateTime(TimeOnly.MinValue);
            baseQuery = baseQuery.Where(c => c.CreateTime >= from);
        }

        if (query.DateTo.HasValue)
        {
            var to = query.DateTo.Value.ToDateTime(TimeOnly.MaxValue);
            baseQuery = baseQuery.Where(c => c.CreateTime <= to);
        }

        return await baseQuery
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

using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using hhh.api.contracts.admin.Agents;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Agents;

public partial class AgentService : IAgentService
{
    private readonly XoopsContext _db;

    public AgentService(XoopsContext db)
    {
        _db = db;
    }

    /// <inheritdoc />
    public async Task<PagedResponse<AgentListItem>> GetListAsync(
        AgentQuery query,
        ListQuery listQuery,
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: Agent_model::lists()
        var q = _db.AgentForms
            .AsNoTracking()
            .Where(a => a.IsDel == 0);

        // 日期篩選
        if (query.StartDate.HasValue)
        {
            var startDateTime = query.StartDate.Value.ToDateTime(TimeOnly.MinValue);
            q = q.Where(a => a.DateAdded >= startDateTime);
        }

        if (query.EndDate.HasValue)
        {
            var endDateTime = query.EndDate.Value.ToDateTime(new TimeOnly(23, 59, 59));
            q = q.Where(a => a.DateAdded <= endDateTime);
        }

        // 關鍵字篩選
        var keyword = query.Keyword?.Trim();
        if (!string.IsNullOrEmpty(keyword))
        {
            if (keyword.StartsWith("09"))
            {
                // 手機號碼：格式化為 XXXX-XXX-XXX 後搜 cellphone
                if (keyword.Length == 10)
                    keyword = MobilePhoneRegex().Replace(keyword, "$1-$2-$3");

                q = q.Where(a => a.Cellphone != null && a.Cellphone.Contains(keyword));
            }
            else
            {
                // 市話格式化
                if (keyword.Length == 10)
                    keyword = LandlinePhone10Regex().Replace(keyword, "$1-$2-$3");
                else if (keyword.Length == 9)
                    keyword = LandlinePhone9Regex().Replace(keyword, "$1-$2-$3");

                // need_item 欄位以 JSON 陣列格式儲存，比對時需轉為 JSON 格式
                var jsonKeyword = JsonSerializer.Serialize(keyword.Split(','));

                var kw = keyword; // 避免 closure 捕獲可變變數
                q = q.Where(a =>
                    (a.Phone != null && a.Phone.Contains(kw)) ||
                    a.Fullname.Contains(kw) ||
                    a.ContentFor.Contains(kw) ||
                    a.NeedItem.Contains(jsonKeyword) ||
                    (a.LocationPingReal != null && a.LocationPingReal.Contains(kw)) ||
                    (a.Budget != null && a.Budget.Contains(kw)) ||
                    (a.Mbudget != null && a.Mbudget.Contains(kw)) ||
                    (a.HouseType != null && a.HouseType.Contains(kw)) ||
                    (a.County != null && a.County.Contains(kw)) ||
                    (a.District != null && a.District.Contains(kw)) ||
                    (a.AgentSource != null && a.AgentSource.Contains(kw)) ||
                    (a.AgentWhere != null && a.AgentWhere.Contains(kw)) ||
                    (a.DesignCompany != null && a.DesignCompany.Contains(kw)));
            }
        }

        // 先算總數再分頁（因需後處理 JSON 欄位，無法用 ToPagedResponseAsync）
        var ordered = q.OrderByDescending(a => a.AgentId);
        var total = await ordered.LongCountAsync(cancellationToken);
        var rows = await ordered
            .Skip((listQuery.Page - 1) * listQuery.PageSize)
            .Take(listQuery.PageSize)
            .Select(a => new AgentListItem
            {
                AgentId = a.AgentId,
                ContentFor = a.ContentFor,
                Fullname = a.Fullname,
                District = a.District,
                Phone = a.Phone,
                Cellphone = a.Cellphone,
                County = a.County,
                FollowTime = a.FollowTime,
                AgentNote = a.AgentNote,       // 先取原始值，後面再解碼
                AgentSource = a.AgentSource,
                AgentWhere = a.AgentWhere,     // 先取原始值
                DesignCompany = a.DesignCompany,
                NeedItem = a.NeedItem,         // 先取原始值
                HouseType = a.HouseType,
                LocationPingReal = a.LocationPingReal,
                Budget = a.Budget,
                Mbudget = a.Mbudget,
            })
            .ToListAsync(cancellationToken);

        // 後處理：解碼 JSON 欄位（對應 PHP controller 的 lists_get）
        foreach (var item in rows)
        {
            item.AgentNote = DecodeJsonString(item.AgentNote);
            item.AgentWhere = DecodeJsonArray(item.AgentWhere);
            item.NeedItem = DecodeJsonArray(item.NeedItem);
        }

        return new PagedResponse<AgentListItem>
        {
            Items = rows,
            Total = total,
            Page = listQuery.Page,
            PageSize = listQuery.PageSize,
        };
    }

    /// <inheritdoc />
    public async Task<OperationResult> DeleteAsync(
        uint agentId,
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: Agent_model::delete() — 軟刪除
        var affected = await _db.AgentForms
            .Where(a => a.AgentId == agentId && a.IsDel == 0)
            .ExecuteUpdateAsync(s => s
                .SetProperty(a => a.IsDel, (sbyte)1)
                .SetProperty(a => a.DateModified, DateTime.UtcNow),
                cancellationToken);

        return affected == 0
            ? OperationResult.NotFound($"找不到經紀人 agent_id={agentId}")
            : OperationResult.Ok("經紀人刪除成功");
    }

    /// <inheritdoc />
    public async Task<List<AgentFileListItem>> GetFilesAsync(
        uint agentId,
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: Agent_model::file_lists()
        return await _db.AgentFiles
            .AsNoTracking()
            .Where(f => f.AgentId == agentId && f.IsDel == 0)
            .Select(f => new AgentFileListItem
            {
                FileId = f.FileId,
                AgentId = f.AgentId,
                FileName = f.FileName,
                FileUrl = f.FileUrl,
                DateAdded = f.DateAdded,
            })
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<OperationResult> DeleteFileAsync(
        uint fileId,
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: Agent_model::file_delete() — 軟刪除
        var affected = await _db.AgentFiles
            .Where(f => f.FileId == fileId && f.IsDel == 0)
            .ExecuteUpdateAsync(s => s
                .SetProperty(f => f.IsDel, (sbyte)1),
                cancellationToken);

        return affected == 0
            ? OperationResult.NotFound($"找不到檔案 file_id={fileId}")
            : OperationResult.Ok("檔案刪除成功");
    }

    // ── 私有輔助方法 ──

    /// <summary>
    /// 解碼 JSON 編碼的字串欄位，並去除 HTML 標籤。
    /// 對應 PHP: strip_tags(html_entity_decode(htmlspecialchars_decode(json_decode($val))))
    /// </summary>
    private static string DecodeJsonString(string? json)
    {
        if (string.IsNullOrEmpty(json)) return string.Empty;
        try
        {
            var decoded = JsonSerializer.Deserialize<string>(json);
            if (decoded is null) return string.Empty;
            decoded = WebUtility.HtmlDecode(decoded);
            return HtmlTagRegex().Replace(decoded, string.Empty);
        }
        catch
        {
            return json;
        }
    }

    /// <summary>
    /// 解碼 JSON 陣列欄位為逗號分隔字串。
    /// 對應 PHP: implode(',', json_decode($val, true))
    /// </summary>
    private static string DecodeJsonArray(string? json)
    {
        if (string.IsNullOrEmpty(json)) return string.Empty;
        try
        {
            var arr = JsonSerializer.Deserialize<string[]>(json);
            return arr is not null ? string.Join(",", arr) : string.Empty;
        }
        catch
        {
            return json;
        }
    }

    // ── 正規表示式（source generated）──

    /// <summary>手機號碼格式化：0912345678 → 0912-345-678</summary>
    [GeneratedRegex(@"(\d{4})(\d{3})(\d{3})")]
    private static partial Regex MobilePhoneRegex();

    /// <summary>市話 10 碼格式化：0212345678 → 02-1234-5678</summary>
    [GeneratedRegex(@"(\d{2})(\d{4})(\d{4})")]
    private static partial Regex LandlinePhone10Regex();

    /// <summary>市話 9 碼格式化（可能含 -）：021234567 → 02-1234-567</summary>
    [GeneratedRegex(@"(\d{2})-?(\d{4})-?(\d{3})")]
    private static partial Regex LandlinePhone9Regex();

    /// <summary>HTML 標籤移除</summary>
    [GeneratedRegex(@"<[^>]+>")]
    private static partial Regex HtmlTagRegex();
}

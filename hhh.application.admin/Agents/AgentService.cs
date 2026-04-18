using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using hhh.api.contracts.admin.Agents;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
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
        AgentListQuery query,
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
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
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
            Page = query.Page,
            PageSize = query.PageSize,
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

    /// <inheritdoc />
    public async Task<OperationResult<AgentDetailResponse>> GetByIdAsync(
        uint agentId,
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: Agent_model::form_get()
        var entity = await _db.AgentForms
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.AgentId == agentId && a.IsDel == 0, cancellationToken);

        if (entity is null)
            return OperationResult<AgentDetailResponse>.NotFound($"找不到經紀人 agent_id={agentId}");

        var detail = new AgentDetailResponse
        {
            AgentId = entity.AgentId,
            ContentFor = entity.ContentFor,
            Fullname = entity.Fullname,
            Phone = entity.Phone,
            Cellphone = entity.Cellphone,
            // outside_phone 欄位未在 scaffolded entity 中，暫不映射
            Email = entity.Email,
            NeedItem = DeserializeJsonList(entity.NeedItem),
            DecorationTime = entity.DecorationTime,
            County = entity.County,
            District = entity.District,
            Address = entity.Address,
            HouseType = entity.HouseType,
            HouseTypeYear = entity.HouseTypeYear,
            HouseStatus = entity.HouseStatus,
            HouseStatusHigh = entity.HouseStatusHigh,
            HouseStatusFloor = entity.HouseStatusFloor,
            LocationPingPaper = entity.LocationPingPaper,
            LocationPingReal = entity.LocationPingReal,
            Plecement = new PlecementInfo
            {
                H = entity.PlecementH,
                C = entity.PlecementC,
                T = entity.PlecementT,
            },
            Higher = entity.Higher,
            Family = DeserializeJson<FamilyInfo>(entity.Family),
            NeedStyle = DeserializeJsonList(entity.NeedStyle),
            NeedStyleOther = entity.NeedStyleOther,
            NeedUpdateArray = DeserializeJson<NeedUpdateInfo>(entity.NeedUpdateArray),
            DesignerContent = entity.DesignerContent,
            ContentWay = entity.ContentWay,
            ContentTime = entity.ContentTime,
            AgentWhere = DeserializeJsonList(entity.AgentWhere),
            AgentWhereOther = entity.AgentWhereOther,
            AgentSource = entity.AgentSource,
            MarketRule = entity.MarketRule,
            MarketRule1 = entity.MarketRule1,
            Budget = entity.Budget,
            Mbudget = entity.Mbudget,
            AgentLoan = entity.AgentLoan,
            DesignCompany = entity.DesignCompany,
            FollowTime = entity.FollowTime,
            InterviewTime = entity.InterviewTime,
            CustomerNote = DecodeHtmlString(entity.CustomerNote),
            AgentNote = DecodeHtmlString(entity.AgentNote),
            DateAdded = entity.DateAdded,
            DateModified = entity.DateModified,
        };

        return OperationResult<AgentDetailResponse>.Ok(detail);
    }

    /// <inheritdoc />
    public async Task<OperationResult<uint>> CreateAsync(
        CreateAgentRequest request,
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: Agent_model::insert()
        var now = DateTime.UtcNow;
        var entity = new AgentForm
        {
            ContentFor = request.ContentFor,
            Fullname = request.Fullname,
            Phone = request.Phone,
            Cellphone = request.Cellphone,
            Email = request.Email,
            NeedItem = SerializeJson(request.NeedItem),
            DecorationTime = request.DecorationTime ?? DateOnly.MinValue,
            County = request.County,
            District = request.District,
            Address = request.Address,
            HouseType = request.HouseType,
            HouseTypeYear = request.HouseTypeYear,
            HouseStatus = request.HouseStatus,
            HouseStatusHigh = request.HouseStatusHigh,
            HouseStatusFloor = request.HouseStatusFloor,
            LocationPingPaper = request.LocationPingPaper,
            LocationPingReal = request.LocationPingReal,
            PlecementH = request.Plecement?.H,
            PlecementC = request.Plecement?.C,
            PlecementT = request.Plecement?.T,
            Higher = request.Higher,
            Family = SerializeJsonOrNull(request.Family),
            NeedStyle = SerializeJsonOrNull(request.NeedStyle),
            NeedStyleOther = request.NeedStyleOther,
            NeedUpdateArray = SerializeJsonOrNull(request.NeedUpdateArray),
            DesignerContent = request.DesignerContent,
            ContentWay = request.ContentWay,
            ContentTime = request.ContentTime,
            AgentWhere = SerializeJsonOrNull(request.AgentWhere),
            AgentWhereOther = request.AgentWhereOther,
            AgentSource = request.AgentSource,
            MarketRule = request.MarketRule,
            MarketRule1 = request.MarketRule1,
            Budget = request.Budget,
            Mbudget = request.Mbudget,
            AgentLoan = request.AgentLoan,
            DesignCompany = request.DesignCompany,
            FollowTime = request.FollowTime,
            InterviewTime = request.InterviewTime,
            CustomerNote = request.CustomerNote,
            AgentNote = request.AgentNote,
            IsDel = 0,
            DateAdded = now,
            DateModified = now,
        };

        _db.AgentForms.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        return OperationResult<uint>.Created(entity.AgentId, "經紀人新增成功");
    }

    /// <inheritdoc />
    public async Task<OperationResult> UpdateAsync(
        uint agentId,
        UpdateAgentRequest request,
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: Agent_model::update()
        var entity = await _db.AgentForms
            .FirstOrDefaultAsync(a => a.AgentId == agentId && a.IsDel == 0, cancellationToken);

        if (entity is null)
            return OperationResult.NotFound($"找不到經紀人 agent_id={agentId}");

        entity.ContentFor = request.ContentFor;
        entity.Fullname = request.Fullname;
        entity.Phone = request.Phone;
        entity.Cellphone = request.Cellphone;
        entity.Email = request.Email;
        entity.NeedItem = SerializeJson(request.NeedItem);
        entity.DecorationTime = request.DecorationTime;
        entity.County = request.County;
        entity.District = request.District;
        entity.Address = request.Address;
        entity.HouseType = request.HouseType;
        entity.HouseTypeYear = request.HouseTypeYear;
        entity.HouseStatus = request.HouseStatus;
        entity.HouseStatusHigh = request.HouseStatusHigh;
        entity.HouseStatusFloor = request.HouseStatusFloor;
        entity.LocationPingPaper = request.LocationPingPaper;
        entity.LocationPingReal = request.LocationPingReal;
        entity.PlecementH = request.Plecement?.H;
        entity.PlecementC = request.Plecement?.C;
        entity.PlecementT = request.Plecement?.T;
        entity.Higher = request.Higher;
        entity.Family = SerializeJsonOrNull(request.Family);
        entity.NeedStyle = SerializeJsonOrNull(request.NeedStyle);
        entity.NeedStyleOther = request.NeedStyleOther;
        entity.NeedUpdateArray = SerializeJsonOrNull(request.NeedUpdateArray);
        entity.DesignerContent = request.DesignerContent;
        entity.ContentWay = request.ContentWay;
        entity.ContentTime = request.ContentTime;
        entity.AgentWhere = SerializeJsonOrNull(request.AgentWhere);
        entity.AgentWhereOther = request.AgentWhereOther;
        entity.AgentSource = request.AgentSource;
        entity.MarketRule = request.MarketRule;
        entity.MarketRule1 = request.MarketRule1;
        entity.Budget = request.Budget;
        entity.Mbudget = request.Mbudget;
        entity.AgentLoan = request.AgentLoan;
        entity.DesignCompany = request.DesignCompany;
        entity.FollowTime = request.FollowTime;
        entity.InterviewTime = request.InterviewTime;
        entity.CustomerNote = request.CustomerNote;
        entity.AgentNote = request.AgentNote;
        entity.DateModified = DateTime.UtcNow;

        await _db.SaveChangesAsync(cancellationToken);

        return OperationResult.Ok("經紀人更新成功");
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

    /// <summary>
    /// 反序列化 JSON 陣列字串為 List&lt;string&gt;。
    /// 對應 PHP: json_decode($val, true)（用於 need_item, need_style, agent_where）
    /// </summary>
    private static List<string> DeserializeJsonList(string? json)
    {
        if (string.IsNullOrEmpty(json)) return [];
        try
        {
            return JsonSerializer.Deserialize<List<string>>(json) ?? [];
        }
        catch
        {
            return [];
        }
    }

    /// <summary>
    /// 反序列化 JSON 物件字串。
    /// 對應 PHP: json_decode($val, true)（用於 family, need_update_array）
    /// </summary>
    private static T? DeserializeJson<T>(string? json) where T : class
    {
        if (string.IsNullOrEmpty(json)) return null;
        try
        {
            return JsonSerializer.Deserialize<T>(json, JsonNamingOptions);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 解碼 HTML entity 並去除 HTML 標籤（用於 detail 回傳）。
    /// 對應 PHP: strip_tags(html_entity_decode($val))
    /// </summary>
    private static string? DecodeHtmlString(string? val)
    {
        if (string.IsNullOrEmpty(val)) return null;
        var decoded = WebUtility.HtmlDecode(val);
        return HtmlTagRegex().Replace(decoded, string.Empty);
    }

    /// <summary>將物件序列化為 JSON 字串，null 時回傳空 JSON 陣列 "[]"。</summary>
    private static string SerializeJson<T>(T value)
        => JsonSerializer.Serialize(value);

    /// <summary>將物件序列化為 JSON 字串，null 時回傳 null。</summary>
    private static string? SerializeJsonOrNull<T>(T? value)
        => value is null ? null : JsonSerializer.Serialize(value, JsonNamingOptions);

    /// <summary>PHP 端 JSON key 為 snake_case，但 DTO 為 PascalCase，需配合序列化選項。</summary>
    private static readonly JsonSerializerOptions JsonNamingOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        PropertyNameCaseInsensitive = true,
    };
}

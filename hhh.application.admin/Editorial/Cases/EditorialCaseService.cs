using hhh.api.contracts.admin.Editorial.Cases;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using hhh.infrastructure.Logging;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Editorial.Cases;

public class EditorialCaseService : IEditorialCaseService
{
    private const string PageName = "編輯部個案";

    private readonly XoopsContext _db;
    private readonly IOperationLogWriter _logWriter;

    public EditorialCaseService(XoopsContext db, IOperationLogWriter logWriter)
    {
        _db = db;
        _logWriter = logWriter;
    }

    public async Task<List<EditorialCaseListItem>> GetListAsync(CancellationToken cancellationToken = default)
    {
        // 對應舊 PHP case_model::get_case_lists():
        //   SELECT c.hcase_id, c.caption, c.cover, c.onoff, c.sdate, c.creat_time, c.viewed,
        //          c.vr360_id, c.istaging, c.auto_count_fee, c.hdesigner_id, c.style, c.condition,
        //          c.location, c.type, c.update_time,
        //          (SELECT title FROM _hdesigner d WHERE c.hdesigner_id = d.hdesigner_id) AS title
        //   FROM _hcase c
        //   ORDER BY c.sdate DESC, c.hcase_id DESC
        //
        // 沒有 paging、沒有 filter,全量回給前端 Kendo Grid 自行分頁。
        return await (
            from c in _db.Hcases.AsNoTracking()
            orderby c.Sdate descending, c.HcaseId descending
            select new EditorialCaseListItem
            {
                HcaseId = c.HcaseId,
                Caption = c.Caption,
                Cover = c.Cover,
                Onoff = c.Onoff == 1,
                Sdate = c.Sdate,
                CreatTime = c.CreatTime,
                Viewed = c.Viewed,
                Vr360Id = c.Vr360Id,
                Istaging = c.Istaging,
                AutoCountFee = c.AutoCountFee,
                HdesignerId = c.HdesignerId,
                Style = c.Style,
                Condition = c.Condition,
                Location = c.Location,
                Type = c.Type,
                UpdateTime = c.UpdateTime,
                DesignerTitle = _db.Hdesigners
                    .Where(d => d.HdesignerId == c.HdesignerId)
                    .Select(d => d.Title)
                    .FirstOrDefault() ?? string.Empty,
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<EditorialCaseDetailResponse?> GetByIdAsync(
        uint id,
        CancellationToken cancellationToken = default)
    {
        // 舊 PHP case_model::get_case_info($id) 只回 12 個編輯欄位,
        // 這裡為 REST 一致性回完整 case + 設計師 title。
        return await (
            from c in _db.Hcases.AsNoTracking()
            where c.HcaseId == id
            select new EditorialCaseDetailResponse
            {
                HcaseId = c.HcaseId,
                Caption = c.Caption,
                Cover = c.Cover,
                Onoff = c.Onoff == 1,
                Sdate = c.Sdate,
                CreatTime = c.CreatTime,
                Viewed = c.Viewed,
                Vr360Id = c.Vr360Id,
                Istaging = c.Istaging,
                AutoCountFee = c.AutoCountFee,
                HdesignerId = c.HdesignerId,
                Style = c.Style,
                Condition = c.Condition,
                Location = c.Location,
                Type = c.Type,
                UpdateTime = c.UpdateTime,
                DesignerTitle = _db.Hdesigners
                    .Where(d => d.HdesignerId == c.HdesignerId)
                    .Select(d => d.Title)
                    .FirstOrDefault() ?? string.Empty,
                Tag = c.Tag,
                ShortDesc = c.ShortDesc,
                LongDesc = c.LongDesc,
                Member = c.Member,
                Fee = c.Fee,
                Feedesc = c.Feedesc,
                Area = c.Area,
                AreaDesc = c.AreaDesc,
                Style2 = c.Style2,
                Provider = c.Provider,
                Layout = c.Layout,
                Materials = c.Materials,
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<OperationResult<uint>> CreateAsync(
        CreateEditorialCaseRequest request,
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;
        // 對應舊 PHP:sdate 空白時預設為今天
        var sdate = request.Sdate ?? DateOnly.FromDateTime(DateTime.Now);

        var entity = new Hcase
        {
            // 來自 request 的欄位
            Onoff = (byte)(request.Onoff ? 1 : 0),
            Sdate = sdate,
            HdesignerId = request.HdesignerId,
            Tag = BuildTagOrUseProvided(request.Tag, request.Style, request.Type, request.Condition),
            Caption = request.Caption,
            ShortDesc = request.ShortDesc ?? string.Empty,
            LongDesc = request.LongDesc ?? string.Empty,
            Member = request.Member ?? string.Empty,
            Fee = request.Fee,
            Feedesc = request.Feedesc ?? string.Empty,
            Area = request.Area,
            AreaDesc = request.AreaDesc ?? string.Empty,
            Location = request.Location ?? string.Empty,
            Style = request.Style,
            Style2 = request.Style2 ?? string.Empty,
            Type = request.Type,
            Condition = request.Condition,
            Provider = request.Provider ?? string.Empty,
            Layout = request.Layout ?? string.Empty,
            Materials = request.Materials ?? string.Empty,
            Vr360Id = request.Vr360Id ?? string.Empty,
            Istaging = request.Istaging,

            // 系統預設(對應舊 PHP insert_case_data 的固定值)
            Recommend = 0,
            Corder = 0,
            IsSend = 0,
            AutoCountFee = false,
            CreatTime = now,
            TagDatetime = now,
            UpdateTime = now,

            // _hcase 表必填但編輯部 UI 不開放編輯的欄位,給空字串避免 NOT NULL 違反
            Cover = string.Empty,
            Level = string.Empty,
            CaseTop = string.Empty,
            SdateOrder = string.Empty,
        };

        _db.Hcases.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Create,
            $"新增個案 id={entity.HcaseId} 標題={request.Caption} 設計師={request.HdesignerId}",
            cancellationToken: cancellationToken);

        return OperationResult<uint>.Created(entity.HcaseId);
    }

    public async Task<OperationResult<uint>> UpdateAsync(
        uint id,
        UpdateEditorialCaseRequest request,
        CancellationToken cancellationToken = default)
    {
        var entity = await _db.Hcases.FirstOrDefaultAsync(c => c.HcaseId == id, cancellationToken);
        if (entity is null)
            return OperationResult<uint>.NotFound("找不到個案");

        // 對應舊 PHP update_case_data:覆寫 22 個欄位
        entity.Onoff = (byte)(request.Onoff ? 1 : 0);
        entity.Sdate = request.Sdate;
        entity.HdesignerId = request.HdesignerId;
        entity.Tag = BuildTagOrUseProvided(request.Tag, request.Style, request.Type, request.Condition);
        entity.Caption = request.Caption;
        entity.ShortDesc = request.ShortDesc ?? string.Empty;
        entity.LongDesc = request.LongDesc ?? string.Empty;
        entity.Member = request.Member ?? string.Empty;
        entity.Fee = request.Fee;
        entity.Feedesc = request.Feedesc ?? string.Empty;
        entity.Area = request.Area;
        entity.AreaDesc = request.AreaDesc ?? string.Empty;
        entity.Location = request.Location ?? string.Empty;
        entity.Style = request.Style;
        entity.Style2 = request.Style2 ?? string.Empty;
        entity.Type = request.Type;
        entity.Condition = request.Condition;
        entity.Provider = request.Provider ?? string.Empty;
        entity.Layout = request.Layout ?? string.Empty;
        entity.Materials = request.Materials ?? string.Empty;
        entity.Vr360Id = request.Vr360Id ?? string.Empty;
        entity.Istaging = request.Istaging;

        // 注意:舊 PHP update_case_data 會強制 recommend = 0,
        // 這個行為看起來像 bug(把所有更新過的個案的推薦狀態清掉),
        // 但既然編輯部沿用至今,維持原行為以免破壞既有資料流。
        // 若之後確認這是 bug,把下一行刪掉即可。
        entity.Recommend = 0;

        // 注意:舊 PHP 不更新 corder / is_send / auto_count_fee / creat_time / tag_datetime,
        // 完全交由其他流程管理 — 這裡也不動。
        entity.UpdateTime = DateTime.UtcNow;

        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Update,
            $"修改個案 id={id} 標題={request.Caption} 設計師={request.HdesignerId}",
            cancellationToken: cancellationToken);

        return OperationResult<uint>.Ok(id, "修改成功");
    }

    /// <summary>
    /// Tag 處理:如果 request 有給就原樣用(編輯部慣例),
    /// 沒給就用 style + ',' + type + ',' + condition 組起來(對應舊 case_lists.js 的前端組法)。
    /// </summary>
    private static string BuildTagOrUseProvided(string? requestTag, string style, string type, string condition)
    {
        if (!string.IsNullOrWhiteSpace(requestTag))
            return requestTag;

        return $"{style},{type},{condition}";
    }
}

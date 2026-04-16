using hhh.api.contracts.admin.Brokers.Decos;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using hhh.infrastructure.Extensions;
using hhh.infrastructure.Logging;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Brokers.Decos;

/// <summary>
/// 軟裝需求單服務實作（對應 PHP Renovation_model::*deco* 系列）
/// </summary>
public class DecoRequestService : IDecoRequestService
{
    private const string PageName = "軟裝需求單";

    private readonly XoopsContext _db;
    private readonly IOperationLogWriter _logWriter;

    public DecoRequestService(XoopsContext db, IOperationLogWriter logWriter)
    {
        _db = db;
        _logWriter = logWriter;
    }

    /// <inheritdoc />
    public async Task<PagedResponse<DecoRequestListItem>> GetListAsync(
        DecoRequestListQuery query,
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: Renovation_model::get_deco_lists()
        //   WHERE payment_status='N' AND is_delete='N'
        //   AND (type = {type} if type != '全部')
        //   ORDER BY seq DESC
        var q = _db.DecoRequests
            .AsNoTracking()
            .Where(d => d.PaymentStatus == "N" && d.IsDelete == "N");

        var type = query.Type?.Trim();
        if (!string.IsNullOrEmpty(type) && type != "全部")
        {
            q = q.Where(d => d.Type == type);
        }

        return await q
            .OrderByDescending(d => d.Seq)
            .Select(d => new DecoRequestListItem
            {
                Seq = d.Seq,
                Guid = d.Guid,
                Name = d.Name,
                Phone = d.Phone,
                LineId = d.LineId,
                Email = d.Email,
                Type = d.Type,
                Time = d.Time,
                MeetingDate = d.MeetingDate,
                SendStatus = d.SendStatus,
                SendDatetime = d.SendDatetime,
                PaymentStatus = d.PaymentStatus,
                PaymentTime = d.PaymentTime,
                DecoSetPrice = d.DecoSetPrice,
                ProposalPrice = d.ProposalPrice,
                DecoPrice = d.DecoPrice,
                Source = d.Source,
                FirstContact = d.FirstContact,
                SetDate = d.SetDate,
                CreateTime = d.CreateTime,
                UpdateTime = d.UpdateTime,
            })
            .ToPagedResponseAsync(query.Page, query.PageSize, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<OperationResult<DecoRequestDetailResponse>> GetByIdAsync(
        int seq,
        CancellationToken cancellationToken = default)
    {
        var entity = await _db.DecoRequests
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Seq == seq && d.IsDelete == "N", cancellationToken);

        if (entity is null)
            return OperationResult<DecoRequestDetailResponse>.NotFound($"找不到軟裝需求單 seq={seq}");

        return OperationResult<DecoRequestDetailResponse>.Ok(MapToDetail(entity));
    }

    /// <inheritdoc />
    public async Task<OperationResult<int>> CreateAsync(
        CreateDecoRequestRequest request,
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: Renovation_model::insert_by_backstage()
        var now = DateTime.Now;
        var entity = new DecoRequest
        {
            Guid = Guid.NewGuid(),
            Name = request.Name,
            Sex = request.Sex,
            Phone = request.Phone,
            PhoneRepeat = "N",
            LineId = request.LineId,
            NeedRequest = request.NeedRequest,
            Address = request.Address,
            HouseType = request.HouseType,
            HouseYears = request.HouseYears,
            HouseMode = request.HouseMode,
            HouseAllLv = request.HouseAllLv,
            HouseInLv = request.HouseInLv,
            Email = request.Email,
            Time = request.Time,
            Budget = request.Budget,
            Pin = request.Pin,
            HouseHight = request.HouseHight,
            Style = request.Style,
            Pattern = request.Pattern,
            Functions = request.Functions,
            NowLayout = request.NowLayout,
            Layout = request.Layout,
            HowToKnow = request.HowToKnow,
            NoteText = request.NoteText,
            MeetingDate = request.MeetingDate,
            IsAgree = request.IsAgree,
            SendStatus = "Y",       // 後台代填視為已寄出
            SendDatetime = now,
            CreateTime = now,
            UpdateTime = now,       // 舊 PHP 原本為 '0000-00-00 00:00:00',.NET 不支援,改用 now
            DecoSetPrice = request.DecoSetPrice,
            ProposalPrice = request.ProposalPrice,
            DecoPrice = request.DecoPrice,
            PaymentStatus = "N",
            IsDelete = "N",
            Type = request.Type,
            FirstContact = request.FirstContact,
            Source = request.Source ?? string.Empty,
            SetDate = request.SetDate,
            UtmSource = request.UtmSource,
            Loan = request.Loan,
            DecoRequestcol = request.DecoRequestcol,
            WantDecoMatters = request.WantDecoMatters ?? string.Empty,
            WantDecoBudget = request.WantDecoBudget ?? string.Empty,
            WantDecoTime = request.WantDecoTime ?? string.Empty,
            UtmMedium = request.UtmMedium,
            UtmCampaign = request.UtmCampaign,
        };

        _db.DecoRequests.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Create,
            $"新增軟裝需求單 seq={entity.Seq} name={entity.Name}",
            cancellationToken: cancellationToken);

        return OperationResult<int>.Created(entity.Seq, "軟裝需求單新增成功");
    }

    /// <inheritdoc />
    public async Task<OperationResult> UpdateAsync(
        int seq,
        UpdateDecoRequestRequest request,
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: Renovation_model::update_by_backstage()
        var entity = await _db.DecoRequests
            .FirstOrDefaultAsync(d => d.Seq == seq && d.IsDelete == "N", cancellationToken);

        if (entity is null)
            return OperationResult.NotFound($"找不到軟裝需求單 seq={seq}");

        entity.Name = request.Name;
        entity.Sex = request.Sex;
        entity.Phone = request.Phone;
        entity.LineId = request.LineId;
        entity.NeedRequest = request.NeedRequest;
        entity.Address = request.Address;
        entity.HouseType = request.HouseType;
        entity.HouseYears = request.HouseYears;
        entity.HouseMode = request.HouseMode;
        entity.HouseAllLv = request.HouseAllLv;
        entity.HouseInLv = request.HouseInLv;
        entity.Email = request.Email;
        entity.Time = request.Time;
        entity.Budget = request.Budget;
        entity.Pin = request.Pin;
        entity.HouseHight = request.HouseHight;
        entity.Style = request.Style;
        entity.Pattern = request.Pattern;
        entity.Functions = request.Functions;
        entity.NowLayout = request.NowLayout;
        entity.Layout = request.Layout;
        entity.HowToKnow = request.HowToKnow;
        entity.NoteText = request.NoteText;
        entity.MeetingDate = request.MeetingDate;
        entity.IsAgree = request.IsAgree;
        entity.DecoSetPrice = request.DecoSetPrice;
        entity.ProposalPrice = request.ProposalPrice;
        entity.DecoPrice = request.DecoPrice;
        entity.Type = request.Type;
        entity.FirstContact = request.FirstContact;
        entity.Source = request.Source ?? entity.Source;
        entity.SetDate = request.SetDate;
        entity.UtmSource = request.UtmSource;
        entity.Loan = request.Loan;
        entity.DecoRequestcol = request.DecoRequestcol;
        entity.WantDecoMatters = request.WantDecoMatters ?? entity.WantDecoMatters;
        entity.WantDecoBudget = request.WantDecoBudget ?? entity.WantDecoBudget;
        entity.WantDecoTime = request.WantDecoTime ?? entity.WantDecoTime;
        entity.UtmMedium = request.UtmMedium;
        entity.UtmCampaign = request.UtmCampaign;
        entity.UpdateTime = DateTime.Now;

        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Update,
            $"更新軟裝需求單 seq={seq}",
            cancellationToken: cancellationToken);

        return OperationResult.Ok("軟裝需求單更新成功");
    }

    /// <inheritdoc />
    public async Task<OperationResult> BatchSoftDeleteAsync(
        BatchDeleteDecoRequest request,
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: Renovation_model::set_delete()
        var affected = await _db.DecoRequests
            .Where(d => request.Seqs.Contains(d.Seq) && d.IsDelete == "N")
            .ExecuteUpdateAsync(s => s.SetProperty(d => d.IsDelete, "Y"), cancellationToken);

        if (affected == 0)
            return OperationResult.NotFound("找不到任何可刪除的軟裝需求單");

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Delete,
            $"批次軟刪除軟裝需求單 seqs=[{string.Join(",", request.Seqs)}] affected={affected}",
            cancellationToken: cancellationToken);

        return OperationResult.Ok($"已軟刪除 {affected} 筆");
    }

    /// <inheritdoc />
    public async Task<List<DecoRequestFileItem>> GetFilesAsync(
        int seq,
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: Renovation_model::get_deco_request_files_by_backstage()
        return await _db.DecoRequestFiles
            .AsNoTracking()
            .Where(f => f.Seq == seq)
            .OrderByDescending(f => f.DecoFileId)
            .Select(f => new DecoRequestFileItem
            {
                DecoFileId = f.DecoFileId,
                Seq = f.Seq,
                OrigName = f.OrigName,
                FileName = f.FileName,
                CreateTime = f.CreateTime,
            })
            .ToListAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<OperationResult> CreateFilesAsync(
        int seq,
        CreateDecoRequestFilesRequest request,
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: Renovation_model::insert_deco_request_files_by_backstage() — 用 transaction 批次 insert
        var existsParent = await _db.DecoRequests
            .AsNoTracking()
            .AnyAsync(d => d.Seq == seq && d.IsDelete == "N", cancellationToken);

        if (!existsParent)
            return OperationResult.NotFound($"找不到軟裝需求單 seq={seq}");

        var now = DateTime.Now;
        var entities = request.Files.Select(f => new DecoRequestFile
        {
            Seq = seq,
            OrigName = f.OrigName,
            FileName = f.FileName,
            CreateTime = now,
        }).ToList();

        await using var tx = await _db.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            _db.DecoRequestFiles.AddRange(entities);
            await _db.SaveChangesAsync(cancellationToken);
            await tx.CommitAsync(cancellationToken);
        }
        catch
        {
            await tx.RollbackAsync(cancellationToken);
            throw;
        }

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Create,
            $"新增軟裝需求單附件 seq={seq} count={entities.Count}",
            cancellationToken: cancellationToken);

        return OperationResult.Ok($"已新增 {entities.Count} 個附件");
    }

    /// <inheritdoc />
    public async Task<OperationResult> DeleteFilesAsync(
        BatchDeleteDecoRequestFiles request,
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: Renovation_model::del_deco_request_files() — 硬刪除,配合 transaction
        await using var tx = await _db.Database.BeginTransactionAsync(cancellationToken);
        int affected;
        try
        {
            affected = await _db.DecoRequestFiles
                .Where(f => request.FileIds.Contains(f.DecoFileId))
                .ExecuteDeleteAsync(cancellationToken);
            await tx.CommitAsync(cancellationToken);
        }
        catch
        {
            await tx.RollbackAsync(cancellationToken);
            throw;
        }

        if (affected == 0)
            return OperationResult.NotFound("找不到任何可刪除的附件");

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Delete,
            $"刪除軟裝需求單附件 fileIds=[{string.Join(",", request.FileIds)}] affected={affected}",
            cancellationToken: cancellationToken);

        return OperationResult.Ok($"已刪除 {affected} 個附件");
    }

    /// <inheritdoc />
    public async Task<OperationResult> SetPriceAsync(
        int seq,
        SetDecoPriceRequest request,
        CancellationToken cancellationToken = default)
    {
        // 對應 PHP: Renovation_model::set_price()
        //   舊版會更新價格 + 寄 email 通知客戶(CC 經紀人 & 技術主管),
        //   .NET 尚未整合 SMTP,這裡只更新 DB + 寫 _hoplog 紀錄動作,待 SMTP 接通後再補。
        var entity = await _db.DecoRequests
            .FirstOrDefaultAsync(d => d.Seq == seq && d.IsDelete == "N", cancellationToken);

        if (entity is null)
            return OperationResult.NotFound($"找不到軟裝需求單 seq={seq}");

        var now = DateTime.Now;
        entity.DecoSetPrice = request.DecoSetPrice;
        entity.ProposalPrice = request.ProposalPrice;
        entity.DecoPrice = request.DecoPrice;
        entity.PaymentStatus = "Y";
        entity.PaymentTime = now;
        entity.UpdateTime = now;

        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Update,
            $"付款通知 seq={seq} deco_price={request.DecoPrice} (尚未寄發 email,待 SMTP 接通)",
            cancellationToken: cancellationToken);

        return OperationResult.Ok("付款狀態已更新(email 通知待 SMTP 接通後補送)");
    }

    // ── 私有輔助 ──

    private static DecoRequestDetailResponse MapToDetail(DecoRequest entity) => new()
    {
        Seq = entity.Seq,
        Guid = entity.Guid,
        Name = entity.Name,
        Sex = entity.Sex,
        Phone = entity.Phone,
        LineId = entity.LineId,
        NeedRequest = entity.NeedRequest,
        Address = entity.Address,
        HouseType = entity.HouseType,
        HouseYears = entity.HouseYears,
        HouseMode = entity.HouseMode,
        HouseAllLv = entity.HouseAllLv,
        HouseInLv = entity.HouseInLv,
        Email = entity.Email,
        Time = entity.Time,
        Budget = entity.Budget,
        Pin = entity.Pin,
        HouseHight = entity.HouseHight,
        Style = entity.Style,
        Pattern = entity.Pattern,
        Functions = entity.Functions,
        NowLayout = entity.NowLayout,
        Layout = entity.Layout,
        HowToKnow = entity.HowToKnow,
        NoteText = entity.NoteText,
        MeetingDate = entity.MeetingDate,
        IsAgree = entity.IsAgree,
        SendStatus = entity.SendStatus,
        SendDatetime = entity.SendDatetime,
        CreateTime = entity.CreateTime,
        UpdateTime = entity.UpdateTime,
        DecoSetPrice = entity.DecoSetPrice,
        ProposalPrice = entity.ProposalPrice,
        DecoPrice = entity.DecoPrice,
        PaymentStatus = entity.PaymentStatus,
        PaymentTime = entity.PaymentTime,
        IsDelete = entity.IsDelete,
        Type = entity.Type,
        FirstContact = entity.FirstContact,
        Source = entity.Source,
        SetDate = entity.SetDate,
        UtmSource = entity.UtmSource,
        Loan = entity.Loan,
        DecoRequestcol = entity.DecoRequestcol,
        WantDecoMatters = entity.WantDecoMatters,
        WantDecoBudget = entity.WantDecoBudget,
        WantDecoTime = entity.WantDecoTime,
        UtmMedium = entity.UtmMedium,
        UtmCampaign = entity.UtmCampaign,
    };
}

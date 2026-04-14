using hhh.api.contracts.admin.Main.Execute;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using hhh.infrastructure.Extensions;
using hhh.infrastructure.Logging;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Main.Execute;

public class ExecuteFormService : IExecuteFormService
{
    private const string PageName = "執行表單";

    private readonly XoopsContext _db;
    private readonly IOperationLogWriter _logWriter;

    public ExecuteFormService(XoopsContext db, IOperationLogWriter logWriter)
    {
        _db = db;
        _logWriter = logWriter;
    }

    public async Task<PagedResponse<ExecuteFormListItem>> GetListAsync(ListQuery query, CancellationToken cancellationToken = default)
    {
        return await _db.ExecuteForms
            .AsNoTracking()
            .Where(e => e.IsDelete == "N")
            .OrderByDescending(e => e.ExfId)
            .Select(e => MapToListItem(e))
            .ToPagedResponseAsync(query.Page, query.PageSize, cancellationToken);
    }

    public async Task<ExecuteFormListItem?> GetByIdAsync(uint exfId, CancellationToken cancellationToken = default)
    {
        return await _db.ExecuteForms
            .AsNoTracking()
            .Where(e => e.ExfId == exfId)
            .Select(e => MapToListItem(e))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<OperationResult<uint>> CreateAsync(
        CreateExecuteFormRequest request,
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        var entity = new ExecuteForm
        {
            Num = request.Num,
            Company = request.Company,
            Designer = request.Designer,
            Mobile = request.Mobile ?? string.Empty,
            Telete = request.Telete ?? string.Empty,
            Sdate = request.Sdate ?? DateOnly.FromDateTime(DateTime.Now),
            Edate = request.Edate ?? DateOnly.FromDateTime(DateTime.Now),
            ContractTime = request.ContractTime,
            ContractPerson = request.ContractPerson,
            SalesMan = request.SalesMan,
            IsClose = request.IsClose,
            IsDelete = "N",
            DetailStatus = request.DetailStatus,
            SalesDept = request.SalesDept,
            TaxIncludedPrice = request.TaxIncludedPrice,
            Price = request.Price,
            Creator = request.Creator,
            LastUpdate = request.Creator,
            Note = request.Note,
            FbPrice = request.FbPrice ?? string.Empty,
            YtPrice = request.YtPrice ?? string.Empty,
            PhotoOutsidePrice = request.PhotoOutsidePrice ?? string.Empty,
            PhotoTransPrice = request.PhotoTransPrice ?? string.Empty,
            HostPrice = request.HostPrice ?? string.Empty,
            TransferNum = request.TransferNum,
            CreateTime = now,
            UpdateTime = now,
            // 核准欄位預設空值
            AllowSales = string.Empty,
            AllowFinance = string.Empty,
            AllowMan = string.Empty,
        };

        _db.ExecuteForms.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName, OperationAction.Create,
            $"新增執行表單 exf_id={entity.ExfId} 合約={request.Num} 公司={request.Company}",
            cancellationToken: cancellationToken);

        return OperationResult<uint>.Created(entity.ExfId);
    }

    public async Task<OperationResult<uint>> UpdateAsync(
        uint exfId,
        UpdateExecuteFormRequest request,
        CancellationToken cancellationToken = default)
    {
        var entity = await _db.ExecuteForms.FirstOrDefaultAsync(e => e.ExfId == exfId, cancellationToken);
        if (entity is null)
            return OperationResult<uint>.NotFound("找不到執行表單");

        if (request.Num is not null) entity.Num = request.Num;
        if (request.Company is not null) entity.Company = request.Company;
        if (request.Designer is not null) entity.Designer = request.Designer;
        if (request.Mobile is not null) entity.Mobile = request.Mobile;
        if (request.Telete is not null) entity.Telete = request.Telete;
        if (request.SalesDept is not null) entity.SalesDept = request.SalesDept;
        if (request.Sdate is { } sdate) entity.Sdate = sdate;
        if (request.Edate is { } edate) entity.Edate = edate;
        if (request.ContractTime is { } ct) entity.ContractTime = ct;
        if (request.ContractPerson is not null) entity.ContractPerson = request.ContractPerson;
        if (request.SalesMan is not null) entity.SalesMan = request.SalesMan;
        if (request.IsClose is not null) entity.IsClose = request.IsClose;
        if (request.LastUpdate is not null) entity.LastUpdate = request.LastUpdate;
        if (request.Note is not null) entity.Note = request.Note;
        if (request.FbPrice is not null) entity.FbPrice = request.FbPrice;
        if (request.YtPrice is not null) entity.YtPrice = request.YtPrice;
        if (request.PhotoOutsidePrice is not null) entity.PhotoOutsidePrice = request.PhotoOutsidePrice;
        if (request.PhotoTransPrice is not null) entity.PhotoTransPrice = request.PhotoTransPrice;
        if (request.HostPrice is not null) entity.HostPrice = request.HostPrice;
        if (request.TransferNum is not null) entity.TransferNum = request.TransferNum;
        if (request.TaxIncludedPrice is { } tip) entity.TaxIncludedPrice = tip;
        if (request.Price is { } price) entity.Price = price;

        entity.UpdateTime = DateTime.UtcNow;

        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName, OperationAction.Update,
            $"修改執行表單 exf_id={exfId} 合約={entity.Num}",
            cancellationToken: cancellationToken);

        return OperationResult<uint>.Ok(exfId, "修改成功");
    }

    public async Task<OperationResult> SoftDeleteAsync(uint exfId, CancellationToken cancellationToken = default)
    {
        var entity = await _db.ExecuteForms.FirstOrDefaultAsync(e => e.ExfId == exfId, cancellationToken);
        if (entity is null)
            return OperationResult.NotFound("找不到執行表單");

        entity.IsDelete = "Y";
        entity.UpdateTime = DateTime.UtcNow;

        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName, OperationAction.Delete,
            $"刪除執行表單 exf_id={exfId} 合約={entity.Num}",
            cancellationToken: cancellationToken);

        return OperationResult.Ok("刪除成功");
    }

    // -------------------------------------------------------------------------
    // Projection helper
    // -------------------------------------------------------------------------

    private static ExecuteFormListItem MapToListItem(ExecuteForm e) => new()
    {
        ExfId = e.ExfId,
        Num = e.Num,
        Company = e.Company,
        Designer = e.Designer,
        ContractTime = e.ContractTime,
        SalesMan = e.SalesMan,
        SalesDept = e.SalesDept,
        IsClose = e.IsClose,
        DetailStatus = e.DetailStatus,
        TaxIncludedPrice = e.TaxIncludedPrice,
        Price = e.Price,
        Sdate = e.Sdate,
        Edate = e.Edate,
        CreateTime = e.CreateTime,
        UpdateTime = e.UpdateTime,
        Creator = e.Creator,
        LastUpdate = e.LastUpdate,
        Quota = e.Quota,
        Note = e.Note,
        Mobile = e.Mobile,
        Telete = e.Telete,
        ContractPerson = e.ContractPerson,
        FbPrice = e.FbPrice,
        YtPrice = e.YtPrice,
        PhotoOutsidePrice = e.PhotoOutsidePrice,
        PhotoTransPrice = e.PhotoTransPrice,
        HostPrice = e.HostPrice,
        TransferNum = e.TransferNum,
    };
}

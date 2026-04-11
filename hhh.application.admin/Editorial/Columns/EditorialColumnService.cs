using hhh.api.contracts.admin.Editorial.Columns;
using hhh.application.admin.Common;
using hhh.infrastructure.Context;
using hhh.infrastructure.Dto.Xoops;
using hhh.infrastructure.Logging;
using Microsoft.EntityFrameworkCore;

namespace hhh.application.admin.Editorial.Columns;

public class EditorialColumnService : IEditorialColumnService
{
    private const string PageName = "編輯部專欄";

    private readonly XoopsContext _db;
    private readonly IOperationLogWriter _logWriter;

    public EditorialColumnService(XoopsContext db, IOperationLogWriter logWriter)
    {
        _db = db;
        _logWriter = logWriter;
    }

    public async Task<List<EditorialColumnListItem>> GetListAsync(CancellationToken cancellationToken = default)
    {
        // 對應舊 PHP column_model::get_column_lists():
        //   SELECT hcolumn_id, builder_product_id, ctag, ctype, ctype_sub, ctitle,
        //          cshort_title, cdesc, clogo, extend_str, viewed, recommend, onoff,
        //          creat_time, bid, hdesigner_ids, sdate, seo_title, seo_image,
        //          seo_description, update_time
        //   FROM _hcolumn
        //   ORDER BY sdate DESC, hcolumn_id DESC
        //
        // 沒有 paging、沒有 filter,全量回給前端 Kendo Grid 自行分頁。
        return await _db.Hcolumns
            .AsNoTracking()
            .OrderByDescending(c => c.Sdate)
            .ThenByDescending(c => c.HcolumnId)
            .Select(c => new EditorialColumnListItem
            {
                HcolumnId = c.HcolumnId,
                BuilderProductId = c.BuilderProductId,
                Ctag = c.Ctag,
                Ctype = c.Ctype,
                CtypeSub = c.CtypeSub,
                Ctitle = c.Ctitle,
                CshortTitle = c.CshortTitle,
                Cdesc = c.Cdesc,
                Clogo = c.Clogo,
                ExtendStr = c.ExtendStr,
                Viewed = c.Viewed,
                Recommend = c.Recommend,
                Onoff = c.Onoff == 1,
                CreatTime = c.CreatTime,
                Bid = c.Bid,
                HdesignerIds = c.HdesignerIds,
                Sdate = c.Sdate,
                SeoTitle = c.SeoTitle,
                SeoImage = c.SeoImage,
                SeoDescription = c.SeoDescription,
                UpdateTime = c.UpdateTime,
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<EditorialColumnDetailResponse?> GetByIdAsync(
        uint id,
        CancellationToken cancellationToken = default)
    {
        // 舊 PHP get_column_page_content($id) 只回 page_content,
        // 這裡為 REST 一致性回完整 column 資料。
        return await _db.Hcolumns
            .AsNoTracking()
            .Where(c => c.HcolumnId == id)
            .Select(c => new EditorialColumnDetailResponse
            {
                HcolumnId = c.HcolumnId,
                BuilderProductId = c.BuilderProductId,
                Ctag = c.Ctag,
                Ctype = c.Ctype,
                CtypeSub = c.CtypeSub,
                Ctitle = c.Ctitle,
                CshortTitle = c.CshortTitle,
                Cdesc = c.Cdesc,
                Clogo = c.Clogo,
                ExtendStr = c.ExtendStr,
                Viewed = c.Viewed,
                Recommend = c.Recommend,
                Onoff = c.Onoff == 1,
                CreatTime = c.CreatTime,
                Bid = c.Bid,
                HdesignerIds = c.HdesignerIds,
                Sdate = c.Sdate,
                SeoTitle = c.SeoTitle,
                SeoImage = c.SeoImage,
                SeoDescription = c.SeoDescription,
                UpdateTime = c.UpdateTime,
                PageContent = c.PageContent,
                Tag = c.Tag,
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<OperationResult<uint>> CreateAsync(
        CreateEditorialColumnRequest request,
        CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;
        // 對應舊 PHP:sdate 空白時預設為今天
        var sdate = request.Sdate ?? DateOnly.FromDateTime(DateTime.Now);

        var entity = new Hcolumn
        {
            // 來自 request 的欄位
            Onoff = (byte)(request.Onoff ? 1 : 0),
            Sdate = sdate,
            Ctitle = request.Ctitle,
            Ctype = request.Ctype,
            CtypeSub = request.CtypeSub,
            Ctag = request.Ctag ?? string.Empty,
            CshortTitle = request.CshortTitle ?? string.Empty,
            Cdesc = request.Cdesc,
            ExtendStr = request.ExtendStr,
            SeoTitle = request.SeoTitle,
            SeoDescription = request.SeoDescription,
            Bid = request.Bid,
            HdesignerIds = request.HdesignerIds,
            BuilderProductId = request.BuilderProductId,
            PageContent = request.PageContent,

            // 系統預設(對應舊 PHP insert_column_data 的固定值)
            // 注意:舊 PHP 寫了兩次 'recommend',第二次覆寫成 0,實際結果就是 0
            Recommend = 0,
            IsSend = 0,
            CreatTime = now,
            TagDatetime = now,
            UpdateTime = now,

            // _hcolumn 表必填但編輯部 UI 不開放編輯的欄位,給空字串避免 NOT NULL 違反
            Clogo = string.Empty,
            JsonDid = string.Empty,
            JsonBid = string.Empty,
        };

        _db.Hcolumns.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Create,
            $"新增專欄 id={entity.HcolumnId} 標題={request.Ctitle} 類別={request.Ctype}",
            cancellationToken: cancellationToken);

        return OperationResult<uint>.Created(entity.HcolumnId);
    }

    public async Task<OperationResult<uint>> UpdateAsync(
        uint id,
        UpdateEditorialColumnRequest request,
        CancellationToken cancellationToken = default)
    {
        var entity = await _db.Hcolumns.FirstOrDefaultAsync(c => c.HcolumnId == id, cancellationToken);
        if (entity is null)
            return OperationResult<uint>.NotFound("找不到專欄");

        // 對應舊 PHP update_column_data:覆寫 14 個欄位
        // 注意:舊 PHP PUT 邏輯比 POST 寬鬆很多 (只必填 onoff + hcolumn_id),
        // 其他欄位都會原樣覆寫。這裡比照舊行為:nullable 欄位用 ??(舊值維持空字串/null),
        // 必填的 string non-nullable 欄位若 request 沒給就不動。
        entity.Onoff = (byte)(request.Onoff ? 1 : 0);
        entity.Sdate = request.Sdate;

        if (request.Ctitle is not null) entity.Ctitle = request.Ctitle;
        if (request.Ctype is not null) entity.Ctype = request.Ctype;
        if (request.Cdesc is not null) entity.Cdesc = request.Cdesc;
        if (request.ExtendStr is not null) entity.ExtendStr = request.ExtendStr;

        entity.CtypeSub = request.CtypeSub;
        entity.Ctag = request.Ctag ?? string.Empty;
        entity.CshortTitle = request.CshortTitle ?? string.Empty;
        entity.SeoTitle = request.SeoTitle;
        entity.SeoDescription = request.SeoDescription;
        entity.Bid = request.Bid;
        entity.HdesignerIds = request.HdesignerIds;
        entity.BuilderProductId = request.BuilderProductId;
        entity.PageContent = request.PageContent;

        // 注意:舊 PHP update_column_data 會強制 recommend = 0,
        // 跟 Cases.php 同一個 legacy bug 行為,維持原貌避免破壞編輯部既有資料流。
        entity.Recommend = 0;
        entity.UpdateTime = DateTime.UtcNow;

        await _db.SaveChangesAsync(cancellationToken);

        await _logWriter.WriteAsync(
            PageName,
            OperationAction.Update,
            $"修改專欄 id={id} 標題={request.Ctitle ?? entity.Ctitle}",
            cancellationToken: cancellationToken);

        return OperationResult<uint>.Ok(id, "修改成功");
    }
}

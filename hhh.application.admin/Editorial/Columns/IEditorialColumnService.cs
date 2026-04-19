using hhh.api.contracts.admin.Editorial.Columns;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;

namespace hhh.application.admin.Editorial.Columns;

/// <summary>
/// 編輯部 - 專欄服務
/// </summary>
/// <remarks>
/// 對應舊版 PHP:hhh-api/application/controllers/base/v1/Column.php(column_model)
/// 後台 view:hhh-backstage/backend/views/backend/event/column_lists.php
/// </remarks>
public interface IEditorialColumnService
{
    /// <summary>
    /// 取得專欄列表(無 paging,全量回傳)
    /// 對應舊版 column_model::get_column_lists()
    /// </summary>
    Task<PagedResponse<EditorialColumnListItem>> GetListAsync(EditorialColumnListQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    /// 取得單一專欄完整資料
    /// 對應舊版 column_model::get_column_page_content($id)
    /// (舊版只回 page_content 一個欄位,這裡為 REST 一致性回完整 column)
    /// </summary>
    Task<EditorialColumnDetailResponse?> GetByIdAsync(uint id, CancellationToken cancellationToken = default);

    /// <summary>
    /// 新增專欄
    /// 對應舊版 column_model::insert_column_data()
    /// 寫入時系統設定:recommend=0(legacy 行為)、creat_time=now、sdate(空白)=今天
    /// </summary>
    Task<OperationResult<uint>> CreateAsync(
        CreateEditorialColumnRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 更新專欄
    /// 對應舊版 column_model::update_column_data()
    /// 寫入時:強制 recommend=0(legacy 行為,保留)、update_time=now
    /// </summary>
    Task<OperationResult<uint>> UpdateAsync(
        uint id,
        UpdateEditorialColumnRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 取得專欄精簡列表（不分頁，供下拉選單 / 關聯選擇使用）。
    /// 僅回傳上線中(onoff=1)的專欄，依 sdate DESC 排序。
    /// 可選 keyword 做標題模糊搜尋、ctype 做類別篩選，以支援前端 combo-box 即時過濾。
    /// </summary>
    Task<List<ColumnSelectItem>> GetSelectListAsync(
        string? keyword = null,
        string? ctype = null,
        CancellationToken cancellationToken = default);
}

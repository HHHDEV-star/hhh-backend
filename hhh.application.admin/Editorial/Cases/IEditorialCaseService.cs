using hhh.api.contracts.admin.Editorial.Cases;
using hhh.api.contracts.Common;
using hhh.application.admin.Common;

namespace hhh.application.admin.Editorial.Cases;

/// <summary>
/// 編輯部 - 個案服務
/// </summary>
/// <remarks>
/// 對應舊版 PHP:hhh-api/application/controllers/base/v1/Cases.php(case_model)
/// 後台 view:hhh-backstage/backend/views/backend/event/case_lists.php
///
/// 注意:此服務跟 Designers/Hcases 的 IHcaseService 寫同一張 _hcase 表,
/// 但是給編輯部後台用的不同 view / 不同流程。兩者的差異:
///  - 編輯部:flat 全量列表(無 paging)、批次工作流(舊 PHP 是 batch,但這版改 single)、
///    update 強制 recommend=0(legacy 行為)、tag 欄位由前端組好原樣存
///  - 設計師管理:server paging + 多欄位 filter、單筆 CRUD、
///    update 保留 recommend、tag 由 service 自動組
/// </remarks>
public interface IEditorialCaseService
{
    /// <summary>
    /// 取得個案列表(無 paging,全量回傳)
    /// 對應舊版 case_model::get_case_lists()
    /// </summary>
    Task<PagedResponse<EditorialCaseListItem>> GetListAsync(ListQuery query, CancellationToken cancellationToken = default);

    /// <summary>
    /// 取得單一個案完整資料
    /// 對應舊版 case_model::get_case_info($id)
    /// (舊版只回 12 個欄位,這裡為 REST 一致性回完整 ~29 欄)
    /// </summary>
    Task<EditorialCaseDetailResponse?> GetByIdAsync(uint id, CancellationToken cancellationToken = default);

    /// <summary>
    /// 新增個案
    /// 對應舊版 case_model::insert_case_data()
    /// 寫入時設定:recommend=0、corder=0、is_send=0、auto_count_fee=0、creat_time=now、tag_datetime=now
    /// 必填欄位驗證由 DataAnnotations 處理
    /// </summary>
    Task<OperationResult<uint>> CreateAsync(
        CreateEditorialCaseRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 更新個案
    /// 對應舊版 case_model::update_case_data()
    /// 寫入時:強制 recommend=0(legacy 行為,保留)、update_time=now
    /// 不會動到:creat_time、corder、is_send、auto_count_fee、tag_datetime
    /// </summary>
    Task<OperationResult<uint>> UpdateAsync(
        uint id,
        UpdateEditorialCaseRequest request,
        CancellationToken cancellationToken = default);
}

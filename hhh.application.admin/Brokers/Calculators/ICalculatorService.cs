using hhh.api.contracts.admin.Brokers.Calculators;

namespace hhh.application.admin.Brokers.Calculators;

/// <summary>
/// 經紀人 - 裝修計算機 服務
/// </summary>
/// <remarks>
/// 對應舊版 PHP:hhh-api/application/controllers/third/v1/Calculator.php
/// 後台 view:hhh-backstage/backend/views/backend/event/calculator.php
/// </remarks>
public interface ICalculatorService
{
    /// <summary>
    /// 取得裝修計算機列表
    /// (對應舊版 Calculator/index_get → Calculator_model::get())
    /// 業務規則:只回傳 contact_status='Y' 的紀錄,依 id DESC 排序、無分頁、無查詢條件。
    /// </summary>
    Task<List<CalculatorListItem>> GetListAsync(CancellationToken cancellationToken = default);
}

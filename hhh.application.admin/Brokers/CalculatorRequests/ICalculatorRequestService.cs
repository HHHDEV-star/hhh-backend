using hhh.api.contracts.admin.Brokers.CalculatorRequests;

namespace hhh.application.admin.Brokers.CalculatorRequests;

/// <summary>
/// 經紀人 - 裝修需求預算單 服務
/// </summary>
/// <remarks>
/// 對應舊版 PHP:hhh-api/application/controllers/third/v1/Calculator.php (requestindex_get)
/// 後台 view:hhh-backstage/backend/views/backend/event/calculator_request.php
/// </remarks>
public interface ICalculatorRequestService
{
    /// <summary>
    /// 取得裝修計算機需求列表
    /// (對應舊版 Calculator/requestindex_get → Calculator_model::requestget)
    /// </summary>
    Task<CalculatorRequestListResponse> GetListAsync(
        CalculatorRequestListQuery query,
        CancellationToken cancellationToken = default);
}
